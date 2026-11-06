using BankApplication.Windows;
using ReaLTaiizor.Controls;
using ReaLTaiizor.Design.Metro;
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
using System.Windows.Forms.VisualStyles;

namespace BankApplication
{
    public partial class ClientMenu : MetroForm
    {
        readonly List<CyberButton> controlsUsuallyCard = [];
        readonly List<CyberButton> controlsDebitCard = [];
        readonly List<CyberButton> controlsCreditCard = [];
        readonly List<CyberButton> controlsBussinesCard = [];
        readonly Client CurrentClient;
        public ClientMenu()
        {
            InitializeComponent();
            DoubleBuffered = true;

            CurrentClient = Session.CurrentClient;

            // Создаем списки с кнопками
            controlsUsuallyCard = [UsuallyCard1, UsuallyCard2, UsuallyCard3];
            controlsDebitCard = [DebitCard1, DebitCard2, DebitCard3];
            controlsCreditCard = [CreditCard1, CreditCard2, CreditCard3];
            controlsBussinesCard = [BussinesCard1, BussinesCard2, BussinesCard3];

            // По умолчанию при открытии нового окна открывается главное меню с данными клиента
            MainMenuPanel.Visible = true;
            MyCardsPanel.Visible = false;
            PanelRequest.Visible = false;
            PanelNotifications.Visible = false;
        }

        //---------------------[При активации формы]-----------------------//
        private async void ClientMenu_Activated(object sender, EventArgs e)
        {
            // Отображение ФИО в углу
            string name = $"{CurrentClient.Name1} {CurrentClient.Name2} {CurrentClient.Name3}";
            LabelName.Text = name;

            // Поиск активных уведомлений клиента при каждой активации формы
            var notifications = await DataBase.SearchNotificationsClient(CurrentClient.Id);
            CurrentClient.Notifications = notifications.Notifications;

            int countNoti = CurrentClient.Notifications.Count;
            NotificationsButton.TextButton = $"Уведомления ({countNoti})";
        }

        //---------------------[Кнопка "Закрыть форму"]-----------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            StartWindow w1 = new();

            Hide();
            w1.Show();
        }

        //---------------------[Кнопка чтобы увидеть логин]-----------------------//
        private void LoginLabel_Click(object sender, EventArgs e)
        {
            if (LoginLabel.Text == "************") { LoginLabel.Text = CurrentClient.Login; }
            else LoginLabel.Text = "************";
        }

        //---------------------[Кнопка чтобы увидеть пароль]-----------------------//
        private void PasswordLabel_Click(object sender, EventArgs e)
        {
            if (PasswordLabel.Text == "************") { PasswordLabel.Text = CurrentClient.Password; }
            else PasswordLabel.Text = "************";
        }

        //---------------------[Кнопка чтобы увидеть номер телефона]-----------------------//
        private void NumberPhoneLabel_Click(object sender, EventArgs e)
        {
            if (NumberPhoneLabel.Text == "************") { NumberPhoneLabel.Text = CurrentClient.NumberPhone; }
            else NumberPhoneLabel.Text = "************";
        }

        //---------------------[Кнопка чтобы увидеть почту]-----------------------//
        private void MailLabel_Click(object sender, EventArgs e)
        {
            if (MailLabel.Text == "************") { MailLabel.Text = CurrentClient.Mail; }
            else MailLabel.Text = "************";
        }

        //---------------------[Кнопка "Главное меню"]-----------------------//
        private async void MainMenuButton_Click(object sender, EventArgs e)
        {
            if (MainMenuPanel.Visible == true) return;
            else
            {
                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;
                MyCardsPanel.Visible = false;

                PanelRequest.Visible = false;

                PanelNotifications.Visible = false;

                // Включаем панель
                MainMenuPanel.Visible = true;
            }
        }

        //---------------------[Кнопка "Мои карты"]-----------------------//
        private async void MyCardButton_Click(object sender, EventArgs e)
        {
            if (MyCardsPanel.Visible == true) return;
            else
            {
                MainMenuPanel.Visible = false;

                PanelRequest.Visible = false;

                PanelNotifications.Visible = false;

                // Включаем панель
                MyCardsPanel.Visible = true;
            }
        }

        //---------------------[Кнопка "Обычные" в панели "Мои карты"]-----------------------//
        private async void ShowUsuallyCardsButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Считаем количество обычных карт
                int count_usually = CurrentClient.UsuallyCard.Count;
                string message = "";

                #region Вкл\Выкл кнопок

                DebitCard1.Visible = false;
                DebitCard2.Visible = false;
                DebitCard3.Visible = false;

                CreditCard1.Visible = false;
                CreditCard2.Visible = false;
                CreditCard3.Visible = false;

