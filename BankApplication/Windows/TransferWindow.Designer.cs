namespace BankApplication
{
    partial class TransferWindow
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
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            panel1 = new System.Windows.Forms.Panel();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            TransferSelfMainButton = new ReaLTaiizor.Controls.CyberButton();
            TransferMainButton = new ReaLTaiizor.Controls.CyberButton();
            spaceSeparatorHorizontal1 = new ReaLTaiizor.Controls.SpaceSeparatorHorizontal();
            spaceSeparatorHorizontal2 = new ReaLTaiizor.Controls.SpaceSeparatorHorizontal();
            PanelSelf = new ReaLTaiizor.Controls.Panel();
            TransferSelfButton = new ReaLTaiizor.Controls.CyberButton();
            InfoText = new ReaLTaiizor.Controls.BigLabel();
            Amount = new System.Windows.Forms.TextBox();
            bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            InfoTransferText = new ReaLTaiizor.Controls.BigLabel();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            panel3 = new ReaLTaiizor.Controls.Panel();
            BoxTarget = new ReaLTaiizor.Controls.MaterialComboBox();
            PanelSelf.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
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
            CloseButton.Location = new System.Drawing.Point(712, 6);
            CloseButton.Name = "CloseButton";
            CloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.Size = new System.Drawing.Size(25, 25);
            CloseButton.TabIndex = 97;
            CloseButton.Text = "X";
            CloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseButton.Click += CloseButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-4, -3);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(46, 778);
            panel1.TabIndex = 98;
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(261, -3);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(180, 50);
            bigLabel3.TabIndex = 99;
            bigLabel3.Text = "Перевод";
            // 
            // TransferSelfMainButton
            // 
            TransferSelfMainButton.Alpha = 20;
            TransferSelfMainButton.BackColor = System.Drawing.Color.Transparent;
            TransferSelfMainButton.Background = true;
            TransferSelfMainButton.Background_WidthPen = 4F;
            TransferSelfMainButton.BackgroundPen = true;
            TransferSelfMainButton.ColorBackground = System.Drawing.Color.Transparent;
            TransferSelfMainButton.ColorBackground_1 = System.Drawing.Color.White;
            TransferSelfMainButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            TransferSelfMainButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            TransferSelfMainButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            TransferSelfMainButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            TransferSelfMainButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            TransferSelfMainButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            TransferSelfMainButton.Effect_1 = true;
            TransferSelfMainButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            TransferSelfMainButton.Effect_1_Transparency = 25;
            TransferSelfMainButton.Effect_2 = true;
            TransferSelfMainButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            TransferSelfMainButton.Effect_2_Transparency = 20;
            TransferSelfMainButton.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            TransferSelfMainButton.ForeColor = System.Drawing.Color.Tan;
            TransferSelfMainButton.Lighting = false;
            TransferSelfMainButton.LinearGradient_Background = false;
            TransferSelfMainButton.LinearGradientPen = false;
            TransferSelfMainButton.Location = new System.Drawing.Point(77, 73);
            TransferSelfMainButton.Name = "TransferSelfMainButton";
            TransferSelfMainButton.PenWidth = 15;
            TransferSelfMainButton.Rounding = true;
            TransferSelfMainButton.RoundingInt = 20;
            TransferSelfMainButton.Size = new System.Drawing.Size(241, 83);
            TransferSelfMainButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            TransferSelfMainButton.TabIndex = 95;
            TransferSelfMainButton.Tag = "Cyber";
            TransferSelfMainButton.TextButton = "Перевод себе";
            TransferSelfMainButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            TransferSelfMainButton.Timer_Effect_1 = 5;
            TransferSelfMainButton.Timer_RGB = 300;
            TransferSelfMainButton.Click += TransferSelfMainButton_Click;
            // 
            // TransferMainButton
            // 
            TransferMainButton.Alpha = 20;
            TransferMainButton.BackColor = System.Drawing.Color.Transparent;
            TransferMainButton.Background = true;
            TransferMainButton.Background_WidthPen = 4F;
            TransferMainButton.BackgroundPen = true;
            TransferMainButton.ColorBackground = System.Drawing.Color.Transparent;
            TransferMainButton.ColorBackground_1 = System.Drawing.Color.White;
            TransferMainButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            TransferMainButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            TransferMainButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            TransferMainButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            TransferMainButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            TransferMainButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            TransferMainButton.Effect_1 = true;
            TransferMainButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            TransferMainButton.Effect_1_Transparency = 25;
            TransferMainButton.Effect_2 = true;
            TransferMainButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            TransferMainButton.Effect_2_Transparency = 20;
            TransferMainButton.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            TransferMainButton.ForeColor = System.Drawing.Color.Tan;
            TransferMainButton.Lighting = false;
            TransferMainButton.LinearGradient_Background = false;
            TransferMainButton.LinearGradientPen = false;
            TransferMainButton.Location = new System.Drawing.Point(456, 73);
            TransferMainButton.Name = "TransferMainButton";
            TransferMainButton.PenWidth = 15;
            TransferMainButton.Rounding = true;
            TransferMainButton.RoundingInt = 20;
            TransferMainButton.Size = new System.Drawing.Size(241, 83);
            TransferMainButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            TransferMainButton.TabIndex = 101;
            TransferMainButton.Tag = "Cyber";
            TransferMainButton.TextButton = "Перевод на другую карту / счет";
            TransferMainButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            TransferMainButton.Timer_Effect_1 = 5;
            TransferMainButton.Timer_RGB = 300;
            TransferMainButton.Click += TransferMainButton_Click;
            // 
            // spaceSeparatorHorizontal1
            // 
            spaceSeparatorHorizontal1.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            spaceSeparatorHorizontal1.Font = new System.Drawing.Font("Verdana", 8F);
            spaceSeparatorHorizontal1.Image = null;
            spaceSeparatorHorizontal1.Location = new System.Drawing.Point(66, 175);
            spaceSeparatorHorizontal1.Name = "spaceSeparatorHorizontal1";
            spaceSeparatorHorizontal1.NoRounding = false;
            spaceSeparatorHorizontal1.Size = new System.Drawing.Size(650, 4);
            spaceSeparatorHorizontal1.TabIndex = 108;
            spaceSeparatorHorizontal1.Text = "spaceSeparatorHorizontal1";
            spaceSeparatorHorizontal1.Transparent = false;
            // 
            // spaceSeparatorHorizontal2
            // 
            spaceSeparatorHorizontal2.Customization = "Kioq/yoqKv8jIyP/Kioq/w==";
            spaceSeparatorHorizontal2.Font = new System.Drawing.Font("Verdana", 8F);
            spaceSeparatorHorizontal2.Image = null;
            spaceSeparatorHorizontal2.Location = new System.Drawing.Point(66, 749);
            spaceSeparatorHorizontal2.Name = "spaceSeparatorHorizontal2";
            spaceSeparatorHorizontal2.NoRounding = false;
            spaceSeparatorHorizontal2.Size = new System.Drawing.Size(650, 4);
            spaceSeparatorHorizontal2.TabIndex = 109;
            spaceSeparatorHorizontal2.Text = "spaceSeparatorHorizontal2";
            spaceSeparatorHorizontal2.Transparent = false;
            // 
            // PanelSelf
            // 
            PanelSelf.BackColor = System.Drawing.Color.FromArgb(55, 55, 55);
            PanelSelf.Controls.Add(Amount);
            PanelSelf.Controls.Add(InfoTransferText);
            PanelSelf.Controls.Add(panel3);
            PanelSelf.Controls.Add(TransferSelfButton);
            PanelSelf.Controls.Add(bigLabel1);
            PanelSelf.Controls.Add(InfoText);
            PanelSelf.Controls.Add(bigLabel2);
            PanelSelf.EdgeColor = System.Drawing.Color.FromArgb(31, 31, 31);
            PanelSelf.Location = new System.Drawing.Point(77, 185);
            PanelSelf.Name = "PanelSelf";
            PanelSelf.Padding = new System.Windows.Forms.Padding(5);
            PanelSelf.Size = new System.Drawing.Size(620, 539);
            PanelSelf.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            PanelSelf.TabIndex = 100;
            PanelSelf.Tag = "30; 30; 30";
            PanelSelf.Text = "panel2";
            PanelSelf.Visible = false;
            // 
            // TransferSelfButton
            // 
            TransferSelfButton.Alpha = 20;
            TransferSelfButton.BackColor = System.Drawing.Color.Transparent;
            TransferSelfButton.Background = true;
            TransferSelfButton.Background_WidthPen = 4F;
            TransferSelfButton.BackgroundPen = true;
            TransferSelfButton.ColorBackground = System.Drawing.Color.Transparent;
            TransferSelfButton.ColorBackground_1 = System.Drawing.Color.White;
            TransferSelfButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            TransferSelfButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            TransferSelfButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            TransferSelfButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            TransferSelfButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            TransferSelfButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            TransferSelfButton.Effect_1 = true;
            TransferSelfButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            TransferSelfButton.Effect_1_Transparency = 25;
            TransferSelfButton.Effect_2 = true;
            TransferSelfButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            TransferSelfButton.Effect_2_Transparency = 20;
            TransferSelfButton.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            TransferSelfButton.ForeColor = System.Drawing.Color.Tan;
            TransferSelfButton.Lighting = false;
            TransferSelfButton.LinearGradient_Background = false;
            TransferSelfButton.LinearGradientPen = false;
            TransferSelfButton.Location = new System.Drawing.Point(15, 448);
            TransferSelfButton.Name = "TransferSelfButton";
            TransferSelfButton.PenWidth = 15;
            TransferSelfButton.Rounding = true;
            TransferSelfButton.RoundingInt = 20;
            TransferSelfButton.Size = new System.Drawing.Size(241, 83);
            TransferSelfButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            TransferSelfButton.TabIndex = 102;
            TransferSelfButton.Tag = "Cyber";
            TransferSelfButton.TextButton = "Перевести";
            TransferSelfButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            TransferSelfButton.Timer_Effect_1 = 5;
            TransferSelfButton.Timer_RGB = 300;
            TransferSelfButton.Click += TransferSelfButton_Click;
            // 
            // InfoText
            // 
            InfoText.AutoSize = true;
            InfoText.BackColor = System.Drawing.Color.Transparent;
            InfoText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            InfoText.ForeColor = System.Drawing.Color.Red;
            InfoText.Location = new System.Drawing.Point(15, 318);
            InfoText.Name = "InfoText";
            InfoText.Size = new System.Drawing.Size(48, 21);
            InfoText.TabIndex = 105;
            InfoText.Text = "Текст";
            InfoText.Visible = false;
            // 
            // Amount
            // 
            Amount.Location = new System.Drawing.Point(15, 270);
            Amount.Name = "Amount";
            Amount.Size = new System.Drawing.Size(580, 27);
            Amount.TabIndex = 109;
            Amount.KeyUp += Amount_TextChanged;
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel2.ForeColor = System.Drawing.Color.Tan;
            bigLabel2.Location = new System.Drawing.Point(15, 218);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(211, 32);
            bigLabel2.TabIndex = 103;
            bigLabel2.Text = "Сумма перевода:";
            // 
            // InfoTransferText
            // 
            InfoTransferText.AutoSize = true;
            InfoTransferText.BackColor = System.Drawing.Color.Transparent;
            InfoTransferText.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            InfoTransferText.ForeColor = System.Drawing.Color.Tan;
            InfoTransferText.Location = new System.Drawing.Point(15, 138);
            InfoTransferText.Name = "InfoTransferText";
            InfoTransferText.Size = new System.Drawing.Size(48, 21);
            InfoTransferText.TabIndex = 108;
            InfoTransferText.Text = "Текст";
            InfoTransferText.Visible = false;
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel1.ForeColor = System.Drawing.Color.Tan;
            bigLabel1.Location = new System.Drawing.Point(10, 5);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(272, 32);
            bigLabel1.TabIndex = 106;
            bigLabel1.Text = "Выберите карту / счет:";
            // 
            // panel3
            // 
            panel3.BackColor = System.Drawing.Color.Linen;
            panel3.Controls.Add(BoxTarget);
            panel3.EdgeColor = System.Drawing.Color.FromArgb(32, 41, 50);
            panel3.Location = new System.Drawing.Point(15, 70);
            panel3.Name = "panel3";
            panel3.Padding = new System.Windows.Forms.Padding(5);
            panel3.Size = new System.Drawing.Size(580, 49);
            panel3.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            panel3.TabIndex = 107;
            panel3.Text = "panel3";
            // 
            // BoxTarget
            // 
            BoxTarget.AutoResize = false;
            BoxTarget.BackColor = System.Drawing.Color.FromArgb(255, 255, 255);
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
            BoxTarget.Location = new System.Drawing.Point(0, 0);
            BoxTarget.MaxDropDownItems = 4;
            BoxTarget.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            BoxTarget.Name = "BoxTarget";
            BoxTarget.Size = new System.Drawing.Size(580, 49);
            BoxTarget.StartIndex = 0;
            BoxTarget.TabIndex = 0;
            // 
            // TransferWindow
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(752, 768);
            Controls.Add(spaceSeparatorHorizontal2);
            Controls.Add(spaceSeparatorHorizontal1);
            Controls.Add(TransferMainButton);
            Controls.Add(TransferSelfMainButton);
            Controls.Add(PanelSelf);
            Controls.Add(bigLabel3);
            Controls.Add(panel1);
            Controls.Add(CloseButton);
            Name = "TransferWindow";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            Load += TransferWindow_Load;
            PanelSelf.ResumeLayout(false);
            PanelSelf.PerformLayout();
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.CyberButton TransferSelfMainButton;
        private ReaLTaiizor.Controls.CyberButton TransferMainButton;
        private ReaLTaiizor.Controls.SpaceSeparatorHorizontal spaceSeparatorHorizontal1;
        private ReaLTaiizor.Controls.SpaceSeparatorHorizontal spaceSeparatorHorizontal2;
        private ReaLTaiizor.Controls.Panel PanelSelf;
        private System.Windows.Forms.TextBox Amount;
        private ReaLTaiizor.Controls.BigLabel InfoTransferText;
        private ReaLTaiizor.Controls.Panel panel3;
        private ReaLTaiizor.Controls.MaterialComboBox BoxTarget;
        private ReaLTaiizor.Controls.CyberButton TransferSelfButton;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.BigLabel InfoText;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
    }
}