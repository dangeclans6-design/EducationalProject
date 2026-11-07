using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankApplication
{
    public partial class ManagerCard : MetroForm
    {
        readonly Client CurrentClient;
        readonly int id0;
        readonly string def;
        readonly int x_panel;
        readonly int y_panel;
        readonly int height_panel;
        readonly int width_panel;

        public ManagerCard(string type_card, int id0)
        {
            InitializeComponent();
            DoubleBuffered = true;

            CurrentClient = Session.CurrentClient;

            Size = new Size(1420, 880);

            def = $"{type_card}{id0}";
            this.id0 = id0;

            if (def == "U0" || def == "U1" || def == "U2")
            {
                // Отображение заголовка
                LabelUsuallyCard.Visible = true;

                // Отображаем панель обычной карты
                PanelUsuallyCard.Visible = true;

                U_numberCardLabel.Text = CurrentClient.UsuallyCard[id0].CardNumber;
                U_balanceLabel.Text = $"{CurrentClient.UsuallyCard[id0].Balance} рублей.";
                U_balanceDepositLabel.Text = $"{CurrentClient.UsuallyCard[id0].Deposit} рублей.";
                U_limitDepositLabel.Text = $"{CurrentClient.UsuallyCard[id0].LimitDeposit} рублей.";
                U_limitBalanceLabel.Text = $"{CurrentClient.UsuallyCard[id0].LimitBalance} рублей.";

                #region Размеры панели

                x_panel = PanelUsuallyCard.Location.X;
                y_panel = PanelUsuallyCard.Location.Y;

                height_panel = PanelUsuallyCard.Height;
                width_panel = PanelUsuallyCard.Width;

                #endregion
            }
            if (def == "D0" || def == "D1" || def == "D2")
            {
                // Отображение заголовка
                LabelDebitCard.Visible = true;

                // Отображаем панель обычной карты
                PanelDebitCard.Visible = true;

                D_numberCardLabel.Text = CurrentClient.DebitCard[id0].CardNumber;
                D_balanceLabel.Text = $"{CurrentClient.DebitCard[id0].Balance} рублей.";
                D_balanceDepositLabel.Text = $"{CurrentClient.DebitCard[id0].Deposit} рублей.";
                D_limitDepositLabel.Text = $"{CurrentClient.DebitCard[id0].LimitDeposit} рублей.";
                D_limitBalanceLabel.Text = $"{CurrentClient.DebitCard[id0].LimitBalance} рублей.";
                D_cashbackBalanceLabel.Text = $"{CurrentClient.DebitCard[id0].Cashback} (лимит: {CurrentClient.DebitCard[id0].LimitCashback} рублей).";

                #region Размеры панели

                x_panel = PanelDebitCard.Location.X;
                y_panel = PanelDebitCard.Location.Y;

                height_panel = PanelDebitCard.Height;
                width_panel = PanelDebitCard.Width;

                #endregion
            }
            if (def == "C0" || def == "C1" || def == "C2")
            {
                // Отображение заголовка
                LabelCreditCard.Visible = true;

                // Отображаем панель обычной карты
                PanelCreditCard.Visible = true;

                C_numberCardLabel.Text = CurrentClient.CreditCard[id0].CardNumber;
                C_balanceLabel.Text = $"{CurrentClient.CreditCard[id0].Balance} рублей.";
                C_balanceDepositLabel.Text = $"{CurrentClient.CreditCard[id0].Deposit} рублей.";
                C_limitDepositLabel.Text = $"{CurrentClient.CreditCard[id0].LimitDeposit} рублей.";
                C_limitBalanceLabel.Text = $"{CurrentClient.CreditCard[id0].LimitBalance} рублей.";
                C_debtLabel.Text = $"{CurrentClient.CreditCard[id0].Debt} (лимит: {CurrentClient.CreditCard[id0].LimitDebt} рублей).";

                #region Размеры панели

                x_panel = PanelCreditCard.Location.X;
                y_panel = PanelCreditCard.Location.Y;

                height_panel = PanelCreditCard.Height;
                width_panel = PanelCreditCard.Width;

                #endregion
            }
            if (def == "B0" || def == "B1" || def == "B2")
            {
                // Отображение заголовка
                LabelBussinesCard.Visible = true;

                // Отображаем панель обычной карты
                PanelBussinesCard.Visible = true;

                B_numberCardLabel.Text = CurrentClient.BusinessCard[id0].CardNumber;
                B_balanceLabel.Text = $"{CurrentClient.BusinessCard[id0].Balance} рублей.";
                B_balanceDepositLabel.Text = $"{CurrentClient.BusinessCard[id0].Deposit} рублей.";
                B_limitDepositLabel.Text = $"{CurrentClient.BusinessCard[id0].LimitDeposit} рублей.";
                B_limitBalanceLabel.Text = $"{CurrentClient.BusinessCard[id0].LimitBalance} рублей.";
                B_percentLabel.Text = $"{CurrentClient.BusinessCard[id0].Percent}%";

                #region Размеры панели

                x_panel = PanelBussinesCard.Location.X;
                y_panel = PanelBussinesCard.Location.Y;

                height_panel = PanelBussinesCard.Height;
                width_panel = PanelBussinesCard.Width;

                #endregion
            }

            #region Координаты кнопок

            // Координаты элементов "Оплатить" относительно панели
            PayTxt.Location = new Point(x_panel, y_panel + height_panel + 10);
            Amount.Location = new Point(x_panel, y_panel + height_panel + 60);
            PayButton.Location = new Point(x_panel + width_panel - PayButton.Width, y_panel + height_panel + 35);

            // Координаты элементов "Полполнить баланс" относительно панели
            ReplenishBalanceTxt.Location = PayTxt.Location;
            ReplenishBalanceButton.Location = PayButton.Location;

            // Координаты элементов "Пополнить депозит" относительно панели
            ReplenishDepositButton.Location = PayButton.Location;

            // Координаты элементов "Изменить лимит депозита (запрос)" относительно панели
            ChangeLimitDepositTxt1.Location = PayTxt.Location;
            ChangeLimitDepositTxt2.Location = new Point(x_panel, y_panel + height_panel + 100);
            Description.Location = new Point(x_panel, y_panel + height_panel + 150);
            ChangeLimitDepositButton.Location = new Point(x_panel + width_panel - ChangeLimitDepositButton.Width, y_panel + height_panel + 240);

            // Координаты элементов "Закрыть карту (запрос)" относительно панели
            CloseAndOpenTxt.Location = ChangeLimitDepositTxt2.Location;
            CloseCardButton.Location = ChangeLimitDepositButton.Location;

            // Координаты элементов "Открыть карту (запрос)" относительно панели
            OpenCardButton.Location = ChangeLimitDepositButton.Location;

            #endregion


        }

        //-------------------------[Кнопка "Закрыть форму"]----------------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //-------------------------[Кнопка "Оплатить" (боковое меню)]----------------------------//
        private void PaySideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Скрываем ненужные поля
                ReplenishBalanceTxt.Visible = false;
                ReplenishBalanceButton.Visible = false;

                ReplenishDepositButton.Visible = false;

                ChangeLimitDepositTxt1.Visible = false;
                ChangeLimitDepositTxt2.Visible = false;
                Description.Visible = false;
                ChangeLimitDepositButton.Visible = false;

                CloseAndOpenTxt.Visible = false;
                CloseCardButton.Visible = false;

                OpenCardButton.Visible = false;

                // Отображаем нужные поля
                PayTxt.Visible = true;
                Amount.Visible = true;
                PayButton.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Оплатить"]----------------------------//
        private async void PayButton_Click(object sender, EventArgs e)
        {
            // Для лога (указывает в каком окне произошла ошибка)
            string windowError = "";
            string ErrorMessage = "Введенно некорректное значение";

            try
            {
                switch (def)
                {
                    case "U0":
                    case "U1":
                    case "U2":
                        {
                            windowError = "Обычная карта";

                            // Парсим то, что ввели
                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            // Переменная-ссылка на рабочую карту
                            var CurrentCard = CurrentClient.UsuallyCard[id0];

                            // Отправляем данные на проверку и если все хорошо получаем структуру
                            var preview = CurrentCard.PreparePay(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите оплатить {preview.Amount} рублей?\n",
                                                                  "Оплата", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                            if (result == DialogResult.OK)
                            {
                                // Замораживаем форму на время ожидания ответа от БД
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                ErrorMessage = "Произошла непредвиденная ошибка!";

                                // Применяем списание (если все хорошо). Иначе вернется ошибка
                                await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.CardNumber, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit);

                                // Выдаем сообщение
                                MessageBox.Show($"Вы успешно оплатили {preview.Amount} рублей.\n" +
                                                $"Ваш баланс: {CurrentCard.Balance} рублей.");

                                // Обновляем надпись
                                U_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";

                                // Отправляем сообщение об оплате в лог
                                string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {preview.Amount} рублей.";
                                await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (обычная карта)");
                            }
                            else return;

                            break;
                        }
                    case "D0":
                    case "D1":
                    case "D2":
                        {
                            windowError = "Дебетовая карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);
                            var CurrentCard = CurrentClient.DebitCard[id0];
                            var preview = CurrentCard.PreparePay(amount);

                            if (amount >= 10000)
                            {
                                if (preview.NewCashback > CurrentCard.LimitCashback)
                                {
                                    DialogResult result = MessageBox.Show($"Вы действительно хотите оплатить {amount} рублей?" +
                                                                          $"После оплаты Ваш кешбек счет достигнет лимита!" +
                                                                          $"Учтите, что та часть кешбека, которая не входит в лимит, сгорит!",
                                                                          "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        Enabled = false;
                                        Cursor = Cursors.WaitCursor;

                                        ErrorMessage = "Произошла непредвиденная ошибка!";

                                        await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Cashback);

                                        MessageBox.Show($"Вы успешно оплатили {amount} рублей.\n" +
                                                        $"Ваш баланс кешбека: {CurrentCard.Cashback} рублей.",
                                                        "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else return;

                                    // Отправляем сообщение об оплате в лог
                                    string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {amount} рублей.\n" +
                                                           $"Остаток: {CurrentCard.Balance} рублей.\n" +
                                                           $"Кешбек: {CurrentCard.LimitCashback - preview.NewCashback + preview.Cashback} рублей.\n" +
                                                           $"Баланс кешбека: {CurrentCard.Cashback} рублей.\n";
                                    await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (дебетовая карта)");
                                }
                                else
                                {
                                    DialogResult result = MessageBox.Show($"Вы действительно хотите оплатить {amount} рублей?" +
                                                                          $"Ваш кешбек после оплаты составит {preview.Cashback} рублей.",
                                                                          "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        Enabled = false;
                                        Cursor = Cursors.WaitCursor;

                                        ErrorMessage = "Произошла непредвиденная ошибка!";

                                        await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Cashback);

                                        MessageBox.Show($"Вы успешно оплатили {amount} рублей.\n" +
                                                        $"Ваш баланс кешбека: {CurrentCard.Cashback} рублей.",
                                                        "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else return;

                                    // Отправляем сообщение об оплате в лог
                                    string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {amount} рублей.\n" +
                                                           $"Остаток: {CurrentCard.Balance} рублей.\n" +
                                                           $"Кешбек: {preview.Cashback} рублей.\n" +
                                                           $"Баланс кешбека: {CurrentCard.Cashback} рублей.\n";
                                    await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (дебетовая карта)");
                                }
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show($"Вы действительно хотите оплатить {amount} рублей?" +
                                                                      $"Так как сумма оплаты меньше 10000 рублей, то Ваш кешбек после оплаты будет 0 рублей!",
                                                                      "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    Enabled = false;
                                    Cursor = Cursors.WaitCursor;

                                    ErrorMessage = "Произошла непредвиденная ошибка!";

                                    await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Cashback);

                                    MessageBox.Show($"Вы успешно оплатили {amount} рублей.\n" +
                                                    $"Ваш баланс кешбека: {CurrentCard.Cashback} рублей.",
                                                    "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else return;

                                // Отправляем сообщение об оплате в лог
                                string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {amount} рублей.\n" +
                                                       $"Остаток: {CurrentCard.Balance} рублей.\n" +
                                                       $"Кешбек: {preview.Cashback} рублей.\n" +
                                                       $"Баланс кешбека: {CurrentCard.Cashback} рублей.\n";
                                await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (дебетовая карта)");
                            }

                            D_balanceLabel.Text = $"{CurrentCard.Balance}  рублей.";
                            D_cashbackBalanceLabel.Text = $"{CurrentCard.Cashback} рублей (лимит: {CurrentCard.LimitCashback} рублей).";
                            break;
                        }
                    case "C0":
                    case "C1":
                    case "C2":
                        {
                            windowError = "Кредитная карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.CreditCard[id0];
                            var preview = CurrentCard.PreparePay(amount);

                            if (preview.Difference >= 0)
                            {
                                DialogResult result = MessageBox.Show($"Вы действительно хотите оплатить {amount} рублей?\n" +
                                                                      $"Так как на Вашем балансе достаточно средств, то сумма долга не изменится!" +
                                                                      $"Ваш текущий долг: {CurrentCard.Debt} рублей.",
                                                                      "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    Enabled = false;
                                    Cursor = Cursors.WaitCursor;

                                    ErrorMessage = "Произошла непредвиденная ошибка!";

                                    await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Debt);

                                    MessageBox.Show($"Вы успешно оплатили {amount} рублей.\n" +
                                                    $"Ваш баланс: {CurrentCard.Balance} рублей.\n" +
                                                    $"Ваш долг: {CurrentCard.Debt} рублей.");
                                }
                                else return;

                                // Отправляем сообщение об оплате в лог
                                string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {amount} рублей.\n" +
                                                       $"Остаток: {CurrentCard.Balance} рублей.\n" +
                                                       $"Прибавилось задолженности: 0 рублей.\n" +
                                                       $"Оставшаяся задоложенность: {CurrentCard.Debt} рублей.\n";
                                await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (Кредитная карта)");
                            }
                            else
                            {
                                DialogResult result = MessageBox.Show($"Вы действительно хотите оплатить {amount} рублей?\n" +
                                                                      $"Так как на Вашем балансе недостаточно средств, то сумма долга увеличится на {Math.Abs(preview.Difference)} рублей!" +
                                                                      $"Ваш текущий долг: {CurrentCard.Debt} рублей.",
                                                                      "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {
                                    Enabled = false;
                                    Cursor = Cursors.WaitCursor;

                                    ErrorMessage = "Произошла непредвиденная ошибка!";

                                    await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Debt);

                                    MessageBox.Show($"Вы успешно оплатили {amount} рублей.\n" +
                                                    $"Ваш баланс: {CurrentCard.Balance} рублей.\n" +
                                                    $"Ваш текущий долг: {CurrentCard.Debt} рублей.");
                                }
                                else return;

                                // Отправляем сообщение об оплате в лог
                                string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {amount} рублей.\n" +
                                                       $"Остаток: {CurrentCard.Balance} рублей.\n" +
                                                       $"Прибавилось задолженности: {Math.Abs(preview.Difference)} рублей.\n" +
                                                       $"Текущая задоложенность: {CurrentCard.Debt} рублей.\n";
                                await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (Кредитная карта)");
                            }

                            C_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";
                            C_debtLabel.Text = $"{CurrentCard.Debt} рублей.";
                            break;
                        }
                    case "B0":
                    case "B1":
                    case "B2":
                        {
                            windowError = "Бизнес карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.BusinessCard[id0];
                            var preview = CurrentCard.PreparePay(amount);

                            DialogResult resilt = MessageBox.Show($"Вы действительно хотите оплатить {preview.NewPay} рублей?\n" +
                                                                  $"Комиссия при оплате {CurrentCard.Percent}%",
                                                                  "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resilt == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                ErrorMessage = "Произошла непредвиденная ошибка!";

                                await CurrentCard.Pay(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Percent);

                                MessageBox.Show($"Вы успешно оплатили {preview.NewPay} рублей.\n" +
                                                $"Ваш баланс: {CurrentCard.Balance} рублей.");
                            }

                            B_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";

                            // Отправляем сообщение об оплате в лог
                            string messegeForLog = $"Пользователь оплатил с карты (Номер: {CurrentCard.CardNumber}) {preview.NewPay} рублей.\n" +
                                                   $"Остаток: {CurrentCard.Balance} рублей.\n" +
                                                   $"Процент оплаты: {preview.NewPay - amount} рублей ({CurrentCard.Percent}%).\n";
                            await DataBase.AddLogs(Session.CurrentClient.Id, "INFO", messegeForLog, "Окно оплаты (бизнес карта)");
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ErrorMessage}\nДетали: {ex}", $"Окно оплаты ({windowError})");
            }
            finally
            {
                // Включаем форму обратно
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //-------------------------[Кнопка "Пополнить баланс" (боковое меню)]----------------------------//
        private void ReplenishBalanceSideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Скрываем ненужные поля
                PayTxt.Visible = false;
                PayButton.Visible = false;

                ReplenishDepositButton.Visible = false;

                ChangeLimitDepositTxt1.Visible = false;
                ChangeLimitDepositTxt2.Visible = false;
                Description.Visible = false;
                ChangeLimitDepositButton.Visible = false;

                CloseAndOpenTxt.Visible = false;
                CloseCardButton.Visible = false;

                OpenCardButton.Visible = false;

                // Отображаем нужные поля
                ReplenishBalanceTxt.Visible = true;
                Amount.Visible = true;
                ReplenishBalanceButton.Visible = true; ;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Пополнить баланс"]----------------------------//
        private async void ReplenishBalanceButton_Click(object sender, EventArgs e)
        {
            // Для лога (указывает в каком окне произошла ошибка)
            string windowError = "";
            string ErrorMessage = "Введенно некорректное значение";

            try
            {
                switch (def)
                {
                    case "U0":
                    case "U1":
                    case "U2":
                        {
                            windowError = "Обычная карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.UsuallyCard[id0];
                            var preview = CurrentCard.PrepareReplenishBalance(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить баланс на {amount} рублей?\n",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                // Замораживаем форму на время ожидания ответа от БД
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishBalance(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit);

                                MessageBox.Show($"Вы успешно пополнили баланс на {amount} рублей.\n" +
                                                $"Ваш баланс: {CurrentCard.Balance} рублей.",
                                                "Пополнение", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                            else return;

                            U_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";
                            break;
                        }
                    case "D0":
                    case "D1":
                    case "D2":
                        {
                            windowError = "Дебетовая карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.DebitCard[id0];
                            var preview = CurrentCard.PrepareReplenishBalance(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить баланс на {amount} рублей?\n",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishBalance(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Cashback);

                                MessageBox.Show($"Вы успешно пополнили баланс на {amount} рублей.\n" +
                                                $"Ваш баланс: {CurrentCard.Balance} рублей.",
                                                "Пополнение", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                            else return;

                            D_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";
                            break;
                        }
                    case "C0":
                    case "C1":
                    case "C2":
                        {
                            windowError = "Кредитная карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.CreditCard[id0];
                            var preview = CurrentCard.PrepareReplenishBalance(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить баланс на {amount} рублей?\n",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishBalance(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Debt);

                                MessageBox.Show($"Вы успешно пополнили баланс на {amount} рублей.\n" +
                                                $"Ваш баланс: {CurrentCard.Balance} рублей.",
                                                "Пополнение", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                            else return;

                            C_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";
                            break;
                        }
                    case "B0":
                    case "B1":
                    case "B2":
                        {
                            windowError = "Бизнес карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.BusinessCard[id0];
                            var preview = CurrentCard.PrepareReplenishBalance(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить баланс на {preview.Amount} рублей?\n" +
                                                                  $"Комиссия пополнения {preview.Commission} рублей.",
                                                                  "Оплата", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishBalance(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Percent);

                                MessageBox.Show($"Вы успешно пополнили баланс на {preview.Amount - preview.Commission} рублей.\n" +
                                                $"Ваш баланс: {CurrentCard.Balance} рублей.",
                                                "Пополнение", MessageBoxButtons.OK, MessageBoxIcon.Question);
                            }
                            else return;

                            B_balanceLabel.Text = $"{CurrentCard.Balance} рублей.";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ErrorMessage}\nДетали: {ex}", $"Окно оплаты ({windowError})");
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //-------------------------[Кнопка "Пополнить депозит" (боковое меню)]----------------------------//
        private void ReplenishDepositSideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Скрываем ненужные поля
                PayTxt.Visible = false;
                PayButton.Visible = false;

                ReplenishBalanceButton.Visible = false;

                ChangeLimitDepositTxt1.Visible = false;
                ChangeLimitDepositTxt2.Visible = false;
                Description.Visible = false;
                ChangeLimitDepositButton.Visible = false;

                CloseAndOpenTxt.Visible = false;
                CloseCardButton.Visible = false;

                OpenCardButton.Visible = false;

                // Отображаем нужные поля
                ReplenishBalanceTxt.Visible = true;
                Amount.Visible = true;
                ReplenishDepositButton.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Пополнить депозит"]----------------------------//
        private async void ReplenishDepositButton_Click(object sender, EventArgs e)
        {
            // Для лога (указывает в каком окне произошла ошибка)
            string windowError = "";
            string ErrorMessage = "Введенно некорректное значение";

            try
            {
                switch (def)
                {
                    case "U0":
                    case "U1":
                    case "U2":
                        {
                            windowError = "Обычная карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.UsuallyCard[id0];
                            var preview = CurrentCard.PrepareReplenishDeposit(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить депозит на {amount} рублей?",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishDeposit(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit);

                                MessageBox.Show($"Вы успешно пополнили депозит на {amount} рублей.\n" +
                                                $"Ваш депозит: {CurrentCard.Deposit} рублей.");
                            }
                            else return;

                            ErrorMessage = "Введенно некорректное значение";

                            U_balanceDepositLabel.Text = $"{CurrentCard.Deposit} рублей.";
                            break;
                        }
                    case "D0":
                    case "D1":
                    case "D2":
                        {
                            windowError = "Дебетовая карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.DebitCard[id0];
                            var preview = CurrentCard.PrepareReplenishDeposit(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить депозит на {amount} рублей?",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishDeposit(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Cashback);

                                MessageBox.Show($"Вы успешно пополнили депозит на {amount} рублей.\n" +
                                                $"Ваш депозит: {CurrentCard.Deposit} рублей.");
                            }
                            else return;

                            ErrorMessage = "Введенно некорректное значение";

                            D_balanceDepositLabel.Text = $"{CurrentCard.Deposit} рублей.";
                            break;
                        }
                    case "C0":
                    case "C1":
                    case "C2":
                        {
                            windowError = "Кредитная карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.CreditCard[id0];
                            var preview = CurrentCard.PrepareReplenishDeposit(amount);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить депозит на {amount} рублей?",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishDeposit(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Debt);

                                MessageBox.Show($"Вы успешно пополнили депозит на {amount} рублей.\n" +
                                                $"Ваш депозит: {CurrentCard.Deposit} рублей.");
                            }
                            else return;

                            ErrorMessage = "Введенно некорректное значение";

                            C_balanceDepositLabel.Text = $"{CurrentCard.Deposit} рублей.";
                            break;
                        }
                    case "B0":
                    case "B1":
                    case "B2":
                        {
                            windowError = "Бизнес карта";

                            decimal amount = TryReg.TryDecimal(Amount.Text);

                            var CurrentCard = CurrentClient.BusinessCard[id0];
                            var preview = CurrentCard.PrepareReplenishDeposit(amount);

                            DialogResult resilt = MessageBox.Show($"Вы действительно хотите пополнить депозит на {amount} рублей?\n" +
                                                                  $"Комиссия пополнения {preview.Commission} рублей (3% от суммы пополнения)\n" +
                                                                  $"Сумма зачисления {preview.NewReplenish} рублей\n",
                                                                  "Пополнение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (resilt == DialogResult.Yes)
                            {
                                ErrorMessage = "Непредвиденная ошибка! Пожалуйста, обновите страницу или повторите попытку позже.";

                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.ReplenishDeposit(preview, CurrentCard.Id, CurrentCard.Balance, CurrentCard.Deposit, CurrentCard.LimitDeposit, CurrentCard.Percent);

                                MessageBox.Show($"Вы успешно пополнили депозит на {preview.NewReplenish} рублей.\n" +
                                                $"Ваш депозит: {CurrentCard.Deposit} рублей.");
                            }
                            else return;

                            ErrorMessage = "Введенно некорректное значение";

                            B_balanceDepositLabel.Text = $"{CurrentCard.Deposit} рублей.";
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ErrorMessage}\nДетали: {ex}", $"Окно оплаты ({windowError})");
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //-------------------------[Кнопка "Изменить лимит депозита (запрос)" (боковое меню)]----------------------------//
        private void ChangeLimitDepositSideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Скрываем ненужные поля
                PayTxt.Visible = false;
                PayButton.Visible = false;

                ReplenishBalanceTxt.Visible = false;
                ReplenishBalanceButton.Visible = false;

                ReplenishDepositButton.Visible = false;

                CloseAndOpenTxt.Visible = false;
                CloseCardButton.Visible = false;

                OpenCardButton.Visible = false;

                // Отображаем нужные поля
                Amount.Visible = true;
                ChangeLimitDepositTxt1.Visible = true;
                ChangeLimitDepositTxt2.Visible = true;
                Description.Visible = true;
                ChangeLimitDepositButton.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Отправить" (изменить лимит депозита)]----------------------------//
        private async void ChangeLimitDepositButton_Click(object sender, EventArgs e)
        {
            // Для лога (указывает для какого типа карт произошла ошибка)
            string windowError = "";

            try
            {
                switch (def)
                {
                    case "U0":
                    case "U1":
                    case "U2":
                        {
                            decimal newDeposit = TryReg.TryDecimal(Amount.Text);
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.UsuallyCard[id0];
                            var preview = CurrentCard.PrepareChangeLimitDeposit(newDeposit, Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку на изменние депозита?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Ваш текущий лимит: {CurrentCard.LimitDeposit} рублей, Вы меняете на: {newDeposit} рублей.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestChangeLimitDeposit(preview);

                                MessageBox.Show($"Вы успешно отправили запрос на изменение депозита.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");
                            }
                            else return;

                            break;
                        }
                    case "D0":
                    case "D1":
                    case "D2":
                        {
                            decimal newDeposit = TryReg.TryDecimal(Amount.Text);
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.DebitCard[id0];
                            var preview = CurrentCard.PrepareChangeLimitDeposit(newDeposit, Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку на изменние депозита?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Ваш текущий лимит: {CurrentCard.LimitDeposit} рублей, Вы меняете на: {newDeposit} рублей.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestChangeLimitDeposit(preview);

                                MessageBox.Show($"Вы успешно отправили запрос на изменение депозита.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");
                            }
                            else return;

                            break;
                        }
                    case "C0":
                    case "C1":
                    case "C2":
                        {
                            decimal newDeposit = TryReg.TryDecimal(Amount.Text);
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.CreditCard[id0];
                            var preview = CurrentCard.PrepareChangeLimitDeposit(newDeposit, Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку на изменние депозита?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Ваш текущий лимит: {CurrentCard.LimitDeposit} рублей, Вы меняете на: {newDeposit} рублей.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestChangeLimitDeposit(preview);

                                MessageBox.Show($"Вы успешно отправили запрос на изменение депозита.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");
                            }
                            else return;

                            break;
                        }
                    case "B0":
                    case "B1":
                    case "B2":
                        {
                            decimal newDeposit = TryReg.TryDecimal(Amount.Text);
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.BusinessCard[id0];
                            var preview = CurrentCard.PrepareChangeLimitDeposit(newDeposit, Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку на изменние депозита?" +
                                                                  $"С Вашего баланса будет списано 400.000 рублей. В случае, если Вам откажут, деньги будут зачисленны обратно." +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Ваш текущий лимит: {CurrentCard.LimitDeposit} рублей, Вы меняете на: {newDeposit} рублей.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestChangeLimitDeposit(preview);

                                MessageBox.Show($"Вы успешно отправили запрос на изменение депозита.\n" +
                                                $"С Вашего баланса было списано 400.000 рублей. Ваш баланс: {CurrentCard.Balance} рублей" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");

                                B_balanceLabel.Text = $"{CurrentCard.Balance} рублей";
                            }
                            else return;

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ex.Message}", $"Окно изменения лимита депозита (запрос) ({windowError})");
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //-------------------------[Кнопка "Закрыть карту (запрос)" (боковое меню)]----------------------------//
        private void CloseCardSideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Скрываем ненужные поля
                PayTxt.Visible = false;
                PayButton.Visible = false;

                ReplenishBalanceTxt.Visible = false;
                ReplenishBalanceButton.Visible = false;

                ReplenishDepositButton.Visible = false;

                Amount.Visible = false;
                ChangeLimitDepositTxt1.Visible = false;
                ChangeLimitDepositTxt2.Visible = false;
                ChangeLimitDepositButton.Visible = false;

                OpenCardButton.Visible = false;

                // Отображаем нужные поля
                Description.Visible = true;
                CloseAndOpenTxt.Visible = true;
                CloseCardButton.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Отправить" (закрыть карту)]----------------------------//
        private async void CloseCardButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (def)
                {
                    case "U0":
                    case "U1":
                    case "U2":
                        {
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.UsuallyCard[id0];
                            var preview = CurrentCard.PrepareCloseCard("U", Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку - Закрыть карту номер: {CurrentCard.CardNumber}?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Также Вы не сможете пользоваться картой, она будет заморожена.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestCloseCard(preview);

                                Close();

                                MessageBox.Show($"Вы успешно отправили запрос на закрытие карты.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");

                                CurrentCard.Status = 0;
                                CurrentCard.Reason = $"Заявка - Закрыть карту";
                            }
                            else return;

                            break;
                        }
                    case "D0":
                    case "D1":
                    case "D2":
                        {
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.DebitCard[id0];
                            var preview = CurrentCard.PrepareCloseCard("D", Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку - Закрыть карту номер: {CurrentCard.CardNumber}?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Также Вы не сможете пользоваться картой, она будет заморожена.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestCloseCard(preview);

                                Close();

                                MessageBox.Show($"Вы успешно отправили запрос на закрытие карты.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");

                                CurrentCard.Status = 0;
                                CurrentCard.Reason = $"Заявка - Закрыть карту";
                            }
                            else return;

                            break;
                        }
                    case "C0":
                    case "C1":
                    case "C2":
                        {
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.CreditCard[id0];
                            var preview = CurrentCard.PrepareCloseCard("C", Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку - Закрыть карту номер: {CurrentCard.CardNumber}?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Также Вы не сможете пользоваться картой, она будет заморожена.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestCloseCard(preview);

                                Close();

                                MessageBox.Show($"Вы успешно отправили запрос на закрытие карты.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");

                                CurrentCard.Status = 0;
                                CurrentCard.Reason = $"Заявка - Закрыть карту";
                            }
                            else return;

                            break;
                        }
                    case "B0":
                    case "B1":
                    case "B2":
                        {
                            TryReg.TryString1000(Description.Text);

                            var CurrentCard = CurrentClient.BusinessCard[id0];
                            var preview = CurrentCard.PrepareCloseCard("B", Description.Text);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку - Закрыть карту номер:: {CurrentCard.CardNumber}? " +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!" +
                                                                  $"Также Вы не сможете пользоваться картой, она будет заморожена.",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestCloseCard(preview);

                                Close();

                                MessageBox.Show($"Вы успешно отправили запрос на закрытие карты.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");

                                CurrentCard.Status = 0;
                                CurrentCard.Reason = $"Заявка - Закрыть карту";
                            }
                            else return;

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ex.Message}", $"Окно изменения лимита депозита (запрос)");
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //-------------------------[Кнопка "Открыть карту (запрос)" (боковое меню)]----------------------------//
        private async void OpenCardSideButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Скрываем ненужные поля
                PayTxt.Visible = false;
                PayButton.Visible = false;

                ReplenishBalanceTxt.Visible = false;
                ReplenishBalanceButton.Visible = false;

                ReplenishDepositButton.Visible = false;

                Amount.Visible = false;
                ChangeLimitDepositTxt1.Visible = false;
                ChangeLimitDepositTxt2.Visible = false;
                ChangeLimitDepositButton.Visible = false;

                CloseCardButton.Visible = false;

                // Отображаем нужные поля
                Description.Visible = true;
                CloseAndOpenTxt.Visible = true;
                OpenCardButton.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ex}", $"Окно 'Открыть карту (запрос)'");
            }
        }

        //-------------------------[Кнопка "Отправить" (открыть карту)]----------------------------//
        private async void OpenCardButton_Click(object sender, EventArgs e)
        {
            // Для лога (указывает для какого типа карт произошла ошибка)
            string windowError = "";

            try
            {
                switch (def)
                {
                    case "U0":
                    case "U1":
                    case "U2":
                        {
                            windowError = "Обычная карта";

                            TryReg.TryString1000(Description.Text);

                            int countUsually = CurrentClient.UsuallyCard.Count;
                            var CurrentCard = CurrentClient.UsuallyCard[id0];

                            var preview = CurrentCard.PrepareOpenCard("U", Description.Text, countUsually);

                            DialogResult result = MessageBox.Show($"Вы действительно хотите отправить заявку - Открыть карту, тип: Обычная?" +
                                                                  $"Учтите, после подтверждения, отменить заявку будет нельзя!",
                                                                  "Изменение лимита депозита", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                            if (result == DialogResult.Yes)
                            {
                                Enabled = false;
                                Cursor = Cursors.WaitCursor;

                                await CurrentCard.RequestOpenCard(preview);

                                MessageBox.Show($"Вы успешно отправили запрос на открытие карты.\n" +
                                                $"Ожидайте рассмотрения заявки в течение 72 часов.");
                            }
                            else return;
                            break;
                        }
                    case "D0":
                    case "D1":
                    case "D2":
                        {

                            break;
                        }
                    case "C0":
                    case "C1":
                    case "C2":
                        {

                            break;
                        }
                    case "B0":
                    case "B1":
                    case "B2":
                        {

                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Отправляем сообщение об ошибке оплате в лог
                await DataBase.AddLogs(Session.CurrentClient.Id, "ERROR", $"{ex}", $"Окно изменения лимита депозита (запрос) ({windowError})");
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }
    }
}
