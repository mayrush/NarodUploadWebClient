using System;
using System.Windows.Forms;
using NarodUploadWebClient.Properties;

namespace NarodUploadWebClient
{
    public partial class SelectFilesToUpload : Form
    {
        public SelectFilesToUpload()
        {
            InitializeComponent();
        }

        bool _t; // Если была нажата кнопка Ok тогда t = true
        // если была нажата кнопка Cancel или в текстовое поле ничего не введено, то t = false
        string[] _temp;

        public static bool Input(out string[] s)
        {
            var bform = new SelectFilesToUpload { }; // создаём форму
            bform.ShowDialog(); // показываем форму
            s = bform._temp; // возвращаем введнное значение в s
            return bform._t;
        }

        private void ButtonOpenFileClick(object sender, EventArgs e)
        {
            //
            // openFileDialog1
            //
            var openFileDialog1 = new OpenFileDialog
            {
                Filter = Resources.MainForm_OpenFileDialog_Filter_All_Files__,
                Multiselect = true,
                RestoreDirectory = true,
                Title = Resources.MainForm_OpenFileDialog_Caption__
            };

            if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
            _sFile.Lines = openFileDialog1.FileNames;
        }

        private void Upload(object sender, EventArgs e)
        {
            if (_sFile.Lines.Length == 0)
            {
                _t = false;
                Close();
            }
            else
            {
                _temp = _sFile.Lines;
                _t = true;
                Close();
            }
        }

        private void SelectFilesToUpload_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_sFile.Lines.Length == 0)
            {
                _t = false;
            }
            else
            {
                _temp = _sFile.Lines;
                _t = true;
            }
        }
    }
}