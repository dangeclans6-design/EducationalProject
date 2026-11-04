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
    public partial class AuthAdmin : MetroForm
    {
        private int count = 0;
        public AuthAdmin()
        {
            InitializeComponent();
        }
        //--------------------[Кнопка "Войти" в меню авторизации]---------------------//
        private async void GoAuthClick(object sender, EventArgs e)
        {
            string login = login_auth.Text.Trim();
            string password = password_auth.Text.Trim();
            string secureCode = code_auth.Text.Trim();

            try
            {
                var check = await DataBase.CheckPasswordAndOutDataAdmin(login);

                if (check.checkStatus) // Если нашел
                {
                    if (check.password == password & check.secureCode == secureCode) // Если пароли и код совпадают
                    {
                        // Поиск запросов (личные и не принятые) (Запускаем поиск всех)
                        var find_my_request = DataBase.SearchMyRequestAdmin(check.id);
                        var find_request = DataBase.SearchRequestAdmin();

                        // Ждем завершения всех запросов за один раз
                        await Task.WhenAll(find_request, find_my_request);

                        List<RequestAdminFull> request_full = [];

                        Admin CurrentAdmin = new(
                            check.id,
                            check.login,
                            check.password,
                            check.secureCode,
                            check.status,
                            (await find_my_request).MyRequest,
                            (await find_request).Request,
                            request_full);

                        Session.CurrentAdmin = CurrentAdmin;

                        AdminPanel adminPanel = new();

                        Hide();

                        MessageBox.Show($"Вы успешно авторизировались как Администратор!", "Авторизация в админ панели", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        adminPanel.Show();

                        count++;

                        // Отправляем лог
                        string messegeForLog = $"Авторизировался. Попыток: {count}.";
                        await DataBase.AddLogsAdmin(Session.CurrentAdmin.Id, "INFO", $"{messegeForLog}", $"AuthAdmin");
                    }
                    else
                    {
                        MessageBox.Show($"Вы ввели неверный пароль или секретный код!", "Авторизация в админ панели", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        count++;
                        return;
                    }
                }
                else
                {                    
                    MessageBox.Show($"Вы ввели неверный логин!", "Авторизация в админ панели", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    count++;
                    return;
                }
            }
            catch (Exception ex)
            {                               
                MessageBox.Show($"{ex}", "Авторизация в админ панели", MessageBoxButtons.OK, MessageBoxIcon.Information);
                count++;
            }
        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseAuth_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