                BussinesCard1.Visible = false;
                BussinesCard2.Visible = false;
                BussinesCard3.Visible = false;

                TxtCard.Text = "Ваши обычные карты";
                TxtCard.Visible = true;
                TxtCard1.Visible = true;

                #endregion

                // Отрисовка кнопок
                for (int i = 0; i < count_usually; i++)
                {                    
                    message = $"Карта #{i + 1}: Номер карты: {CurrentClient.UsuallyCard[0].CardNumber}\n" +
                              $"Баланс карты: {CurrentClient.UsuallyCard[0].Balance}";

                    controlsUsuallyCard[i].TextButton = message;
                    controlsUsuallyCard[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", $"{ex}", "Обычные карты");
            }
        }

        //---------------------[Кнопка "Дебетовые" в панели "Мои карты"]-----------------------//
        private async void ShowDebitCardsButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count_debit = CurrentClient.DebitCard.Count;
                string message = "";

                #region Вкл\Выкл кнопок

                UsuallyCard1.Visible = false;
                UsuallyCard2.Visible = false;
                UsuallyCard3.Visible = false;

                CreditCard1.Visible = false;
                CreditCard2.Visible = false;
                CreditCard3.Visible = false;

                BussinesCard1.Visible = false;
                BussinesCard2.Visible = false;
                BussinesCard3.Visible = false;

                TxtCard.Text = "Ваши дебетовые карты";
                TxtCard.Visible = true;
                TxtCard1.Visible = true;

                #endregion

