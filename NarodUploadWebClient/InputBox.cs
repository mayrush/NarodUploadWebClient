using System;
using System.Windows.Forms;

namespace NarodUploadWebClient
{
    ///<summary>Окно ввода значения</summary>
    public partial class InputBox : Form
    {
        ///<summary>Конструктор класса</summary>
        public InputBox()
        {
            InitializeComponent();
        }

        bool _t; // Если была нажата кнопка Ok тогда t = true
        // если была нажата кнопка Cancel или в текстовое поле ничего не введено, то t = false
        String _temp;

        ///<summary>Функция вы</summary>
        ///<param name="bhead"></param>
        ///<param name="blabel"></param>
        ///<param name="s"></param>
        ///<returns></returns>
        public static bool Input(String bhead, // заголовок формы
        String blabel, // текст, который будет отображен в lable1
        out String s // значение введенное в текстовое поле, вернется из метода
        )
        {
            var bform = new InputBox { Text = bhead, InputText = { Text = blabel } }; // создаём форму
            bform.ShowDialog(); // показываем форму
            s = bform._temp; // возвращаем введнное значение в s
            return bform._t;
        }

        private void InputTextClick(object sender, EventArgs e)
        {
            if (!InputText.Focused)
            {
                InputText.Controls[0].Text = "";
            }
        }

        private void SubmitInput()
        {
            if (InputText.Text == "")
            {
                _t = false;
                Close();
            }
            else
            {
                _temp = InputText.Text;
                _t = true;
                Close();
            }
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            SubmitInput();
        }

        private void InputTextKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SubmitInput();
            }
        }
    }
}