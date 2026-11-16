using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class TransferWindow : MetroForm
    {
        private readonly ITransferable CurrentSource;               // Источник
        private readonly Client CurrentClient;                      // Клиент
        private ITarget Target;                                     // Цель перевода
        private decimal AmountTransfer;                             // Сумма перевода
        private bool IsTransferAmount = false;                      // Доуступность перевода


        public TransferWindow(ITransferable currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.CurrentSource = currentSource;
            CurrentClient = Session.CurrentClient;
        }

        #region Загрузка формы
        private void TransferWindow_Load(object sender, EventArgs e)
        {
            List<ITarget> listTarget = [];

            foreach (IBankCard cards in CurrentClient.Cards)
            {
                if (cards == CurrentSource) continue;

                listTarget.Add(cards);
            }

            foreach (PaymentAccount payment in CurrentClient.PaymentAccounts)
            {
                if (payment == CurrentSource) continue;

                listTarget.Add(payment);
            }

            BoxTarget.DisplayMember = nameof(ITarget.NameTarget);
            BoxTarget.DataSource = listTarget;

            BoxTarget.SelectedIndex = -1;
        }
        #endregion

        #region Закрыть
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        private void BoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxTarget.SelectedItem is ITarget target)
            {
                this.Target = target;                                                        // Запоминаем выбранную цель
                InfoTransferText.Text = $"Перевод на: {target.NameTarget}";                  // Выводим снизу название 
            }
            else
            {
                this.Target = null;
                InfoTransferText.Text = String.Empty;
            }

            InfoTransferText.Visible = true;
        }

        private void TransferSelfMainButton_Click(object sender, EventArgs e)
        {
            PanelSelf.Visible = true;
        }

        private void TransferMainButton_Click(object sender, EventArgs e)
        {

        }

        #region Перевести (себе)
        private async void TransferSelfButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsTransferAmount)
                {
                    var preview = CurrentSource.PrepareTransfer(AmountTransfer, Target);
                    var nl = Environment.NewLine;
                    string message = $"Вы действительно хотите перевести {AmountTransfer} рублей?{nl}" +
                                     $"Источник: {CurrentSource.NameSource}{nl}" +
                                     $"Куда будут зачислены: {Target.NameTarget}";

                    DialogResult result = MessageBox.Show(message, "Информация о переводе", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (result == DialogResult.Yes)
                    {
                        await CurrentSource.Transfer(preview);

                        MessageBox.Show($"Вы успешно перевели {AmountTransfer} рублей.", "Успешный перевод", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Close();
                    }
                    else return;                    
                }
                else throw new Exception("Низя");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
        #endregion

        private void Amount_TextChanged(object sender, EventArgs e)
        {
            var resultTryReg = TryReg.TryTransferAmount(Amount.Text);

            InfoText.Text = resultTryReg.Message;
            InfoText.ForeColor = resultTryReg.ColorText;
            InfoText.Visible = true;

            AmountTransfer = resultTryReg.AmountTransfer;
            IsTransferAmount = resultTryReg.IsTransferAmount;

        }
    }
}
