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
using static System.Windows.Forms.DataFormats;

namespace BankApplication
{
    public partial class ClientMenu : MetroForm
    {
        private readonly Client currentClient;
        public ClientMenu()
        {
            InitializeComponent();
            DoubleBuffered = true;

            currentClient = Session.CurrentClient;

            PanelCards.Controls.Clear();

            int y_card = 10;
            int y_payment = 10;
            foreach (IBankCard card in currentClient.Cards)
            {
                CardItem item = new(card);

                PanelCards.Controls.Add(item);
                item.Location = new Point((PanelCards.Width - item.Width) / 2, y_card);

                item.OnReplenishButton += OpenReplenishWindow;
                item.OnPayButton += OpenPayhWindow;

                y_card += item.Height + 30;
            }

            foreach (PaymentAccount paymant in currentClient.PaymentAccounts)
            {
                CardItem item = new(paymant);

                PanelPayment.Controls.Add(item);
                item.Location = new Point((PanelCards.Width - item.Width) / 2, y_payment);

                item.OnReplenishButton += OpenReplenishWindow;
                item.OnPayButton += OpenPayhWindow;

                y_payment += item.Height + 30;
            }
        }


        private void OpenReplenishWindow(ITarget currentTarget)
        {
            WindowReplenish wr = new(currentTarget);

            wr.ShowDialog();

            UpdateList();
        }
        private void OpenPayhWindow(ISource currentSource)
        {
            WindowPay wr = new(currentSource);

            wr.ShowDialog();

            UpdateList();
        }


        //---------------------[Кнопка "Закрыть форму"]-----------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //---------------------[Кнопка "Обновить страницу"]-----------------------//
        private async void UpdatePageClientMenu_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #region Нажатие на уведомление
        private void NotificationsList_Click(object sender, EventArgs e)
        {
            // Распознаем, какая именно кнопка вызвала это событие
            var clickedButton = (Control)sender;

            // Достаем из её свойства Tag данные запроса (i-тый элемент в списке), который мы туда положили в цикле
            var (index, currentNoti) = ((int, Notifications))clickedButton.Tag;

            ShowNoti notification = new(index, currentNoti);

            notification.ShowDialog();
        }
        #endregion

        //----------------------------[Кнопка "Контроль"]-------------------------------//
        private void dataClient_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{currentClient.Id}\n{currentClient.ClientType}\n{currentClient.Login}\n{currentClient.Password}\n{currentClient.Name1}\n" +
                            $"{currentClient.Name2}\n{currentClient.Name3}\n{currentClient.DateBirth}\n{currentClient.NumberPhone}\n" +
                            $"{currentClient.Mail}\n{currentClient.CreateAt}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #region Обновление спика карт / счетов UpdateList()
        private void UpdateList()
        {
            PanelCards.Controls.Clear();
            PanelPayment.Controls.Clear();

            int y = 10;
            int y_payment = 10;
            foreach (IBankCard card in currentClient.Cards)
            {
                CardItem item = new(card);

                PanelCards.Controls.Add(item);
                item.Location = new Point((PanelCards.Width - item.Width) / 2, y);

                item.OnReplenishButton += OpenReplenishWindow;
                item.OnPayButton += OpenPayhWindow;

                y += item.Height + 30;
            }
            foreach (PaymentAccount paymant in currentClient.PaymentAccounts)
            {
                CardItem item = new(paymant);

                PanelPayment.Controls.Add(item);
                item.Location = new Point((PanelCards.Width - item.Width) / 2, y_payment);

                item.OnReplenishButton += OpenReplenishWindow;
                item.OnPayButton += OpenPayhWindow;

                y_payment += item.Height + 30;
            }
        }
        #endregion
    }
}
