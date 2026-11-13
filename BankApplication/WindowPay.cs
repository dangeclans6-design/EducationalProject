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
    public partial class WindowPay : MetroForm
    {
        private ISource currentSource;
        public WindowPay(ISource currentSource)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.currentSource = currentSource;
        }
    }
}
