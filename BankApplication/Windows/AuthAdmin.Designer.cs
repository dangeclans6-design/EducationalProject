namespace BankApplication
{
    partial class AuthAdmin
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
            LoginCloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            go_auth = new ReaLTaiizor.Controls.CyberButton();
            panel1 = new System.Windows.Forms.Panel();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            login_auth = new ReaLTaiizor.Controls.DungeonRichTextBox();
            password_auth = new ReaLTaiizor.Controls.DungeonRichTextBox();
            bigLabel4 = new ReaLTaiizor.Controls.BigLabel();
            code_auth = new ReaLTaiizor.Controls.DungeonRichTextBox();
            SuspendLayout();
            // 
            // LoginCloseButton
            // 
            LoginCloseButton.BackColor = System.Drawing.Color.Transparent;
            LoginCloseButton.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            LoginCloseButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            LoginCloseButton.Image = null;
            LoginCloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            LoginCloseButton.InactiveColorA = System.Drawing.Color.FromArgb(253, 252, 252);
            LoginCloseButton.InactiveColorB = System.Drawing.Color.FromArgb(239, 237, 236);
            LoginCloseButton.Location = new System.Drawing.Point(1069, 3);
            LoginCloseButton.Name = "LoginCloseButton";
            LoginCloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            LoginCloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            LoginCloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            LoginCloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            LoginCloseButton.Size = new System.Drawing.Size(25, 25);
            LoginCloseButton.TabIndex = 8;
            LoginCloseButton.Text = "X";
            LoginCloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            LoginCloseButton.Click += CloseAuth_Click;
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            bigLabel1.ForeColor = System.Drawing.Color.Azure;
            bigLabel1.Location = new System.Drawing.Point(65, 94);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(247, 45);
            bigLabel1.TabIndex = 5;
            bigLabel1.Text = "Введите логин:";
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            bigLabel2.ForeColor = System.Drawing.Color.Azure;
            bigLabel2.Location = new System.Drawing.Point(65, 180);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(268, 45);
            bigLabel2.TabIndex = 7;
            bigLabel2.Text = "Введите пароль:";
            // 
            // go_auth
            // 
            go_auth.Alpha = 20;
            go_auth.BackColor = System.Drawing.Color.Transparent;
            go_auth.Background = true;
            go_auth.Background_WidthPen = 4F;
            go_auth.BackgroundPen = true;
            go_auth.ColorBackground = System.Drawing.Color.Transparent;
            go_auth.ColorBackground_1 = System.Drawing.Color.White;
            go_auth.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            go_auth.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            go_auth.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            go_auth.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            go_auth.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            go_auth.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            go_auth.Effect_1 = true;
            go_auth.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            go_auth.Effect_1_Transparency = 25;
            go_auth.Effect_2 = true;
            go_auth.Effect_2_ColorBackground = System.Drawing.Color.White;
            go_auth.Effect_2_Transparency = 20;
            go_auth.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            go_auth.ForeColor = System.Drawing.Color.Tan;
            go_auth.Lighting = false;
            go_auth.LinearGradient_Background = false;
            go_auth.LinearGradientPen = false;
            go_auth.Location = new System.Drawing.Point(830, 352);
            go_auth.Name = "go_auth";
            go_auth.PenWidth = 15;
            go_auth.Rounding = true;
            go_auth.RoundingInt = 20;
            go_auth.Size = new System.Drawing.Size(241, 83);
            go_auth.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            go_auth.TabIndex = 9;
            go_auth.Tag = "Cyber";
            go_auth.TextButton = "Войти";
            go_auth.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            go_auth.Timer_Effect_1 = 5;
            go_auth.Timer_RGB = 300;
            go_auth.Click += GoAuthClick;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-19, -4);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(54, 457);
            panel1.TabIndex = 10;
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(249, 3);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(648, 45);
            bigLabel3.TabIndex = 11;
            bigLabel3.Text = "Авторизация в панели администратора";
            // 
            // login_auth
            // 
            login_auth.AutoWordSelection = false;
            login_auth.BackColor = System.Drawing.Color.Transparent;
            login_auth.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            login_auth.EdgeColor = System.Drawing.Color.White;
            login_auth.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            login_auth.ForeColor = System.Drawing.Color.Black;
            login_auth.Location = new System.Drawing.Point(490, 94);
            login_auth.Name = "login_auth";
            login_auth.ReadOnly = false;
            login_auth.Size = new System.Drawing.Size(581, 36);
            login_auth.TabIndex = 84;
            login_auth.TextBackColor = System.Drawing.Color.White;
            login_auth.WordWrap = true;
            // 
            // password_auth
            // 
            password_auth.AutoWordSelection = false;
            password_auth.BackColor = System.Drawing.Color.Transparent;
            password_auth.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            password_auth.EdgeColor = System.Drawing.Color.White;
            password_auth.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            password_auth.ForeColor = System.Drawing.Color.Black;
            password_auth.Location = new System.Drawing.Point(490, 180);
            password_auth.Name = "password_auth";
            password_auth.ReadOnly = false;
            password_auth.Size = new System.Drawing.Size(581, 36);
            password_auth.TabIndex = 85;
            password_auth.TextBackColor = System.Drawing.Color.White;
            password_auth.WordWrap = true;
            // 
            // bigLabel4
            // 
            bigLabel4.AutoSize = true;
            bigLabel4.BackColor = System.Drawing.Color.Transparent;
            bigLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold);
            bigLabel4.ForeColor = System.Drawing.Color.Azure;
            bigLabel4.Location = new System.Drawing.Point(65, 263);
            bigLabel4.Name = "bigLabel4";
            bigLabel4.Size = new System.Drawing.Size(411, 45);
            bigLabel4.TabIndex = 86;
            bigLabel4.Text = "Введите секретный ключ:";
            // 
            // code_auth
            // 
            code_auth.AutoWordSelection = false;
            code_auth.BackColor = System.Drawing.Color.Transparent;
            code_auth.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            code_auth.EdgeColor = System.Drawing.Color.White;
            code_auth.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            code_auth.ForeColor = System.Drawing.Color.Black;
            code_auth.Location = new System.Drawing.Point(490, 263);
            code_auth.Name = "code_auth";
            code_auth.ReadOnly = false;
            code_auth.Size = new System.Drawing.Size(581, 36);
            code_auth.TabIndex = 87;
            code_auth.TextBackColor = System.Drawing.Color.White;
            code_auth.WordWrap = true;
            // 
            // AuthAdmin
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.WhiteSmoke;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(1100, 450);
            Controls.Add(code_auth);
            Controls.Add(bigLabel4);
            Controls.Add(password_auth);
            Controls.Add(login_auth);
            Controls.Add(bigLabel3);
            Controls.Add(panel1);
            Controls.Add(go_auth);
            Controls.Add(LoginCloseButton);
            Controls.Add(bigLabel2);
            Controls.Add(bigLabel1);
            MaximumSize = new System.Drawing.Size(1100, 450);
            MinimumSize = new System.Drawing.Size(1100, 450);
            Name = "AuthAdmin";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.DungeonButtonLeft LoginCloseButton;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
        private ReaLTaiizor.Controls.CyberButton go_auth;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.DungeonRichTextBox login_auth;
        private ReaLTaiizor.Controls.DungeonRichTextBox password_auth;
        private ReaLTaiizor.Controls.BigLabel bigLabel4;
        private ReaLTaiizor.Controls.DungeonRichTextBox code_auth;
    }
}