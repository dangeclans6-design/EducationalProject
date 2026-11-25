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
        private ISource CurrentSource { get; }                                      // Источник
        private List<IPayCatalog> PayCatalog { get; set; } = null;                    // Весь каталог
        private List<PayCatalog> CurrentCatalog { get; set; } = null;                 // Текущий каталог (по выбранной категории)
        private PaymentCategories? CurrentCategories { get; set; }                   // Текущаю категория
        private Stack<int> History { get; set; } = [];                              // История
        private int CurrentKeyDraw { get; set; }


        private int XPanelCategories { get; set; }
        private int YPanelCategories { get; set; }

        public WindowPay(ISource currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            CurrentSource = currentSource;

            XPanelCategories = PanelCategories.Width;
            YPanelCategories = PanelCategories.Height;
        }

        #region Отрисовка кнопок
        private void DrawButtons(IEnumerable<IPayCatalog> item)
        {
            while (PanelCategories.Controls.Count > 0) PanelCategories.Controls[0]?.Dispose();

            int walk = 30;
            foreach (var items in item)
            {
                var button = new ReaLTaiizor.Controls.Button()
                {
                    BackColor = Color.Transparent,
                    BorderColor = Color.FromArgb(32, 34, 37),
                    EnteredColor = Color.FromArgb(15, 15, 15),
                    EnteredBorderColor = Color.FromArgb(32, 34, 37),
                    PressedBorderColor = Color.FromArgb(32, 34, 37),
                    PressedColor = Color.FromArgb(26, 26, 26),

                    Size = new Size(400, 80),
                    Text = items.Name,
                    Font = new Font("Segoe UI", 18f),

                    Tag = items     // "Кладем" данные в кнопку
                };

                PanelCategories.Controls.Add(button);
                button.Location = new Point(XPanelCategories / 2 - button.Size.Width / 2, walk);
                button.Click += Category_Click;

                walk += 110;
            }
        }
        #endregion

        #region Загрузка формы
        private async void WindowPay_Load(object sender, EventArgs e)
        {
            PayCatalog = await FindPayCatalogAndPayment();
            var categories = PayCatalog.OfType<PayCategories>()?.ToList();

            DrawButtons(categories);
        }
        #endregion

        private async void Category_Click(object sender, EventArgs e)
        {
            var currentControl = (Control)sender;
            var currentItem = (IPayCatalog)currentControl.Tag ?? throw new Exception("Непредвиденная ошибка. повторите попытку позже.");            

            // Если кнопка - это категория
            if (currentItem is PayCategories)
            {
                CurrentCatalog = PayCatalog.OfType<PayCatalog>().Where(elem => (elem.CategoryType == currentItem.CategoryType))?.ToList();

                CurrentCategories = currentItem.CategoryType;

                var catalog = CurrentCatalog.Where(elem => elem.KeyDraw == 0)?.ToList();
                CurrentKeyDraw = 0;

                DrawButtons(catalog);

                BackButton.Visible = true;
            }
            // Если кнопка - это каталог
            else if (currentItem is PayCatalog)
            {
                var catalog = CurrentCatalog.Where(elem => elem.KeyDraw == currentItem.Id)?.ToList();

                History.Push(CurrentKeyDraw);
                CurrentKeyDraw = currentItem.Id;

                DrawButtons(catalog);
            }
        }

        #region Назад
        private void BackButton_Click(object sender, EventArgs e)
        {
            while (PanelCategories.Controls.Count > 0) PanelCategories.Controls[0]?.Dispose();

            if (CurrentCategories != null && History.Count == 0)
            {                
                var categories = PayCatalog.OfType<PayCategories>()?.ToList();

                // Отрисовывем категории
                DrawButtons(categories);

                BackButton.Visible = false;
            }

            else if (CurrentCategories != null && History.Count > 0)
            {
                var newKeyDraw = History?.Pop();

                var catalog = CurrentCatalog.Where(elem => elem.KeyDraw == newKeyDraw)?.ToList();

                DrawButtons(catalog);
            }
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

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
