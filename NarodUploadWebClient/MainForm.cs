using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Taskbar;
using NarodUploadWebClient.Properties;

namespace NarodUploadWebClient
{
    ///<summary>Основная форма приложения</summary>
    public partial class MainForm : Form
    {
        internal static NuSettings AppSettings = new NuSettings("", "");

        protected const string NuFileName = @".\SavedSettings.bin";

        private string _currentFolder = "root";

        private string[] _foldersarray;

        private Form _selectFolderForm;
        private Form _uploadFolderForm;
        private Form _settingsForm;
        private Form _inputForm;
        private string _inputText;
        private TextBox _sFile;

        protected ImageList imagesSmall;
        protected ImageList imagesLarge;

        protected string SmtpServer = "smtp.yandex.ru";

        public static string AppVersion;

        #region ListView

        private int mintScrollDirection;

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        const int WM_VSCROLL = 0x115; // Vertical scroll
        const int SB_LINEUP = 0; // Scrolls one line up
        const int SB_LINEDOWN = 1; // Scrolls one line down

        #endregion ListView

        #region Настройки WindowsAPICodePack

        internal const string AppID = "Loveworthy.Narod.Disk";
        private static JumpList _jumpList;
        private JumpListCustomCategory _currentCategory;

        #endregion Настройки WindowsAPICodePack

        #region Контролы формы настроек

        private TabControl _tabControl1;
        private TabPage _tabPageGeneral;
        private TabPage _tabPageUpload;
        private TabPage _tabPageView;
        private Label _lLogin;
        private Label _lPassword;
        private Label _label3;
        private Label _label2;
        private Label _label1;
        private Label _labelOpacity;
        private GroupBox _groupBoxTheme;
        public TextBox sLogin;
        public MaskedTextBox sPassword;
        public TrackBar trackBarOpacity;
        public CheckBox checkBoxShowInTaskBar;
        private RadioButton _radioButtonTheme1;
        private RadioButton _radioButtonTheme2;
        private CheckBox _checkBoxCurrentFolder;
        private CheckBox _checkBoxFilesToFolders;
        private TabPage _tabPagePost;
        private GroupBox _groupBoxSmtp;
        private TextBox _tbSmtpServer;
        private Label _labelsmtpserver;
        private TextBox _tbSmtpLogin;
        private Label _labelsmtplogin;
        private Label _labelsmtppassword;
        private MaskedTextBox _tbSmtpPassword;
        private CheckBox _checkBoxUseDefaultEMail;
        private TextBox _tbSenderEmail;
        private Label _labelemail;
        private GroupBox _groupBoxDoubleclickActions;
        private RadioButton _radioButtonDoubleClickClipboard;
        private RadioButton _radioButtonDoubleClickOpen;
        private RadioButton _radioButtonDoubleClickDoNothing;
        private RadioButton _radioButtonDoubleClickOpenDialog;
        private GroupBox _groupBoxLinksInUploadBox;
        private RadioButton _radioButtonHtmlCode;
        private RadioButton _radioButtonBBCode;
        private RadioButton _radioButtonStandartLink;
        private CheckBox _checkBoxUploadWindowPosition;

        #endregion Контролы формы настроек

        ///<summary>Инициализация формы</summary>
        public MainForm()
        {
            InitializeComponent();

            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.ApplicationId = AppID;
            }
        }

        //******************************************************************************
        // JumpList
        //

        // Private readonly property of the Jump List instance
        private static JumpList JumpList
        {
            get
            {
                if (_jumpList == null)
                {
                    _jumpList = JumpList.CreateJumpList();

                    _jumpList.KnownCategoryToDisplay = JumpListKnownCategoryType.Recent;

                    _jumpList.Refresh();
                }
                return _jumpList;
            }
        }

