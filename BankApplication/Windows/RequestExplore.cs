using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class RequestExplore : MetroForm
    {
        public RequestExplore()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //--------------------[Кнопка "Принять запрос"]---------------------//
        private async void ApplyRequestButton_Click(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;
            }
        }

        private void RequestExplore_DoubleClick(object sender, EventArgs e)
        {
            
        }
    }
}
