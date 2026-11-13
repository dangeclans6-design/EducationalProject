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
            bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            PersonalAccount = new ReaLTaiizor.Controls.DungeonRichTextBox();
            FindData = new ReaLTaiizor.Controls.CyberButton();
            panel1 = new System.Windows.Forms.Panel();
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            BoxTarget = new ReaLTaiizor.Controls.MaterialComboBox();
            SuspendLayout();
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel2.ForeColor = System.Drawing.Color.Tan;
            bigLabel2.Location = new System.Drawing.Point(58, 169);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(169, 32);
            bigLabel2.TabIndex = 97;
            bigLabel2.Text = "Лицевой счет";
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(84, -5);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(152, 50);
            bigLabel3.TabIndex = 96;
            bigLabel3.Text = "Оплата";
            // 
            // PersonalAccount
            // 
            PersonalAccount.AutoWordSelection = false;
            PersonalAccount.BackColor = System.Drawing.Color.Transparent;
            PersonalAccount.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            PersonalAccount.EdgeColor = System.Drawing.Color.White;
            PersonalAccount.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            PersonalAccount.ForeColor = System.Drawing.Color.Black;
            PersonalAccount.Location = new System.Drawing.Point(63, 217);
            PersonalAccount.Name = "PersonalAccount";
            PersonalAccount.ReadOnly = false;
            PersonalAccount.Size = new System.Drawing.Size(605, 36);
            PersonalAccount.TabIndex = 95;
            PersonalAccount.TextBackColor = System.Drawing.Color.White;
            PersonalAccount.WordWrap = true;
            // 
            // FindData
            // 
            FindData.Alpha = 20;
            FindData.BackColor = System.Drawing.Color.Transparent;
            FindData.Background = true;
            FindData.Background_WidthPen = 4F;
            FindData.BackgroundPen = true;
            FindData.ColorBackground = System.Drawing.Color.Transparent;
            FindData.ColorBackground_1 = System.Drawing.Color.White;
            FindData.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            FindData.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            FindData.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            FindData.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            FindData.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            FindData.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            FindData.Effect_1 = true;
            FindData.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            FindData.Effect_1_Transparency = 25;
            FindData.Effect_2 = true;
            FindData.Effect_2_ColorBackground = System.Drawing.Color.White;
            FindData.Effect_2_Transparency = 20;
            FindData.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            FindData.ForeColor = System.Drawing.Color.Tan;
            FindData.Lighting = false;
            FindData.LinearGradient_Background = false;
            FindData.LinearGradientPen = false;
            FindData.Location = new System.Drawing.Point(478, 324);
            FindData.Name = "FindData";
            FindData.PenWidth = 15;
            FindData.Rounding = true;
            FindData.RoundingInt = 20;
            FindData.Size = new System.Drawing.Size(241, 83);
            FindData.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            FindData.TabIndex = 94;
            FindData.Tag = "Cyber";
            FindData.TextButton = "Найти";
            FindData.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            FindData.Timer_Effect_1 = 5;
            FindData.Timer_RGB = 300;
            FindData.Click += this.FindData_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-6, -5);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(47, 464);
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
            CloseButton.Location = new System.Drawing.Point(760, 12);
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
            // BoxTarget
            // 
            BoxTarget.AutoResize = false;
            BoxTarget.BackColor = System.Drawing.Color.Tan;
            BoxTarget.Depth = 0;
            BoxTarget.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            BoxTarget.DropDownHeight = 174;
            BoxTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            BoxTarget.DropDownWidth = 121;
            BoxTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            BoxTarget.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            BoxTarget.FormattingEnabled = true;
            BoxTarget.IntegralHeight = false;
            BoxTarget.ItemHeight = 43;
            BoxTarget.Location = new System.Drawing.Point(63, 92);
            BoxTarget.MaxDropDownItems = 4;
            BoxTarget.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            BoxTarget.Name = "BoxTarget";
            BoxTarget.Size = new System.Drawing.Size(605, 49);
            BoxTarget.StartIndex = 0;
            BoxTarget.TabIndex = 98;
            BoxTarget.SelectedIndexChanged += BoxTarget_SelectedIndexChanged;
            // 
            // WindowPay
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(BoxTarget);
            Controls.Add(bigLabel2);
            Controls.Add(bigLabel3);
            Controls.Add(PersonalAccount);
            Controls.Add(FindData);
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

        private ReaLTaiizor.Controls.BigLabel bigLabel2;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.DungeonRichTextBox PersonalAccount;
        private ReaLTaiizor.Controls.CyberButton FindData;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private ReaLTaiizor.Controls.MaterialComboBox BoxTarget;
    }
}