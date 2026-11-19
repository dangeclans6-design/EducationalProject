using ReaLTaiizor.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BankApplication
{
    public partial class AccountSettingsWindow : MetroForm
    {
        private Client CurrentClient { get; }

        public AccountSettingsWindow()
        {
            InitializeComponent();
            DoubleBuffered = true;

            CurrentClient = Session.CurrentClient;
        }

        #region Загрузка формы
        private void AccountSettingsWindow_Load(object sender, EventArgs e)
        {
            Login.Text = "******";
            Password.Text = "******";
            Mail.Text = "******";
            TypeAccount.Text = CurrentClient.ClientType switch
            {
                ClientType.Individual => "Индивидуальный аккаунт",
                _ => "Бизнес аккаунт"
            };
        }
        #endregion

        private void Login_Click(object sender, EventArgs e)
        {
            Login.Text = Login.Text switch
            {
                "******" => $"{CurrentClient.Login}",
                _ => "******"
            };
        }
        private void Login_MouseEnter(object sender, EventArgs e)
        {
            Login.ForeColor = Color.FromArgb(170, 170, 170);
        }
        private void Login_MouseLeave(object sender, EventArgs e)
        {
            Login.ForeColor = Color.FromArgb(220, 220, 220);
        }

        private void Password_Click(object sender, EventArgs e)
        {
            Password.Text = Password.Text switch
            {
                "******" => $"{CurrentClient.Password}",
                _ => "******"
            };
        }
        private void Password_MouseEnter(object sender, EventArgs e)
        {
            Password.ForeColor = Color.FromArgb(170, 170, 170);
        }
        private void Password_MouseLeave(object sender, EventArgs e)
        {
            Password.ForeColor = Color.FromArgb(220, 220, 220);
        }

        private void Mail_Click(object sender, EventArgs e)
        {
            Mail.Text = Mail.Text switch
            {
                "******" => $"{CurrentClient.Mail}",
                _ => "******"
            };
        }
        private void Mail_MouseEnter(object sender, EventArgs e)
        {
            Mail.ForeColor = Color.FromArgb(170, 170, 170);
        }
        private void Mail_MouseLeave(object sender, EventArgs e)
        {
            Mail.ForeColor = Color.FromArgb(220, 220, 220);
        }


        private void ChangeLoginButton_Click(object sender, EventArgs e)
        {
            NewText.Text = "Изменить логин:";
            NewText.Visible = true;
            
            New.Visible = true;
            New.Tag = ChangeDataClient.Login;
        }
        private void ChangePasswordButton_Click(object sender, EventArgs e)
        {
            NewText.Text = "Изменить пароль:";
            NewText.Visible = true;

            New.Visible = true;
            New.Tag = ChangeDataClient.Password;
        }
        private void ChangeMailButton_Click(object sender, EventArgs e)
        {
            NewText.Text = "Изменить почту:";
            NewText.Visible = true;

            New.Visible = true;
            New.Tag = ChangeDataClient.Mail;
        }

        #region Подтвердить
        private async void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                var preview = await CurrentClient.PrepareChangeDataClient(New.Text, (ChangeDataClient)New.Tag);

                string message = New.Tag switch
                {
                    ChangeDataClient.Login => $"Вы действительно желаете изменить текущий логин ({CurrentClient.Login}) на новый ({New.Text})?)",
                    ChangeDataClient.Password => $"Вы действительно желаете изменить текущий пароль ({CurrentClient.Password}) на новый ({New.Text})?)",
                    _ => $"Вы действительно желаете изменить текущую почту ({CurrentClient.Mail}) на новую ({New.Text})?)"
                };

                DialogResult result = MessageBox.Show(message, "Подтверждение изменений", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    await CurrentClient.FuncChangeDataClient(preview);

                    string messageConfirm = New.Tag switch
                    {
                        ChangeDataClient.Login => $"Вы успешно изменили логин!",
                        ChangeDataClient.Password => $"Вы успешно изменили пароль!",
                        _ => $"Вы успешно изменили почту!"
                    };

                    MessageBox.Show(messageConfirm, "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка авторизации", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }
        #endregion

        #region Закрыть окно
        private void CloseAdmPanel_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion
    }
}
