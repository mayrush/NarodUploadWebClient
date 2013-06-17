namespace NarodUploadWebClient
{
    partial class LinksForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.labelLinks = new System.Windows.Forms.Label();
            this.textBoxLinks = new System.Windows.Forms.TextBox();
            this.textBoxBBCode = new System.Windows.Forms.TextBox();
            this.labelBBCode = new System.Windows.Forms.Label();
            this.textBoxHtmlCode = new System.Windows.Forms.TextBox();
            this.labelHtmlCode = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelLinks
            // 
            this.labelLinks.AutoSize = true;
            this.labelLinks.Location = new System.Drawing.Point(12, 9);
            this.labelLinks.Name = "labelLinks";
            this.labelLinks.Size = new System.Drawing.Size(107, 13);
            this.labelLinks.TabIndex = 0;
            this.labelLinks.Text = "Текстовые ссылки:";
            // 
            // textBoxLinks
            // 
            this.textBoxLinks.Location = new System.Drawing.Point(15, 25);
            this.textBoxLinks.Multiline = true;
            this.textBoxLinks.Name = "textBoxLinks";
            this.textBoxLinks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLinks.Size = new System.Drawing.Size(447, 113);
            this.textBoxLinks.TabIndex = 1;
            this.textBoxLinks.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxLinks_KeyDown);
            // 
            // textBoxBBCode
            // 
            this.textBoxBBCode.Location = new System.Drawing.Point(15, 164);
            this.textBoxBBCode.Multiline = true;
            this.textBoxBBCode.Name = "textBoxBBCode";
            this.textBoxBBCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxBBCode.Size = new System.Drawing.Size(447, 113);
            this.textBoxBBCode.TabIndex = 3;
            this.textBoxBBCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxBBCode_KeyDown);
            // 
            // labelBBCode
            // 
            this.labelBBCode.AutoSize = true;
            this.labelBBCode.Location = new System.Drawing.Point(12, 148);
            this.labelBBCode.Name = "labelBBCode";
            this.labelBBCode.Size = new System.Drawing.Size(129, 13);
            this.labelBBCode.TabIndex = 2;
            this.labelBBCode.Text = "Коды BB (для форумов):";
            // 
            // textBoxHtmlCode
            // 
            this.textBoxHtmlCode.Location = new System.Drawing.Point(15, 304);
            this.textBoxHtmlCode.Multiline = true;
            this.textBoxHtmlCode.Name = "textBoxHtmlCode";
            this.textBoxHtmlCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHtmlCode.Size = new System.Drawing.Size(447, 113);
            this.textBoxHtmlCode.TabIndex = 5;
            this.textBoxHtmlCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxHtmlCode_KeyDown);
            // 
            // labelHtmlCode
            // 
            this.labelHtmlCode.AutoSize = true;
            this.labelHtmlCode.Location = new System.Drawing.Point(12, 288);
            this.labelHtmlCode.Name = "labelHtmlCode";
            this.labelHtmlCode.Size = new System.Drawing.Size(103, 13);
            this.labelHtmlCode.TabIndex = 4;
            this.labelHtmlCode.Text = "Ссылки для HTML:";
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(387, 423);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // LinksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(474, 456);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxHtmlCode);
            this.Controls.Add(this.labelHtmlCode);
            this.Controls.Add(this.textBoxBBCode);
            this.Controls.Add(this.labelBBCode);
            this.Controls.Add(this.textBoxLinks);
            this.Controls.Add(this.labelLinks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LinksForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ссылки";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLinks;
        private System.Windows.Forms.TextBox textBoxLinks;
        private System.Windows.Forms.TextBox textBoxBBCode;
        private System.Windows.Forms.Label labelBBCode;
        private System.Windows.Forms.TextBox textBoxHtmlCode;
        private System.Windows.Forms.Label labelHtmlCode;
        private System.Windows.Forms.Button buttonCancel;
    }
}