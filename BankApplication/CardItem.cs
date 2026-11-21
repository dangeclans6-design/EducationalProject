using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ReaLTaiizor.Controls;

namespace BankApplication
{
    public partial class CardItem : UserControl
    {
        private ITarget CurrentTarget { get; }
        private List<string> NameButtonForCard { get; } = ["Пополнить", "Оплатить", "Перевести", "Заблокировать"];
        private List<string> NameButtonForPayment { get; } = ["Пополнить", "Оплатить", "Перевести", "Реквизиты","Заблокировать"];


        public CardItem(ITarget currentTarget)
        {
            InitializeComponent();

            CurrentTarget = currentTarget;

            int width = Size.Width;
            int height = Size.Height;            

            #region Отрисовка кнопок для карт
            if (currentTarget is IBankCard card)
            {
                NameTarget.Text = "Карта";
                NumberTarget.Text = $"Номер: {card.CardNumber}";
                BalanceTarget.Text = $"{card.Balance} ₽";
                StatusTarget.Text = $"{card.Status}";

                if (card.Status == CardStatus.Active) StatusTarget.ForeColor = Color.Green;
                if (card.Status == CardStatus.Frozen) StatusTarget.ForeColor = Color.Tan;
                if (card.Status == CardStatus.Closed) StatusTarget.ForeColor = Color.Red;

                int walk = 10;
                foreach (var nameButton in NameButtonForCard)
                {
                    var button = new ReaLTaiizor.Controls.Button()
                    {
                        BackColor = Color.Transparent,
                        BorderColor = Color.FromArgb(32, 34, 37),
                        EnteredColor = Color.FromArgb(32, 34, 37),
                        EnteredBorderColor = nameButton switch
                        {
                            "Заблокировать" => Color.Maroon,
                            "Реквизиты" => Color.LightGray,
                            _ => Color.Tan
                        },
                        PressedBorderColor = nameButton switch
                        {
                            "Заблокировать" => Color.Maroon,
                            "Реквизиты" => Color.LightGray,
                            _ => Color.Tan
                        },
                        PressedColor = nameButton switch
                        {
                            "Заблокировать" => Color.Maroon,
                            "Реквизиты" => Color.LightGray,
                            _ => Color.Tan
                        },

                        Size = new Size(103, 30),
                        Text = nameButton,
                        Font = new Font("Segoe UI", 10f),

                        Tag = nameButton,
                    };

                    button.Location = new Point(walk, height - button.Size.Height - 10);
                    button.Click += CardItemButton_Click;

                    MainPanel.Controls.Add(button);
                    walk += button.Size.Width + 10; // Шаг кнопки 
                }

            }
            #endregion

            #region Отрисовка кнопок для счетов
            else if (currentTarget is PaymentAccount payment)
            {
                NameTarget.Text = "Расчетный счет";
                NumberTarget.Text = $"Номер: {payment.AccountNumber}";
                BalanceTarget.Text = $"{payment.Balance} ₽";
                StatusTarget.Text = $"{payment.Status}";

                if (payment.Status == CardStatus.Active) StatusTarget.ForeColor = Color.Green;
                if (payment.Status == CardStatus.Frozen) StatusTarget.ForeColor = Color.Tan;
                if (payment.Status == CardStatus.Closed) StatusTarget.ForeColor = Color.Red;

                int walk = 5;
                foreach (string nameButton in NameButtonForPayment)
                {
                    var button = new ReaLTaiizor.Controls.Button()
                    {
                        BackColor = Color.Transparent,
                        BorderColor = Color.FromArgb(32, 34, 37),
                        EnteredColor = Color.FromArgb(32, 34, 37),
                        EnteredBorderColor = nameButton switch
                        {
                            "Заблокировать" => Color.Maroon,
                            "Реквизиты" => Color.LightGray,
                            _ => Color.Tan
                        },
                        PressedBorderColor = nameButton switch
                        {
                            "Заблокировать" => Color.Maroon,
                            "Реквизиты" => Color.LightGray,
                            _ => Color.Tan
                        },
                        PressedColor = nameButton switch
                        {
                            "Заблокировать" => Color.Maroon,
                            "Реквизиты" => Color.LightGray,
                            _ => Color.Tan
                        },

                        Size = new Size(85, 30), // Размер кнопки
                        Text = nameButton,
                        Font = new Font("Segoe UI", 8f),

                        Tag = nameButton,
                    };

                    button.Location = new Point(walk, height - button.Size.Height - 10);
                    button.Click += CardItemButton_Click;

                    MainPanel.Controls.Add(button);
                    walk += button.Size.Width + 5; // Шаг кнопки 
                }
            }
            #endregion
        }

        #region Делегаты
        public event Action<ITarget> OnReplenishButton;
        public event Action<ISource> OnPayButton;
        public event Action<ITransferable> OnTransferButton;
        public event Action<ISource> OnRequisitesButton;
        #endregion

        private void CardItemButton_Click(object sender, EventArgs e)
        {
            var clickButton = (Control)sender;
            var nameButton = clickButton.Tag?.ToString();

            if (CurrentTarget is IBankCard card)
            {
                if (card.Status != CardStatus.Active) throw new Exception($"Карта на данный момент недоступна. Причина: {card.Reason}.");

                if (nameButton == "Пополнить") OnReplenishButton?.Invoke(card);
                else if (nameButton == "Оплатить") OnPayButton?.Invoke(card);
                else if (nameButton == "Перевести") OnTransferButton?.Invoke(card);
                else if (nameButton == "Заблокировать") MessageBox.Show("Пока пусто");

            }
            else if (CurrentTarget is PaymentAccount payment)
            {
                if (payment.Status != CardStatus.Active) throw new Exception($"Счет на данный момент недоступен. Причина: {payment.Reason}.");

                if (nameButton == "Пополнить") OnReplenishButton?.Invoke(payment);
                else if (nameButton == "Оплатить") OnPayButton?.Invoke(payment);
                else if (nameButton == "Перевести") OnTransferButton?.Invoke(payment);
                else if (nameButton == "Реквизиты") OnRequisitesButton?.Invoke(payment);
                else if (nameButton == "Заблокировать") MessageBox.Show("Пока пусто");
            }
        }
    }
}
