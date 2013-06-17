using System;
using System.Windows.Forms;

namespace NarodUploadWebClient
{
    ///<summary>Подкласс основной формы</summary>
    public class SettingsForm : Form
    {
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
        public RadioButton radioButtonTheme1;
        public RadioButton radioButtonTheme2;
        public CheckBox checkBoxCurrentFolder;
        private TabPage _tabPagePost;
        private GroupBox groupBoxSMTP;
        private TextBox tbSmtpServer;
        private Label labelsmtpserver;
        public TextBox tbSmtpLogin;
        private Label labelsmtplogin;
        private Label labelsmtppassword;
        public MaskedTextBox tbSmtpPassword;
        private CheckBox checkBoxUseDefaultEMail;
        private TextBox tbSenderEmail;
        private Label labelemail;
        private GroupBox groupBoxDoubleclickActions;
        private RadioButton radioButtonDoubleClickClipboard;
        private RadioButton radioButtonDoubleClickOpen;
        private RadioButton radioButtonDoubleClickDoNothing;
        private CheckBox checkBoxUploadWindowPosition;
        private RadioButton radioButtonDoubleClickOpenDialog;
        private GroupBox groupBoxLinksInUploadBox;
        private RadioButton radioButtonHtmlCode;
        private RadioButton radioButtonBBCode;
        private RadioButton radioButtonStandartLink;
        public CheckBox checkBoxFilesToFolders;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._tabControl1 = new System.Windows.Forms.TabControl();
            this._tabPageGeneral = new System.Windows.Forms.TabPage();
            this.checkBoxUploadWindowPosition = new System.Windows.Forms.CheckBox();
            this.groupBoxDoubleclickActions = new System.Windows.Forms.GroupBox();
            this.radioButtonDoubleClickOpenDialog = new System.Windows.Forms.RadioButton();
            this.radioButtonDoubleClickClipboard = new System.Windows.Forms.RadioButton();
            this.radioButtonDoubleClickOpen = new System.Windows.Forms.RadioButton();
            this.radioButtonDoubleClickDoNothing = new System.Windows.Forms.RadioButton();
            this.checkBoxCurrentFolder = new System.Windows.Forms.CheckBox();
            this.sLogin = new System.Windows.Forms.TextBox();
            this._lLogin = new System.Windows.Forms.Label();
            this._lPassword = new System.Windows.Forms.Label();
            this.sPassword = new System.Windows.Forms.MaskedTextBox();
            this._tabPageUpload = new System.Windows.Forms.TabPage();
            this.checkBoxFilesToFolders = new System.Windows.Forms.CheckBox();
            this.checkBoxShowInTaskBar = new System.Windows.Forms.CheckBox();
            this._label3 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._label1 = new System.Windows.Forms.Label();
            this._labelOpacity = new System.Windows.Forms.Label();
            this.trackBarOpacity = new System.Windows.Forms.TrackBar();
            this._tabPageView = new System.Windows.Forms.TabPage();
            this._groupBoxTheme = new System.Windows.Forms.GroupBox();
            this.radioButtonTheme1 = new System.Windows.Forms.RadioButton();
            this.radioButtonTheme2 = new System.Windows.Forms.RadioButton();
            this._tabPagePost = new System.Windows.Forms.TabPage();
            this.groupBoxSMTP = new System.Windows.Forms.GroupBox();
            this.tbSenderEmail = new System.Windows.Forms.TextBox();
            this.labelemail = new System.Windows.Forms.Label();
            this.tbSmtpServer = new System.Windows.Forms.TextBox();
            this.labelsmtpserver = new System.Windows.Forms.Label();
            this.tbSmtpLogin = new System.Windows.Forms.TextBox();
            this.labelsmtplogin = new System.Windows.Forms.Label();
            this.labelsmtppassword = new System.Windows.Forms.Label();
            this.tbSmtpPassword = new System.Windows.Forms.MaskedTextBox();
            this.checkBoxUseDefaultEMail = new System.Windows.Forms.CheckBox();
            this.groupBoxLinksInUploadBox = new System.Windows.Forms.GroupBox();
            this.radioButtonHtmlCode = new System.Windows.Forms.RadioButton();
            this.radioButtonBBCode = new System.Windows.Forms.RadioButton();
            this.radioButtonStandartLink = new System.Windows.Forms.RadioButton();
            this._tabControl1.SuspendLayout();
            this._tabPageGeneral.SuspendLayout();
            this.groupBoxDoubleclickActions.SuspendLayout();
            this._tabPageUpload.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).BeginInit();
            this._tabPageView.SuspendLayout();
            this._groupBoxTheme.SuspendLayout();
            this._tabPagePost.SuspendLayout();
            this.groupBoxSMTP.SuspendLayout();
            this.groupBoxLinksInUploadBox.SuspendLayout();
            this.SuspendLayout();
            //
            // _tabControl1
            //
            this._tabControl1.Controls.Add(this._tabPageGeneral);
            this._tabControl1.Controls.Add(this._tabPageUpload);
            this._tabControl1.Controls.Add(this._tabPageView);
            this._tabControl1.Controls.Add(this._tabPagePost);
            this._tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl1.Location = new System.Drawing.Point(0, 0);
            this._tabControl1.Multiline = true;
            this._tabControl1.Name = "_tabControl1";
            this._tabControl1.SelectedIndex = 0;
            this._tabControl1.Size = new System.Drawing.Size(534, 302);
            this._tabControl1.TabIndex = 0;
            //
            // _tabPageGeneral
            //
            this._tabPageGeneral.Controls.Add(this.checkBoxUploadWindowPosition);
            this._tabPageGeneral.Controls.Add(this.groupBoxDoubleclickActions);
            this._tabPageGeneral.Controls.Add(this.checkBoxCurrentFolder);
            this._tabPageGeneral.Controls.Add(this.sLogin);
            this._tabPageGeneral.Controls.Add(this._lLogin);
            this._tabPageGeneral.Controls.Add(this._lPassword);
            this._tabPageGeneral.Controls.Add(this.sPassword);
            this._tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this._tabPageGeneral.Name = "_tabPageGeneral";
            this._tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this._tabPageGeneral.Size = new System.Drawing.Size(526, 276);
            this._tabPageGeneral.TabIndex = 0;
            this._tabPageGeneral.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_Tabs_General__;
            this._tabPageGeneral.UseVisualStyleBackColor = true;
            //
            // checkBoxUploadWindowPosition
            //
            this.checkBoxUploadWindowPosition.AutoSize = true;
            this.checkBoxUploadWindowPosition.Location = new System.Drawing.Point(10, 206);
            this.checkBoxUploadWindowPosition.Name = "checkBoxUploadWindowPosition";
            this.checkBoxUploadWindowPosition.Size = new System.Drawing.Size(208, 17);
            this.checkBoxUploadWindowPosition.TabIndex = 27;
            this.checkBoxUploadWindowPosition.Text = "Особое размещение окон загрузки";
            this.checkBoxUploadWindowPosition.UseVisualStyleBackColor = true;
            //
            // groupBoxDoubleclickActions
            //
            this.groupBoxDoubleclickActions.Controls.Add(this.radioButtonDoubleClickOpenDialog);
            this.groupBoxDoubleclickActions.Controls.Add(this.radioButtonDoubleClickClipboard);
            this.groupBoxDoubleclickActions.Controls.Add(this.radioButtonDoubleClickOpen);
            this.groupBoxDoubleclickActions.Controls.Add(this.radioButtonDoubleClickDoNothing);
            this.groupBoxDoubleclickActions.Location = new System.Drawing.Point(10, 83);
            this.groupBoxDoubleclickActions.Name = "groupBoxDoubleclickActions";
            this.groupBoxDoubleclickActions.Size = new System.Drawing.Size(263, 117);
            this.groupBoxDoubleclickActions.TabIndex = 26;
            this.groupBoxDoubleclickActions.TabStop = false;
            this.groupBoxDoubleclickActions.Text = "Действие при двойном щелчке на файле";
            //
            // radioButtonDoubleClickOpenDialog
            //
            this.radioButtonDoubleClickOpenDialog.AutoSize = true;
            this.radioButtonDoubleClickOpenDialog.Location = new System.Drawing.Point(6, 88);
            this.radioButtonDoubleClickOpenDialog.Name = "radioButtonDoubleClickOpenDialog";
            this.radioButtonDoubleClickOpenDialog.Size = new System.Drawing.Size(184, 17);
            this.radioButtonDoubleClickOpenDialog.TabIndex = 28;
            this.radioButtonDoubleClickOpenDialog.TabStop = true;
            this.radioButtonDoubleClickOpenDialog.Text = "Отображать окно со ссылками";
            this.radioButtonDoubleClickOpenDialog.UseVisualStyleBackColor = true;
            //
            // radioButtonDoubleClickClipboard
            //
            this.radioButtonDoubleClickClipboard.AutoSize = true;
            this.radioButtonDoubleClickClipboard.Location = new System.Drawing.Point(6, 65);
            this.radioButtonDoubleClickClipboard.Name = "radioButtonDoubleClickClipboard";
            this.radioButtonDoubleClickClipboard.Size = new System.Drawing.Size(149, 17);
            this.radioButtonDoubleClickClipboard.TabIndex = 28;
            this.radioButtonDoubleClickClipboard.TabStop = true;
            this.radioButtonDoubleClickClipboard.Text = "Копировать всё в буфер";
            this.radioButtonDoubleClickClipboard.UseVisualStyleBackColor = true;
            //
            // radioButtonDoubleClickOpen
            //
            this.radioButtonDoubleClickOpen.AutoSize = true;
            this.radioButtonDoubleClickOpen.Location = new System.Drawing.Point(6, 42);
            this.radioButtonDoubleClickOpen.Name = "radioButtonDoubleClickOpen";
            this.radioButtonDoubleClickOpen.Size = new System.Drawing.Size(223, 17);
            this.radioButtonDoubleClickOpen.TabIndex = 27;
            this.radioButtonDoubleClickOpen.TabStop = true;
            this.radioButtonDoubleClickOpen.Text = "Открывать ссылку/ссылки в браузере";
            this.radioButtonDoubleClickOpen.UseVisualStyleBackColor = true;
            //
            // radioButtonDoubleClickDoNothing
            //
            this.radioButtonDoubleClickDoNothing.AutoSize = true;
            this.radioButtonDoubleClickDoNothing.Location = new System.Drawing.Point(6, 19);
            this.radioButtonDoubleClickDoNothing.Name = "radioButtonDoubleClickDoNothing";
            this.radioButtonDoubleClickDoNothing.Size = new System.Drawing.Size(94, 17);
            this.radioButtonDoubleClickDoNothing.TabIndex = 0;
            this.radioButtonDoubleClickDoNothing.TabStop = true;
            this.radioButtonDoubleClickDoNothing.Text = "Нет действия";
            this.radioButtonDoubleClickDoNothing.UseVisualStyleBackColor = true;
            //
            // checkBoxCurrentFolder
            //
            this.checkBoxCurrentFolder.AutoSize = true;
            this.checkBoxCurrentFolder.Location = new System.Drawing.Point(10, 60);
            this.checkBoxCurrentFolder.Name = "checkBoxCurrentFolder";
            this.checkBoxCurrentFolder.Size = new System.Drawing.Size(242, 17);
            this.checkBoxCurrentFolder.TabIndex = 25;
            this.checkBoxCurrentFolder.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_Memorize_last_folder_on_exit;
            this.checkBoxCurrentFolder.UseVisualStyleBackColor = true;
            //
            // sLogin
            //
            this.sLogin.Location = new System.Drawing.Point(109, 6);
            this.sLogin.Name = "sLogin";
            this.sLogin.Size = new System.Drawing.Size(164, 20);
            this.sLogin.TabIndex = 18;
            //
            // _lLogin
            //
            this._lLogin.AutoSize = true;
            this._lLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this._lLogin.Location = new System.Drawing.Point(7, 9);
            this._lLogin.Name = "_lLogin";
            this._lLogin.Size = new System.Drawing.Size(96, 13);
            this._lLogin.TabIndex = 17;
            this._lLogin.Text = "Пользователь:";
            //
            // _lPassword
            //
            this._lPassword.AutoSize = true;
            this._lPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this._lPassword.Location = new System.Drawing.Point(7, 37);
            this._lPassword.Name = "_lPassword";
            this._lPassword.Size = new System.Drawing.Size(59, 13);
            this._lPassword.TabIndex = 19;
            this._lPassword.Text = "Пароль: ";
            //
            // sPassword
            //
            this.sPassword.Location = new System.Drawing.Point(109, 34);
            this.sPassword.Name = "sPassword";
            this.sPassword.PasswordChar = '*';
            this.sPassword.Size = new System.Drawing.Size(164, 20);
            this.sPassword.TabIndex = 20;
            //
            // _tabPageUpload
            //
            this._tabPageUpload.Controls.Add(this.groupBoxLinksInUploadBox);
            this._tabPageUpload.Controls.Add(this.checkBoxFilesToFolders);
            this._tabPageUpload.Controls.Add(this.checkBoxShowInTaskBar);
            this._tabPageUpload.Controls.Add(this._label3);
            this._tabPageUpload.Controls.Add(this._label2);
            this._tabPageUpload.Controls.Add(this._label1);
            this._tabPageUpload.Controls.Add(this._labelOpacity);
            this._tabPageUpload.Controls.Add(this.trackBarOpacity);
            this._tabPageUpload.Location = new System.Drawing.Point(4, 22);
            this._tabPageUpload.Name = "_tabPageUpload";
            this._tabPageUpload.Padding = new System.Windows.Forms.Padding(3);
            this._tabPageUpload.Size = new System.Drawing.Size(526, 276);
            this._tabPageUpload.TabIndex = 1;
            this._tabPageUpload.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_Uploading;
            this._tabPageUpload.UseVisualStyleBackColor = true;
            //
            // checkBoxFilesToFolders
            //
            this.checkBoxFilesToFolders.AutoSize = true;
            this.checkBoxFilesToFolders.Location = new System.Drawing.Point(11, 100);
            this.checkBoxFilesToFolders.Name = "checkBoxFilesToFolders";
            this.checkBoxFilesToFolders.Size = new System.Drawing.Size(273, 17);
            this.checkBoxFilesToFolders.TabIndex = 30;
            this.checkBoxFilesToFolders.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_Files_To_Folder_After_Upload_;
            this.checkBoxFilesToFolders.UseVisualStyleBackColor = true;
            this.checkBoxFilesToFolders.CheckedChanged += new System.EventHandler(this.CheckBoxCurrentFolderCheckedChanged);
            //
            // checkBoxShowInTaskBar
            //
            this.checkBoxShowInTaskBar.AutoSize = true;
            this.checkBoxShowInTaskBar.Location = new System.Drawing.Point(11, 77);
            this.checkBoxShowInTaskBar.Name = "checkBoxShowInTaskBar";
            this.checkBoxShowInTaskBar.Size = new System.Drawing.Size(223, 17);
            this.checkBoxShowInTaskBar.TabIndex = 29;
            this.checkBoxShowInTaskBar.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_Show_Uploading_Form_In_Taskbar;
            this.checkBoxShowInTaskBar.UseVisualStyleBackColor = true;
            this.checkBoxShowInTaskBar.CheckedChanged += new System.EventHandler(this.CheckBoxShowInTaskBarCheckedChanged);
            //
            // _label3
            //
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(131, 51);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(27, 13);
            this._label3.TabIndex = 28;
            this._label3.Text = "50%";
            //
            // _label2
            //
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(246, 51);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(33, 13);
            this._label2.TabIndex = 27;
            this._label2.Text = "100%";
            //
            // _label1
            //
            this._label1.AutoSize = true;
            this._label1.Location = new System.Drawing.Point(16, 50);
            this._label1.Name = "_label1";
            this._label1.Size = new System.Drawing.Size(21, 13);
            this._label1.TabIndex = 26;
            this._label1.Text = "1%";
            //
            // _labelOpacity
            //
            this._labelOpacity.AutoSize = true;
            this._labelOpacity.Location = new System.Drawing.Point(8, 3);
            this._labelOpacity.Name = "_labelOpacity";
            this._labelOpacity.Size = new System.Drawing.Size(158, 13);
            this._labelOpacity.TabIndex = 25;
            this._labelOpacity.Text = "Прозрачность окна загрузки:";
            //
            // trackBarOpacity
            //
            this.trackBarOpacity.BackColor = System.Drawing.SystemColors.Window;
            this.trackBarOpacity.LargeChange = 50;
            this.trackBarOpacity.Location = new System.Drawing.Point(9, 19);
            this.trackBarOpacity.Margin = new System.Windows.Forms.Padding(1);
            this.trackBarOpacity.Maximum = 100;
            this.trackBarOpacity.Name = "trackBarOpacity";
            this.trackBarOpacity.Size = new System.Drawing.Size(263, 45);
            this.trackBarOpacity.TabIndex = 24;
            this.trackBarOpacity.TickFrequency = 10;
            this.trackBarOpacity.Value = 90;
            this.trackBarOpacity.Scroll += new System.EventHandler(this.TrackBarOpacityScroll);
            //
            // _tabPageView
            //
            this._tabPageView.Controls.Add(this._groupBoxTheme);
            this._tabPageView.Location = new System.Drawing.Point(4, 22);
            this._tabPageView.Name = "_tabPageView";
            this._tabPageView.Size = new System.Drawing.Size(526, 276);
            this._tabPageView.TabIndex = 2;
            this._tabPageView.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_View__;
            this._tabPageView.UseVisualStyleBackColor = true;
            //
            // _groupBoxTheme
            //
            this._groupBoxTheme.Controls.Add(this.radioButtonTheme1);
            this._groupBoxTheme.Controls.Add(this.radioButtonTheme2);
            this._groupBoxTheme.Location = new System.Drawing.Point(8, 3);
            this._groupBoxTheme.Name = "_groupBoxTheme";
            this._groupBoxTheme.Size = new System.Drawing.Size(144, 78);
            this._groupBoxTheme.TabIndex = 29;
            this._groupBoxTheme.TabStop = false;
            this._groupBoxTheme.Text = "Темы значков";
            //
            // radioButtonTheme1
            //
            this.radioButtonTheme1.AutoSize = true;
            this.radioButtonTheme1.Location = new System.Drawing.Point(6, 19);
            this.radioButtonTheme1.Name = "radioButtonTheme1";
            this.radioButtonTheme1.Size = new System.Drawing.Size(61, 17);
            this.radioButtonTheme1.TabIndex = 27;
            this.radioButtonTheme1.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_Тheme_1;
            this.radioButtonTheme1.UseVisualStyleBackColor = true;
            this.radioButtonTheme1.CheckedChanged += new System.EventHandler(this.RadioButtonTheme1CheckedChanged);
            //
            // radioButtonTheme2
            //
            this.radioButtonTheme2.AutoSize = true;
            this.radioButtonTheme2.Location = new System.Drawing.Point(6, 42);
            this.radioButtonTheme2.Name = "radioButtonTheme2";
            this.radioButtonTheme2.Size = new System.Drawing.Size(61, 17);
            this.radioButtonTheme2.TabIndex = 28;
            this.radioButtonTheme2.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_InitializeSettingForm_Theme_2;
            this.radioButtonTheme2.UseVisualStyleBackColor = true;
            this.radioButtonTheme2.CheckedChanged += new System.EventHandler(this.RadioButtonTheme2CheckedChanged);
            //
            // _tabPagePost
            //
            this._tabPagePost.Controls.Add(this.groupBoxSMTP);
            this._tabPagePost.Controls.Add(this.checkBoxUseDefaultEMail);
            this._tabPagePost.Location = new System.Drawing.Point(4, 22);
            this._tabPagePost.Name = "_tabPagePost";
            this._tabPagePost.Padding = new System.Windows.Forms.Padding(3);
            this._tabPagePost.Size = new System.Drawing.Size(526, 276);
            this._tabPagePost.TabIndex = 3;
            this._tabPagePost.Text = "Почта";
            this._tabPagePost.UseVisualStyleBackColor = true;
            //
            // groupBoxSMTP
            //
            this.groupBoxSMTP.Controls.Add(this.tbSenderEmail);
            this.groupBoxSMTP.Controls.Add(this.labelemail);
            this.groupBoxSMTP.Controls.Add(this.tbSmtpServer);
            this.groupBoxSMTP.Controls.Add(this.labelsmtpserver);
            this.groupBoxSMTP.Controls.Add(this.tbSmtpLogin);
            this.groupBoxSMTP.Controls.Add(this.labelsmtplogin);
            this.groupBoxSMTP.Controls.Add(this.labelsmtppassword);
            this.groupBoxSMTP.Controls.Add(this.tbSmtpPassword);
            this.groupBoxSMTP.Location = new System.Drawing.Point(8, 29);
            this.groupBoxSMTP.Name = "groupBoxSMTP";
            this.groupBoxSMTP.Size = new System.Drawing.Size(280, 127);
            this.groupBoxSMTP.TabIndex = 1;
            this.groupBoxSMTP.TabStop = false;
            this.groupBoxSMTP.Text = "Настройки внешнего SMTP сервера";
            //
            // tbSenderEmail
            //
            this.tbSenderEmail.Location = new System.Drawing.Point(108, 98);
            this.tbSenderEmail.Name = "tbSenderEmail";
            this.tbSenderEmail.Size = new System.Drawing.Size(164, 20);
            this.tbSenderEmail.TabIndex = 28;
            //
            // labelemail
            //
            this.labelemail.AutoSize = true;
            this.labelemail.Location = new System.Drawing.Point(6, 101);
            this.labelemail.Name = "labelemail";
            this.labelemail.Size = new System.Drawing.Size(104, 13);
            this.labelemail.TabIndex = 27;
            this.labelemail.Text = "e-mail отправителя:";
            //
            // tbSmtpServer
            //
            this.tbSmtpServer.Location = new System.Drawing.Point(108, 69);
            this.tbSmtpServer.Name = "tbSmtpServer";
            this.tbSmtpServer.Size = new System.Drawing.Size(164, 20);
            this.tbSmtpServer.TabIndex = 26;
            //
            // labelsmtpserver
            //
            this.labelsmtpserver.AutoSize = true;
            this.labelsmtpserver.Location = new System.Drawing.Point(6, 72);
            this.labelsmtpserver.Name = "labelsmtpserver";
            this.labelsmtpserver.Size = new System.Drawing.Size(47, 13);
            this.labelsmtpserver.TabIndex = 25;
            this.labelsmtpserver.Text = "Сервер:";
            //
            // tbSmtpLogin
            //
            this.tbSmtpLogin.Location = new System.Drawing.Point(108, 13);
            this.tbSmtpLogin.Name = "tbSmtpLogin";
            this.tbSmtpLogin.Size = new System.Drawing.Size(164, 20);
            this.tbSmtpLogin.TabIndex = 22;
            //
            // labelsmtplogin
            //
            this.labelsmtplogin.AutoSize = true;
            this.labelsmtplogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelsmtplogin.Location = new System.Drawing.Point(6, 16);
            this.labelsmtplogin.Name = "labelsmtplogin";
            this.labelsmtplogin.Size = new System.Drawing.Size(83, 13);
            this.labelsmtplogin.TabIndex = 21;
            this.labelsmtplogin.Text = "Пользователь:";
            //
            // labelsmtppassword
            //
            this.labelsmtppassword.AutoSize = true;
            this.labelsmtppassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelsmtppassword.Location = new System.Drawing.Point(6, 44);
            this.labelsmtppassword.Name = "labelsmtppassword";
            this.labelsmtppassword.Size = new System.Drawing.Size(51, 13);
            this.labelsmtppassword.TabIndex = 23;
            this.labelsmtppassword.Text = "Пароль: ";
            //
            // tbSmtpPassword
            //
            this.tbSmtpPassword.Location = new System.Drawing.Point(108, 41);
            this.tbSmtpPassword.Name = "tbSmtpPassword";
            this.tbSmtpPassword.PasswordChar = '*';
            this.tbSmtpPassword.Size = new System.Drawing.Size(164, 20);
            this.tbSmtpPassword.TabIndex = 24;
            //
            // checkBoxUseDefaultEMail
            //
            this.checkBoxUseDefaultEMail.AutoSize = true;
            this.checkBoxUseDefaultEMail.Location = new System.Drawing.Point(8, 6);
            this.checkBoxUseDefaultEMail.Name = "checkBoxUseDefaultEMail";
            this.checkBoxUseDefaultEMail.Size = new System.Drawing.Size(198, 17);
            this.checkBoxUseDefaultEMail.TabIndex = 0;
            this.checkBoxUseDefaultEMail.Text = "Использовать стандартный ящик";
            this.checkBoxUseDefaultEMail.UseVisualStyleBackColor = true;
            //
            // groupBoxLinksInUploadBox
            //
            this.groupBoxLinksInUploadBox.Controls.Add(this.radioButtonHtmlCode);
            this.groupBoxLinksInUploadBox.Controls.Add(this.radioButtonBBCode);
            this.groupBoxLinksInUploadBox.Controls.Add(this.radioButtonStandartLink);
            this.groupBoxLinksInUploadBox.Location = new System.Drawing.Point(11, 133);
            this.groupBoxLinksInUploadBox.Name = "groupBoxLinksInUploadBox";
            this.groupBoxLinksInUploadBox.Size = new System.Drawing.Size(263, 95);
            this.groupBoxLinksInUploadBox.TabIndex = 31;
            this.groupBoxLinksInUploadBox.TabStop = false;
            this.groupBoxLinksInUploadBox.Text = "Вид ссылок после загрузки";
            //
            // radioButtonHtmlCode
            //
            this.radioButtonHtmlCode.AutoSize = true;
            this.radioButtonHtmlCode.Location = new System.Drawing.Point(6, 65);
            this.radioButtonHtmlCode.Name = "radioButtonHtmlCode";
            this.radioButtonHtmlCode.Size = new System.Drawing.Size(141, 17);
            this.radioButtonHtmlCode.TabIndex = 28;
            this.radioButtonHtmlCode.TabStop = true;
            this.radioButtonHtmlCode.Text = "Ссылка с кодом HTML";
            this.radioButtonHtmlCode.UseVisualStyleBackColor = true;
            this.radioButtonHtmlCode.CheckedChanged += new System.EventHandler(this.radioButtonHtmlCode_CheckedChanged);
            //
            // radioButtonBBCode
            //
            this.radioButtonBBCode.AutoSize = true;
            this.radioButtonBBCode.Location = new System.Drawing.Point(6, 42);
            this.radioButtonBBCode.Name = "radioButtonBBCode";
            this.radioButtonBBCode.Size = new System.Drawing.Size(200, 17);
            this.radioButtonBBCode.TabIndex = 27;
            this.radioButtonBBCode.TabStop = true;
            this.radioButtonBBCode.Text = "Ссылка с кодом BB (для форумов)";
            this.radioButtonBBCode.UseVisualStyleBackColor = true;
            this.radioButtonBBCode.CheckedChanged += new System.EventHandler(this.radioButtonBBCode_CheckedChanged);
            //
            // radioButtonStandartLink
            //
            this.radioButtonStandartLink.AutoSize = true;
            this.radioButtonStandartLink.Location = new System.Drawing.Point(6, 19);
            this.radioButtonStandartLink.Name = "radioButtonStandartLink";
            this.radioButtonStandartLink.Size = new System.Drawing.Size(111, 17);
            this.radioButtonStandartLink.TabIndex = 0;
            this.radioButtonStandartLink.TabStop = true;
            this.radioButtonStandartLink.Text = "Обычная ссылка";
            this.radioButtonStandartLink.UseVisualStyleBackColor = true;
            this.radioButtonStandartLink.CheckedChanged += new System.EventHandler(this.radioButtonStandartLink_CheckedChanged);
            //
            // SettingsForm
            //
            this.ClientSize = new System.Drawing.Size(534, 302);
            this.Controls.Add(this._tabControl1);
            this.Icon = global::NarodUploadWebClient.Properties.Resources.Settings;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 340);
            this.MinimumSize = new System.Drawing.Size(550, 340);
            this.Name = "SettingsForm";
            this.Text = "Настройки";
            this._tabControl1.ResumeLayout(false);
            this._tabPageGeneral.ResumeLayout(false);
            this._tabPageGeneral.PerformLayout();
            this.groupBoxDoubleclickActions.ResumeLayout(false);
            this.groupBoxDoubleclickActions.PerformLayout();
            this._tabPageUpload.ResumeLayout(false);
            this._tabPageUpload.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarOpacity)).EndInit();
            this._tabPageView.ResumeLayout(false);
            this._groupBoxTheme.ResumeLayout(false);
            this._groupBoxTheme.PerformLayout();
            this._tabPagePost.ResumeLayout(false);
            this._tabPagePost.PerformLayout();
            this.groupBoxSMTP.ResumeLayout(false);
            this.groupBoxSMTP.PerformLayout();
            this.groupBoxLinksInUploadBox.ResumeLayout(false);
            this.groupBoxLinksInUploadBox.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Windows Form Designer generated code

        ///<summary>Инициализация формы настроек приложения</summary>
        public SettingsForm()
        {
            new Form();
            InitializeComponent();
        }

        private void RadioButtonTheme1CheckedChanged(object sender, EventArgs e)
        {
        }

        private void RadioButtonTheme2CheckedChanged(object sender, EventArgs e)
        {
        }

        private void TrackBarOpacityScroll(object sender, EventArgs e)
        {
        }

        private void CheckBoxShowInTaskBarCheckedChanged(object sender, EventArgs e)
        {
        }

        private void CheckBoxCurrentFolderCheckedChanged(object sender, EventArgs e)
        {
        }

        private void SettingsFormFormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void radioButtonStandartLink_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonBBCode_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButtonHtmlCode_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}