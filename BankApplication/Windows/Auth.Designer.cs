namespace BankApplication
{
    partial class Auth
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
            back_auth = new ReaLTaiizor.Controls.CyberButton();
            go_auth = new ReaLTaiizor.Controls.CyberButton();
            panel1 = new System.Windows.Forms.Panel();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            login_auth = new ReaLTaiizor.Controls.DungeonRichTextBox();
            password_auth = new ReaLTaiizor.Controls.DungeonRichTextBox();
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
            LoginCloseButton.Location = new System.Drawing.Point(872, 3);
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
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel1.ForeColor = System.Drawing.Color.Azure;
            bigLabel1.Location = new System.Drawing.Point(41, 97);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(283, 50);
            bigLabel1.TabIndex = 5;
            bigLabel1.Text = "Введите логин:";
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel2.ForeColor = System.Drawing.Color.Azure;
            bigLabel2.Location = new System.Drawing.Point(41, 169);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(307, 50);
            bigLabel2.TabIndex = 7;
            bigLabel2.Text = "Введите пароль:";
            // 
            // back_auth
            // 
            back_auth.Alpha = 20;
            back_auth.BackColor = System.Drawing.Color.Transparent;
            back_auth.Background = true;
            back_auth.Background_WidthPen = 4F;
            back_auth.BackgroundPen = true;
            back_auth.ColorBackground = System.Drawing.Color.Transparent;
            back_auth.ColorBackground_1 = System.Drawing.Color.White;
            back_auth.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            back_auth.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            back_auth.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            back_auth.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            back_auth.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            back_auth.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            back_auth.Effect_1 = true;
            back_auth.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            back_auth.Effect_1_Transparency = 25;
            back_auth.Effect_2 = true;
            back_auth.Effect_2_ColorBackground = System.Drawing.Color.White;
            back_auth.Effect_2_Transparency = 20;
            back_auth.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            back_auth.ForeColor = System.Drawing.Color.Tan;
            back_auth.Lighting = false;
            back_auth.LinearGradient_Background = false;
            back_auth.LinearGradientPen = false;
            back_auth.Location = new System.Drawing.Point(71, 289);
            back_auth.Name = "back_auth";
            back_auth.PenWidth = 15;
            back_auth.Rounding = true;
            back_auth.RoundingInt = 20;
            back_auth.Size = new System.Drawing.Size(241, 83);
            back_auth.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            back_auth.TabIndex = 8;
            back_auth.Tag = "Cyber";
            back_auth.TextButton = "Назад";
            back_auth.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            back_auth.Timer_Effect_1 = 5;
            back_auth.Timer_RGB = 300;
            back_auth.Click += BackAuthClick;
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
            go_auth.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            go_auth.ForeColor = System.Drawing.Color.Tan;
            go_auth.Lighting = false;
            go_auth.LinearGradient_Background = false;
            go_auth.LinearGradientPen = false;
            go_auth.Location = new System.Drawing.Point(614, 289);
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
            panel1.Size = new System.Drawing.Size(54, 413);
            panel1.TabIndex = 10;
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(332, 3);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(260, 50);
            bigLabel3.TabIndex = 11;
            bigLabel3.Text = "Авторизация";
            // 
            // login_auth
            // 
            login_auth.AutoWordSelection = false;
            login_auth.BackColor = System.Drawing.Color.Transparent;
            login_auth.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            login_auth.EdgeColor = System.Drawing.Color.White;
            login_auth.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            login_auth.ForeColor = System.Drawing.Color.Black;
            login_auth.Location = new System.Drawing.Point(370, 111);
            login_auth.Name = "login_auth";
            login_auth.ReadOnly = false;
            login_auth.Size = new System.Drawing.Size(485, 36);
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
            password_auth.Location = new System.Drawing.Point(370, 183);
            password_auth.Name = "password_auth";
            password_auth.ReadOnly = false;
            password_auth.Size = new System.Drawing.Size(485, 36);
            password_auth.TabIndex = 85;
            password_auth.TextBackColor = System.Drawing.Color.White;
            password_auth.WordWrap = true;
            // 
            // Auth
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.WhiteSmoke;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(900, 400);
            Controls.Add(password_auth);
            Controls.Add(login_auth);
            Controls.Add(bigLabel3);
            Controls.Add(panel1);
            Controls.Add(back_auth);
            Controls.Add(go_auth);
            Controls.Add(LoginCloseButton);
            Controls.Add(bigLabel2);
            Controls.Add(bigLabel1);
            MaximumSize = new System.Drawing.Size(900, 400);
            MinimumSize = new System.Drawing.Size(900, 400);
            Name = "Auth";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            FormClosed += AuthFormClosed;
            Resize += ResizeAuth;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.DungeonButtonLeft LoginCloseButton;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
        private ReaLTaiizor.Controls.CyberButton back_auth;
        private ReaLTaiizor.Controls.CyberButton go_auth;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.DungeonRichTextBox login_auth;
        private ReaLTaiizor.Controls.DungeonRichTextBox password_auth;
    }
}