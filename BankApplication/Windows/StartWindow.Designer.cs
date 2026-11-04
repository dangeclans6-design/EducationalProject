namespace BankApplication
{
    partial class StartWindow
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new System.Windows.Forms.Panel();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            CloseAuth = new ReaLTaiizor.Controls.DungeonButtonLeft();
            back_auth = new ReaLTaiizor.Controls.CyberButton();
            go_auth = new ReaLTaiizor.Controls.CyberButton();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Lavender;
            panel1.Location = new System.Drawing.Point(-7, -3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(54, 571);
            panel1.TabIndex = 11;
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(90, 9);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(862, 86);
            bigLabel3.TabIndex = 14;
            bigLabel3.Text = "Добро пожаловать в банк";
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel1.ForeColor = System.Drawing.Color.Azure;
            bigLabel1.Location = new System.Drawing.Point(297, 95);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(403, 65);
            bigLabel1.TabIndex = 15;
            bigLabel1.Text = "BankApplication";
            // 
            // CloseAuth
            // 
            CloseAuth.BackColor = System.Drawing.Color.Transparent;
            CloseAuth.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            CloseAuth.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            CloseAuth.Image = null;
            CloseAuth.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            CloseAuth.InactiveColorA = System.Drawing.Color.FromArgb(253, 252, 252);
            CloseAuth.InactiveColorB = System.Drawing.Color.FromArgb(239, 237, 236);
            CloseAuth.Location = new System.Drawing.Point(970, 4);
            CloseAuth.Name = "CloseAuth";
            CloseAuth.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseAuth.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseAuth.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseAuth.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseAuth.Size = new System.Drawing.Size(25, 25);
            CloseAuth.TabIndex = 16;
            CloseAuth.Text = "X";
            CloseAuth.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseAuth.Click += CloseWindow1Click;
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
            back_auth.ColorBackground_Pen = System.Drawing.Color.Linen;
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
            back_auth.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            back_auth.ForeColor = System.Drawing.Color.White;
            back_auth.Lighting = false;
            back_auth.LinearGradient_Background = false;
            back_auth.LinearGradientPen = false;
            back_auth.Location = new System.Drawing.Point(603, 284);
            back_auth.Name = "back_auth";
            back_auth.PenWidth = 15;
            back_auth.Rounding = true;
            back_auth.RoundingInt = 20;
            back_auth.Size = new System.Drawing.Size(349, 83);
            back_auth.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            back_auth.TabIndex = 17;
            back_auth.Tag = "Cyber";
            back_auth.TextButton = "Авторизация";
            back_auth.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            back_auth.Timer_Effect_1 = 5;
            back_auth.Timer_RGB = 300;
            back_auth.Click += AvtorButtonClick;
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
            go_auth.ColorBackground_Pen = System.Drawing.Color.Linen;
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
            go_auth.ForeColor = System.Drawing.Color.White;
            go_auth.Lighting = false;
            go_auth.LinearGradient_Background = false;
            go_auth.LinearGradientPen = false;
            go_auth.Location = new System.Drawing.Point(90, 284);
            go_auth.Name = "go_auth";
            go_auth.PenWidth = 15;
            go_auth.Rounding = true;
            go_auth.RoundingInt = 20;
            go_auth.Size = new System.Drawing.Size(349, 83);
            go_auth.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            go_auth.TabIndex = 18;
            go_auth.Tag = "Cyber";
            go_auth.TextButton = "Регистрация";
            go_auth.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            go_auth.Timer_Effect_1 = 5;
            go_auth.Timer_RGB = 300;
            go_auth.Click += RegButtonClick;
            // 
            // Window1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.LightSlateGray;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(1000, 400);
            Controls.Add(back_auth);
            Controls.Add(go_auth);
            Controls.Add(CloseAuth);
            Controls.Add(bigLabel1);
            Controls.Add(bigLabel3);
            Controls.Add(panel1);
            MaximumSize = new System.Drawing.Size(1000, 400);
            MinimumSize = new System.Drawing.Size(1000, 400);
            Name = "Window1";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            FormClosed += Window1_FormClosed;
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseAuth;
        private ReaLTaiizor.Controls.CyberButton back_auth;
        private ReaLTaiizor.Controls.CyberButton go_auth;
    }
}

