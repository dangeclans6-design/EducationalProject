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
        private PaymentCategories CurrentCategories { get; set; }                   // Текущаю категория
        private Stack<int> History { get; set; } = [];                              // История 


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

        #region Загрузка формы
        private async void WindowPay_Load(object sender, EventArgs e)
        {
            PayCatalog = await FindPayCatalogAndPayment();

            var categories = PayCatalog.Where(elem => elem is PayCategories)?.ToList();             // Выбрать из всего каталога только категории (знак ? - на тот случай если окажется пустым)

            int walk = 30;
            foreach (var category in categories)
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
                    Text = category.Name,
                    Font = new Font("Segoe UI", 18f),

                    Tag = category
                };

                PanelCategories.Controls.Add(button);
                button.Location = new Point(XPanelCategories / 2 - button.Size.Width / 2, walk);
                button.Click += Category_Click;

                walk += 110;
            }
        }
        #endregion

        private async void Category_Click(object sender, EventArgs e)
        {
            var currentControl = (Control)sender;
            var currentCategory = (IPayCatalog)currentControl.Tag ?? throw new Exception("Непредвиденная ошибка. повторите попытку позже.");

            if (currentCategory is PayCategories)
            {
                CurrentCatalog = PayCatalog.OfType<PayCatalog>().Where(elem => (elem.CategoryType == currentCategory.CategoryType)).ToList();
                var currentKeyDraw = CurrentCatalog.Where(elem => elem.KeyDraw == 0).ToList();

                while (PanelCategories.Controls.Count > 0) PanelCategories.Controls[0].Dispose();

                int walk = 30;
                foreach (var catalog in currentKeyDraw)
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
                        Text = catalog.Name,
                        Font = new Font("Segoe UI", 18f),

                        Tag = catalog
                    };

                    PanelCategories.Controls.Add(button);
                    button.Location = new Point(XPanelCategories / 2 - button.Size.Width / 2, walk);
                    button.Click += Category_Click;

                    walk += 110;
                }
            }
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
                             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion
    }
}
