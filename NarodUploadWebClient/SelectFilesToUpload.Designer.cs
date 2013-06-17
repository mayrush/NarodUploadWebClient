namespace NarodUploadWebClient
{
    partial class SelectFilesToUpload
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox _sFile;
        private System.Windows.Forms.Button bUpload;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Label lFile;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectFilesToUpload));
            this._sFile = new System.Windows.Forms.TextBox();
            this.bUpload = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.lFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _sFile
            // 
            this._sFile.Location = new System.Drawing.Point(63, 3);
            this._sFile.Multiline = true;
            this._sFile.Name = "_sFile";
            this._sFile.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._sFile.Size = new System.Drawing.Size(354, 134);
            this._sFile.TabIndex = 2;
            // 
            // bUpload
            // 
            this.bUpload.Location = new System.Drawing.Point(422, 42);
            this.bUpload.Name = "bUpload";
            this.bUpload.Size = new System.Drawing.Size(75, 23);
            this.bUpload.TabIndex = 4;
            this.bUpload.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_Upload__;
            this.bUpload.UseVisualStyleBackColor = true;
            this.bUpload.Click += new System.EventHandler(this.Upload);
            // 
            // bOpen
            // 
            this.bOpen.Location = new System.Drawing.Point(422, 6);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(75, 23);
            this.bOpen.TabIndex = 3;
            this.bOpen.Text = global::NarodUploadWebClient.Properties.Resources.MainForm_Open__;
            this.bOpen.UseVisualStyleBackColor = true;
            this.bOpen.Click += new System.EventHandler(this.ButtonOpenFileClick);
            // 
            // lFile
            // 
            this.lFile.AutoSize = true;
            this.lFile.Location = new System.Drawing.Point(6, 3);
            this.lFile.Name = "lFile";
            this.lFile.Size = new System.Drawing.Size(47, 13);
            this.lFile.TabIndex = 1;
            this.lFile.Text = "Файлы:";
            // 
            // SelectFilesToUpload
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 140);
            this.Controls.Add(this.bUpload);
            this.Controls.Add(this.lFile);
            this.Controls.Add(this._sFile);
            this.Controls.Add(this.bOpen);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectFilesToUpload";
            this.ShowInTaskbar = false;
            this.Text = "Выберите папку";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectFilesToUpload_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion Windows Form Designer generated code
    }
}