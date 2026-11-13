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
        private ISource currentSource;
        private StructServiceProviders serviceProviders;
        private ITarget target;

        public WindowPay(ISource currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentSource = currentSource;
        }

        private async void WindowPay_Load(object sender, EventArgs e)
        {
            serviceProviders = await FindServiceProviders();

            if (serviceProviders.serviceProviders.Count > 0)
            {
                BoxTarget.DataSource = serviceProviders.serviceProviders;
            }
        }

        private void BoxTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexService = BoxTarget.SelectedIndex;

            target = serviceProviders.paymentAccount[indexService];
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FindData_Click(object sender, EventArgs e)
        {
            decimal paymentPersonal = TryReg.TryInt(PersonalAccount.Text);


        }
    }
}
