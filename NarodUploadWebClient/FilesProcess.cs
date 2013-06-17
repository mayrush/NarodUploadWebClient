using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace NarodUploadWebClient
{
    ///<summary>Процедуры по загрузке файла на Яндекс.Диск</summary>
    public class FilesProcess : Form
    {
        ///<summary>Тип клиентского приложение, в данном контексте эмуляция конкретного клиентского приложения</summary>
        protected const string UserAgentString = "Mozilla/5.0 (Windows NT 6.1; YB/3.5.2.0; MRA/5.5 rv:2.0) Gecko/20100101 Firefox/4.0";
        ///<summary>Общая ссылка на Яндекс</summary>
        protected static string Url = @"http://narod.yandex.ru/";
        ///<summary>Ссылка для аутентификации на сервере яндекса</summary>
        protected static string Purl = @"https://passport.yandex.ru/passport?mode=auth";
        ///<summary>Ссылка для получения данных для формирования ссылки для закачки на сервер</summary>
        protected static string Uurl = @"http://narod.yandex.ru/disk/getstorage/";
        ///<summary>Ссылка для получения итоговой ссылки на файл</summary>
        protected static string Lurl = @"http://narod.yandex.ru/disk/last/";
        ///<summary>Начальная ссылка для получения списка файлов и страниц</summary>
        protected static string Aurl = @"http://narod.yandex.ru/disk/all/";
        ///<summary>Boundary</summary>
        protected static string Boundary = "--AaB03x";
        ///<summary>Тип содержимого HTTP для ответа</summary>
        protected static string ContentType = "multipart/form-data";
        ///<summary>Размер максимального буфера для отправки в поток</summary>
        protected static int BufferSize = 4096;
        ///<summary>Таймаут ожидания для сервера</summary>
        protected static int ServerTimeout = (24 * 60 * 60 * 1000); // Таймаут равняется суткам, в милисекундах
        ///<summary>Проверка пройдена ли аутентификация на yandex паспорте</summary>
        protected static bool AuthTest;
        ///<summary>Cookies</summary>
        public static CookieContainer Cookies = new CookieContainer();
        ///<summary>Данные аутентификации</summary>
        public static CredentialCache MyCredential;

        protected static string Login;
        protected static string Password;

        protected static string LastLogin = "";

        protected FileStream UpfileStream;

        protected Stream requestStream = null;

        protected HttpWebRequest serverRequest;

        protected bool abortthread = false;

        ///<summary>Имя файла для загрузки</summary>
        public string Filename;

        protected const int Min = 60, Has = 3600, Kbyte = 1024, Proc = 100;
        protected int Dowloaded, Dowloaded2, TimeSec, TimeMin, TimeHas, Length;

        protected bool Pause;

        protected bool Stop;

        protected static bool abort;

        protected string SLink = "";

        ///<summary>Общее количество открытых окон загрузки</summary>
        public static int WndCol;
        protected int WndId;

        protected int CordX;
        protected int CordY;

        ///<summary>Доступ к настройкам приложения</summary>
        public static NuSettings AppSettings;

        ///<summary>Конструктор класса FilesProcess, выполняет вызов формы</summary>
        ///<param name="login">Логин на Яндекс</param>
        ///<param name="password">Пароль на Яндекс</param>
        ///<param name="filename">Имя файла для загрузки</param>
        ///<param name="args">Массив имён файлов для загрузки</param>
        public FilesProcess(string login, string password, string filename = "", string[] args = null)
        {
            Login = login;
            Password = password;
            Filename = filename;
        }

        protected FilesProcess()
        {
            //throw new NotImplementedException();
        }

        //******************************************************************************
        // ОБЩИЙ РАЗДЕЛ
        // Подключение к серверу, аутентификация, получение ответа
        //

        ///<summary>Аутентификация на Yandex.Passport</summary>
        public static void PassportAuthentication(string login = "", string password = "")
        {
            try
            {
                if (((Login == null) || (Login != login)) && (login != ""))
                {
                    Login = login;
                }
                if (Password == null) Password = password;

                if ((!AuthTest) || (Login != LastLogin))
                {
                    MyCredential = new CredentialCache { { new Uri(Purl), "Basic", new NetworkCredential(Login, Password) } };

                    AuthTest = true;

                    var purl = @Purl + "&login=" + Login + "&passwd=" + Password + "&twoweeks=yes";

                    var serverRequest = (HttpWebRequest)WebRequest.Create(purl);
                    FillRequestParameters(serverRequest, Cookies, MyCredential);

                    var outdata = GetResponceFrom(serverRequest);

                    int indexOf = outdata.IndexOf("Неправильная пара логин-пароль!");

                    if (indexOf != -1)
                    {
                        MessageBox.Show("Неправильная пара логин-пароль!");
                        abort = true;
                    }

                    LastLogin = Login;
                }
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new WebException("Error authorizing.", e);
            }
        }

        ///<summary>Заполнение параметров запроса к серверу</summary>
        ///<param name="serverRequest">Запрос к серверу</param>
        ///<param name="cookies">Cookies пользователя</param>
        ///<param name="myCredential">Данные аутентификации</param>
        ///<param name="method">Метод запроса POST или GET</param>
        public static void FillRequestParameters(HttpWebRequest serverRequest, CookieContainer cookies, CredentialCache myCredential, string method = "POST")
        {
            serverRequest.Credentials = myCredential;
            serverRequest.UserAgent = UserAgentString;
            serverRequest.CookieContainer = cookies;
            serverRequest.ContentType = ContentType + "; boundary=" + Boundary;
            serverRequest.KeepAlive = true;
            //serverRequest.SendChunked = true;
            serverRequest.Pipelined = true;
            serverRequest.ReadWriteTimeout = ServerTimeout;
            //serverRequest.ServicePoint.Expect100Continue = true;
            serverRequest.Timeout = ServerTimeout;
            serverRequest.ServicePoint.SetTcpKeepAlive(true, ServerTimeout, 1000);
            serverRequest.Method = method;
            serverRequest.Accept = "*/*";
        }

        ///<summary>Получение ответа сервера</summary>
        ///<param name="serverRequest">Запрос к серверу</param>
        ///<returns>Данные от сервера</returns>
        public static string GetResponceFrom(HttpWebRequest serverRequest)
        {
            string outdata = null;
            try
            {
                var responce = (HttpWebResponse)serverRequest.GetResponse();
                var s = responce.GetResponseStream();
                if (s != null)
                {
                    var sr = new StreamReader(s);

                    outdata = sr.ReadToEnd();
                }

                responce.Close();
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new WebException("Error on request", e);
            }

            return outdata;
        }

        //******************************************************************************
        // ЗАГРУЗКА ФАЙЛА
        //
        private static ManualResetEvent allDone = new ManualResetEvent(false);
        private byte[] postHeaderBytesAs;
        private byte[] boundaryBytesAs;

        protected void FillUploadParametersAs()
        {
            // Build up the post message header
            var sb = new StringBuilder();
            sb.Append("--");
            sb.Append(Boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(Filename);
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Transfer-Encoding: binary");
            sb.Append("\r\n");
            sb.Append("\r\n");

            var postHeader = sb.ToString();
            postHeaderBytesAs = Encoding.UTF8.GetBytes(postHeader);

            boundaryBytesAs =
                Encoding.ASCII.GetBytes("\r\n--" + Boundary + "\r\n");

            Dowloaded = 0;
            Dowloaded2 = 0;
        }

        protected void GetRequestStreamCallback(IAsyncResult asynchronousResult)
        {
            var request = (HttpWebRequest)asynchronousResult.AsyncState;

            // End the operation
            Stream postStream = request.EndGetRequestStream(asynchronousResult);

            // Запись заголовка
            postStream.Write(postHeaderBytesAs, 0, postHeaderBytesAs.Length);
            Stop = false;
            // Запись содержимого
            //WriteToStream(fileStream, requestStream, fileStream.Length);
            var buffer = new Byte[checked((uint)Math.Min(BufferSize, Length))];
            int bytesRead;
            while ((bytesRead = UpfileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                while (Pause)
                {
                    if (abortthread) break;
                }

                if (abortthread) break;

                postStream.Write(buffer, 0, bytesRead);
                Dowloaded += bytesRead;
            }

            if (abortthread)
            {
                UpfileStream.Close();
            }
            else
            {
                // Запись boundary
                postStream.Write(boundaryBytesAs, 0, boundaryBytesAs.Length);
            }
            postStream.Close();
            Stop = true;

            // Start the asynchronous operation to get the response
            allDone.Set();
        }

        ///<summary>Составление параметров для отправки файла. Загрузка файла</summary>
        ///<param name="serverRequest">Запрос к серверу</param>
        protected void FillUploadParameters(HttpWebRequest serverRequest)
        {
            requestStream = null;
            try
            {
                // Build up the post message header
                var sb = new StringBuilder();
                sb.Append("--");
                sb.Append(Boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data; name=\"");
                sb.Append("file");
                sb.Append("\"; filename=\"");
                sb.Append(Filename);
                sb.Append("\"");
                sb.Append("\r\n");
                sb.Append("Content-Transfer-Encoding: binary");
                sb.Append("\r\n");
                sb.Append("\r\n");

                var postHeader = sb.ToString();
                var postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

                var boundaryBytes =
                    Encoding.ASCII.GetBytes("\r\n--" + Boundary + "\r\n");

                Thread.Sleep(3000);

                serverRequest.ContentLength = postHeaderBytes.Length + Length + boundaryBytes.Length;

                Dowloaded = 0;
                Dowloaded2 = 0;

                requestStream = serverRequest.GetRequestStream();
                requestStream.WriteTimeout = ServerTimeout;
                requestStream.ReadTimeout = ServerTimeout;

                // Запись заголовка
                requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                Stop = false;
                // Запись содержимого
                //WriteToStream(fileStream, requestStream, fileStream.Length);
                var buffer = new Byte[checked((uint)Math.Min(BufferSize, Length))];
                int bytesRead;
                while ((bytesRead = UpfileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    while (Pause)
                    {
                        if (abortthread) break;
                    }

                    if (abortthread) break;

                    requestStream.Write(buffer, 0, bytesRead);
                    Dowloaded += bytesRead;
                }

                if (abortthread)
                {
                    UpfileStream.Close();
                }
                else
                {
                    // Запись boundary
                    requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                }
                requestStream.Close();
                Stop = true;
            }
            catch (ThreadAbortException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (WebException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                throw new WebException("Error uploading file.", e);
            }
            finally
            {
                if (UpfileStream != null)
                {
                    UpfileStream.Close();
                    UpfileStream.Dispose();
                }

                if (requestStream != null)
                {
                    requestStream.Close();
                    requestStream.Dispose();
                }
            }
        }

        protected void UploadFormFormClosing(object sender, FormClosingEventArgs e)
        {
            abortthread = true;
        }

        ///<summary>Вызов функций авторизации и загрузки</summary>
        public void UploadFile(Object stateInfo)
        {
            // Аутентификация на сервере
            PassportAuthentication();

            // Получение данных для загрузки файла
            serverRequest = (HttpWebRequest)WebRequest.Create(Uurl);
            FillRequestParameters(serverRequest, Cookies, MyCredential);

            var outdata = GetResponceFrom(serverRequest);

            // Парсим страницу, чтобы получить ссылку на сервер
            const string pattern = "\"(\\S+)\":\"(\\S+)\"";

            string[] mtc = { "", "", "" };
            byte i = 0;
            foreach (Match mt in Regex.Matches(outdata, pattern))
            {
                mtc.SetValue(mt.Groups[2].Value, i);
                i++;
            }

            var sUrl = mtc[0] + "?tid=" + mtc[1];

            // Загружает файл на сервер
            serverRequest = (HttpWebRequest)WebRequest.Create(sUrl);
            FillRequestParameters(serverRequest, Cookies, MyCredential);

            //FillUploadParametersAs();

            //serverRequest.BeginGetRequestStream(GetRequestStreamCallback, serverRequest);

            //allDone.WaitOne();
            FillUploadParameters(serverRequest);

            //Надо вставить ожидание, секунд 10
            Thread.Sleep(5000);

            GetLinkToLastFile();
        }

        ///<summary>Получаем ссылку на последний загруженный файл</summary>
        protected void GetLinkToLastFile()
        {
            // Получаем страницу с ответом
            serverRequest = (HttpWebRequest)WebRequest.Create(Lurl);
            FillRequestParameters(serverRequest, Cookies, MyCredential);

            var outdata = GetResponceFrom(serverRequest);

            // Парсим её чтобы получить ссылку на файл
            const string pattern = "<span class='b-fname'><a href=\"(.*?)\">";
            foreach (var mt in from Match mt in Regex.Matches(outdata, pattern) let groups = mt.Groups select mt)
            {
                SLink = SetLinkToRightView(mt.Groups[1].Value);
            }
        }

        protected string SetLinkToRightView(string link)
        {
            var result = link;

            switch (AppSettings.LinksViewInUploadBox)
            {
                case 2:
                    result = "<a target=\"_blank\" href=\"" + link + "\">" + Path.GetFileName(Filename) + "</a>"; ;
                    break;
                case 1:
                    result = "[url=" + link + "]" + Path.GetFileName(Filename) + "[/url]";
                    break;
                default:
                    break;
            }

            return result;
        }

        //******************************************************************************
        // РАБОТА СО СПИСКОМ ФАЙЛОВ
        // Получение страниц, выделение файлов
        //

        ///<summary>Получает следующую страницу со списком файлов</summary>
        ///<param name="gurl">Ссылка на следующую страницу</param>
        ///<returns>Текст страницы</returns>
        public static string GetPage(string gurl)
        {
            var serverRequest = (HttpWebRequest)WebRequest.Create(gurl);
            FillRequestParameters(serverRequest, Cookies, MyCredential);

            var outdata = GetResponceFrom(serverRequest);
            outdata = outdata.Replace("<td class=\"date\"><nobr>", "<td class=\"date\">");
            outdata = outdata.Replace("</nobr><!--", "</td><!--");

            return outdata.Replace("<wbr/>", "");
        }

        ///<summary>Получить список файлов на сервере</summary>
        ///<returns>Массив ссылок</returns>
        public static void GetFilesList()
        {
            // Yandex Passpotr Authentication
            PassportAuthentication();

            var gurl = Aurl + "page1/?sort=cdate%20desc";

            var outdata = GetPage(gurl);

            const string patternfile = "class=\"\\S+icon\\s(\\S+)\"[^<]+<img[^<]+</i[^<]+</td[^<]+<td[^<]+<input[^v]+value=\"(\\d+)\"[^<]+</td[^<]+<td[^<]+<span\\sclass='b-fname'><a\\shref=\"(\\S+)\">([^<]+)";
            const string patterndate = "td\\sclass=\"date\\sprolongate\"><nobr>(\\d+)";
            const string patterntoken = "td\\sclass=\"checkboxes\"><input\\stype=\"checkbox\"\\sname=\"fid\"\\svalue=\"(\\S+)\"\\sdata-token=\"(\\S+)\"";
            const string patternsize = "td\\sclass=\"size\">(\\S+)&nbsp;(\\S+)<";
            const string patternstat = "td\\sclass=\"date\"><span\\stitle=\"(.+)\">[^<]+</span></td>[^<]+<td\\sclass=\"date\">([^<]+)";

            byte k = 0;

            var xmlFile = new XmlFile();
            xmlFile.SetRefreshedToZero();

            var atext = new string[10][];
            foreach (var mt in from Match mt in Regex.Matches(outdata, patternfile) let groups = mt.Groups select mt)
            {
                atext[k] = new string[9];
                atext[k][0] = mt.Groups[1].Value;
                atext[k][1] = mt.Groups[2].Value;
                atext[k][2] = mt.Groups[3].Value;
                atext[k][3] = mt.Groups[4].Value;
                k++;
            }
            k = 0;
            foreach (var mt in from Match mt in Regex.Matches(outdata, patterndate) let groups = mt.Groups select mt)
            {
                atext[k][4] = mt.Groups[1].Value;
                k++;
            }
            k = 0;
            foreach (var mt in from Match mt in Regex.Matches(outdata, patterntoken) let groups = mt.Groups select mt)
            {
                atext[k][5] = mt.Groups[2].Value;
                k++;
            }
            k = 0;
            foreach (var mt in from Match mt in Regex.Matches(outdata, patternsize) let groups = mt.Groups select mt)
            {
                atext[k][6] = mt.Groups[1].Value + " " + mt.Groups[2].Value;
                k++;
            }
            k = 0;
            foreach (var mt in from Match mt in Regex.Matches(outdata, patternstat) let groups = mt.Groups select mt)
            {
                var val = mt.Groups[2].Value;
                val = val.Replace("&ndash;", "0");
                val = val.Replace(" раза", "");
                val = val.Replace(" раз", "");
                atext[k][7] = val.Trim();
                atext[k][8] = mt.Groups[1].Value;
                xmlFile.AddFile(atext[k]);
                k++;
            }

            const string patternnextpage = "<a\\sid=\"next_page\"\\shref=\"([^\"]+)\"";
            var lastpageurl = gurl;
            foreach (var mt in from Match mt in Regex.Matches(outdata, patternnextpage) let groups = mt.Groups select mt)
            {
                gurl = "http://narod.yandex.ru" + mt.Groups[1].Value;
            }

            while (gurl != lastpageurl)
            {
                lastpageurl = gurl;

                outdata = GetPage(gurl);

                foreach (var mt in from Match mt in Regex.Matches(outdata, patternnextpage) let groups = mt.Groups select mt)
                {
                    gurl = "http://narod.yandex.ru" + mt.Groups[1].Value;
                }
                atext = new string[10][];
                k = 0;
                foreach (var mt in from Match mt in Regex.Matches(outdata, patternfile) let groups = mt.Groups select mt)
                {
                    atext[k] = new string[9];
                    atext[k][0] = mt.Groups[1].Value;
                    atext[k][1] = mt.Groups[2].Value;
                    atext[k][2] = mt.Groups[3].Value;
                    atext[k][3] = mt.Groups[4].Value;
                    k++;
                }
                k = 0;
                foreach (var mt in from Match mt in Regex.Matches(outdata, patterndate) let groups = mt.Groups select mt)
                {
                    atext[k][4] = mt.Groups[1].Value;
                    k++;
                }
                k = 0;
                foreach (var mt in from Match mt in Regex.Matches(outdata, patterntoken) let groups = mt.Groups select mt)
                {
                    atext[k][5] = mt.Groups[2].Value;
                    k++;
                }
                k = 0;
                foreach (var mt in from Match mt in Regex.Matches(outdata, patternsize) let groups = mt.Groups select mt)
                {
                    atext[k][6] = mt.Groups[1].Value + " " + mt.Groups[2].Value;
                    k++;
                }
                k = 0;
                foreach (var mt in from Match mt in Regex.Matches(outdata, patternstat) let groups = mt.Groups select mt)
                {
                    var val = mt.Groups[2].Value;
                    val = val.Replace("&ndash;", "0");
                    val = val.Replace(" раза", "");
                    val = val.Replace(" раз", "");
                    atext[k][7] = val.Trim();
                    atext[k][8] = mt.Groups[1].Value;
                    xmlFile.AddFile(atext[k]);
                    k++;
                }
            }
            xmlFile.RemoveNotRefreshed();
        }

        ///<summary>Действие с файлами на сервере - продление или удаление</summary>
        ///<param name="fileids">Список идентификаторов файлов для удаления</param>
        ///<param name="action">Действие которое необходимо произвести: delete, prolongate</param>
        public static void ProcessFiles(string[] fileids, string action = "prolongate")
        {
            // Yandex Passpotr Authentication
            PassportAuthentication();

            // Build up the post message header
            var sb = new StringBuilder();
            sb.Append("action=" + action);

            if (action == "delete")
            {
                var xmldata = new XmlFile();
                xmldata.OpenXmlFile();
                foreach (var fileid in fileids)
                {
                    sb.Append("&fid=" + fileid + "&token-" + fileid + "=" + xmldata.GetToken(fileid));
                }
            }
            else
            {
                foreach (var fileid in fileids)
                {
                    sb.Append("&fid=" + fileid);
                }
            }

            var postHeader = sb.ToString();

            var durl = Aurl + "?" + postHeader;

            var serverRequest = (HttpWebRequest)WebRequest.Create(durl);

            serverRequest.Credentials = MyCredential;
            serverRequest.UserAgent = UserAgentString;
            serverRequest.CookieContainer = Cookies;
            serverRequest.Referer = Aurl;
            serverRequest.Method = "POST";
            serverRequest.Headers.Add("Accept-Language", "ru");
            serverRequest.AllowAutoRedirect = false;

            GetResponceFrom(serverRequest);

            //if (action == "delete")
            //{
            //    var xmlFile = new XmlFile();
            //    xmlFile.DeleteFile(fileids);
            //}
        }
    }
}