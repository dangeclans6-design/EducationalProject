using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace BankApplication
{
    public partial class StartWindow : MetroForm
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void Window1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void RegButtonClick(object sender, EventArgs e)
        {
            // Создаем экземпляр новой формы регистрации
            Reg registerform = new Reg();

            // Показываем форму регистрации
            registerform.Show();

            // Скрываем или закрываем текущее окно (закрыть Close())
            Hide();
        }

        private void AvtorButtonClick(object sender, EventArgs e)
        {
            // Создаем экземпляр новой формы регистрации
            Auth authform = new Auth();

            // Показываем форму регистрации
            authform.Show();

            // Скрываем или закрываем текущее окно (закрыть Close())
            Hide();
        }

        private void CloseWindow1Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
