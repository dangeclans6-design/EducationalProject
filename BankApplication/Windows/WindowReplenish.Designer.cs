namespace BankApplication
{
    partial class WindowReplenish
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
            BoxSource = new ReaLTaiizor.Controls.MaterialComboBox();
            panel1 = new System.Windows.Forms.Panel();
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            Amount = new ReaLTaiizor.Controls.DungeonRichTextBox();
            ReplenishButton = new ReaLTaiizor.Controls.CyberButton();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            SuspendLayout();
            // 
            // BoxSource
            // 
            BoxSource.AutoResize = false;
            BoxSource.BackColor = System.Drawing.Color.Tan;
            BoxSource.Depth = 0;
            BoxSource.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            BoxSource.DropDownHeight = 174;
            BoxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            BoxSource.DropDownWidth = 121;
            BoxSource.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            BoxSource.ForeColor = System.Drawing.Color.FromArgb(222, 0, 0, 0);
            BoxSource.FormattingEnabled = true;
            BoxSource.IntegralHeight = false;
            BoxSource.ItemHeight = 43;
            BoxSource.Location = new System.Drawing.Point(54, 127);
            BoxSource.MaxDropDownItems = 4;
            BoxSource.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            BoxSource.Name = "BoxSource";
            BoxSource.Size = new System.Drawing.Size(605, 49);
            BoxSource.StartIndex = 0;
            BoxSource.TabIndex = 0;
            BoxSource.SelectedIndexChanged += BoxSource_SelectedIndexChanged;
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-6, -6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(54, 504);
            panel1.TabIndex = 12;
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
            CloseButton.Location = new System.Drawing.Point(649, 4);
            CloseButton.Name = "CloseButton";
            CloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.Size = new System.Drawing.Size(25, 25);
            CloseButton.TabIndex = 11;
            CloseButton.Text = "X";
            CloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseButton.Click += CloseButton_Click;
            // 
            // Amount
            // 
            Amount.AutoWordSelection = false;
            Amount.BackColor = System.Drawing.Color.Transparent;
            Amount.BorderColor = System.Drawing.Color.FromArgb(180, 180, 180);
            Amount.EdgeColor = System.Drawing.Color.White;
            Amount.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            Amount.ForeColor = System.Drawing.Color.Black;
            Amount.Location = new System.Drawing.Point(54, 318);
            Amount.Name = "Amount";
            Amount.ReadOnly = false;
            Amount.Size = new System.Drawing.Size(605, 36);
            Amount.TabIndex = 87;
            Amount.TextBackColor = System.Drawing.Color.White;
            Amount.WordWrap = true;
            // 
            // ReplenishButton
            // 
            ReplenishButton.Alpha = 20;
            ReplenishButton.BackColor = System.Drawing.Color.Transparent;
            ReplenishButton.Background = true;
            ReplenishButton.Background_WidthPen = 4F;
            ReplenishButton.BackgroundPen = true;
            ReplenishButton.ColorBackground = System.Drawing.Color.Transparent;
            ReplenishButton.ColorBackground_1 = System.Drawing.Color.White;
            ReplenishButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ReplenishButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            ReplenishButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            ReplenishButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            ReplenishButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ReplenishButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            ReplenishButton.Effect_1 = true;
            ReplenishButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            ReplenishButton.Effect_1_Transparency = 25;
            ReplenishButton.Effect_2 = true;
            ReplenishButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            ReplenishButton.Effect_2_Transparency = 20;
            ReplenishButton.Font = new System.Drawing.Font("Segoe UI Semibold", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ReplenishButton.ForeColor = System.Drawing.Color.Tan;
            ReplenishButton.Lighting = false;
            ReplenishButton.LinearGradient_Background = false;
            ReplenishButton.LinearGradientPen = false;
            ReplenishButton.Location = new System.Drawing.Point(418, 382);
            ReplenishButton.Name = "ReplenishButton";
            ReplenishButton.PenWidth = 15;
            ReplenishButton.Rounding = true;
            ReplenishButton.RoundingInt = 20;
            ReplenishButton.Size = new System.Drawing.Size(241, 83);
            ReplenishButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ReplenishButton.TabIndex = 86;
            ReplenishButton.Tag = "Cyber";
            ReplenishButton.TextButton = "Пополнить";
            ReplenishButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            ReplenishButton.Timer_Effect_1 = 5;
            ReplenishButton.Timer_RGB = 300;
            ReplenishButton.Click += ReplenishButton_Click;
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(163, 4);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(406, 50);
            bigLabel3.TabIndex = 89;
            bigLabel3.Text = "Пополнение баланса";
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel1.ForeColor = System.Drawing.Color.Tan;
            bigLabel1.Location = new System.Drawing.Point(54, 80);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(610, 30);
            bigLabel1.TabIndex = 90;
            bigLabel1.Text = "Выберите карту (счет), с которой хотите пополнить баланс:";
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel2.ForeColor = System.Drawing.Color.Tan;
            bigLabel2.Location = new System.Drawing.Point(54, 269);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(334, 32);
            bigLabel2.TabIndex = 91;
            bigLabel2.Text = "Введите сумму пополнения:";
            // 
            // WindowReplenish
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(684, 479);
            Controls.Add(bigLabel2);
            Controls.Add(bigLabel1);
            Controls.Add(bigLabel3);
            Controls.Add(Amount);
            Controls.Add(ReplenishButton);
            Controls.Add(panel1);
            Controls.Add(CloseButton);
            Controls.Add(BoxSource);
            Name = "WindowReplenish";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.MaterialComboBox BoxSource;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private ReaLTaiizor.Controls.DungeonRichTextBox Amount;
        private ReaLTaiizor.Controls.CyberButton ReplenishButton;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
    }
}