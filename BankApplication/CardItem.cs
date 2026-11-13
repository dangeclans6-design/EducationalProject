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
        private readonly IBankCard currentCard;
        public CardItem(IBankCard currentCard)
        {
            InitializeComponent();

            this.currentCard = currentCard;

            NameTarget.Text = currentCard.Name;
            NumberTarget.Text = $"Номер: {currentCard.CardNumber}";
            BalanceTarget.Text = $"{currentCard.Balance} ₽";
            StatusTarget.Text = $"{currentCard.Status}";

            if (currentCard.Status == CardStatus.Active) StatusTarget.ForeColor = Color.Green;
            if (currentCard.Status == CardStatus.Frozen) StatusTarget.ForeColor = Color.Tan;
            if (currentCard.Status == CardStatus.Closed) StatusTarget.ForeColor = Color.Red;
        }

        public event Action<IBankCard> OnReplenishButton;

        private void ReplenishButton_Click(object sender, EventArgs e)
        {
            if (currentCard.Status != CardStatus.Active) MessageBox.Show($"Карта на данный момент недоступна. Причина: {currentCard.Reason}.",
                                                                           "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                OnReplenishButton?.Invoke(currentCard);
            }
        }

        private void PayButton_Click(object sender, EventArgs e)
        {

        }
    }
}
