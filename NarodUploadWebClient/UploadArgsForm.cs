using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using NarodUploadWebClient.Properties;

namespace NarodUploadWebClient
{
    public partial class UploadArgsForm : FilesProcess
    {
        ///<summary>Массив файлов для загрузки</summary>
        public string[] Filenames;

        protected int ProcVal, Proc1;

        private int nFile;

        private string[] SLinks;
        private int ColLinks;
        private bool cont;

        private ThumbnailToolBarButton buttonPause;

        private int _t1;

        public UploadArgsForm(string login, string password, string fname, string[] Filename)
        {
            InitializeComponent();
            this.Load += OnLoad;

            if (Filename.Length > 0)
            {
                if (Filename[0] == "-upload")
                {
                    if (SelectFilesToUpload.Input(out Filenames))
                    {
                        cont = true;
                    }
                }
                else
                {
                    Filenames = Filename;
                    cont = true;
                }
                if (cont)
                {
                    Login = login;
                    Password = password;

                    ColLinks = Filenames.Length;
                    SLinks = new string[ColLinks];
                    ColLinks = 0;

                    _timer1.Start();

                    _labelSize.Text = "Подготовка к загрузке...";

                    ThreadPool.SetMaxThreads(1, 1);
                    ThreadPool.QueueUserWorkItem(Upload);
                }
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
            if (!cont)
            {
                Application.Exit();
            }
            else
            {
                if (TaskbarManager.IsPlatformSupported)
                {
                    TaskbarManager.Instance.ApplicationId = MainForm.AppID + ".UplWndArg";
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, this.Handle);
                    buttonPause = new ThumbnailToolBarButton(Resources.pauseico, "Пауза") { Enabled = true };
                    buttonPause.Click += ButtonPauseClick;
                    TaskbarManager.Instance.ThumbnailToolBars.AddButtons(this.Handle, buttonPause);
                }
            }
        }

        private void Upload(object state)
        {
            foreach (var filename in Filenames)
            {
                Filename = filename;

                UpfileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
                Length = (int)UpfileStream.Length;
                nFile = 1;

                UploadFile();
            }
        }

        private void ButtonLockClick(object sender, EventArgs e)
        {
            if (TopMost == false)
            {
                _buttonLock.Image = Resources.pinned;
                TopMost = true;
            }
            else
            {
                _buttonLock.Image = Resources.unpinned;
                TopMost = false;
            }
        }

        private void ButtonPauseClick(object sender, EventArgs e)
        {
            Pause = !Pause;
        }

        //******************************************************************************
        // ЗАГРУЗКА ФАЙЛА
        //

