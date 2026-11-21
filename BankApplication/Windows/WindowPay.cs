using Microsoft.Extensions.DependencyInjection;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BankApplication.DataBase;

namespace BankApplication
{
    public partial class WindowPay : MetroForm
    {
        private readonly ISource currentSource;                                 // Источник (откуда уйдет деньги)
        private List<ServiceProviders> serviceProviders;                        // Сервисы для оплаты + их реквизиты
        private PaymentAccount target;                                          // Счет оплаты (цель) (можно использовать ITarget target, но в логике программы, на данный момент, невозможен иной сценарий)
        private int choiseIndexService;                                         // Индекс выбранной услуги оплаты
        private (int Id, string Adress, decimal DebtAmount)? tupleReader;       // Кортеж с данными по ЛС

        public WindowPay(ISource currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentSource = currentSource;
        }

        #region Загрузка формы
        private async void WindowPay_Load(object sender, EventArgs e)
        {
            serviceProviders = await FindServiceProviders();

            if (serviceProviders.serviceProviders.Count > 0)
            {
                BoxTarget.DataSource = serviceProviders.serviceProviders;
                
                BoxTarget.SelectedIndex = -1;            // Сбрасываем выбор, чтобы поле при открытии оставалось пустым
            }
        }
        #endregion

        private void BoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BoxTarget.SelectedIndex >= 0)
            {
                choiseIndexService = BoxTarget.SelectedIndex;                       // Передаем значение в переменную
                target = serviceProviders.paymentAccount[choiseIndexService];       // Передаем в цель реквизиты выбранной услуги
                FindData.Visible = true;                                            // Разблокируем кнопку найти
            }
            else FindData.Visible = false;                                          // Блокируем кнопку найти
        }

        #region Закрыть
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Найти
        private async void FindData_Click(object sender, EventArgs e)
        {
            try
            {
                TryReg.TryPersonalAccount(PersonalAccount.Text);                

                tupleReader = await FindPersonalAccount(serviceProviders.paymentAccount[choiseIndexService].Id, PersonalAccount.Text.Trim());

                if (tupleReader == null)
                {
                    Txt1.ForeColor = Color.Red;
                    Txt2.Visible = false;

                    Txt1.Text = "Лицевой счет не найден.";

                    PayButton.Visible = false;
                }
                else
                {
                    Txt1.ForeColor = Color.Green;
                    Txt1.Text = $"Адрес: {tupleReader.Value.Adress}";

                    Txt2.ForeColor = Color.Green;
                    Txt2.Text = $"Задолженность: {tupleReader.Value.DebtAmount} рублей";
                    Txt2.Visible = true;

                    Amount.Text = $"{tupleReader.Value.DebtAmount}";

                    PayButton.Visible = true;
                }

                Txt1.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Оплатить
        private async void PayButton_Click(object sender, EventArgs e)
        {
            try
            {
                var currentRequisites = serviceProviders.paymentAccount[choiseIndexService];
                decimal amount = TryReg.TryDecimal(Amount.Text);
                var preview = currentSource.PreparePay(amount, target);

                string nl = Environment.NewLine;
                string requisites = $"Способ оплаты: {currentSource.NameSource}{nl}" +
                                    $"Сумма оплаты: {amount} рублей{nl}{nl}" +
                                    $"Реквизиты:{nl}" +
                                    $"Номер счета: {currentRequisites.AccountNumber}{nl}" +
                                    $"БИК: {currentRequisites.Bic}{nl}" +
                                    $"Кор. счет: {currentRequisites.CorAccount}";

                string message = preview switch
                {
                    DebitCardPreparePay debit => debit.CashBackStatus switch
                    {
                        CashBackStatus.NormalCashback => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей? Ваш кешбек составит {debit.CashbackAmount} рублей.",
                        CashBackStatus.LimitExceede => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей? Учтите, после оплаты Вы достигнете лимита по кешбеку. Часть, которая не войдет в лимит, сгорит!",
                        _ => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей?{nl}Кешбек по данной операции начислен не будет."
                    },

                    _ => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей?"
                };

                DialogResult result = MessageBox.Show(message, "Подтверждение оплаты", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {                    
                    await currentSource.Pay(preview);

                    string payMessage = $"Вы успешно оплатили {amount} рублей.";

                    MessageBox.Show(payMessage, "Оплата", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();
                }
                else return;                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
        #endregion
    }
}
