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
    public partial class WindowReplenish : MetroForm
    {
        private readonly ITarget currentTarget;                 // Текущая цель (куда пополняются деньги)
        private readonly Client currentClient;                  // Клиент
        private readonly List<ISource> source = [];             // Список всех доступных источников
        private ISource choiseSource;                           // Выбранный источник


        public WindowReplenish(ITarget currentTarget)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentTarget = currentTarget;
            currentClient = Session.CurrentClient;

            foreach (ISource source in currentClient.Cards)
            {
                if (source == currentTarget) continue;
                else
                {
                    this.source.Add(source);
                }
            }

            foreach (ISource source in currentClient.PaymentAccounts)
            {
                if (source == currentTarget) continue;
                else
                {
                    this.source.Add(source);
                }
            }

            BoxSource.DisplayMember = nameof(ISource.NameSource);
            BoxSource.DataSource = source;
        }

        #region Закрыть окно
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Выбор карты
        private void BoxSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            choiseSource = (ISource)BoxSource.SelectedItem;

        }
        #endregion

        #region Пополнить
        private async void ReplenishButton_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = TryReg.TryDecimal(Amount.Text);

                var preview = currentTarget.PrepareReplenishBalance(amount, choiseSource);

                DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить баланс {currentTarget.NameTarget} с {choiseSource.NameSource} на сумму {amount} рублей?",
                                                      "Пополнение баланса", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    await currentTarget.ReplenishBalance(preview);

                    MessageBox.Show($"Вы успешнно пополнили баланс {currentTarget.NameTarget} с {choiseSource.NameSource} на сумму {amount} рублей.",
                                    "Пополнение баланса", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();
                }
                else return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка пополнения", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
        #endregion
    }
}
