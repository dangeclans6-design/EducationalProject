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

            NameCard.Text = currentCard.Name;
            NumberCard.Text = $"Номер: {currentCard.CardNumber}";
            BalanceCard.Text = $"{currentCard.Balance} ₽";
            Status.Text = $"{currentCard.Status}";

            if (currentCard.Status == CardStatus.Active) Status.ForeColor = Color.Green;
            if (currentCard.Status == CardStatus.Frozen) Status.ForeColor = Color.Tan;
            if (currentCard.Status == CardStatus.Closed) Status.ForeColor = Color.Red;
        }

        private void ReplenishButton_Click(object sender, EventArgs e)
        {
            if (currentCard.Status != CardStatus.Active) MessageBox.Show($"Карта на данный момент недоступна. Причина: {currentCard.Reason}.",
                                                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                WindowReplenish wr = new(currentCard);

                wr.ShowDialog();
            }            
        }
    }
}
