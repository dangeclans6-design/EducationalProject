using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankApplication
{
    public partial class MyRequest : MetroForm
    {
        readonly RequestAdmin currentMyRequest;
        readonly int index;

        public MyRequest(int index, RequestAdmin currentMyRequest)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentMyRequest = currentMyRequest;
            this.index = index;

            LabelForm.Text = $"Запрос #{currentMyRequest.Id}";

            if (currentMyRequest.Type == 1)
            {
                Text1.Text = $"Изменение депозита (Тип: {currentMyRequest.TypeCard})";
                Text2.Text = $"{currentMyRequest.DefCard} (ID карты: {currentMyRequest.IdCard})";
                Text3.Text = $"{currentMyRequest.NewDeposit}";
                Text4.Text = $"{currentMyRequest.ClientId}";
                Text5.Text = $"{currentMyRequest.Message}";
                Text6.Text = $"{currentMyRequest.Timestamp}";

                GoRequestButton.Visible = true;
                NumberCardButton.Visible = false;
            }
            if (currentMyRequest.Type == 2)
            {
                Text1.Text = $"Закрыть карту (Тип: {currentMyRequest.TypeCard})";
                Text2.Text = $"{currentMyRequest.DefCard} (ID карты: {currentMyRequest.IdCard})";
                Text3.Text = $"-";
                Text4.Text = $"{currentMyRequest.ClientId}";
                Text5.Text = $"{currentMyRequest.Message}";
                Text6.Text = $"{currentMyRequest.Timestamp}";

                GoRequestButton.Visible = true;
                NumberCardButton.Visible = false;
            }
            if (currentMyRequest.Type == 3)
            {
                Text1.Text = $"Открыть карту (Тип: {currentMyRequest.TypeCard})";
                Text2.Text = $"-";
                Text3.Text = $"-";
                Text4.Text = $"{currentMyRequest.ClientId}";
                Text5.Text = $"{currentMyRequest.Message}";
                Text6.Text = $"{currentMyRequest.Timestamp}";

                GoRequestButton.Visible = false;
                NumberCardButton.Visible = true;
            }
        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //--------------------[Кнопка "Отправить"]---------------------//
        private async void GoRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                string comment = CommentText.Text.Trim();
                var CurrentAdmin = Session.CurrentAdmin;
                string answer = "";

                if (FalseCheck.Checked || TrueCheck.Checked)
                {
                    if (TrueCheck.Checked) answer = "Одобрено";
                    else answer = "Отказано";

                    TryReg.TryString300(comment);

                    DialogResult result = MessageBox.Show($"Вы действительно готовы дать окончательный ответ клиенту?" +
                                                          $"Учтите, после подтверждения, отменить это действие нельзя." +
                                                          $"Клиент получит уведомление и, в случае несогласия, подать жалобу на Вас.",
                                                          "Отправить ответ на запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Enabled = false;
                        Cursor = Cursors.WaitCursor;

                        await CurrentAdmin.AnswerRequest(currentMyRequest.ClientId, currentMyRequest.Id, answer, comment, index,
                            currentMyRequest.Type, currentMyRequest.NewDeposit, currentMyRequest.IdCard, currentMyRequest.TypeCard, Text7.Text);

                        Close();
                        MessageBox.Show($"Вы успешно отправили ответ клиенту", "Ответ на запрос клиента", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Отправляем лог
                        await DataBase.AddLogsAdmin(CurrentAdmin.Id, "INFO", $"Дал ответ на запрос #{currentMyRequest.Id}.", $"MyRequest");
                    }
                    else return;
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await DataBase.AddLogsAdmin(Session.CurrentAdmin.Id, "INFO", $"{ex}", $"AuthAdmin");
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        //--------------------[Кнопка "Отказано"]---------------------//
        private void FalseCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (FalseCheck.Checked) TrueCheck.Enabled = false;
            else TrueCheck.Enabled = true;
        }

        //--------------------[Кнопка "Одобрено"]---------------------//
        private void TrueCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TrueCheck.Checked) FalseCheck.Enabled = false;
            else FalseCheck.Enabled = true;
        }

        //--------------------[Кнопка "Номер карты"]---------------------//
        private void NumberCardButton_Click(object sender, EventArgs e)
        {
            string numberCard = TryReg.GenerateCardNumber(currentMyRequest.ClientId);

            Text7.Text = numberCard;

            GoRequestButton.Visible = true;
        }
    }
}
