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
    public partial class RequestExplore : MetroForm
    {
        readonly RequestAdmin currentRequest;
        readonly int index;
        public RequestExplore(int index, RequestAdmin currentRequest)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentRequest = currentRequest;
            this.index = index;

            LabelForm.Text = $"Запрос #{currentRequest.Id}";

            if (currentRequest.Type == 1)
            {
                Text1.Text = $"Изменить депозит";
                Text2.Text = $"{currentRequest.DefCard} (Тип карты: {currentRequest.TypeCard}. ID карты: {currentRequest.IdCard})";
                Text3.Text = $"{currentRequest.NewDeposit}";
                Text4.Text = $"{currentRequest.ClientId}";
                Text5.Text = $"{currentRequest.Message}";
                Text6.Text = $"{currentRequest.Timestamp}";
            }
            if (currentRequest.Type == 2)
            {
                Text1.Text = $"Закрыть карту";
                Text2.Text = $"{currentRequest.DefCard} (Тип карты: {currentRequest.TypeCard}. ID карты: {currentRequest.IdCard})";
                Text3.Text = $"-";
                Text4.Text = $"{currentRequest.ClientId}";
                Text5.Text = $"{currentRequest.Message}";
                Text6.Text = $"{currentRequest.Timestamp}";
            }
            if (currentRequest.Type == 3)
            {
                Text1.Text = $"Открыть карту";
                Text2.Text = $"{currentRequest.TypeCard}";
                Text3.Text = $"-";
                Text4.Text = $"{currentRequest.ClientId}";
                Text5.Text = $"{currentRequest.Message}";
                Text6.Text = $"{currentRequest.Timestamp}";
            }


        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //--------------------[Кнопка "Принять запрос"]---------------------//
        private async void ApplyRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show($"Вы действительно хотите принять запрос #{currentRequest.Id}?" +
                                                  $"Учтите, что отменить это действие будет невозможно." +
                                                  $"Если вы захотите отменить это дейстивие, обратитесь к старшему администратору.",
                                                  "Приянть запрос в обработку", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                var CurrentAdmin = Session.CurrentAdmin;

                if (result == DialogResult.Yes)
                {
                    Enabled = false;
                    Cursor = Cursors.WaitCursor;

                    await CurrentAdmin.ApplyRequest(currentRequest.Id, index);

                    MessageBox.Show($"Вы успешно приняли запрос #{currentRequest.Id}.\n" +
                                    $"Он появится в меню 'Мои запросы'.");

                    // Отправляем лог
                    await DataBase.AddLogsAdmin(Session.CurrentAdmin.Id, "INFO", $"Принял запрос #{currentRequest.Id} в обработку.", $"RequestExplore");

                    // Закрываем форму
                    Close();
                }
                else return;
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

        private void RequestExplore_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}
