using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using NarodUploadWebClient.Properties;

namespace NarodUploadWebClient
{
    ///<summary>Подкласс формы загрузки файла на Яндекс.Диск</summary>
    public class UploadForm : FilesProcess
    {
        private System.ComponentModel.IContainer components;
        private Button _buttonLock;
        private Button _buttonPause;
        private Label _label2;
        private Label _label3;
        private Label _label4;
        private Label _label5;
        private Label _label6;
        private Label _labelLong;
        private Label _labelSize;
        private Label _labelSpeed;
        private Label _labelTime;
        private Label _labelUpload;
        private TextBox _textBox1;
        private Timer _timer1;
        private ProgressBar _uploadingProgress;

        private ThumbnailToolBarButton buttonPause;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._labelLong = new System.Windows.Forms.Label();
            this._labelTime = new System.Windows.Forms.Label();
            this._labelSpeed = new System.Windows.Forms.Label();
            this._labelUpload = new System.Windows.Forms.Label();
            this._labelSize = new System.Windows.Forms.Label();
            this._label5 = new System.Windows.Forms.Label();
            this._label4 = new System.Windows.Forms.Label();
            this._label3 = new System.Windows.Forms.Label();
            this._label2 = new System.Windows.Forms.Label();
            this._label6 = new System.Windows.Forms.Label();
            this._uploadingProgress = new System.Windows.Forms.ProgressBar();
            this._timer1 = new System.Windows.Forms.Timer(this.components);
            this._textBox1 = new System.Windows.Forms.TextBox();
            this._buttonLock = new System.Windows.Forms.Button();
            this._buttonPause = new System.Windows.Forms.Button();

            this.SuspendLayout();
            //
            // labelLong
            //
            this._labelLong.AutoSize = true;
            this._labelLong.Location = new System.Drawing.Point(125, 138);
            this._labelLong.Name = "_labelLong";
            this._labelLong.Size = new System.Drawing.Size(61, 13);
            this._labelLong.TabIndex = 31;
            this._labelLong.Text = "00 : 00 : 00";
            //
            // labelTime
            //
            this._labelTime.AutoSize = true;
            this._labelTime.Location = new System.Drawing.Point(125, 115);
            this._labelTime.Name = "_labelTime";
            this._labelTime.Size = new System.Drawing.Size(61, 13);
            this._labelTime.TabIndex = 30;
            this._labelTime.Text = "00 : 00 : 00";
            //
            // labelSpeed
            //
            this._labelSpeed.AutoSize = true;
            this._labelSpeed.Location = new System.Drawing.Point(125, 92);
            this._labelSpeed.Name = "_labelSpeed";
            this._labelSpeed.Size = new System.Drawing.Size(40, 13);
            this._labelSpeed.TabIndex = 29;
            this._labelSpeed.Text = "0 Кб/с";
            //
            // labelUpload
            //
            this._labelUpload.AutoSize = true;
            this._labelUpload.Location = new System.Drawing.Point(125, 69);
            this._labelUpload.Name = "_labelUpload";
            this._labelUpload.Size = new System.Drawing.Size(22, 13);
            this._labelUpload.TabIndex = 28;
            this._labelUpload.Text = "0 б";
            //
            // labelSize
            //
            this._labelSize.AutoSize = true;
            this._labelSize.Location = new System.Drawing.Point(125, 46);
            this._labelSize.Name = "_labelSize";
            this._labelSize.Size = new System.Drawing.Size(68, 13);
            this._labelSize.TabIndex = 27;
            this._labelSize.Text = "Неизвестно";
            //
            // label5
            //
            this._label5.AutoSize = true;
            this._label5.Location = new System.Drawing.Point(9, 138);
            this._label5.Name = "_label5";
            this._label5.Size = new System.Drawing.Size(59, 13);
            this._label5.TabIndex = 26;
            this._label5.Text = "Осталось:";
            //
            // label4
            //
            this._label4.AutoSize = true;
            this._label4.Location = new System.Drawing.Point(9, 115);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(83, 13);
            this._label4.TabIndex = 25;
            this._label4.Text = "Длительность:";
            //
            // label3
            //
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(9, 92);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(58, 13);
            this._label3.TabIndex = 24;
            this._label3.Text = "Скорость:";
            //
            // label2
            //
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(9, 69);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(65, 13);
            this._label2.TabIndex = 23;
            this._label2.Text = "Загружено:";
            //
            // label6
            //
            this._label6.AutoSize = true;
            this._label6.Location = new System.Drawing.Point(9, 46);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(84, 13);
            this._label6.TabIndex = 22;
            this._label6.Text = "Размер файла:";
            //
            // UploadingProgress
            //
            this._uploadingProgress.Location = new System.Drawing.Point(12, 12);
            this._uploadingProgress.Name = "_uploadingProgress";
            this._uploadingProgress.Size = new System.Drawing.Size(239, 23);
            this._uploadingProgress.Step = 1;
            this._uploadingProgress.TabIndex = 32;
            this._uploadingProgress.Visible = true;
            //
            // timer1
            //
            this._timer1.Interval = 1000;
            this._timer1.Tick += new System.EventHandler(this.Timer1Tick);
            //
            // textBox1
            //
            this._textBox1.Location = new System.Drawing.Point(6, 6);
            this._textBox1.Multiline = true;
            this._textBox1.Name = "_textBox1";
            this._textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox1.Size = new System.Drawing.Size(282, 150);
            this._uploadingProgress.TabIndex = 33;
            this._textBox1.Visible = false;
            //
            // buttonLock
            //
            this._buttonLock.Image = global::NarodUploadWebClient.Properties.Resources.pinned;
            this._buttonLock.Location = new System.Drawing.Point(257, 12);
            this._buttonLock.Name = "_buttonLock";
            this._buttonLock.Size = new System.Drawing.Size(25, 23);
            this._buttonLock.TabIndex = 34;
            this._buttonLock.UseVisualStyleBackColor = true;
            this._buttonLock.Click += new System.EventHandler(this.ButtonLockClick);
            //
            // buttonPause
            //
            this._buttonPause.Image = global::NarodUploadWebClient.Properties.Resources.pause;
            this._buttonPause.Location = new System.Drawing.Point(257, 40);
            this._buttonPause.Name = "_buttonPause";
            this._buttonPause.Size = new System.Drawing.Size(25, 23);
            this._buttonPause.TabIndex = 34;
            this._buttonPause.UseVisualStyleBackColor = true;
            this._buttonPause.Click += new System.EventHandler(this.ButtonPauseClick);
            //
            // UploadForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 162);
            this.ControlBox = true;
            this.Controls.Add(this._uploadingProgress);
            this.Controls.Add(this._labelLong);
            this.Controls.Add(this._labelTime);
            this.Controls.Add(this._labelSpeed);
            this.Controls.Add(this._labelUpload);
            this.Controls.Add(this._labelSize);
            this.Controls.Add(this._label5);
            this.Controls.Add(this._label4);
            this.Controls.Add(this._label3);
            this.Controls.Add(this._label2);
            this.Controls.Add(this._label6);
            this.Controls.Add(this._textBox1);
            this.Controls.Add(this._buttonLock);
            this.Controls.Add(this._buttonPause);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            if (AppSettings.UploadWindowPosition)
            {
                this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            }
            this.Location = new System.Drawing.Point(CordX, CordY);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Icon = global::NarodUploadWebClient.Properties.Resources.Uploading;
            this.Name = "UploadForm";
            this.Opacity = ((Double)AppSettings.WndOpacity) / 100;
            this.ShowInTaskbar = AppSettings.ShowInTaskBar;
            this.Text = Filename; //"Upload";
            this.TopMost = true;
            this.FormClosing += UploadFormFormClosing;
            this.ResumeLayout(false);
            this.PerformLayout();
            this.Show();
        }