        private void AddTask()
        {
            try
            {
                var tskUpload = new JumpListLink(Application.ExecutablePath, "Загрузить файлы")
                                    {
                                        IconReference = new IconReference(Application.ExecutablePath, 2),
                                        Arguments = "-upload"
                                    };
                JumpList.AddUserTasks(tskUpload);
                JumpList.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //******************************************************************************
        // ОБЩИЙ РАЗДЕЛ
        //

        ///<summary>Получение номера картинки для файла по коду</summary>
        ///<param name="img">Код картинки</param>
        ///<returns>Номер картинки</returns>
        public int GetImageNum(string img)
        {
            switch (img)
            {
                case "b-old-icon-soft":
                    return 0;
                case "b-old-icon-music":
                    return 1;
                case "b-old-icon-picture":
                    return 2;
                case "b-old-icon-doc":
                    return 3;
                case "root":
                    return 4;
                case "b-old-icon-arc":
                    return 5;
                case "folder":
                    return 7;
                case "b-old-icon-unknown":
                    return 8;
                default:
                    return 6;
            }
        }

        public string GetImageFromExtention(string url)
        {
            var ext = Path.GetExtension(url);
            if (ext != null) { ext = ext.ToLower(); }
            if ((ext == ".7z") || (ext == ".iso"))
            {
                return "b-old-icon-arc";
            }
            else if ((ext == ".fb2") || (ext == ".fb3"))
            {
                return "b-old-icon-doc";
            }
            else
            {
                return "no";
            }
        }

        private void SendLinks(IEnumerable<string> links, string mailto)
        {
            var charray = mailto.ToCharArray();
            var ret = 0;
            foreach (var c in charray)
            {
                if (c == '@') ret = 1;
            }

            if (ret == 0) return;

            string login;
            string password;
            string mailfrom;
            if (_checkBoxUseDefaultEMail.Checked)
            {
                login = sLogin.Text;
                password = sPassword.Text;
                SmtpServer = "smtp.yandex.ru";
                mailfrom = sLogin.Text + "@yandex.ru";
            }
            else
            {
                login = _tbSmtpLogin.Text;
                password = _tbSmtpPassword.Text;
                SmtpServer = _tbSmtpServer.Text;
                mailfrom = _tbSenderEmail.Text;
            }

            var sText = "Ссылка на файл:";
            var sSubject = "Отправка файла";
            if (links.Count() > 1)
            {
                sSubject = "Отправка файлов";
                sText = "Ссылки на файлы:";
            }

            sText = links.Aggregate(sText, (current, link) => current + "\n" + link);

            var client = new SmtpClient(SmtpServer);
            client.Credentials = new System.Net.NetworkCredential(login, password);
            var from = new MailAddress(mailfrom);
            var to = new MailAddress(mailto);
            var message = new MailMessage(from, to)
                              {
                                  Body = sText,
                                  BodyEncoding = System.Text.Encoding.UTF8,
                                  Subject = sSubject,
                                  SubjectEncoding = System.Text.Encoding.UTF8
                              };
            try
            {
                client.Send(message);
                MessageBox.Show(Resources.MainForm_SendLinks_Message_sended_ + mailto);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            message.Dispose();
        }

        //******************************************************************************
        // ОБРАБОТЧИКИ ФОРМЫ
        //
        private void Upload(IEnumerable<string> lines)
        {
            FilesProcess.WndCol = 0;
            ThreadPool.SetMaxThreads(4, 4);
            foreach (var line in lines)
            {
                var uploadform = new UploadForm(sLogin.Text, sPassword.Text, line);
                ThreadPool.QueueUserWorkItem(uploadform.UploadFile);
            }
        }

        private void TsUploadClick(object sender, EventArgs e)
        {
            string[] lines;

            if (SelectFilesToUpload.Input(out lines))
            {
                Upload(lines);
            }
        }

        private static void OpenLinkInBrowser(string sText)
        {
            var process = new Process { StartInfo = { UseShellExecute = true, FileName = sText } };
            process.Start();
        }

        private void MainFormFormClosed(object sender, FormClosedEventArgs e)
        {
            var crPass = Crypt.Encrypt(sPassword.Text, "XWERD");
            var crsmtpPass = Crypt.Encrypt(_tbSmtpPassword.Text, "XWERD");

            AppSettings.Login = sLogin.Text;
            AppSettings.Password = crPass;

            AppSettings.UseOtherSmtpServer = _checkBoxUseDefaultEMail.Checked;
            AppSettings.OtherSmtpLogin = _tbSmtpLogin.Text;
            AppSettings.OtherSmtpPassword = crsmtpPass;
            AppSettings.OtherSmtpServer = _tbSmtpServer.Text;
            AppSettings.OtherEMail = _tbSenderEmail.Text;

            if (AppSettings.CheckCurrentFolder)
            {
                AppSettings.CurrentFolder = _currentFolder;
            }

            AppSettings.SavedView = listView1.View;

            if (_radioButtonDoubleClickDoNothing.Checked)
            {
                AppSettings.DoubleClickAction = 1;
            }
            else if (_radioButtonDoubleClickOpen.Checked)
            {
                AppSettings.DoubleClickAction = 2;
            }
            else if (_radioButtonDoubleClickOpenDialog.Checked)
            {
                AppSettings.DoubleClickAction = 4;
            }
            else
            {
                AppSettings.DoubleClickAction = 3;
            }

            if (_radioButtonHtmlCode.Checked)
            {
                AppSettings.LinksViewInUploadBox = 2;
            }
            else if (_radioButtonBBCode.Checked)
            {
                AppSettings.LinksViewInUploadBox = 1;
            }
            else
            {
                AppSettings.LinksViewInUploadBox = 0;
            }

            AppSettings.UploadWindowPosition = _checkBoxUploadWindowPosition.Checked;

            AppSettings.ColumnsWidth = new int[10];
            for (var i = 0; i < 10; i++)
            {
                if (listView1.Columns.Count > i)
                {
                    AppSettings.ColumnsWidth[i] = listView1.Columns[i].Width;
                }
                else
                {
                    AppSettings.ColumnsWidth[i] = 75;
                }
            }

            AppSettings.ColumnsArrange = new int[10];
            for (var i = 0; i < 10; i++)
            {
                if (listView1.Columns.Count > i)
                {
                    AppSettings.ColumnsArrange[i] = listView1.Columns[i].DisplayIndex;
                }
                else
                {
                    AppSettings.ColumnsArrange[i] = i;
                }
            }

            //Stream appFileStream = File.Create(NuFileName);
            //var serializer = new BinaryFormatter();
            //serializer.Serialize(appFileStream, AppSettings);
            //appFileStream.Close();

            NuSettings.SaveSettings(AppSettings, Application.StartupPath);
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            InitializeSettingsForm();

            AppVersion = Application.ProductVersion;

            if (File.Exists(NuFileName))
            {
                Stream appFileStream = File.OpenRead(NuFileName);
                var deserializer = new BinaryFormatter();
                AppSettings = (NuSettings)deserializer.Deserialize(appFileStream);
                appFileStream.Close();

                File.Delete(NuFileName);
            }
            else
            {
                NuSettings.LoadSettings(AppSettings, Application.StartupPath);
            }

            var tPass = AppSettings.Password;
            var crPass = "";
            if (tPass != "") crPass = Crypt.Decrypt(tPass, "XWERD");

            var smtpPass = AppSettings.OtherSmtpPassword;
            var crsmtpPass = "";
            if (smtpPass != "") crsmtpPass = Crypt.Decrypt(smtpPass, "XWERD");

            sLogin.Text = AppSettings.Login;
            sPassword.Text = crPass;

            _checkBoxUseDefaultEMail.Checked = AppSettings.UseOtherSmtpServer;
            _tbSmtpLogin.Text = AppSettings.OtherSmtpLogin;
            _tbSmtpPassword.Text = crsmtpPass;
            _tbSmtpServer.Text = AppSettings.OtherSmtpServer;
            _tbSenderEmail.Text = AppSettings.OtherEMail;

            checkBoxShowInTaskBar.Checked = AppSettings.ShowInTaskBar;
            trackBarOpacity.Value = AppSettings.WndOpacity;
            _checkBoxCurrentFolder.Checked = AppSettings.CheckCurrentFolder;
            _checkBoxUploadWindowPosition.Checked = AppSettings.UploadWindowPosition;

            switch (AppSettings.DoubleClickAction)
            {
                case 4:
                    _radioButtonDoubleClickOpenDialog.Checked = true;
                    break;
                case 3:
                    _radioButtonDoubleClickClipboard.Checked = true;
                    break;
                case 2:
                    _radioButtonDoubleClickOpen.Checked = true;
                    break;
                default:
                    _radioButtonDoubleClickDoNothing.Checked = true;
                    break;
            }

            switch (AppSettings.LinksViewInUploadBox)
            {
                case 2:
                    _radioButtonHtmlCode.Checked = true;
                    break;
                case 1:
                    _radioButtonBBCode.Checked = true;
                    break;
                default:
                    _radioButtonStandartLink.Checked = true;
                    break;
            }

            if (AppSettings.CheckCurrentFolder)
            {
                _currentFolder = AppSettings.CurrentFolder;
            }

            if (AppSettings.IconsTheme == 2)
            {
                _radioButtonTheme2.Checked = true;
            }
            else
            {
                _radioButtonTheme1.Checked = true;
            }

            FilesProcess.AppSettings = AppSettings;

            try
            {
                if (AppSettings.ColumnsArrange != null)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (AppSettings.ColumnsArrange.Length > i)
                        {
                            listView1.Columns[i].DisplayIndex = AppSettings.ColumnsArrange[i];
                        }
                        else
                        {
                            listView1.Columns[i].DisplayIndex = i;
                        }
                    }
                }

                if (AppSettings.ColumnsWidth != null)
                {
                    for (var i = 0; i < 10; i++)
                    {
                        if (AppSettings.ColumnsWidth.Length > i)
                        {
                            listView1.Columns[i].Width = AppSettings.ColumnsWidth[i];
                        }
                        else
                        {
                            listView1.Columns[i].Width = 75;
                        }
                    }
                }
            }
            catch (Exception ex)
            { }
            listView1.View = AppSettings.SavedView;

            SwitchView(listView1.View);

            FillList();
        }

        ///<summary>Установка значков оформления вида 1</summary>
        ///<param name="theme">Номер темы (1 по умолчанию)</param>
        protected void SetIconsTheme(int theme = 1)
        {
            listView1.BeginUpdate();

            imagesSmall = new ImageList();
            imagesLarge = new ImageList();

            imagesSmall.ImageSize = new Size(32, 32);
            imagesSmall.ColorDepth = ColorDepth.Depth32Bit;

            imagesLarge.ImageSize = new Size(64, 64);
            imagesLarge.ColorDepth = ColorDepth.Depth32Bit;

            if (theme == 1)
            {
                imagesSmall.Images.Add(Resources.soft32);
                imagesSmall.Images.Add(Resources.music32);
                imagesSmall.Images.Add(Resources.picture32);
                imagesSmall.Images.Add(Resources.document32);
                imagesSmall.Images.Add(Resources.root32);
                imagesSmall.Images.Add(Resources.arc32);
                imagesSmall.Images.Add(Resources.unknown32);
                imagesSmall.Images.Add(Resources.folder32);

                imagesLarge.Images.Add(Resources.soft64);
                imagesLarge.Images.Add(Resources.music64);
                imagesLarge.Images.Add(Resources.picture64);
                imagesLarge.Images.Add(Resources.document64);
                imagesLarge.Images.Add(Resources.root64);
                imagesLarge.Images.Add(Resources.arc64);
                imagesLarge.Images.Add(Resources.unknown64);
                imagesLarge.Images.Add(Resources.folder64);
            }
            else
            {
                imagesSmall.Images.Add(Resources.soft32_2);
                imagesSmall.Images.Add(Resources.music32_2);
                imagesSmall.Images.Add(Resources.picture32_2);
                imagesSmall.Images.Add(Resources.document32_2);
                imagesSmall.Images.Add(Resources.root32_2);
                imagesSmall.Images.Add(Resources.arc32_2);
                imagesSmall.Images.Add(Resources.unknown32_2);
                imagesSmall.Images.Add(Resources.folder32_2);

                imagesLarge.Images.Add(Resources.soft64_2);
                imagesLarge.Images.Add(Resources.music64_2);
                imagesLarge.Images.Add(Resources.picture64_2);
                imagesLarge.Images.Add(Resources.document64_2);
                imagesLarge.Images.Add(Resources.root64_2);
                imagesLarge.Images.Add(Resources.arc64_2);
                imagesLarge.Images.Add(Resources.unknown64_2);
                imagesLarge.Images.Add(Resources.folder64_2);
            }
            listView1.SmallImageList = imagesSmall;
            listView1.LargeImageList = imagesLarge;
            listView1.EndUpdate();
        }

        private void BDeleteClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var count = listView1.SelectedItems.Count;
            if (count > 0)
            {
                var sText = new string[count];
                for (int i = 0; i < count; i++)
                {
                    sText[i] = listView1.SelectedItems[i].SubItems[1].Text;
                }
                FilesProcess.PassportAuthentication(sLogin.Text, sPassword.Text);
                FilesProcess.ProcessFiles(sText, "delete");
            }
            FilesProcess.PassportAuthentication(sLogin.Text, sPassword.Text);
            FilesProcess.GetFilesList();
            FillList();
            Cursor = Cursors.Arrow;
        }

