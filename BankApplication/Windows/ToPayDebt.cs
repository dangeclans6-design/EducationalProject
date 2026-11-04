using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class ToPayDebt : Form
    {
        //---------[Для суммы]----------------//
        ToolTip payTip = new ToolTip {  };
        string messege_pay = "- Введите сумму до 1.000.000 рублей";

        private readonly int id0;
        private readonly Client currentClient;
        public ToPayDebt(int id0)
        {
            InitializeComponent();

            currentClient = Session.CurrentClient; // Копируем адресс в памяти

            debt_info.Items.Add($"{currentClient.CreditCard[id0].Debt}");

            this.id0 = id0;
        }
        //-----------------------[При закрытии формы]-------------------------------//
        private void ToPayDebt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        //-----------------------[Подсказка]-------------------------------//
        private void replenish_amount_Enter(object sender, EventArgs e) { payTip.Show(messege_pay, debt_pay_amount, 0, debt_pay_amount.Height); }

        private void debt_pay_amount_Leave(object sender, EventArgs e) { payTip.Hide(debt_pay_amount); }

        //----------------------[Кнопка "Назад"]-----------------------//
        private void back_Click(object sender, EventArgs e)
        {

        }
        //----------------------[Кнопка "Оплатить"]-----------------------//
        private void pay_Click(object sender, EventArgs e)
        {
            try
            {
                var currentCard = currentClient.CreditCard[id0];
                var preview = currentCard.PrepareToPayDebt(debt_pay_amount.Value);

                DialogResult result = MessageBox.Show($"Вы действительно хотите погасить задолженность на {preview.amount} рублей?",
                                                      "Погашение задолженности", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    currentCard.ToPayDebt(preview);
                    DataBase.ToPayDebt(currentCard.Id, currentCard.Debt);

                    MessageBox.Show($"Вы успешно погасили задолженность на {preview.amount} рублей.\n" +
                                    $"Ваша задолженность: {currentCard.Debt} рублей.");
                }
                else return;

                debt_info.Items.Clear();
                debt_info.Items.Add($"{currentCard.Debt}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
