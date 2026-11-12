using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace BankApplication
{
    public partial class MyRequest : MetroForm
    {

        public MyRequest(int index, RequestAdmin currentMyRequest)
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        //--------------------[Кнопка "Закрыть"]---------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        //--------------------[Кнопка "Отправить"]---------------------//
        private async void GoRequestButton_Click(object sender, EventArgs e)
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

        //--------------------[Кнопка "Отказано"]---------------------//
        private void FalseCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (FalseCheck.Checked) TrueCheck.Enabled = false;
            else TrueCheck.Enabled = true;
        }

        //--------------------[Кнопка "Одобрено"]---------------------//
        private void TrueCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TrueCheck.Checked) FalseCheck.Enabled = false;
            else FalseCheck.Enabled = true;
        }

        //--------------------[Кнопка "Номер карты"]---------------------//
        private void NumberCardButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
