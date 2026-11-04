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
        private int count = 0;
        public Auth()
        {
            InitializeComponent();
        }
        //--------------------[Кнопка "Войти" в меню авторизации]---------------------//
        private async void GoAuthClick(object sender, EventArgs e)
        {
            string login = login_auth.Text.Trim();
            string password = password_auth.Text.Trim();

            try
            {
                var check = await DataBase.CheckPasswordAndOutDataClient(login);

                if (check.checkStatus) // Если нашел пользователя с Login == login
                {
                    if (check.password == password) // Если пароли совпадают
                    {
                        // Количество попыток
                        if (count == 0) { count = 1; }
                        else { count++; }

                        // Поиск карт клиента (Запускаем поиск ВСЕХ карт одновременно)
                        var find_usually_cards = DataBase.CheckUsuallyCardsClient(check.id);
                        var find_debit_cards = DataBase.CheckDebitCardsClient(check.id);
                        var find_credit_cards = DataBase.CheckCreditCardsClient(check.id);
                        var find_business_cards = DataBase.CheckBusinessCardsClient(check.id);
                        var find_request = DataBase.SearchRequestClient(check.id);

                        // При авторизации не ищем уведомления (искать будем в самой форме)
                        var notifications = new List<Notifications>();
                        var isReadNotifications = new List<Notifications>();

                        // Ждем завершения всех запросов за ОДИН раз
                        await Task.WhenAll(find_usually_cards, find_debit_cards, find_credit_cards, find_business_cards, find_request);

                        // Создание нового экземпляра класса И Передача данных клиента в экземпляр (с ним и будем работать)
                        Client currentClient = new(
                            check.id,
                            check.login,
                            check.password,
                            check.name1,
                            check.name2,
                            check.name3,
                            check.dateBirth,
                            check.numberPhone,
                            check.mail,
                            (await find_usually_cards).UsuallyCards,
                            (await find_debit_cards).DebitCards,
                            (await find_credit_cards).CreditCards,
                            (await find_business_cards).BussinesCards,
                            (await find_request).Request,
                            notifications,
                            isReadNotifications
                            );

                        // Передаем в "контейнер" нашего клиента (чтобы в других окнах не перекидывать в конструкторах CurrentClient)
                        Session.CurrentClient = currentClient;

                        // Создание окна "Меню Клиента"
                        ClientMenu clientMenu1 = new();

                        // Скрывает текущее окно
                        Hide();

                        // Выдаем сообщение об успешной авторизации
                        MessageBox.Show($"Вы успешно авторизировались!", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Открывает меню (окно) клиента
                        clientMenu1.Show();
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

                // Отправляем сообщение об авторизации в лог
                string messegeForLog = $"Пользователь с ID: {Session.CurrentClient.Id} авторизировался. Попыток: {count}.";
                await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно авторизации");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                count++;
            }
        }
        //--------------------[Кнопка "Назад"]---------------------//
        private void BackAuthClick(object sender, EventArgs e)
        {
            // Закрываем окно при нажатии на кнопку назад
            Hide();

            // Создаем новый экземпляр окна Window1
            StartWindow window1 = new();

            // Открываем Window1 (возвращаемся назад)
            window1.Show();
        }

        //--------------------[Закрытие формы]---------------------//
        private void AuthFormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //--------------------[Изменение размеров формы]---------------------//
        private void ResizeAuth(object sender, EventArgs e)
        {

        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseAuth_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
