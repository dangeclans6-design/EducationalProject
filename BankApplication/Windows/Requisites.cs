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
                IBankCard card => $"Реквизиты карты: {card.CardNumber}",
                PaymentAccount payment => $"Реквизиты счета: {payment.AccountNumber}",
                _ => "Реквизиты"
            };
        }

        #region Загрузка формы
        private void Requisites_Load(object sender, EventArgs e)
        {

        }
        #endregion
    }
}