        private void Timer1Tick(object sender, EventArgs e)
        {
            if (abort)
            {
                Close();
            }
            if (_t1 == -2)
            {
                _buttonLock.Visible = false;
                _buttonPause.Visible = false;
                _label2.Visible = false;
                _label3.Visible = false;
                _label4.Visible = false;
                _label5.Visible = false;
                _label6.Visible = false;
                _labelLong.Visible = false;
                _labelSize.Visible = false;
                _labelSpeed.Visible = false;
                _labelTime.Visible = false;
                _labelUpload.Visible = false;
                _uploadingProgress.Visible = false;
                _textBox1.Visible = true;
                _textBox1.Lines = SLinks;
                _timer1.Stop();
            }
            if ((Dowloaded > 0) && (Dowloaded != Length))
            {
                _labelSize.Text = Length.ToString("### ### ##0") + " б.";
            }
            if ((Dowloaded == Length) || (Length == 0))
            {
                _labelSize.Text = "Ожидание...";
                if (TaskbarManager.IsPlatformSupported)
                {
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Indeterminate, this.Handle);
                }
            }
            else
            {
                if (TaskbarManager.IsPlatformSupported)
                {
                    TaskbarManager.Instance.SetProgressState(
                        Pause ? TaskbarProgressBarState.Paused : TaskbarProgressBarState.Normal, this.Handle);
                    buttonPause.Icon = !Pause ? Resources.pauseico : Resources.playico;
                    _buttonPause.Image = !Pause ? Resources.pause : Resources.play;
                }
            }
            //разница между кол-вом загруженных байт в интервале 1 сек.
            float speed = Dowloaded - Dowloaded2;
            var temp = (int)speed;
            //скорость в Кб
            if ((speed > 0) && (Kbyte > 0))
            {
                speed = speed / Kbyte;
            }
            //метод вывода времени загрузки
            Time();
            //вывод информации о скорости загрузки
            _labelSpeed.Text = speed.ToString("0.00") + " Кб/сек.";
            //вывод информации о кол-ве загруженных байт
            _labelUpload.Text = Dowloaded.ToString("### ### ##0") + " б.";
            //вывод информации о процентах загруженного файла в строке заголовка формы
            int t = 0, h, m, s;
            //если скорость != 0
            if ((temp > 1) && ((Length - Dowloaded) > 0))
            {   //делю разницу от размера загружаемого файла и кол-вом
                //уже загруженных байт на скорость
                t = (Length - Dowloaded) / temp;
                //подсчет остатка времени загрузки
                Ostatok(t, out h, out m, out s);
                //вывод времени остатка загрузки(приблизительно)
                _labelLong.Text = h.ToString("00") + " : " + m.ToString("00") + " : " + s.ToString("00");
            }
            else _labelLong.Text = string.Empty;//если скорость 0,то время не отображается
            //текущее значение для "прогресс-бара"
            ProcVal = 0;
            if (nFile == 1)
            {
                nFile = 0;
                Dowloaded = 0;
            }
            if ((Dowloaded > 0) && (Length > 0) && (Dowloaded <= Length))
            {
                Proc1 = Length / 100;
                if (Proc1 == 0) Proc1 = 1;
                ProcVal = Dowloaded / Proc1;
                if (ProcVal > 100) ProcVal = 100;
            }
            _uploadingProgress.Value = ProcVal;
            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.SetProgressValue(_uploadingProgress.Value, _uploadingProgress.Maximum, this.Handle);
            }
            //сравниваю кол-во загруженных байт
            Dowloaded2 = Dowloaded;
            //если скорость 0 и флаг остановки
            if (speed == 0 && Stop)
            {   //остановка таймера и обнуление переменных
                TimeSec = 0; TimeMin = 0;
                TimeHas = 0;
                if (TaskbarManager.IsPlatformSupported)
                {
                    TaskbarManager.Instance.SetProgressValue(0, 100, this.Handle);
                    TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, this.Handle);
                }
            }
        }

        private void Time()
        {   //счет секунд.Интервал таймера 1000 мс.
            TimeSec += 1;
            //если число секунд 60, то прибавляю +1 минута и обнуляю сек.
            if (TimeSec == Min) { TimeMin += 1; TimeSec = 0; }
            //тоже, увеличиваю на +1 час.(мин=60)
            if (TimeMin == Min) { TimeHas += 1; TimeMin = 0; }
            //вывод информации о длительности загрузки
            _labelTime.Text = TimeHas.ToString("00") + " : " + TimeMin.ToString("00") + " : " + TimeSec.ToString("00");
        }

        private static void Ostatok(int t, out int h, out int m, out int s)
        {   //аргумент t - это расчетное время остатка загрузки в сек.
            //если t меньше  3600
            if (t < Has)
            {
                h = 0; //час = 0
                m = t / Min; //минуты = целое число,приблизительно результат деления
                s = t % Min; //сек. = остаток от деления t на 60
            }
            else //если t >= 3600
            {
                h = t / Has; //час = целое число от деления
                //если остаток от деления t на 3600 меньше 60,то мин.=0,сек = остаток от деления t на 3600
                //короче,если t от 3601 до 4019
                if ((t % Has) < Min) { m = 0; s = t % Has; }
                //иначе,мин. = остаток от деления t на 3600 делить на 60
                //сек. = остаток от деления мин. на 60
                else { m = (t % Has) / Min; s = m % Min; }
            }
        }

        //******************************************************************************
        // ЗАГРУЗКА ФАЙЛА
        //

        ///<summary>Вызов функций авторизации и загрузки</summary>
        public void UploadFile()
        {
            // Аутентификация на сервере
            PassportAuthentication();
            // Получение данных для загрузки файла
            serverRequest = (HttpWebRequest)WebRequest.Create(Uurl);
            FillRequestParameters(serverRequest, Cookies, MyCredential);

            var outdata = GetResponceFrom(serverRequest);

            // Парсим страницу, чтобы получить ссылку на сервер
            var pattern = "\"(\\S+)\":\"(\\S+)\"";

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

            FillUploadParameters(serverRequest);

            //Надо вставить ожидание, секунд 10

            Thread.Sleep(5000);

            // Получаем страницу с ответом
            serverRequest = (HttpWebRequest)WebRequest.Create(Lurl);
            FillRequestParameters(serverRequest, Cookies, MyCredential);

            outdata = GetResponceFrom(serverRequest);

            // Парсим её чтобы получить ссылку на файл
            pattern = "<span class='b-fname'><a href=\"(.*?)\">";
            foreach (var mt in from Match mt in Regex.Matches(outdata, pattern) let groups = mt.Groups select mt)
            {
                SLink = mt.Groups[1].Value;
            }

            SLinks[ColLinks] = SetLinkToRightView(SLink);
            if (SLinks.Length == (ColLinks + 1))
            {
                _t1 = -2;
            }
            ColLinks++;
        }
    }
}