        private void BProlongateClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            var count = listView1.SelectedItems.Count;
            if (count > 0)
            {
                var sText = new string[count];
                for (int i = 0; i < count; i++)
                {
                    sText[i] = listView1.SelectedItems[i].SubItems[1].Text;
                }
                FilesProcess.PassportAuthentication(sLogin.Text, sPassword.Text);
                FilesProcess.ProcessFiles(sText);
            }
            FilesProcess.PassportAuthentication(sLogin.Text, sPassword.Text);
            FilesProcess.GetFilesList();
            FillList();
            Cursor = Cursors.Arrow;
        }

        private static int SearchInArray(IEnumerable<string> array, string s)
        {
            if (array.Any(s1 => s1 == s))
            {
                return 1;
            }
            return -1;
        }

        private void FillList()
        {
            var dsStore = new DataSet();
            if (File.Exists("files.xml"))
            {
                dsStore.ReadXml("files.xml");

                var table = dsStore.Tables["file"];

                if (table != null)
                {
                    listView1.Items.Clear();
                    // Suspending automatic refreshes as items are added/removed.
                    listView1.BeginUpdate();
                    if (!tsChangeViewType.Checked)
                    {
                        if (_currentFolder == "root")
                        {
                            _foldersarray = new string[100];

                            byte i = 0;
                            foreach (DataRow dr in table.Rows)
                            {
                                var sFolder = dr["folder"].ToString();
                                if (sFolder != "root")
                                {
                                    int myIndex = SearchInArray(_foldersarray, sFolder);

                                    if (myIndex == -1)
                                    {
                                        _foldersarray[i] = sFolder;
                                        i++;
                                    }
                                }
                            }
                            for (int j = 0; j < i; j++)
                            {
                                var listItem = new ListViewItem(_foldersarray[j])
                                                   {
                                                       ImageIndex = GetImageNum("folder"),
                                                       Tag = _foldersarray[j]
                                                   };
                                listView1.Items.Add(listItem);
                            }
                        }
                        else
                        {
                            var listItem = new ListViewItem("root")
                                               {
                                                   ImageIndex = GetImageNum("root"),
                                                   Tag = "root"
                                               };
                            listView1.Items.Add(listItem);
                        }
                    }
                    foreach (DataRow dr in table.Rows)
                    {
                        if (!tsChangeViewType.Checked)
                        {
                            var sFolder = dr["folder"].ToString();
                            if (sFolder != _currentFolder)
                            {
                                continue;
                            }
                        }

                        var imgnum = GetImageNum(dr["icon"].ToString());
                        if (imgnum == 8)
                        {
                            imgnum = GetImageNum(GetImageFromExtention(dr["name"].ToString()));
                        }

                        var listItem = new ListViewItem(dr["name"].ToString())
                        {
                            ImageIndex = imgnum
                        };

                        // Add sub-items for Details view.
                        listItem.SubItems.Add(dr["fid"].ToString());
                        listItem.SubItems.Add(dr["date"].ToString());
                        listItem.SubItems.Add(dr["size"].ToString());
                        listItem.SubItems.Add(dr["stat"].ToString());
                        listItem.SubItems.Add(dr["uploaddate"].ToString());
                        listItem.SubItems.Add(dr["tag"].ToString());
                        listItem.SubItems.Add(dr["folder"].ToString());

                        listView1.Items.Add(listItem);
                    }

                    // Re-enable the display.
                    listView1.EndUpdate();
                }
            }
        }

        private void BFillClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            FilesProcess.PassportAuthentication(sLogin.Text, sPassword.Text);
            FilesProcess.GetFilesList();

            FillList();
            Cursor = Cursors.Arrow;
        }

        private void TsFolderClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0) return;
            if (listView1.SelectedItems[0].ImageIndex == 7) return;

            for (int i = 0; i < listView1.SelectedItems.Count; i++)
                if (listView1.SelectedItems[i].ImageIndex == 7) return;

            var selectFolderBox = new ComboBox { Location = new Point(6, 6), Size = new Size(282, 150) };
            if (_foldersarray.Length > 0)
            {
                if (_currentFolder != "root")
                {
                    selectFolderBox.Items.Add("root");
                }
                foreach (var s in _foldersarray)
                {
                    if (s == null)
                    {
                        break;
                    }
                    if (_currentFolder != s)
                    {
                        selectFolderBox.Items.Add(s);
                    }
                }
                selectFolderBox.Text = _foldersarray[0];
            }

            var buttonSelect = new Button
                                   {
                                       Location = new Point(100, 50),
                                       Name = "_buttonSelect",
                                       Size = new Size(75, 25),
                                       Text = Resources.MainForm_tsFolder_Click_OK,
                                       TabIndex = 34,
                                       UseVisualStyleBackColor = true
                                   };
            buttonSelect.Click += ButtonSelectClick;

            _selectFolderForm = new Form
                                   {
                                       AutoScaleDimensions = new SizeF(6F, 13F),
                                       AutoScaleMode = AutoScaleMode.Font,
                                       ClientSize = new Size(294, 140),
                                       ControlBox = true,
                                       FormBorderStyle = FormBorderStyle.FixedToolWindow,
                                       MaximizeBox = false,
                                       MinimizeBox = false,
                                       Name = "FolderForm",
                                       ShowInTaskbar = false,
                                       Text = Resources.MainForm_tsFolder_Click_Select_folder,
                                       TopMost = true
                                   };
            _selectFolderForm.Controls.Add(buttonSelect);
            _selectFolderForm.Controls.Add(selectFolderBox);
            _selectFolderForm.ResumeLayout(false);
            _selectFolderForm.PerformLayout();
            _selectFolderForm.Show();
        }

        private void ButtonSelectClick(object sender, EventArgs e)
        {
            var sFolder = _selectFolderForm.Controls[1].Text;
            _selectFolderForm.Close();

            var sItems = listView1.SelectedItems;

            if ((sItems.Count > 0) && (sFolder != ""))
            {
                var fileids = new string[sItems.Count];
                for (int i = 0; i < sItems.Count; i++)
                {
                    fileids[i] = sItems[i].SubItems[1].Text;
                }
                var xmlFile = new XmlFile();
                xmlFile.ChangeFolder(fileids, sFolder);
                FillList();
            }
        }

        private void BViewClick(object sender, EventArgs e)
        {
            if (listView1.View == View.Details)
            {
                listView1.View = View.LargeIcon;
            }
            else if (listView1.View == View.LargeIcon)
            {
                listView1.View = View.SmallIcon;
            }
            else if (listView1.View == View.SmallIcon)
            {
                listView1.View = View.List;
            }
            else if (listView1.View == View.List)
            {
                listView1.View = View.Tile;
            }
            else
            {
                listView1.View = View.Details;
            }
            SwitchView(listView1.View);
        }

        private void SwitchView(View view)
        {
            switch (view)
            {
                case View.Details:
                    {
                        tsViewDetailed.Checked = true;
                        tsViewLarge.Checked = false;
                        tsViewSmall.Checked = false;
                        tsViewList.Checked = false;
                        tsViewTile.Checked = false;
                        break;
                    }

                case View.LargeIcon:
                    {
                        tsViewDetailed.Checked = false;
                        tsViewLarge.Checked = true;
                        tsViewSmall.Checked = false;
                        tsViewList.Checked = false;
                        tsViewTile.Checked = false;
                        break;
                    }

                case View.SmallIcon:
                    {
                        tsViewDetailed.Checked = false;
                        tsViewLarge.Checked = false;
                        tsViewSmall.Checked = true;
                        tsViewList.Checked = false;
                        tsViewTile.Checked = false;
                        break;
                    }

                case View.List:
                    {
                        tsViewDetailed.Checked = false;
                        tsViewLarge.Checked = false;
                        tsViewSmall.Checked = false;
                        tsViewList.Checked = true;
                        tsViewTile.Checked = false;
                        break;
                    }

                case View.Tile:
                    {
                        tsViewDetailed.Checked = false;
                        tsViewLarge.Checked = false;
                        tsViewSmall.Checked = false;
                        tsViewList.Checked = false;
                        tsViewTile.Checked = true;
                        break;
                    }
            }
        }

        private void BViewClickDetails(object sender, EventArgs e)
        {
            listView1.View = View.Details;
            SwitchView(listView1.View);
        }

        private void BViewClickLarge(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
            SwitchView(listView1.View);
        }

        private void BViewClickSmall(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
            SwitchView(listView1.View);
        }

        private void BViewClickList(object sender, EventArgs e)
        {
            listView1.View = View.List;
            SwitchView(listView1.View);
        }

        private void BViewClickTile(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
            SwitchView(listView1.View);
        }

        private void ListView1ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var listViewItemSorter = (ListViewSorter)listView1.ListViewItemSorter;
            if (listViewItemSorter == null)
            {
                listViewItemSorter = new ListViewSorter();
                listView1.ListViewItemSorter = listViewItemSorter;
            }
            if (listViewItemSorter.LastSort == e.Column)
            {
                listView1.Sorting = listView1.Sorting == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                listView1.Sorting = SortOrder.Descending;
            }
            listViewItemSorter.ByColumn = e.Column;
            listViewItemSorter.LastSort = e.Column;
            listView1.Sort();
        }

        private void ListView1DoubleClick(object sender, EventArgs e)
        {
            if ((listView1.SelectedItems[0].ImageIndex == 7) || (listView1.SelectedItems[0].ImageIndex == 4))
            {
                _currentFolder = listView1.SelectedItems[0].Tag.ToString();
                FillList();
            }
            else
            {
                switch (AppSettings.DoubleClickAction)
                {
                    case 2:
                        {
                            var count = listView1.SelectedItems.Count;

                            for (int i = 0; i < count; i++)
                            {
                                OpenLinkInBrowser(listView1.SelectedItems[i].SubItems[2].Text);
                            }
                        }
                        break;
                    case 3:
                        {
                            var count = listView1.SelectedItems.Count;
                            var sText = "";
                            for (int i = 0; i < count; i++)
                            {
                                sText += listView1.SelectedItems[i].SubItems[2].Text + "\n";
                            }

                            Clipboard.SetText(sText);
                        }
                        break;
                    case 4:
                        {
                            ShowLinksInWindow();
                        }
                        break;
                }
            }
        }

        private void ShowLinksInWindow()
        {
            var xmlFile = new XmlFile();
            xmlFile.OpenXmlFile();

            var count = listView1.SelectedItems.Count;
            var sText = new string[count];
            var sTextForum = new string[count];
            var sTextHtml = new string[count];
            for (int i = 0; i < count; i++)
            {
                sText[i] = xmlFile.GetLink(listView1.SelectedItems[i].SubItems[1].Text);
                sTextForum[i] = "[url=" + xmlFile.GetLink(listView1.SelectedItems[i].SubItems[1].Text) + "]" + listView1.SelectedItems[i].Text + "[/url]";
                sTextHtml[i] = "<a target=\"_blank\" href=\"" + xmlFile.GetLink(listView1.SelectedItems[i].SubItems[1].Text) + "\">" + listView1.SelectedItems[i].Text + "</a>";
            }
            LinksForm.ShowLinks(sText, sTextForum, sTextHtml);
        }

        private void ListView1SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count != 0)
            {
                //var text = listView1.SelectedItems[0].SubItems[0].Text;
                //var str2 = listView1.SelectedItems[0].SubItems[1].Text;
            }
        }

        private void TsSettingsClick(object sender, EventArgs e)
        {
            _settingsForm.Show();
            if ((_settingsForm.WindowState == FormWindowState.Minimized) || (_settingsForm.Focused == false))
            {
                _settingsForm.Activate();
                _settingsForm.WindowState = FormWindowState.Normal;
            }
        }

        private void TsSendClick(object sender, EventArgs e)
        {
            string mailto;

            if (InputBox.Input("Ввод строки", "Введите e-mail получателя", out mailto))
            {
                Cursor = Cursors.WaitCursor;
                var count = listView1.SelectedItems.Count;
                if (count > 0)
                {
                    var sText = new string[count];
                    for (int i = 0; i < count; i++)
                    {
                        sText[i] = listView1.SelectedItems[i].SubItems[2].Text;
                    }
                    SendLinks(sText, mailto);
                }
                Cursor = Cursors.Arrow;
            }
        }

        //******************************************************************************
        // ФОРМА НАСТРОЕК
        //

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeSettingsForm()
        {
            _tabControl1 = new TabControl();
            _tabPageGeneral = new TabPage();
            _checkBoxCurrentFolder = new CheckBox();
            sLogin = new TextBox();
            _lLogin = new Label();
            _lPassword = new Label();
            sPassword = new MaskedTextBox();
            _tabPageUpload = new TabPage();
            checkBoxShowInTaskBar = new CheckBox();
            _label3 = new Label();
            _label2 = new Label();
            _label1 = new Label();
            _labelOpacity = new Label();
            trackBarOpacity = new TrackBar();
            _tabPageView = new TabPage();
            _groupBoxTheme = new GroupBox();
            _radioButtonTheme1 = new RadioButton();
            _radioButtonTheme2 = new RadioButton();
            _checkBoxFilesToFolders = new CheckBox();
            _tabPagePost = new TabPage();
            _checkBoxUseDefaultEMail = new CheckBox();
            _groupBoxSmtp = new GroupBox();
            _tbSmtpLogin = new TextBox();
            _labelsmtplogin = new Label();
            _labelsmtppassword = new Label();
            _tbSmtpPassword = new MaskedTextBox();
            _labelsmtpserver = new Label();
            _tbSmtpServer = new TextBox();
            _labelemail = new Label();
            _tbSenderEmail = new TextBox();
            _groupBoxDoubleclickActions = new GroupBox();
            _radioButtonDoubleClickDoNothing = new RadioButton();
            _radioButtonDoubleClickOpen = new RadioButton();
            _radioButtonDoubleClickClipboard = new RadioButton();
            _groupBoxLinksInUploadBox = new GroupBox();
            _radioButtonHtmlCode = new RadioButton();
            _radioButtonBBCode = new RadioButton();
            _radioButtonStandartLink = new RadioButton();
            _checkBoxUploadWindowPosition = new CheckBox();
            _radioButtonDoubleClickOpenDialog = new RadioButton();
            _tabControl1.SuspendLayout();
            _tabPageGeneral.SuspendLayout();
            _tabPageUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(trackBarOpacity)).BeginInit();
            _tabPageView.SuspendLayout();
            _groupBoxTheme.SuspendLayout();
            _tabPagePost.SuspendLayout();
            _groupBoxDoubleclickActions.SuspendLayout();
            _groupBoxSmtp.SuspendLayout();
            //_settingsForm.SuspendLayout();
            //
            // tabControl1
            //
            _tabControl1.Controls.Add(_tabPageGeneral);
            _tabControl1.Controls.Add(_tabPageUpload);
            _tabControl1.Controls.Add(_tabPageView);
            _tabControl1.Controls.Add(_tabPagePost);
            _tabControl1.Dock = DockStyle.Fill;
            _tabControl1.Location = new Point(0, 0);
            _tabControl1.Multiline = true;
            _tabControl1.Name = "_tabControl1";
            _tabControl1.SelectedIndex = 0;
            _tabControl1.Size = new Size(534, 312);
            _tabControl1.TabIndex = 0;
            //
            // tabPageGeneral
            //
            _tabPageGeneral.Controls.Add(_checkBoxUploadWindowPosition);
            _tabPageGeneral.Controls.Add(_groupBoxDoubleclickActions);
            _tabPageGeneral.Controls.Add(_checkBoxCurrentFolder);
            _tabPageGeneral.Controls.Add(sLogin);
            _tabPageGeneral.Controls.Add(_lLogin);
            _tabPageGeneral.Controls.Add(_lPassword);
            _tabPageGeneral.Controls.Add(sPassword);
            _tabPageGeneral.Location = new Point(4, 22);
            _tabPageGeneral.Name = "_tabPageGeneral";
            _tabPageGeneral.Padding = new Padding(3);
            _tabPageGeneral.Size = new Size(525, 277);
            _tabPageGeneral.TabIndex = 0;
            _tabPageGeneral.Text = Resources.MainForm_Tabs_General__;
            _tabPageGeneral.UseVisualStyleBackColor = true;
            //
            // checkBoxCurrentFolder
            //
            _checkBoxCurrentFolder.AutoSize = true;
            _checkBoxCurrentFolder.Location = new Point(10, 60);
            _checkBoxCurrentFolder.Name = "checkBoxCurrentFolder";
            _checkBoxCurrentFolder.Size = new Size(242, 17);
            _checkBoxCurrentFolder.TabIndex = 25;
            _checkBoxCurrentFolder.Text = Resources.MainForm_InitializeSettingForm_Memorize_last_folder_on_exit;
            _checkBoxCurrentFolder.UseVisualStyleBackColor = true;
            //
            // sLogin
            //
            sLogin.Location = new Point(109, 6);
            sLogin.Name = "sLogin";
            sLogin.Size = new Size(164, 20);
            sLogin.TabIndex = 18;
            //
            // lLogin
            //
            _lLogin.AutoSize = true;
            _lLogin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            _lLogin.Location = new Point(7, 9);
            _lLogin.Name = "_lLogin";
            _lLogin.Size = new Size(96, 13);
            _lLogin.TabIndex = 17;
            _lLogin.Text = Resources.MainForm_InitializeSettingForm_Login_;
            //
            // lPassword
            //
            _lPassword.AutoSize = true;
            _lPassword.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            _lPassword.Location = new Point(7, 37);
            _lPassword.Name = "_lPassword";
            _lPassword.Size = new Size(59, 13);
            _lPassword.TabIndex = 19;
            _lPassword.Text = Resources.MainForm_InitializeSettingForm_Password__;
            //
            // sPassword
            //
            sPassword.Location = new Point(109, 34);
            sPassword.Name = "sPassword";
            sPassword.PasswordChar = '*';
            sPassword.Size = new Size(164, 20);
            sPassword.TabIndex = 20;
            //
            // tabPageUpload
            //
            _tabPageUpload.Controls.Add(_groupBoxLinksInUploadBox);
            _tabPageUpload.Controls.Add(_checkBoxFilesToFolders);
            _tabPageUpload.Controls.Add(checkBoxShowInTaskBar);
            _tabPageUpload.Controls.Add(_label3);
            _tabPageUpload.Controls.Add(_label2);
            _tabPageUpload.Controls.Add(_label1);
            _tabPageUpload.Controls.Add(_labelOpacity);
            _tabPageUpload.Controls.Add(trackBarOpacity);
            _tabPageUpload.Location = new Point(4, 22);
            _tabPageUpload.Name = "_tabPageUpload";
            _tabPageUpload.Padding = new Padding(3);
            _tabPageUpload.Size = new Size(525, 277);
            _tabPageUpload.TabIndex = 1;
            _tabPageUpload.Text = Resources.MainForm_InitializeSettingForm_Uploading;
            _tabPageUpload.UseVisualStyleBackColor = true;
            //
            // checkBoxShowInTaskBar
            //
            checkBoxShowInTaskBar.AutoSize = true;
            checkBoxShowInTaskBar.Location = new Point(11, 77);
            checkBoxShowInTaskBar.Name = "checkBoxShowInTaskBar";
            checkBoxShowInTaskBar.Size = new Size(223, 17);
            checkBoxShowInTaskBar.TabIndex = 29;
            checkBoxShowInTaskBar.Text = Resources.MainForm_InitializeSettingForm_Show_Uploading_Form_In_Taskbar;
            checkBoxShowInTaskBar.UseVisualStyleBackColor = true;
            checkBoxShowInTaskBar.CheckedChanged += CheckBoxShowInTaskBarCheckedChanged;
            //
            // label3
            //
            _label3.AutoSize = true;
            _label3.Location = new Point(131, 51);
            _label3.Name = "_label3";
            _label3.Size = new Size(27, 13);
            _label3.TabIndex = 28;
            _label3.Text = "50%";
            //
            // label2
            //
            _label2.AutoSize = true;
            _label2.Location = new Point(246, 51);
            _label2.Name = "_label2";
            _label2.Size = new Size(33, 13);
            _label2.TabIndex = 27;
            _label2.Text = "100%";
            //
            // label1
            //
            _label1.AutoSize = true;
            _label1.Location = new Point(16, 50);
            _label1.Name = "_label1";
            _label1.Size = new Size(21, 13);
            _label1.TabIndex = 26;
            _label1.Text = "1%";
            //
            // labelOpacity
            //
            _labelOpacity.AutoSize = true;
            _labelOpacity.Location = new Point(8, 3);
            _labelOpacity.Name = "_labelOpacity";
            _labelOpacity.Size = new Size(158, 13);
            _labelOpacity.TabIndex = 25;
            _labelOpacity.Text = Resources.MainForm_InitializeSettingForm_Opacity_Uploading_Form__;
            //
            // trackBarOpacity
            //
            trackBarOpacity.BackColor = SystemColors.Window;
            trackBarOpacity.LargeChange = 50;
            trackBarOpacity.Location = new Point(9, 19);
            trackBarOpacity.Margin = new Padding(1);
            trackBarOpacity.Maximum = 100;
            trackBarOpacity.Name = "trackBarOpacity";
            trackBarOpacity.Size = new Size(263, 45);
            trackBarOpacity.TabIndex = 24;
            trackBarOpacity.TickFrequency = 10;
            trackBarOpacity.Value = 90;
            trackBarOpacity.Scroll += TrackBarOpacityScroll;
            //
            // tabPageView
            //
            _tabPageView.Controls.Add(_groupBoxTheme);
            _tabPageView.Location = new Point(4, 22);
            _tabPageView.Name = "_tabPageView";
            _tabPageView.Size = new Size(526, 286);
            _tabPageView.TabIndex = 2;
            _tabPageView.Text = Resources.MainForm_InitializeSettingForm_View__;
            _tabPageView.UseVisualStyleBackColor = true;
            //
            // groupBoxTheme
            //
            _groupBoxTheme.Controls.Add(_radioButtonTheme1);
            _groupBoxTheme.Controls.Add(_radioButtonTheme2);
            _groupBoxTheme.Location = new Point(8, 3);
            _groupBoxTheme.Name = "_groupBoxTheme";
            _groupBoxTheme.Size = new Size(144, 78);
            _groupBoxTheme.TabIndex = 29;
            _groupBoxTheme.TabStop = false;
            _groupBoxTheme.Text = Resources.MainForm_InitializeSettingForm_Icon_Themes_;
            //
            // radioButtonTheme1
            //
            _radioButtonTheme1.AutoSize = true;
            _radioButtonTheme1.Location = new Point(6, 19);
            _radioButtonTheme1.Name = "radioButtonTheme1";
            _radioButtonTheme1.Size = new Size(61, 17);
            _radioButtonTheme1.TabIndex = 27;
            _radioButtonTheme1.Text = Resources.MainForm_InitializeSettingForm_Тheme_1;
            _radioButtonTheme1.UseVisualStyleBackColor = true;
            _radioButtonTheme1.CheckedChanged += RadioButtonTheme1CheckedChanged;
            //
            // radioButtonTheme2
            //
            _radioButtonTheme2.AutoSize = true;
            _radioButtonTheme2.Location = new Point(6, 42);
            _radioButtonTheme2.Name = "radioButtonTheme2";
            _radioButtonTheme2.Size = new Size(61, 17);
            _radioButtonTheme2.TabIndex = 28;
            _radioButtonTheme2.Text = Resources.MainForm_InitializeSettingForm_Theme_2;
            _radioButtonTheme2.UseVisualStyleBackColor = true;
            _radioButtonTheme2.CheckedChanged += RadioButtonTheme2CheckedChanged;
            //
            // checkBoxFilesToFolders
            //
            _checkBoxFilesToFolders.AutoSize = true;
            _checkBoxFilesToFolders.Location = new Point(11, 100);
            _checkBoxFilesToFolders.Name = "checkBoxFilesToFolders";
            _checkBoxFilesToFolders.Size = new Size(273, 17);
            _checkBoxFilesToFolders.TabIndex = 30;
            _checkBoxFilesToFolders.Text = Resources.MainForm_InitializeSettingForm_Files_To_Folder_After_Upload_;
            _checkBoxFilesToFolders.UseVisualStyleBackColor = true;
            _checkBoxFilesToFolders.CheckedChanged += CheckBoxCurrentFolderCheckedChanged;
            //
            // _tabPagePost
            //
            _tabPagePost.Controls.Add(_groupBoxSmtp);
            _tabPagePost.Controls.Add(_checkBoxUseDefaultEMail);
            _tabPagePost.Location = new Point(4, 22);
            _tabPagePost.Name = "_tabPagePost";
            _tabPagePost.Padding = new Padding(3);
            _tabPagePost.Size = new Size(526, 276);
            _tabPagePost.TabIndex = 3;
            _tabPagePost.Text = "Почта";
            _tabPagePost.UseVisualStyleBackColor = true;
            //
            // checkBoxUseDefaultEMail
            //
            _checkBoxUseDefaultEMail.AutoSize = true;
            _checkBoxUseDefaultEMail.Location = new Point(8, 6);
            _checkBoxUseDefaultEMail.Name = "checkBoxUseDefaultEMail";
            _checkBoxUseDefaultEMail.Size = new Size(198, 17);
            _checkBoxUseDefaultEMail.TabIndex = 0;
            _checkBoxUseDefaultEMail.Text = "Использовать стандартный ящик";
            _checkBoxUseDefaultEMail.UseVisualStyleBackColor = true;
            _checkBoxUseDefaultEMail.CheckedChanged += CheckBoxUseDefaultEMailCheckedChanged;
            //
            // groupBoxSMTP
            //
            _groupBoxSmtp.Controls.Add(_tbSenderEmail);
            _groupBoxSmtp.Controls.Add(_labelemail);
            _groupBoxSmtp.Controls.Add(_tbSmtpServer);
            _groupBoxSmtp.Controls.Add(_labelsmtpserver);
            _groupBoxSmtp.Controls.Add(_tbSmtpLogin);
            _groupBoxSmtp.Controls.Add(_labelsmtplogin);
            _groupBoxSmtp.Controls.Add(_labelsmtppassword);
            _groupBoxSmtp.Controls.Add(_tbSmtpPassword);
            _groupBoxSmtp.Location = new Point(8, 29);
            _groupBoxSmtp.Name = "groupBoxSmtp";
            _groupBoxSmtp.Size = new Size(280, 127);
            _groupBoxSmtp.TabIndex = 1;
            _groupBoxSmtp.TabStop = false;
            _groupBoxSmtp.Text = "Настройки внешнего SMTP сервера";
            //
            // tbSmtpLogin
            //
            _tbSmtpLogin.Location = new Point(108, 13);
            _tbSmtpLogin.Name = "tbSmtpLogin";
            _tbSmtpLogin.Size = new Size(164, 20);
            _tbSmtpLogin.TabIndex = 22;
            //
            // labelsmtplogin
            //
            _labelsmtplogin.AutoSize = true;
            _labelsmtplogin.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            _labelsmtplogin.Location = new Point(6, 16);
            _labelsmtplogin.Name = "labelsmtplogin";
            _labelsmtplogin.Size = new Size(83, 13);
            _labelsmtplogin.TabIndex = 21;
            _labelsmtplogin.Text = "Пользователь:";
            //
            // labelsmtppassword
            //
            _labelsmtppassword.AutoSize = true;
            _labelsmtppassword.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            _labelsmtppassword.Location = new System.Drawing.Point(6, 44);
            _labelsmtppassword.Name = "labelsmtppassword";
            _labelsmtppassword.Size = new Size(51, 13);
            _labelsmtppassword.TabIndex = 23;
            _labelsmtppassword.Text = "Пароль: ";
            //
            // tbSmtpPassword
            //
            _tbSmtpPassword.Location = new Point(108, 41);
            _tbSmtpPassword.Name = "tbSmtpPassword";
            _tbSmtpPassword.PasswordChar = '*';
            _tbSmtpPassword.Size = new Size(164, 20);
            _tbSmtpPassword.TabIndex = 24;
            //
            // labelsmtpserver
            //
            _labelsmtpserver.AutoSize = true;
            _labelsmtpserver.Location = new Point(6, 72);
            _labelsmtpserver.Name = "labelsmtpserver";
            _labelsmtpserver.Size = new Size(47, 13);
            _labelsmtpserver.TabIndex = 25;
            _labelsmtpserver.Text = "Сервер:";
            //
            // tbSmtpServer
            //
            _tbSmtpServer.Location = new Point(108, 69);
            _tbSmtpServer.Name = "tbSmtpServer";
            _tbSmtpServer.Size = new Size(164, 20);
            _tbSmtpServer.TabIndex = 26;
            //
            // labelemail
            //
            _labelemail.AutoSize = true;
            _labelemail.Location = new Point(6, 101);
            _labelemail.Name = "labelemail";
            _labelemail.Size = new Size(104, 13);
            _labelemail.TabIndex = 27;
            _labelemail.Text = "e-mail отправителя:";
            //
            // tbSenderEmail
            //
            _tbSenderEmail.Location = new Point(108, 98);
            _tbSenderEmail.Name = "tbSenderEmail";
            _tbSenderEmail.Size = new Size(164, 20);
            _tbSenderEmail.TabIndex = 28;
            //
            // groupBoxDoubleclickActions
            //
            _groupBoxDoubleclickActions.Controls.Add(_radioButtonDoubleClickOpenDialog);
            _groupBoxDoubleclickActions.Controls.Add(_radioButtonDoubleClickClipboard);
            _groupBoxDoubleclickActions.Controls.Add(_radioButtonDoubleClickOpen);
            _groupBoxDoubleclickActions.Controls.Add(_radioButtonDoubleClickDoNothing);
            _groupBoxDoubleclickActions.Location = new Point(10, 83);
            _groupBoxDoubleclickActions.Name = "groupBoxDoubleclickActions";
            _groupBoxDoubleclickActions.Size = new System.Drawing.Size(263, 117);
            _groupBoxDoubleclickActions.TabIndex = 26;
            _groupBoxDoubleclickActions.TabStop = false;
            _groupBoxDoubleclickActions.Text = "Действие при двойном щелчке на файле";
            //
            // radioButtonDoubleClickDoNothing
            //
            _radioButtonDoubleClickDoNothing.AutoSize = true;
            _radioButtonDoubleClickDoNothing.Location = new Point(6, 19);
            _radioButtonDoubleClickDoNothing.Name = "radioButtonDoubleClickDoNothing";
            _radioButtonDoubleClickDoNothing.Size = new Size(94, 17);
            _radioButtonDoubleClickDoNothing.TabIndex = 0;
            _radioButtonDoubleClickDoNothing.TabStop = true;
            _radioButtonDoubleClickDoNothing.Text = "Нет действия";
            _radioButtonDoubleClickDoNothing.UseVisualStyleBackColor = true;
            _radioButtonDoubleClickDoNothing.CheckedChanged += RadioButtonDoubleClickDoNothingCheckedChanged;
            //
            // radioButtonDoubleClickOpen
            //
            _radioButtonDoubleClickOpen.AutoSize = true;
            _radioButtonDoubleClickOpen.Location = new Point(6, 42);
            _radioButtonDoubleClickOpen.Name = "radioButtonDoubleClickOpen";
            _radioButtonDoubleClickOpen.Size = new Size(223, 17);
            _radioButtonDoubleClickOpen.TabIndex = 27;
            _radioButtonDoubleClickOpen.TabStop = true;
            _radioButtonDoubleClickOpen.Text = "Открывать ссылку/ссылки в браузере";
            _radioButtonDoubleClickOpen.UseVisualStyleBackColor = true;
            _radioButtonDoubleClickOpen.CheckedChanged += RadioButtonDoubleClickOpenCheckedChanged;
            //
            // radioButtonDoubleClickClipboard
            //
            _radioButtonDoubleClickClipboard.AutoSize = true;
            _radioButtonDoubleClickClipboard.Location = new Point(6, 65);
            _radioButtonDoubleClickClipboard.Name = "radioButtonDoubleClickClipboard";
            _radioButtonDoubleClickClipboard.Size = new Size(149, 17);
            _radioButtonDoubleClickClipboard.TabIndex = 28;
            _radioButtonDoubleClickClipboard.TabStop = true;
            _radioButtonDoubleClickClipboard.Text = "Копировать всё в буфер";
            _radioButtonDoubleClickClipboard.UseVisualStyleBackColor = true;
            _radioButtonDoubleClickClipboard.CheckedChanged += RadioButtonDoubleClickClipboardCheckedChanged;
            //
            // _radioButtonDoubleClickOpenDialog
            //
            _radioButtonDoubleClickOpenDialog.AutoSize = true;
            _radioButtonDoubleClickOpenDialog.Location = new System.Drawing.Point(6, 88);
            _radioButtonDoubleClickOpenDialog.Name = "radioButtonDoubleClickOpenDialog";
            _radioButtonDoubleClickOpenDialog.Size = new System.Drawing.Size(184, 17);
            _radioButtonDoubleClickOpenDialog.TabIndex = 28;
            _radioButtonDoubleClickOpenDialog.TabStop = true;
            _radioButtonDoubleClickOpenDialog.Text = "Отображать окно со ссылками";
            _radioButtonDoubleClickOpenDialog.UseVisualStyleBackColor = true;
            _radioButtonDoubleClickOpenDialog.CheckedChanged += RadioButtonDoubleOpenDialogCheckedChanged;
            //
            // checkBoxUploadWindowPosition
            //
            _checkBoxUploadWindowPosition.AutoSize = true;
            _checkBoxUploadWindowPosition.Location = new Point(10, 206);
            _checkBoxUploadWindowPosition.Name = "checkBoxUploadWindowPosition";
            _checkBoxUploadWindowPosition.Size = new Size(208, 17);
            _checkBoxUploadWindowPosition.TabIndex = 27;
            _checkBoxUploadWindowPosition.Text = "Особое размещение окон загрузки";
            _checkBoxUploadWindowPosition.UseVisualStyleBackColor = true;
            _checkBoxUploadWindowPosition.CheckedChanged += CheckBoxUploadWindowPositionCheckedChanged;
            //
            // groupBoxLinksInUploadBox
            //
            _groupBoxLinksInUploadBox.Controls.Add(_radioButtonHtmlCode);
            _groupBoxLinksInUploadBox.Controls.Add(_radioButtonBBCode);
            _groupBoxLinksInUploadBox.Controls.Add(_radioButtonStandartLink);
            _groupBoxLinksInUploadBox.Location = new System.Drawing.Point(11, 133);
            _groupBoxLinksInUploadBox.Name = "groupBoxLinksInUploadBox";
            _groupBoxLinksInUploadBox.Size = new System.Drawing.Size(263, 95);
            _groupBoxLinksInUploadBox.TabIndex = 31;
            _groupBoxLinksInUploadBox.TabStop = false;
            _groupBoxLinksInUploadBox.Text = "Вид ссылок после загрузки";
            //
            // radioButtonHtmlCode
            //
            _radioButtonHtmlCode.AutoSize = true;
            _radioButtonHtmlCode.Location = new System.Drawing.Point(6, 65);
            _radioButtonHtmlCode.Name = "radioButtonHtmlCode";
            _radioButtonHtmlCode.Size = new System.Drawing.Size(141, 17);
            _radioButtonHtmlCode.TabIndex = 28;
            _radioButtonHtmlCode.TabStop = true;
            _radioButtonHtmlCode.Text = "Ссылка с кодом HTML";
            _radioButtonHtmlCode.UseVisualStyleBackColor = true;
            _radioButtonHtmlCode.CheckedChanged += radioButtonHtmlCode_CheckedChanged;
            //
            // radioButtonBBCode
            //
            _radioButtonBBCode.AutoSize = true;
            _radioButtonBBCode.Location = new System.Drawing.Point(6, 42);
            _radioButtonBBCode.Name = "radioButtonBBCode";
            _radioButtonBBCode.Size = new System.Drawing.Size(200, 17);
            _radioButtonBBCode.TabIndex = 27;
            _radioButtonBBCode.TabStop = true;
            _radioButtonBBCode.Text = "Ссылка с кодом BB (для форумов)";
            _radioButtonBBCode.UseVisualStyleBackColor = true;
            _radioButtonBBCode.CheckedChanged += radioButtonBBCode_CheckedChanged;
            //
            // _radioButtonStandartLink
            //
            _radioButtonStandartLink.AutoSize = true;
            _radioButtonStandartLink.Location = new System.Drawing.Point(6, 19);
            _radioButtonStandartLink.Name = "radioButtonStandartLink";
            _radioButtonStandartLink.Size = new System.Drawing.Size(111, 17);
            _radioButtonStandartLink.TabIndex = 0;
            _radioButtonStandartLink.TabStop = true;
            _radioButtonStandartLink.Text = "Обычная ссылка";
            _radioButtonStandartLink.UseVisualStyleBackColor = true;
            _radioButtonStandartLink.CheckedChanged += radioButtonStandartLink_CheckedChanged;
            //
            // SettingsForm
            //
            _settingsForm = new Form();
            _settingsForm.AutoScaleDimensions = new SizeF(6F, 13F);
            _settingsForm.AutoScaleMode = AutoScaleMode.Font;
            _settingsForm.ClientSize = new Size(550, 340);
            _settingsForm.MaximumSize = new Size(550, 340);
            _settingsForm.MinimumSize = new Size(550, 340);
            _settingsForm.MaximizeBox = false;
            _settingsForm.Name = "SettingsForm";
            _settingsForm.Text = Resources.MainForm_Tabs_Settings__;
            _settingsForm.Icon = Resources.Settings;
            _settingsForm.Controls.Add(_tabControl1);
            _settingsForm.FormClosing += SettingsFormFormClosing;
            _tabControl1.ResumeLayout(false);
            _tabPageGeneral.ResumeLayout(false);
            _tabPageGeneral.PerformLayout();
            _tabPageUpload.ResumeLayout(false);
            _tabPageUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(trackBarOpacity)).EndInit();
            _tabPageView.ResumeLayout(false);
            _groupBoxTheme.ResumeLayout(false);
            _groupBoxTheme.PerformLayout();
            _tabPagePost.ResumeLayout(false);
            _tabPagePost.PerformLayout();
            _groupBoxSmtp.ResumeLayout(false);
            _groupBoxSmtp.PerformLayout();
            _groupBoxDoubleclickActions.ResumeLayout(false);
            _groupBoxDoubleclickActions.PerformLayout();
            _settingsForm.ResumeLayout(false);
            _settingsForm.PerformLayout();
        }

        #endregion Windows Form Designer generated code

        private void radioButtonStandartLink_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.LinksViewInUploadBox = 0;
        }

        private void radioButtonBBCode_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.LinksViewInUploadBox = 1;
        }

        private void radioButtonHtmlCode_CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.LinksViewInUploadBox = 2;
        }

        private void RadioButtonTheme1CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.IconsTheme = 1;
            SetIconsTheme();
        }

        private void RadioButtonTheme2CheckedChanged(object sender, EventArgs e)
        {
            AppSettings.IconsTheme = 2;
            SetIconsTheme(2);
        }

        private void RadioButtonDoubleClickDoNothingCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.DoubleClickAction = 1;
        }

        private void RadioButtonDoubleClickOpenCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.DoubleClickAction = 2;
        }

        private void RadioButtonDoubleClickClipboardCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.DoubleClickAction = 3;
        }

        private void RadioButtonDoubleOpenDialogCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.DoubleClickAction = 4;
        }

        private void TrackBarOpacityScroll(object sender, EventArgs e)
        {
            AppSettings.WndOpacity = trackBarOpacity.Value;
        }

        private void CheckBoxUploadWindowPositionCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.UploadWindowPosition = _checkBoxUploadWindowPosition.Checked;
        }

        private void CheckBoxShowInTaskBarCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.ShowInTaskBar = checkBoxShowInTaskBar.Checked;
        }

        private void CheckBoxCurrentFolderCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.CheckCurrentFolder = _checkBoxCurrentFolder.Checked;
        }

        private void CheckBoxUseDefaultEMailCheckedChanged(object sender, EventArgs e)
        {
            AppSettings.UseOtherSmtpServer = _checkBoxUseDefaultEMail.Checked;
        }

        private void SettingsFormFormClosing(object sender, FormClosingEventArgs e)
        {
            _settingsForm.Hide();
            NuSettings.SaveSettings(AppSettings, Application.StartupPath);
            e.Cancel = true;
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            var ItemsCount = listView1.SelectedItems.Count;
            for (int i = 0; i < ItemsCount; i++)
            {
                if ((listView1.SelectedItems[i].ImageIndex == 4) || (listView1.SelectedItems[i].ImageIndex == 7))
                {
                    return;
                }
            }
            listView1.DoDragDrop(listView1.SelectedItems, DragDropEffects.Move);
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            int len = e.Data.GetFormats().Length - 1;
            int i;
            for (i = 0; i <= len; i++)
            {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection"))
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                return;
            }
            Point cp = listView1.PointToClient(new Point(e.X, e.Y));
            ListViewItem dragToItem = listView1.GetItemAt(cp.X, cp.Y);
            if (dragToItem == null)
            {
                return;
            }
            if ((dragToItem.ImageIndex == 4) || (dragToItem.ImageIndex == 7))
            {
                String dragIndex = dragToItem.SubItems[0].Text;
                var sel = new string[listView1.SelectedItems.Count];
                for (int i = 0; i <= listView1.SelectedItems.Count - 1; i++)
                {
                    sel[i] = listView1.SelectedItems[i].SubItems[1].Text;
                }
                for (int i = 0; i < sel.GetLength(0); i++)
                {
                    var xmlFile = new XmlFile();
                    xmlFile.ChangeFolder(sel, dragIndex);
                    FillList();
                }
            }
            else
            {
                return;
            }
        }

        protected void listView1_DragOver(object sender, DragEventArgs e)
        {
            Point position = listView1.PointToClient(new Point(e.X, e.Y));

            if (position.Y <= (Font.Height / 2))
            {
                // getting close to top, ensure previous item is visible
                mintScrollDirection = SB_LINEUP;
                tmrLVScroll.Enabled = true;
            }
            else if (position.Y >= listView1.ClientSize.Height - Font.Height / 2)
            {
                // getting close to bottom, ensure next item is visible
                mintScrollDirection = SB_LINEDOWN;
                tmrLVScroll.Enabled = true;
            }
            else
            {
                tmrLVScroll.Enabled = false;
            }
        }

        private void tmrLVScroll_Tick(object sender, EventArgs e)
        {
            SendMessage(listView1.Handle, WM_VSCROLL, (IntPtr)mintScrollDirection, IntPtr.Zero);
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Back) && (!tsChangeViewType.Checked))
            {
                _currentFolder = "root";
                FillList();
            }
            else if ((e.KeyCode == Keys.Enter) && (listView1.SelectedItems.Count != 0))
            {
                if ((listView1.SelectedItems[0].ImageIndex == 7) || (listView1.SelectedItems[0].ImageIndex == 4))
                {
                    _currentFolder = listView1.SelectedItems[0].Tag.ToString();
                    FillList();
                }
                else
                {
                    ShowLinksInWindow();
                }
            }
        }

        private void tsChangeViewType_Click(object sender, EventArgs e)
        {
            if (!tsChangeViewType.Checked)
            {
                _currentFolder = "root";
            }
            FillList();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (TaskbarManager.IsPlatformSupported)
            {
                AddTask();
            }
        }

        private void tsAboutButton_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.Show();
        }
    }
}