                // Отрисовка кнопок
                for (int i = 0; i < count_debit; i++)
                {
                    message = $"Карта #{i + 1}: Номер карты: {CurrentClient.DebitCard[0].CardNumber}\n" +
                              $"Баланс карты: {CurrentClient.DebitCard[0].Balance}";

                    controlsDebitCard[i].TextButton = message;
                    controlsDebitCard[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------------------[Кнопка "Кредитные" в панели "Мои карты"]-----------------------//
        private async void ShowCreditCardsButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count_credit = CurrentClient.CreditCard.Count;
                string message = "";

                #region Вкл\Выкл кнопок

                UsuallyCard1.Visible = false;
                UsuallyCard2.Visible = false;
                UsuallyCard3.Visible = false;

                DebitCard1.Visible = false;
                DebitCard2.Visible = false;
                DebitCard3.Visible = false;

                BussinesCard1.Visible = false;
                BussinesCard2.Visible = false;
                BussinesCard3.Visible = false;

                TxtCard.Text = "Ваши кредитные карты";
                TxtCard.Visible = true;
                TxtCard1.Visible = true;

                #endregion

                // Отрисовка кнопок
                for (int i = 0; i < count_credit; i++)
                {
                    message = $"Карта #{i + 1}: Номер карты: {CurrentClient.CreditCard[0].CardNumber}\n" +
                              $"Баланс карты: {CurrentClient.CreditCard[0].Balance}";

                    controlsCreditCard[i].TextButton = message;
                    controlsCreditCard[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------------------[Кнопка "Бизнес" в панели "Мои карты"]-----------------------//
        private async void ShowBusinessCardsButton_Click(object sender, EventArgs e)
        {
            try
            {
                int count_bussines = CurrentClient.BusinessCard.Count;
                string message = "";

                #region Вкл\Выкл кнопок

                UsuallyCard1.Visible = false;
                UsuallyCard2.Visible = false;
                UsuallyCard3.Visible = false;

                DebitCard1.Visible = false;
                DebitCard2.Visible = false;
                DebitCard3.Visible = false;

                CreditCard1.Visible = false;
                CreditCard2.Visible = false;
                CreditCard3.Visible = false;

                TxtCard.Text = "Ваши бизнес карты";
                TxtCard.Visible = true;
                TxtCard1.Visible = true;

                #endregion

                // Отрисовка кнопок
                for (int i = 0; i < count_bussines; i++)
                {
                    message = $"Карта #{i + 1}: Номер карты: {CurrentClient.BusinessCard[0].CardNumber}\n" +
                              $"Баланс карты: {CurrentClient.BusinessCard[0].Balance}";

                    controlsBussinesCard[i].TextButton = message;
                    controlsBussinesCard[i].Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------------------[Кнопка "Обновить страницу"]-----------------------//
        private async void UpdatePageClientMenu_Click(object sender, EventArgs e)
        {
            try
            {
                #region Поиск данных

                var check = await DataBase.CheckPasswordAndOutDataClient(CurrentClient.Login);

                // Поиск карт клиента (Запускаем поиск всех карт одновременно)
                var find_usually_cards = DataBase.CheckUsuallyCardsClient(check.id);
                var find_debit_cards = DataBase.CheckDebitCardsClient(check.id);
                var find_credit_cards = DataBase.CheckCreditCardsClient(check.id);
                var find_business_cards = DataBase.CheckBusinessCardsClient(check.id);
                var find_request = DataBase.SearchRequestClient(check.id);
                var find_notifications = DataBase.SearchNotificationsClient(check.id);
                var find_isReadNotifications = new List<Notifications>();

                // Ждем завершения всех запросов за один раз
                await Task.WhenAll(find_usually_cards, find_debit_cards, find_credit_cards, find_business_cards, find_request, find_notifications);

                // Создание нового экземпляра класса
                Client currentClientUpdate = new(
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
                    (await find_notifications).Notifications,
                    find_isReadNotifications
                    );

                #endregion

                // Передаем нашего обновленного клиента
                Session.CurrentClient = currentClientUpdate;

                Close();
                ClientMenu cm = new();
                await Task.Delay(1000);
                cm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------------------[Кнопка "Открытые заявки"]-----------------------//
        private async void OpenRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                var request = CurrentClient.OpenRequest;

                if (PanelRequest.Visible == true) return;
                else
                {                    
                    #region Вкл\Выкл

                    PanelRequest.Controls.Clear();

                    MyCardsPanel.Visible = false;

                    foreach (Control control in MyCardsPanel.Controls)
                    {
                        if ((string)control.Tag == "Cyber") control.Visible = false;
                    }

                    TxtCard.Visible = false;
                    TxtCard1.Visible = false;                    

                    MainMenuPanel.Visible = false;

                    PanelNotifications.Visible = false;

                    PanelRequest.Visible = true;

                    #endregion

                    int x_point = 20;
                    int y_point = 30;
                    string message = "";

                    // Создание списка запросов
                    for (int i = 0; i < request.Count; i++)
                    {
                        if (request[i].Type == 1) message = $"Запрос #{request[i].Id} - Изменение депозита\nЗаявка на смену депозита: {request[i].NewDeposit}\nСоздан: {request[i].Timestamp}";
                        if (request[i].Type == 2) message = $"Запрос #{request[i].Id} - Закрытие карты\nЗаявка на закрытие карты номер: {request[i].DefCard}\nСоздан: {request[i].Timestamp}";
                        if (request[i].Type == 3)
                        {
                            string sw = request[i].TypeCard;

                            switch (sw)
                            {
                                case "U":
                                    message = $"Запрос #{request[i].Id} - Открытие карты\nЗаявка на открытие 'Обычной' карты\nСоздан: {request[i].Timestamp}";
                                    break;
                                case "D":
                                    message = $"Запрос #{request[i].Id} - Открытие карты\nЗаявка на открытие 'Дебетовой' карты\nСоздан: {request[i].Timestamp}";
                                    break;
                                case "C":
                                    message = $"Запрос #{request[i].Id} - Открытие карты\nЗаявка на открытие 'Кредитной' карты\nСоздан: {request[i].Timestamp}";
                                    break;
                                case "B":
                                    message = $"Запрос #{request[i].Id} - Открытие карты\nЗаявка на открытие 'Бизнес' карты\nСоздан: {request[i].Timestamp}";
                                    break;
                            }
                        }

                        var myCyberText = new CyberButton
                        {
                            Width = 650,
                            Height = 100,
                            Location = new Point(x_point, y_point),
                            RoundingInt = 5,

                            Font = new Font("Segoe UI", 16.0f),
                            TextButton = message,

                            Background_WidthPen = 2,
                            ColorBackground_Pen = Color.LightSlateGray,
                            ColorBackground = Color.DimGray,
                            ForeColor = Color.Tan,
                        };

                        PanelRequest.Controls.Add(myCyberText);

                        y_point += 120;
                    }

                    // Отправляем сообщение в лог
                    string messegeForLog = $"Пользователь перешел в 'Открытые заявки'.";
                    await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Меню 'Открытые заявки'");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //---------------------[Кнопка "Уведомления"]-----------------------//
        private void NotificationsButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (PanelNotifications.Visible == true) return;
                else
                {
                    #region Вкл\Выкл

                    PanelNotifications.Controls.Clear();

                    MyCardsPanel.Visible = false;

                    foreach (Control control in MyCardsPanel.Controls)
                    {
                        if ((string)control.Tag == "Cyber") control.Visible = false;
                    }

                    TxtCard.Visible = false;
                    TxtCard1.Visible = false;

                    MainMenuPanel.Visible = false;                    

                    PanelRequest.Visible = false;

                    PanelNotifications.Visible = true;

                    #endregion

                    PanelNotifications.SuspendLayout();

                    int x_point = 85;
                    int y_point = 30;
                    var notifications = CurrentClient.Notifications;
                    string message = "";

                    for (int i = 0; i < notifications.Count; i++)
                    {
                        message = $"Уведомление: {notifications[i].Title}";

                        var NotificationsList = new CyberButton
                        {
                            Width = 650,
                            Height = 100,
                            Location = new Point(x_point, y_point),
                            RoundingInt = 20,

                            Font = new Font("Segoe UI", 16.0f),
                            TextButton = message,

                            Tag = (i, notifications[i]),

                            Background_WidthPen = 2,
                            ColorBackground_Pen = Color.LightSlateGray,
                            ColorBackground = Color.DimGray,
                            ForeColor = Color.Tan,
                        };

                        NotificationsList.Click += NotificationsList_Click;

                        PanelNotifications.Controls.Add(NotificationsList);

                        y_point += 120;
                    }

                    PanelNotifications.ResumeLayout(true);
                }

                PanelNotifications.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //--------------------[При нажатии на одно из уведомлений]---------------------//
        private void NotificationsList_Click(object sender, EventArgs e)
        {
            // Распознаем, какая именно кнопка вызвала это событие
            var clickedButton = (Control)sender;

            // Достаем из её свойства Tag данные запроса (i-тый элемент в списке), который мы туда положили в цикле
            var (index, currentNoti) = ((int, Notifications))clickedButton.Tag;

            PanelNotifications.Visible = false;
            PanelNotifications.Controls.Clear();

            ShowNoti notification = new(index, currentNoti);

            notification.ShowDialog();
        }

        //----------------------------------------------------------------------//
        //---------------------[Кнопка "Обычная карта 1"]-----------------------//
        private void UsuallyCard1_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.UsuallyCard[0].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.UsuallyCard[0].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("U", 0); // Тип карты: "U", номер в списке 1

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Обычная карта 2"]-----------------------//
        private void UsuallyCard2_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.UsuallyCard[1].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.UsuallyCard[1].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("U", 1); // Тип карты: "U", номер в списке 2

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Обычная карта 3"]-----------------------//
        private void UsuallyCard3_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.UsuallyCard[2].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.UsuallyCard[2].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("U", 2); // Тип карты: "U", номер в списке 3

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //----------------------------------------------------------------------//
        //---------------------[Кнопка "Дебетовая карта 1"]-----------------------//
        private void DebitCard1_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.DebitCard[0].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.DebitCard[0].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("D", 0); // Тип карты: "D", номер в списке 1

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Дебетовая карта 2"]-----------------------//
        private void DebitCard2_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.DebitCard[1].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.DebitCard[1].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("D", 1); // Тип карты: "D", номер в списке 2

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Дебетовая карта 3"]-----------------------//
        private void DebitCard3_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.DebitCard[2].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.DebitCard[2].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("D", 2); // Тип карты: "D", номер в списке 3

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //----------------------------------------------------------------------//
        //---------------------[Кнопка "Кредитная карта 1"]-----------------------//
        private void CreditCard1_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.CreditCard[0].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.CreditCard[0].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("C", 0); // Тип карты: "C", номер в списке 1

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Кредитная карта 2"]-----------------------//
        private void CreditCard2_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.CreditCard[1].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.CreditCard[1].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("C", 1); // Тип карты: "C", номер в списке 2

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Кредитная карта 3"]-----------------------//
        private void CreditCard3_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.CreditCard[2].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.CreditCard[2].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("C", 2); // Тип карты: "C", номер в списке 3

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //----------------------------------------------------------------------//
        //---------------------[Кнопка "Бизнес карта 1"]-----------------------//
        private void BussinesCard1_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.BusinessCard[0].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.BusinessCard[0].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("B", 0); // Тип карты: "B", номер в списке 1

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Бизнес карта 2"]-----------------------//
        private void BussinesCard2_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.BusinessCard[1].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.BusinessCard[1].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("B", 1); // Тип карты: "B", номер в списке 2

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }

        //---------------------[Кнопка "Бизнес карта 3"]-----------------------//
        private void BussinesCard3_Click(object sender, EventArgs e)
        {
            if (Session.CurrentClient.BusinessCard[2].Status == 0)
            {
                MessageBox.Show($"Ваша карта заморожена.\nПричина:{Session.CurrentClient.BusinessCard[2].Reason}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                ManagerCard mc = new("B", 2); // Тип карты: "B", номер в списке 3

                MyCardsPanel.Visible = false;

                foreach (Control control in MyCardsPanel.Controls)
                {
                    if ((string)control.Tag == "Cyber") control.Visible = false;
                }

                TxtCard.Visible = false;
                TxtCard1.Visible = false;

                mc.ShowDialog();
            }
        }
    }
}
