using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace NarodUploadWebClient
{
    ///<summary>����� �������� � ������������ �������� ����������</summary>
    [Serializable]
    public class NuSettings : INotifyPropertyChanged
    {
        ///<summary>����� ��� ����������� �� �������</summary>
        public string Login { get; set; }

        ///<summary>������ ��� ����������� �� �������</summary>
        public string Password { get; set; }

        ///<summary>����� �� ���������� ���� �������� � ��������</summary>
        public bool ShowInTaskBar { get; set; }

        ///<summary>������� ������������ ���� ��������</summary>
        public int WndOpacity { get; set; }

        ///<summary>���������� �� ��������� ������� �����</summary>
        public bool CheckCurrentFolder { get; set; }

        ///<summary>������� �����</summary>
        public string CurrentFolder { get; set; }

        ///<summary>��������� ��� ��� ListView</summary>
        public View SavedView { get; set; }

        ///<summary>������ ������� ListView</summary>
        public int[] ColumnsWidth { get; set; }

        ///<summary>������������ ���� ��� ������</summary>
        public byte IconsTheme { get; set; }

        ///<summary>������������� �� ������� SMTP ������</summary>
        public bool UseOtherSmtpServer { get; set; }

        ///<summary>����� ��� �������� SMTP �������</summary>
        public string OtherSmtpLogin { get; set; }

        ///<summary>������ ��� �������� SMTP �������</summary>
        public string OtherSmtpPassword { get; set; }

        ///<summary>����� �������� SMTP �������</summary>
        public string OtherSmtpServer { get; set; }

        ///<summary>E-Mail ��� �������� SMTP �������</summary>
        public string OtherEMail { get; set; }

        ///<summary>�������� ��� ������� ������ �� �����</summary>
        public byte DoubleClickAction { get; set; }

        ///<summary>��� ���������� ����� ���� ��������</summary>
        public bool UploadWindowPosition { get; set; }

        ///<summary>������� ����������� ��������</summary>
        public int[] ColumnsArrange { get; set; }

        ///<summary>��� ������ � ����, ����� ��������</summary>
        public byte LinksViewInUploadBox { get; set; }

        ///<summary>��������� ������� PropertyChangedEventHandler</summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        ///<summary>����������� ������</summary>
        ///<param name="login">����� ��� ����������� �� �������</param>
        ///<param name="password">������ ��� ����������� �� �������</param>
        public NuSettings(string login, string password)
        {
            Login = login;
            Password = password;
        }

        static public View GetView(string view)
        {
            switch (view)
            {
                case "Tile":
                    return View.Tile;
                case "SmallIcon":
                    return View.SmallIcon;
                case "List":
                    return View.List;
                case "LargeIcon":
                    return View.LargeIcon;
                default:
                    return View.Details;
            }
        }

        static public void SaveSettings(NuSettings AppSettings, string ProgramPath)
        {
            if (File.Exists(ProgramPath + @"\Settings.xml"))
            {
                File.Delete(ProgramPath + @"\Settings.xml");
            }
            var xmlFile = new XmlFile();
            xmlFile.Filename = ProgramPath + @"\Settings.xml";
            xmlFile.RootName = "Settings";
            xmlFile.OpenXmlFile();
            var settingsData = xmlFile.XmlData;
            settingsData.SetAttributeValue("Version", MainForm.AppVersion);

            settingsData.Add(new XElement("Login", AppSettings.Login));
            settingsData.Add(new XElement("Password", AppSettings.Password));
            settingsData.Add(new XElement("ShowInTaskBar", AppSettings.ShowInTaskBar));
            settingsData.Add(new XElement("WndOpacity", AppSettings.WndOpacity));
            settingsData.Add(new XElement("CheckCurrentFolder", AppSettings.CheckCurrentFolder));
            settingsData.Add(new XElement("CurrentFolder", AppSettings.CurrentFolder));
            settingsData.Add(new XElement("SavedView", AppSettings.SavedView));
            settingsData.Add(new XElement("IconsTheme", AppSettings.IconsTheme));
            settingsData.Add(new XElement("UseOtherSmtpServer", AppSettings.UseOtherSmtpServer));
            settingsData.Add(new XElement("OtherSmtpLogin", AppSettings.OtherSmtpLogin));
            settingsData.Add(new XElement("OtherSmtpPassword", AppSettings.OtherSmtpPassword));
            settingsData.Add(new XElement("OtherSmtpServer", AppSettings.OtherSmtpServer));
            settingsData.Add(new XElement("OtherEMail", AppSettings.OtherEMail));
            settingsData.Add(new XElement("DoubleClickAction", AppSettings.DoubleClickAction));
            settingsData.Add(new XElement("UploadWindowPosition", AppSettings.UploadWindowPosition));
            settingsData.Add(new XElement("LinksViewInUploadBox", AppSettings.LinksViewInUploadBox));

            if (AppSettings.ColumnsArrange == null)
            {
                AppSettings.ColumnsArrange = new int[10];
            }
            if (AppSettings.ColumnsWidth == null)
            {
                AppSettings.ColumnsWidth = new int[10];
            }
            for (int i = 0; i < 10; i++)
            {
                if (AppSettings.ColumnsArrange.Length > i)
                {
                    settingsData.Add(new XElement("ColumnsArrange_" + Convert.ToString(i), AppSettings.ColumnsArrange[i]));
                }
                else
                {
                    settingsData.Add(new XElement("ColumnsArrange_" + Convert.ToString(i), i));
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (AppSettings.ColumnsWidth.Length > i)
                {
                    settingsData.Add(new XElement("ColumnsWidth_" + Convert.ToString(i), AppSettings.ColumnsWidth[i]));
                }
                else
                {
                    settingsData.Add(new XElement("ColumnsWidth_" + Convert.ToString(i), 20));
                }
            }

            settingsData.Save(ProgramPath + @"\Settings.xml");
        }

        static public void LoadSettings(NuSettings AppSettings, string ProgramPath)
        {
            if (File.Exists(ProgramPath + @"\Settings.xml"))
            {
                try
                {
                    var xmlFile = new XmlFile();
                    xmlFile.Filename = ProgramPath + @"\Settings.xml";
                    xmlFile.RootName = "Settings";
                    xmlFile.OpenXmlFile();

                    var settingsData = xmlFile.XmlData;

                    AppSettings.Login = settingsData.Element("Login").Value;
                    AppSettings.Password = settingsData.Element("Password").Value;
                    AppSettings.ShowInTaskBar = settingsData.Element("ShowInTaskBar").Value == "true" ? true : false;
                    AppSettings.WndOpacity = Convert.ToInt32(settingsData.Element("WndOpacity").Value);
                    AppSettings.CheckCurrentFolder = settingsData.Element("CheckCurrentFolder").Value == "true" ? true : false;
                    AppSettings.CurrentFolder = settingsData.Element("CurrentFolder").Value;
                    AppSettings.SavedView = GetView(settingsData.Element("SavedView").Value);
                    AppSettings.IconsTheme = Convert.ToByte(settingsData.Element("IconsTheme").Value);
                    AppSettings.UseOtherSmtpServer = settingsData.Element("UseOtherSmtpServer").Value == "true" ? true : false;
                    AppSettings.OtherSmtpLogin = settingsData.Element("OtherSmtpLogin").Value;
                    AppSettings.OtherSmtpPassword = settingsData.Element("OtherSmtpPassword").Value;
                    AppSettings.OtherSmtpServer = settingsData.Element("OtherSmtpServer").Value;
                    AppSettings.OtherEMail = settingsData.Element("OtherEMail").Value;
                    AppSettings.DoubleClickAction = Convert.ToByte(settingsData.Element("DoubleClickAction").Value);
                    AppSettings.UploadWindowPosition = settingsData.Element("UploadWindowPosition").Value == "true" ? true : false;
                    AppSettings.LinksViewInUploadBox = Convert.ToByte(settingsData.Element("LinksViewInUploadBox").Value);

                    if (AppSettings.ColumnsArrange == null)
                    {
                        AppSettings.ColumnsArrange = new int[10];
                    }
                    if (AppSettings.ColumnsWidth == null)
                    {
                        AppSettings.ColumnsWidth = new int[10];
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        AppSettings.ColumnsArrange[i] = Convert.ToInt32(settingsData.Element("ColumnsArrange_" + Convert.ToString(i)).Value);
                        AppSettings.ColumnsWidth[i] = Convert.ToInt32(settingsData.Element("ColumnsWidth_" + Convert.ToString(i)).Value);
                    }
                }
                catch
                {
                    MessageBox.Show(
                        "�� ������� ��������� ���� � ����������� ��������� (�������� �� ���������).\n                    ��������� ����� ��������� �� ���������.");
                    DateTime now = DateTime.Now;
                    File.Move(ProgramPath + @"\Settings.xml",
                              ProgramPath + @"\Settings.xml" +
                              (Convert.ToString(now.Year) + Convert.ToString(now.Month) + Convert.ToString(now.Day) +
                               Convert.ToString(now.Hour) + Convert.ToString(now.Minute) + Convert.ToString(now.Second)) +
                              ".xml");
                    LoadDefaultSettings(AppSettings, ProgramPath);
                }
            }
            else
            {
                LoadDefaultSettings(AppSettings, ProgramPath);
            }
        }

        static public void LoadDefaultSettings(NuSettings AppSettings, string ProgramPath)
        {
            AppSettings.Login = "";
            AppSettings.Password = "";
            AppSettings.ShowInTaskBar = true;
            AppSettings.WndOpacity = 90;
            AppSettings.CheckCurrentFolder = false;
            AppSettings.CurrentFolder = "root";
            AppSettings.SavedView = View.Details;
            AppSettings.IconsTheme = 1;
            AppSettings.UseOtherSmtpServer = false;
            AppSettings.OtherSmtpLogin = "";
            AppSettings.OtherSmtpPassword = "";
            AppSettings.OtherSmtpServer = "";
            AppSettings.OtherEMail = "";
            AppSettings.DoubleClickAction = 4;
            AppSettings.UploadWindowPosition = true;
            AppSettings.LinksViewInUploadBox = 0;

            SaveSettings(AppSettings, ProgramPath);
        }
    }
}