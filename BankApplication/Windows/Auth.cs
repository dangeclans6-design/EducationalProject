using ReaLTaiizor.Forms;
using ReaLTaiizor.Manager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class Auth : MetroForm
    {
        private int count = 1;      // Количество попыток
        public Auth()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
        //--------------------[Кнопка "Войти"]---------------------//
        private async void GoAuthClick(object sender, EventArgs e)
        {
            string login = login_auth.Text.Trim();              // Введенный пользователем логин
            string password = password_auth.Text.Trim();        // Введенный пользователем пароль

            try
            {
                var clientData = await DataBase.CheckPasswordAndOutDataClient(login);

                if (clientData.Login != null)                   // Если нашел пользователя с Login == login
                {
                    if (clientData.Password == password)        // Если пароли совпадают
                    {                        
                        var findCardsClient = await DataBase.FindCardsClient(clientData.Id);
                        
                        Client currentClient = new(
                            clientData.Id,
                            clientData.ClientType,
                            clientData.Login,
                            clientData.Password,
                            clientData.Name1,
                            clientData.Name2,
                            clientData.Name3,
                            clientData.DateBirth,
                            clientData.NumberPhone,
                            clientData.Mail,
                            clientData.CreateAt,
                            findCardsClient.cards,
                            findCardsClient.paymentAccounts
                            );

                        Session.CurrentClient = currentClient;

                        ClientMenu clientMenu = new();
                        Hide();

                        MessageBox.Show($"Вы успешно авторизировались!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        clientMenu.Show();
                    }
                    else
                    {
                        MessageBox.Show($"Вы ввели неверный пароль!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        count++;
                        return;
                    }
                }
                else
                {
                    MessageBox.Show($"Вы ввели неверный логин!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    count++;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Information);
                count++;
            }
        }
        //--------------------[Кнопка "Назад"]---------------------//
        private void BackAuthClick(object sender, EventArgs e)
        {
            Hide();
            StartWindow window1 = new();
            window1.Show();
        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseAuth_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
