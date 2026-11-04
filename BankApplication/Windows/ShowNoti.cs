using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankApplication.Windows
{
    public partial class ShowNoti : MetroForm
    {
        readonly int index;
        readonly Notifications currentNoti;
        public ShowNoti(int index, Notifications currentNoti)
        {
            InitializeComponent();
            DoubleBuffered = true;

            this.index = index;
            this.currentNoti = currentNoti;

            MainLabel.Text = currentNoti.Title;
            Comment.Text = currentNoti.Message;
            DateAnswer.Text = $"{currentNoti.CreateAt}";
        }

        //--------------------[Кнопка "Закрыть"]--------------------//
        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Text1_Click(object sender, EventArgs e)
        {

        }
    }
}
