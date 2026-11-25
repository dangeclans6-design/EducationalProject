namespace BankApplication
{
    partial class WindowPay
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
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            panel1 = new System.Windows.Forms.Panel();
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            BackButton = new ReaLTaiizor.Controls.CyberButton();
            TextSupport = new ReaLTaiizor.Controls.BigLabel();
            PanelCategories = new System.Windows.Forms.Panel();
            PaymentAccount = new ReaLTaiizor.Controls.BigTextBox();
            SuspendLayout();
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(273, -5);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(152, 50);
            bigLabel3.TabIndex = 96;
            bigLabel3.Text = "Оплата";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-6, -5);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(47, 850);
            panel1.TabIndex = 93;
            // 
            // CloseButton
            // 
            CloseButton.BackColor = System.Drawing.Color.Transparent;
            CloseButton.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            CloseButton.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            CloseButton.Image = null;
            CloseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            CloseButton.InactiveColorA = System.Drawing.Color.FromArgb(253, 252, 252);
            CloseButton.InactiveColorB = System.Drawing.Color.FromArgb(239, 237, 236);
            CloseButton.Location = new System.Drawing.Point(643, 6);
            CloseButton.Name = "CloseButton";
            CloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.Size = new System.Drawing.Size(25, 25);
            CloseButton.TabIndex = 92;
            CloseButton.Text = "X";
            CloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseButton.Click += CloseButton_Click;
            // 
            // BackButton
            // 
            BackButton.Alpha = 20;
            BackButton.BackColor = System.Drawing.Color.Transparent;
            BackButton.Background = true;
            BackButton.Background_WidthPen = 4F;
            BackButton.BackgroundPen = true;
            BackButton.ColorBackground = System.Drawing.Color.Transparent;
            BackButton.ColorBackground_1 = System.Drawing.Color.White;
            BackButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            BackButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            BackButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            BackButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            BackButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            BackButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            BackButton.Effect_1 = true;
            BackButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            BackButton.Effect_1_Transparency = 25;
            BackButton.Effect_2 = true;
            BackButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            BackButton.Effect_2_Transparency = 20;
            BackButton.Font = new System.Drawing.Font("Segoe UI Semibold", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            BackButton.ForeColor = System.Drawing.Color.Tan;
            BackButton.Lighting = false;
            BackButton.LinearGradient_Background = false;
            BackButton.LinearGradientPen = false;
            BackButton.Location = new System.Drawing.Point(56, 713);
            BackButton.Name = "BackButton";
            BackButton.PenWidth = 15;
            BackButton.Rounding = true;
            BackButton.RoundingInt = 20;
            BackButton.Size = new System.Drawing.Size(241, 83);
            BackButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            BackButton.TabIndex = 87;
            BackButton.Tag = "Cyber";
            BackButton.TextButton = "Назад";
            BackButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            BackButton.Timer_Effect_1 = 5;
            BackButton.Timer_RGB = 300;
            BackButton.Visible = false;
            BackButton.Click += BackButton_Click;
            // 
            // TextSupport
            // 
            TextSupport.AutoSize = true;
            TextSupport.BackColor = System.Drawing.Color.Transparent;
            TextSupport.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            TextSupport.ForeColor = System.Drawing.Color.Tan;
            TextSupport.Location = new System.Drawing.Point(47, 578);
            TextSupport.Name = "TextSupport";
            TextSupport.Size = new System.Drawing.Size(57, 25);
            TextSupport.TabIndex = 86;
            TextSupport.Text = "Текст";
            TextSupport.Visible = false;
            // 
            // PanelCategories
            // 
            PanelCategories.BackColor = System.Drawing.SystemColors.ControlDark;
            PanelCategories.Location = new System.Drawing.Point(56, 73);
            PanelCategories.Name = "PanelCategories";
            PanelCategories.Size = new System.Drawing.Size(611, 386);
            PanelCategories.TabIndex = 99;
            // 
            // PaymentAccount
            // 
            PaymentAccount.BackColor = System.Drawing.Color.Transparent;
            PaymentAccount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            PaymentAccount.ForeColor = System.Drawing.Color.DimGray;
            PaymentAccount.Image = null;
            PaymentAccount.Location = new System.Drawing.Point(56, 517);
            PaymentAccount.MaxLength = 32767;
            PaymentAccount.Multiline = false;
            PaymentAccount.Name = "PaymentAccount";
            PaymentAccount.ReadOnly = false;
            PaymentAccount.Size = new System.Drawing.Size(612, 49);
            PaymentAccount.TabIndex = 100;
            PaymentAccount.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
            PaymentAccount.UseSystemPasswordChar = false;
            PaymentAccount.Visible = false;
            // 
            // WindowPay
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(682, 839);
            Controls.Add(PaymentAccount);
            Controls.Add(TextSupport);
            Controls.Add(PanelCategories);
            Controls.Add(BackButton);
            Controls.Add(bigLabel3);
            Controls.Add(panel1);
            Controls.Add(CloseButton);
            Name = "WindowPay";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            Load += WindowPay_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private ReaLTaiizor.Controls.CyberButton BackButton;
        private ReaLTaiizor.Controls.BigLabel TextSupport;
        private System.Windows.Forms.Panel PanelCategories;
        private ReaLTaiizor.Controls.BigTextBox PaymentAccount;
    }
}