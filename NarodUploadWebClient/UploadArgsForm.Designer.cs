using System;
using NarodUploadWebClient.Properties;

namespace NarodUploadWebClient
{
    partial class UploadArgsForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
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
            // _labelLong
            //
            this._labelLong.AutoSize = true;
            this._labelLong.Location = new System.Drawing.Point(125, 138);
            this._labelLong.Name = "_labelLong";
            this._labelLong.Size = new System.Drawing.Size(61, 13);
            this._labelLong.TabIndex = 31;
            this._labelLong.Text = "00 : 00 : 00";
            //
            // _labelTime
            //
            this._labelTime.AutoSize = true;
            this._labelTime.Location = new System.Drawing.Point(125, 115);
            this._labelTime.Name = "_labelTime";
            this._labelTime.Size = new System.Drawing.Size(61, 13);
            this._labelTime.TabIndex = 30;
            this._labelTime.Text = "00 : 00 : 00";
            //
            // _labelSpeed
            //
            this._labelSpeed.AutoSize = true;
            this._labelSpeed.Location = new System.Drawing.Point(125, 92);
            this._labelSpeed.Name = "_labelSpeed";
            this._labelSpeed.Size = new System.Drawing.Size(40, 13);
            this._labelSpeed.TabIndex = 29;
            this._labelSpeed.Text = "0 Кб/с";
            //
            // _labelUpload
            //
            this._labelUpload.AutoSize = true;
            this._labelUpload.Location = new System.Drawing.Point(125, 69);
            this._labelUpload.Name = "_labelUpload";
            this._labelUpload.Size = new System.Drawing.Size(22, 13);
            this._labelUpload.TabIndex = 28;
            this._labelUpload.Text = "0 б";
            //
            // _labelSize
            //
            this._labelSize.AutoSize = true;
            this._labelSize.Location = new System.Drawing.Point(125, 46);
            this._labelSize.Name = "_labelSize";
            this._labelSize.Size = new System.Drawing.Size(68, 13);
            this._labelSize.TabIndex = 27;
            this._labelSize.Text = "Неизвестно";
            //
            // _label5
            //
            this._label5.AutoSize = true;
            this._label5.Location = new System.Drawing.Point(9, 138);
            this._label5.Name = "_label5";
            this._label5.Size = new System.Drawing.Size(59, 13);
            this._label5.TabIndex = 26;
            this._label5.Text = "Осталось:";
            //
            // _label4
            //
            this._label4.AutoSize = true;
            this._label4.Location = new System.Drawing.Point(9, 115);
            this._label4.Name = "_label4";
            this._label4.Size = new System.Drawing.Size(83, 13);
            this._label4.TabIndex = 25;
            this._label4.Text = "Длительность:";
            //
            // _label3
            //
            this._label3.AutoSize = true;
            this._label3.Location = new System.Drawing.Point(9, 92);
            this._label3.Name = "_label3";
            this._label3.Size = new System.Drawing.Size(58, 13);
            this._label3.TabIndex = 24;
            this._label3.Text = "Скорость:";
            //
            // _label2
            //
            this._label2.AutoSize = true;
            this._label2.Location = new System.Drawing.Point(9, 69);
            this._label2.Name = "_label2";
            this._label2.Size = new System.Drawing.Size(65, 13);
            this._label2.TabIndex = 23;
            this._label2.Text = "Загружено:";
            //
            // _label6
            //
            this._label6.AutoSize = true;
            this._label6.Location = new System.Drawing.Point(9, 46);
            this._label6.Name = "_label6";
            this._label6.Size = new System.Drawing.Size(84, 13);
            this._label6.TabIndex = 22;
            this._label6.Text = "Размер файла:";
            //
            // _uploadingProgress
            //
            this._uploadingProgress.Location = new System.Drawing.Point(12, 12);
            this._uploadingProgress.Name = "_uploadingProgress";
            this._uploadingProgress.Size = new System.Drawing.Size(239, 23);
            this._uploadingProgress.Step = 1;
            this._uploadingProgress.TabIndex = 32;
            this._uploadingProgress.Visible = true;
            //
            // _timer1
            //
            this._timer1.Interval = 1000;
            this._timer1.Tick += new System.EventHandler(this.Timer1Tick);
            //
            // _textBox1
            //
            this._textBox1.Location = new System.Drawing.Point(6, 6);
            this._textBox1.Multiline = true;
            this._textBox1.Name = "_textBox1";
            this._textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox1.Size = new System.Drawing.Size(282, 150);
            this._textBox1.TabIndex = 33;
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
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Icon = global::NarodUploadWebClient.Properties.Resources.Uploading;
            this.Name = "UploadForm";
            this.Opacity = ((Double)AppSettings.WndOpacity) / 100;
            this.ShowInTaskbar = AppSettings.ShowInTaskBar;
            this.Text = Resources.MainForm_InitializeSettingForm_Uploading;
            this.TopMost = true;
            this.FormClosing += UploadFormFormClosing;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion Код, автоматически созданный конструктором форм Windows

        private System.Windows.Forms.Button _buttonLock;
        private System.Windows.Forms.Button _buttonPause;
        private System.Windows.Forms.Label _label2;
        private System.Windows.Forms.Label _label3;
        private System.Windows.Forms.Label _label4;
        private System.Windows.Forms.Label _label5;
        private System.Windows.Forms.Label _label6;
        private System.Windows.Forms.Label _labelLong;
        private System.Windows.Forms.Label _labelSize;
        private System.Windows.Forms.Label _labelSpeed;
        private System.Windows.Forms.Label _labelTime;
        private System.Windows.Forms.Label _labelUpload;
        private System.Windows.Forms.TextBox _textBox1;
        private System.Windows.Forms.Timer _timer1;
        private System.Windows.Forms.ProgressBar _uploadingProgress;
    }
}