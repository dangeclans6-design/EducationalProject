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
    public partial class CardItem : UserControl
    {
        private readonly ITarget currentTarget;
        public CardItem(ITarget currentTraget)
        {
            InitializeComponent();

            this.currentTarget = currentTraget;

            if (currentTraget is IBankCard card)
            {
                NameTarget.Text = card.Name;
                NumberTarget.Text = $"Номер: {card.CardNumber}";
                BalanceTarget.Text = $"{card.Balance} ₽";
                StatusTarget.Text = $"{card.Status}";

                if (card.Status == CardStatus.Active) StatusTarget.ForeColor = Color.Green;
                if (card.Status == CardStatus.Frozen) StatusTarget.ForeColor = Color.Tan;
                if (card.Status == CardStatus.Closed) StatusTarget.ForeColor = Color.Red;
            }
            if (currentTraget is PaymentAccount payment)
            {
                NameTarget.Text = payment.NameTarget;
                NumberTarget.Text = $"Номер: {payment.AccountNumber}";
                BalanceTarget.Text = $"{payment.Balance} ₽";
                StatusTarget.Text = $"{payment.Status}";

                if (payment.Status == CardStatus.Active) StatusTarget.ForeColor = Color.Green;
                if (payment.Status == CardStatus.Frozen) StatusTarget.ForeColor = Color.Tan;
                if (payment.Status == CardStatus.Closed) StatusTarget.ForeColor = Color.Red;
            }
        }

        public event Action<ITarget> OnReplenishButton;
        public event Action<ISource> OnPayButton;

        private void ReplenishButton_Click(object sender, EventArgs e)
        {
            if (currentTarget is IBankCard card)
            {
                if (card.Status != CardStatus.Active) MessageBox.Show($"Карта на данный момент недоступна. Причина: {card.Reason}.",
                                                                           "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else OnReplenishButton?.Invoke(currentTarget);
            }
            if (currentTarget is PaymentAccount payment)
            {
                if (payment.Status != CardStatus.Active) MessageBox.Show($"Счет на данный момент недоступен. Причина: {payment.Reason}.",
                                                                         "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else OnReplenishButton?.Invoke(currentTarget);
            }

        }
        private void PayButton_Click(object sender, EventArgs e)
        {
            if (currentTarget is IBankCard card)
            {
                if (card.Status != CardStatus.Active) MessageBox.Show($"Карта на данный момент недоступна. Причина: {card.Reason}.",
                                                                           "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else OnPayButton?.Invoke(card);
            }
            if (currentTarget is PaymentAccount payment)
            {
                if (payment.Status != CardStatus.Active) MessageBox.Show($"Счет на данный момент недоступен. Причина: {payment.Reason}.",
                                                                         "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else OnPayButton?.Invoke(payment);
            }
        }
    }
}
