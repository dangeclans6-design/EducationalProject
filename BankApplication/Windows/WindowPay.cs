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
        private ISource currentSource { get; };                                     // Источник
        private List<IPayCatalog> payCatalog { get; set; } = [];                    // Каталог

        public WindowPay(ISource currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentSource = currentSource;            
        }

        #region Загрузка формы
        private async void WindowPay_Load(object sender, EventArgs e)
        {
            payCatalog = await FindPayCatalogAndPayment();
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
