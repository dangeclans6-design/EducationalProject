namespace BankApplication
{
    partial class AccountSettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new System.Windows.Forms.Panel();
            ChangeLoginButton = new ReaLTaiizor.Controls.CyberButton();
            CloseAdmPanel = new ReaLTaiizor.Controls.DungeonButtonLeft();
            MainText = new ReaLTaiizor.Controls.BigLabel();
            Login = new ReaLTaiizor.Controls.BigLabel();
            Password = new ReaLTaiizor.Controls.BigLabel();
            Mail = new ReaLTaiizor.Controls.BigLabel();
            ChangePasswordButton = new ReaLTaiizor.Controls.CyberButton();
            ChangeMailButton = new ReaLTaiizor.Controls.CyberButton();
            TypeAccount = new ReaLTaiizor.Controls.BigLabel();
            LabelMail = new ReaLTaiizor.Controls.BigLabel();
            LabelPassword = new ReaLTaiizor.Controls.BigLabel();
            LabelLogin = new ReaLTaiizor.Controls.BigLabel();
            NewText = new ReaLTaiizor.Controls.BigLabel();
            New = new System.Windows.Forms.TextBox();
            ConfirmButton = new ReaLTaiizor.Controls.CyberButton();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-4, -4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(44, 460);
            panel1.TabIndex = 12;
            // 
            // ChangeLoginButton
            // 
            ChangeLoginButton.Alpha = 20;
            ChangeLoginButton.BackColor = System.Drawing.Color.Transparent;
            ChangeLoginButton.Background = true;
            ChangeLoginButton.Background_WidthPen = 4F;
            ChangeLoginButton.BackgroundPen = true;
            ChangeLoginButton.ColorBackground = System.Drawing.Color.Transparent;
            ChangeLoginButton.ColorBackground_1 = System.Drawing.Color.White;
            ChangeLoginButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ChangeLoginButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            ChangeLoginButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            ChangeLoginButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            ChangeLoginButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ChangeLoginButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            ChangeLoginButton.Effect_1 = true;
            ChangeLoginButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            ChangeLoginButton.Effect_1_Transparency = 25;
            ChangeLoginButton.Effect_2 = true;
            ChangeLoginButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            ChangeLoginButton.Effect_2_Transparency = 20;
            ChangeLoginButton.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ChangeLoginButton.ForeColor = System.Drawing.Color.Tan;
            ChangeLoginButton.Lighting = false;
            ChangeLoginButton.LinearGradient_Background = false;
            ChangeLoginButton.LinearGradientPen = false;
            ChangeLoginButton.Location = new System.Drawing.Point(649, 79);
            ChangeLoginButton.Name = "ChangeLoginButton";
            ChangeLoginButton.PenWidth = 15;
            ChangeLoginButton.Rounding = true;
            ChangeLoginButton.RoundingInt = 20;
            ChangeLoginButton.Size = new System.Drawing.Size(136, 38);
            ChangeLoginButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ChangeLoginButton.TabIndex = 45;
            ChangeLoginButton.Tag = "Cyber";
            ChangeLoginButton.TextButton = "Изменить";
            ChangeLoginButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            ChangeLoginButton.Timer_Effect_1 = 5;
            ChangeLoginButton.Timer_RGB = 300;
            ChangeLoginButton.Click += ChangeLoginButton_Click;
            // 
            // CloseAdmPanel
            // 
            CloseAdmPanel.BackColor = System.Drawing.Color.Transparent;
            CloseAdmPanel.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            CloseAdmPanel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            CloseAdmPanel.Image = null;
            CloseAdmPanel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            CloseAdmPanel.InactiveColorA = System.Drawing.Color.FromArgb(253, 252, 252);
            CloseAdmPanel.InactiveColorB = System.Drawing.Color.FromArgb(239, 237, 236);
            CloseAdmPanel.Location = new System.Drawing.Point(769, 9);
            CloseAdmPanel.Name = "CloseAdmPanel";
            CloseAdmPanel.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseAdmPanel.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseAdmPanel.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseAdmPanel.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseAdmPanel.Size = new System.Drawing.Size(25, 25);
            CloseAdmPanel.TabIndex = 44;
            CloseAdmPanel.Text = "X";
            CloseAdmPanel.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseAdmPanel.Click += CloseAdmPanel_Click;
            // 
            // MainText
            // 
            MainText.AutoSize = true;
            MainText.BackColor = System.Drawing.Color.Transparent;
            MainText.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            MainText.ForeColor = System.Drawing.Color.Azure;
            MainText.Location = new System.Drawing.Point(233, -4);
            MainText.Name = "MainText";
            MainText.Size = new System.Drawing.Size(326, 45);
            MainText.TabIndex = 43;
            MainText.Text = "Настройки аккаунта";
            // 
            // Login
            // 
            Login.AutoSize = true;
            Login.BackColor = System.Drawing.Color.Transparent;
            Login.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Login.ForeColor = System.Drawing.Color.Gainsboro;
            Login.Location = new System.Drawing.Point(208, 79);
            Login.Name = "Login";
            Login.Size = new System.Drawing.Size(62, 32);
            Login.TabIndex = 46;
            Login.Text = "Текс";
            Login.Click += Login_Click;
            Login.MouseEnter += Login_MouseEnter;
            Login.MouseLeave += Login_MouseLeave;
            // 
            // Password
            // 
            Password.AutoSize = true;
            Password.BackColor = System.Drawing.Color.Transparent;
            Password.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            Password.ForeColor = System.Drawing.Color.Gainsboro;
            Password.Location = new System.Drawing.Point(208, 133);
            Password.Name = "Password";
            Password.Size = new System.Drawing.Size(62, 32);
            Password.TabIndex = 47;
            Password.Text = "Текс";
            Password.Click += Password_Click;
            Password.MouseEnter += Password_MouseEnter;
            Password.MouseLeave += Password_MouseLeave;
            // 
            // Mail
            // 
            Mail.AutoSize = true;
            Mail.BackColor = System.Drawing.Color.Transparent;
            Mail.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            Mail.ForeColor = System.Drawing.Color.Gainsboro;
            Mail.Location = new System.Drawing.Point(208, 191);
            Mail.Name = "Mail";
            Mail.Size = new System.Drawing.Size(62, 32);
            Mail.TabIndex = 48;
            Mail.Text = "Текс";
            Mail.Click += Mail_Click;
            Mail.MouseEnter += Mail_MouseEnter;
            Mail.MouseLeave += Mail_MouseLeave;
            // 
            // ChangePasswordButton
            // 
            ChangePasswordButton.Alpha = 20;
            ChangePasswordButton.BackColor = System.Drawing.Color.Transparent;
            ChangePasswordButton.Background = true;
            ChangePasswordButton.Background_WidthPen = 4F;
            ChangePasswordButton.BackgroundPen = true;
            ChangePasswordButton.ColorBackground = System.Drawing.Color.Transparent;
            ChangePasswordButton.ColorBackground_1 = System.Drawing.Color.White;
            ChangePasswordButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ChangePasswordButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            ChangePasswordButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            ChangePasswordButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            ChangePasswordButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ChangePasswordButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            ChangePasswordButton.Effect_1 = true;
            ChangePasswordButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            ChangePasswordButton.Effect_1_Transparency = 25;
            ChangePasswordButton.Effect_2 = true;
            ChangePasswordButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            ChangePasswordButton.Effect_2_Transparency = 20;
            ChangePasswordButton.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ChangePasswordButton.ForeColor = System.Drawing.Color.Tan;
            ChangePasswordButton.Lighting = false;
            ChangePasswordButton.LinearGradient_Background = false;
            ChangePasswordButton.LinearGradientPen = false;
            ChangePasswordButton.Location = new System.Drawing.Point(649, 133);
            ChangePasswordButton.Name = "ChangePasswordButton";
            ChangePasswordButton.PenWidth = 15;
            ChangePasswordButton.Rounding = true;
            ChangePasswordButton.RoundingInt = 20;
            ChangePasswordButton.Size = new System.Drawing.Size(136, 38);
            ChangePasswordButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ChangePasswordButton.TabIndex = 49;
            ChangePasswordButton.Tag = "Cyber";
            ChangePasswordButton.TextButton = "Изменить";
            ChangePasswordButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            ChangePasswordButton.Timer_Effect_1 = 5;
            ChangePasswordButton.Timer_RGB = 300;
            ChangePasswordButton.Click += ChangePasswordButton_Click;
            // 
            // ChangeMailButton
            // 
            ChangeMailButton.Alpha = 20;
            ChangeMailButton.BackColor = System.Drawing.Color.Transparent;
            ChangeMailButton.Background = true;
            ChangeMailButton.Background_WidthPen = 4F;
            ChangeMailButton.BackgroundPen = true;
            ChangeMailButton.ColorBackground = System.Drawing.Color.Transparent;
            ChangeMailButton.ColorBackground_1 = System.Drawing.Color.White;
            ChangeMailButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ChangeMailButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            ChangeMailButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            ChangeMailButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            ChangeMailButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ChangeMailButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            ChangeMailButton.Effect_1 = true;
            ChangeMailButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            ChangeMailButton.Effect_1_Transparency = 25;
            ChangeMailButton.Effect_2 = true;
            ChangeMailButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            ChangeMailButton.Effect_2_Transparency = 20;
            ChangeMailButton.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ChangeMailButton.ForeColor = System.Drawing.Color.Tan;
            ChangeMailButton.Lighting = false;
            ChangeMailButton.LinearGradient_Background = false;
            ChangeMailButton.LinearGradientPen = false;
            ChangeMailButton.Location = new System.Drawing.Point(649, 191);
            ChangeMailButton.Name = "ChangeMailButton";
            ChangeMailButton.PenWidth = 15;
            ChangeMailButton.Rounding = true;
            ChangeMailButton.RoundingInt = 20;
            ChangeMailButton.Size = new System.Drawing.Size(136, 38);
            ChangeMailButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ChangeMailButton.TabIndex = 50;
            ChangeMailButton.Tag = "Cyber";
            ChangeMailButton.TextButton = "Изменить";
            ChangeMailButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            ChangeMailButton.Timer_Effect_1 = 5;
            ChangeMailButton.Timer_RGB = 300;
            ChangeMailButton.Click += ChangeMailButton_Click;
            // 
            // TypeAccount
            // 
            TypeAccount.AutoSize = true;
            TypeAccount.BackColor = System.Drawing.Color.Transparent;
            TypeAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            TypeAccount.ForeColor = System.Drawing.Color.Gainsboro;
            TypeAccount.Location = new System.Drawing.Point(46, 406);
            TypeAccount.Name = "TypeAccount";
            TypeAccount.Size = new System.Drawing.Size(161, 32);
            TypeAccount.TabIndex = 51;
            TypeAccount.Text = "Тип аккаунта";
            // 
            // LabelMail
            // 
            LabelMail.AutoSize = true;
            LabelMail.BackColor = System.Drawing.Color.Transparent;
            LabelMail.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            LabelMail.ForeColor = System.Drawing.Color.Tan;
            LabelMail.Location = new System.Drawing.Point(46, 191);
            LabelMail.Name = "LabelMail";
            LabelMail.Size = new System.Drawing.Size(89, 32);
            LabelMail.TabIndex = 54;
            LabelMail.Text = "Почта:";
            // 
            // LabelPassword
            // 
            LabelPassword.AutoSize = true;
            LabelPassword.BackColor = System.Drawing.Color.Transparent;
            LabelPassword.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            LabelPassword.ForeColor = System.Drawing.Color.Tan;
            LabelPassword.Location = new System.Drawing.Point(46, 133);
            LabelPassword.Name = "LabelPassword";
            LabelPassword.Size = new System.Drawing.Size(156, 32);
            LabelPassword.TabIndex = 53;
            LabelPassword.Text = "Ваш пароль:";
            // 
            // LabelLogin
            // 
            LabelLogin.AutoSize = true;
            LabelLogin.BackColor = System.Drawing.Color.Transparent;
            LabelLogin.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            LabelLogin.ForeColor = System.Drawing.Color.Tan;
            LabelLogin.Location = new System.Drawing.Point(46, 79);
            LabelLogin.Name = "LabelLogin";
            LabelLogin.Size = new System.Drawing.Size(139, 32);
            LabelLogin.TabIndex = 52;
            LabelLogin.Text = "Ваш логин:";
            // 
            // NewText
            // 
            NewText.AutoSize = true;
            NewText.BackColor = System.Drawing.Color.Transparent;
            NewText.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold);
            NewText.ForeColor = System.Drawing.Color.Tan;
            NewText.Location = new System.Drawing.Point(46, 284);
            NewText.Name = "NewText";
            NewText.Size = new System.Drawing.Size(185, 32);
            NewText.TabIndex = 57;
            NewText.Text = "Новый пароль:";
            NewText.Visible = false;
            // 
            // New
            // 
            New.Location = new System.Drawing.Point(269, 289);
            New.MaxLength = 100;
            New.Name = "New";
            New.Size = new System.Drawing.Size(516, 27);
            New.TabIndex = 58;
            New.Visible = false;
            // 
            // ConfirmButton
            // 
            ConfirmButton.Alpha = 20;
            ConfirmButton.BackColor = System.Drawing.Color.Transparent;
            ConfirmButton.Background = true;
            ConfirmButton.Background_WidthPen = 4F;
            ConfirmButton.BackgroundPen = true;
            ConfirmButton.ColorBackground = System.Drawing.Color.Transparent;
            ConfirmButton.ColorBackground_1 = System.Drawing.Color.White;
            ConfirmButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ConfirmButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            ConfirmButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            ConfirmButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            ConfirmButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ConfirmButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            ConfirmButton.Effect_1 = true;
            ConfirmButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            ConfirmButton.Effect_1_Transparency = 25;
            ConfirmButton.Effect_2 = true;
            ConfirmButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            ConfirmButton.Effect_2_Transparency = 20;
            ConfirmButton.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ConfirmButton.ForeColor = System.Drawing.Color.Tan;
            ConfirmButton.Lighting = false;
            ConfirmButton.LinearGradient_Background = false;
            ConfirmButton.LinearGradientPen = false;
            ConfirmButton.Location = new System.Drawing.Point(582, 367);
            ConfirmButton.Name = "ConfirmButton";
            ConfirmButton.PenWidth = 15;
            ConfirmButton.Rounding = true;
            ConfirmButton.RoundingInt = 20;
            ConfirmButton.Size = new System.Drawing.Size(203, 71);
            ConfirmButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ConfirmButton.TabIndex = 59;
            ConfirmButton.Tag = "Cyber";
            ConfirmButton.TextButton = "Подтвердить";
            ConfirmButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            ConfirmButton.Timer_Effect_1 = 5;
            ConfirmButton.Timer_RGB = 300;
            ConfirmButton.Click += ConfirmButton_Click;
            // 
            // AccountSettingsWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(800, 453);
            Controls.Add(ConfirmButton);
            Controls.Add(New);
            Controls.Add(NewText);
            Controls.Add(LabelMail);
            Controls.Add(LabelPassword);
            Controls.Add(LabelLogin);
            Controls.Add(TypeAccount);
            Controls.Add(ChangeMailButton);
            Controls.Add(ChangePasswordButton);
            Controls.Add(Mail);
            Controls.Add(Password);
            Controls.Add(Login);
            Controls.Add(ChangeLoginButton);
            Controls.Add(CloseAdmPanel);
            Controls.Add(MainText);
            Controls.Add(panel1);
            Name = "AccountSettingsWindow";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            Load += AccountSettingsWindow_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.CyberButton ChangePasswordButton;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseAdmPanel;
        private ReaLTaiizor.Controls.BigLabel LabelMail;
        private ReaLTaiizor.Controls.BigLabel Login;
        private ReaLTaiizor.Controls.BigLabel Password;
        private ReaLTaiizor.Controls.BigLabel Mail;
        private ReaLTaiizor.Controls.CyberButton ChangeLoginButton;
        private ReaLTaiizor.Controls.CyberButton ChangeMailButton;
        private ReaLTaiizor.Controls.BigLabel TypeAccount;
        private ReaLTaiizor.Controls.BigLabel MainText;
        private ReaLTaiizor.Controls.BigLabel LabelPassword;
        private ReaLTaiizor.Controls.BigLabel LabelLogin;
        private ReaLTaiizor.Controls.BigLabel NewText;
        private System.Windows.Forms.TextBox New;
        private ReaLTaiizor.Controls.CyberButton ConfirmButton;
    }
}