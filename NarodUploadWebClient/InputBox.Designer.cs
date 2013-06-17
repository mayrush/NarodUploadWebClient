using System.Windows.Forms;

namespace NarodUploadWebClient
{
    partial class InputBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private TextBox InputText = new TextBox();
        private Button OkButton = new Button();

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
            //
            // InputText
            //
            this.InputText.Location = new System.Drawing.Point(12, 12);
            this.InputText.Name = "InputText";
            this.InputText.Size = new System.Drawing.Size(238, 20);
            this.InputText.TabIndex = 0;
            this.InputText.Text = "Введите e-mail";
            this.InputText.Click += InputTextClick;
            this.InputText.KeyDown += InputTextKeyDown;
            //
            // OkButton
            //
            this.OkButton.Location = new System.Drawing.Point(255, 10);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(65, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += OkButtonClick;
            //
            // InputString
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 46);
            this.ControlBox = true;
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.InputText);
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InputString";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ввод строки";
            this.TopMost = true;

            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "InputBox";
        }

        #endregion Windows Form Designer generated code
    }
}