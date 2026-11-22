using Microsoft.Extensions.DependencyInjection;
using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static BankApplication.DataBase;

namespace BankApplication
{
    public partial class WindowPay : MetroForm
    {
        private readonly ISource currentSource;                                     // Источник
        private List<ServiceProviders> AllServices { get; set; } = [];                   // Сервисы для оплаты + их реквизиты
        private ServiceProviders SelectedService;                                   // Выбранная организация
        private Stack<int?> NavigationHistory = new();                              // История переходов для кнопки "Назад"
        private int? CurrentKeyDraw { get; set; }                              // Текущий уровень (null = главный экран категорий)
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
            AllServices = await FindServiceProviders();
            RenderCategoryLevel(null);
        }
        #endregion

        private void RenderCategoryLevel(int? keyDraw)
        {
            CurrentKeyDraw = keyDraw;

            // Безопасно уничтожаем старые кнопки перед созданием новых
            while (PanelService.Controls.Count > 0) PanelService.Controls[0].Dispose();

            var currentItem = AllServices.Where(elem => elem.KeyDraw == keyDraw).ToList();

            foreach (var item in currentItem)
            {
                var button = new ReaLTaiizor.Controls.Button()
                {
                    BackColor = Color.Transparent,
                    BorderColor = Color.FromArgb(32, 34, 37),
                    EnteredColor = Color.FromArgb(32, 34, 37),
                    EnteredBorderColor = Color.Tan,
                    PressedBorderColor = Color.Tan,
                    PressedColor = Color.Tan,

                    Size = new Size(300, 80),
                    Text = item.ServiceName,
                    Font = new Font("Segoe UI", 20f),

                    Tag = item,         // Прячем весь объект ServiceProviders внутрь кнопки
                };

                button.Click += CategoryItem_Click;
                PanelService.Controls.Add(button);
            }
        }

        private void CategoryItem_Click(object sender, EventArgs e)
        {
            var button = (Control)sender;
            var clickedItem = (ServiceProviders)button.Tag;

            if (clickedItem.PaymentAccount == null)
            {
                NavigationHistory.Push(CurrentKeyDraw);

                RenderCategoryLevel(clickedItem.Id);
            }
            else
            {
                SelectedService = clickedItem;
                PanelService.Visible = false;
                PanelPay.Visible = true;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            if (PanelPay.Visible)
            {                
                PanelPay.Visible = false;

                RenderCategoryLevel(CurrentKeyDraw);
                PanelService.Visible = true;
                return;
            }

            if (NavigationHistory.Count > 0) RenderCategoryLevel(NavigationHistory.Pop());
        }



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
                //var currentRequisites = serviceProviders.paymentAccount[choiseIndexService];
                //decimal amount = TryReg.TryDecimal(Amount.Text);
                //var preview = currentSource.PreparePay(amount, target);

                //string nl = Environment.NewLine;
                //string requisites = $"Способ оплаты: {currentSource.NameSource}{nl}" +
                //                    $"Сумма оплаты: {amount} рублей{nl}{nl}" +
                //                    $"Реквизиты:{nl}" +
                //                    $"Номер счета: {currentRequisites.AccountNumber}{nl}" +
                //                    $"БИК: {currentRequisites.Bic}{nl}" +
                //                    $"Кор. счет: {currentRequisites.CorAccount}";

                //string message = preview switch
                //{
                //    DebitCardPreparePay debit => debit.CashBackStatus switch
                //    {
                //        CashBackStatus.NormalCashback => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей? Ваш кешбек составит {debit.CashbackAmount} рублей.",
                //        CashBackStatus.LimitExceede => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей? Учтите, после оплаты Вы достигнете лимита по кешбеку. Часть, которая не войдет в лимит, сгорит!",
                //        _ => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей?{nl}Кешбек по данной операции начислен не будет."
                //    },

                //    _ => $"{requisites}{nl}{nl}Вы дейтвительно хотите оплатить {amount} рублей?"
                //};

                //DialogResult result = MessageBox.Show(message, "Подтверждение оплаты", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                //if (result == DialogResult.Yes)
                //{                    
                //    await currentSource.Pay(preview);

                //    string payMessage = $"Вы успешно оплатили {amount} рублей.";

                //    MessageBox.Show(payMessage, "Оплата", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    Close();
                //}
                //else return;                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
