using System;
using System.Windows.Forms;

namespace NarodUploadWebClient
{
    public partial class LinksForm : Form
    {
        public LinksForm()
        {
            InitializeComponent();
        }

        public static void ShowLinks(string[] links = null, string[] bbcode = null, string[] htmlcode = null)
        {
            var linksform = new LinksForm { textBoxLinks = { Lines = links }, textBoxBBCode = { Lines = bbcode }, textBoxHtmlCode = { Lines = htmlcode } };
            linksform.ShowDialog();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxLinks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBoxLinks.SelectAll();
            }
        }

        private void textBoxBBCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBoxBBCode.SelectAll();
            }
        }

        private void textBoxHtmlCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                textBoxHtmlCode.SelectAll();
            }
        }
    }
}