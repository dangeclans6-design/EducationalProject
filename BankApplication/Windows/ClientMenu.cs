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
        private readonly Client currentClient;
        public ClientMenu()
        {
            InitializeComponent();
            DoubleBuffered = true;

            currentClient = Session.CurrentClient;
        }

        //---------------------[Кнопка "Закрыть форму"]-----------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        //---------------------[Кнопка "Дебетовые" в панели "Мои карты"]-----------------------//
        private async void ShowDebitCardsButton_Click(object sender, EventArgs e)
        {
            try
            {

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

            PanelNotifications.Visible = false;
            PanelNotifications.Controls.Clear();

            ShowNoti notification = new(index, currentNoti);

            notification.ShowDialog();
        }
        #endregion


        #region Кнопка "Отправить" (запрос - Открыть карту)
        private async void GoOpenCardButton_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }
        #endregion

        //----------------------------[Кнопка "Контроль"]-------------------------------//
        private void dataClient_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"{currentClient.Id}\n{currentClient.ClientType}\n{currentClient.Login}\n{currentClient.Password}\n{currentClient.Name1}\n" +
                            $"{currentClient.Name2}\n{currentClient.Name3}\n{currentClient.DateBirth}\n{currentClient.NumberPhone}\n" +
                            $"{currentClient.Mail}\n{currentClient.CreateAt}", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
