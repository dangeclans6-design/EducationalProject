using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class ManagerCard : MetroForm
    {
        public ManagerCard()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Size = new Size(1420, 880);
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

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Оплатить"]----------------------------//
        private async void PayButton_Click(object sender, EventArgs e)
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

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //-------------------------[Кнопка "Пополнить баланс"]----------------------------//
        private async void ReplenishBalanceButton_Click(object sender, EventArgs e)
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

        //-------------------------[Кнопка "Закрыть карту (запрос)" (боковое меню)]----------------------------//
        private void CloseCardSideButton_Click(object sender, EventArgs e)
        {
            try
            {

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

        //-------------------------[Кнопка "Отправить" (открыть карту)]----------------------------//
        private async void OpenCardButton_Click(object sender, EventArgs e)
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
    }
}