        #endregion Windows Form Designer generated code

        ///<summary>Конструктор класса UploadForm, выполняет вызов формы</summary>
        ///<param name="login">Логин на Яндекс</param>
        ///<param name="password">Пароль на Яндекс</param>
        ///<param name="filename">Имя файла для загрузки</param>
        public UploadForm(string login, string password, string filename = "")
        {
            Login = login;
            Password = password;
            Filename = filename;

            WndCol++;
            WndId = WndCol - 1;

            if (AppSettings.UploadWindowPosition) GetFormLayout();

            InitializeComponent();

            if (TaskbarManager.IsPlatformSupported)
            {
                TaskbarManager.Instance.ApplicationId = MainForm.AppID + ".UplWnd." + WndId;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.NoProgress, this.Handle);

                buttonPause = new ThumbnailToolBarButton(Resources.pauseico, "Пауза") { Enabled = true };
                buttonPause.Click += ButtonPauseClick;

                TaskbarManager.Instance.ThumbnailToolBars.AddButtons(this.Handle, buttonPause);
            }

            UpfileStream = new FileStream(Filename, FileMode.Open, FileAccess.Read);
            Length = (int)UpfileStream.Length;

            _labelSize.Text = Length.ToString("### ### ##0") + Resources.FilesProcess_Timer1Tick__b;
            _uploadingProgress.Value = 0;
            _uploadingProgress.Maximum = Length;

            _timer1.Start();
        }

        private static int GetRow(int wndid, int fit)
        {
            if (wndid < fit)
            {
                return 0;
            }
            var col = wndid / fit;
            return col;
        }

        private void GetFormLayout()
        {
            var screnwidth = Screen.PrimaryScreen.WorkingArea.Width;
            var fittoscreen = screnwidth / 310;
            var row = GetRow(WndId, fittoscreen);

            while ((WndId + 1) > fittoscreen)
            {
                WndId -= fittoscreen;
            }

            CordX = 310 * (WndId);
            CordY = 195 * row;
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
            speed = speed / Kbyte;
            //метод вывода времени загрузки
            Time();
            //вывод информации о скорости загрузки
            _labelSpeed.Text = speed.ToString("0.00") + Resources.FilesProcess_Timer1Tick__Kb_sek_;
            //вывод информации о кол-ве загруженных байт
            _labelUpload.Text = Dowloaded.ToString("### ### ##0") + Resources.FilesProcess_Timer1Tick__b;
            //вывод информации о процентах загруженного файла в строке заголовка формы
            int t = 0, h, m, s;
            //если скорость != 0
            if (temp > 1)
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
            _uploadingProgress.Value = Dowloaded;
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
                TimeHas = 0; Length = 0;
            }
            if (SLink != "")
            {
                DisplayLinkToFile();
                _timer1.Stop();
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

        private void DisplayLinkToFile()
        {
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
            _buttonLock.Visible = false;
            _textBox1.Visible = true;
            _textBox1.Text = SLink;
        }
    }
}