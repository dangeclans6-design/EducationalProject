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
    public partial class Requisites : MetroForm
    {
        private ISource Source { get; }


        public Requisites(ISource source)
        {
            InitializeComponent();

            Source = source;

            MainLabel.Text = source switch
            {
                PaymentAccount payment => $"Реквизиты: {payment.NameSource}",
                _ => "Реквизиты"
            };
        }

        #region Загрузка формы
        private void Requisites_Load(object sender, EventArgs e)
        {
            if (Source != null && Source is PaymentAccount paymnet)
            {
                AccountNumber.Text = $"Номер счета: {paymnet.AccountNumber}";
                Bic.Text = $"БИК: {paymnet.Bic}";
                CorAccount.Text = $"Кор. номер: {paymnet.CorAccount}";
                Currency.Text = $"Валюта счета: {paymnet.Currency}";
                Status.Text = $"Статус счета: {paymnet.Status}";

                Reason.Text = paymnet.Status switch
                {
                    CardStatus.Active => String.Empty,
                    _ => $"Причина: {paymnet.Reason}"
                };

                OpenedAt.Text = $"Счет открыт: {paymnet.OpenedAt}";
            }
        }
        #endregion

        #region Закрыть окно
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
