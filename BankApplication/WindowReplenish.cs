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
        private readonly IBankCard currentCard;                 // Текущая карта
        private readonly Client currentClient;                  // Клиент
        private readonly List<ISource> source = [];             // Список всех доступных источников
        private ISource _source;                                // Выбранный источник


        public WindowReplenish(IBankCard currentCard)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentCard = currentCard;
            currentClient = Session.CurrentClient;

            foreach (ISource source in currentClient.Cards)
            {
                if (source == currentCard) continue;
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
            _source = (ISource)BoxSource.SelectedItem;

        }
        #endregion

        #region Пополнить
        private async void ReplenishButton_Click(object sender, EventArgs e)
        {
            try
            {
                decimal amount = TryReg.TryDecimal(Amount.Text);

                var preview = currentCard.PrepareReplenishBalance(amount, _source);

                DialogResult result = MessageBox.Show($"Вы действительно хотите пополнить баланс карты {currentCard.Name} с карты {_source.NameSource} на сумму {amount} рублей?",
                                                      "Пополнение баланса", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    await currentCard.ReplenishBalance(preview);

                    MessageBox.Show($"Вы успешнно пополнили баланс карты {currentCard.Name} с карты {_source.NameSource} на сумму {amount} рублей.",
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
