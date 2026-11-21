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
        private readonly ISource currentSource;                                     // Источник
        private List<ServiceProviders> AllServices { get; set; } = [];                   // Сервисы для оплаты + их реквизиты
        private ServiceProviders SelectedService;                                   // Выбранная организация
        private Stack<int?> NavigationHistory = new();                              // История переходов для кнопки "Назад"
        private int? CurrentParentId { get; } = null;                               // Текущий уровень (null = главный экран категорий)
        private (int Id, string Adress, decimal DebtAmount)? tupleReader;           // Кортеж с данными по ЛС

        public WindowPay(ISource currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentSource = currentSource;
        }

        #region Загрузка формы
        private async void WindowPay_Load(object sender, EventArgs e)
        {
            AllServices = await FindServiceProviders(); // Ищем все сервисы
        }
        #endregion



        #region Закрыть
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
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
