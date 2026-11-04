namespace BankApplication
{
    partial class RequestExplore
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
            LabelForm = new ReaLTaiizor.Controls.BigLabel();
            panel1 = new System.Windows.Forms.Panel();
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            ApplyRequestButton = new ReaLTaiizor.Controls.CyberButton();
            Text1 = new ReaLTaiizor.Controls.BigLabel();
            Text2 = new ReaLTaiizor.Controls.BigLabel();
            Text3 = new ReaLTaiizor.Controls.BigLabel();
            Text6 = new ReaLTaiizor.Controls.BigLabel();
            Text4 = new ReaLTaiizor.Controls.BigLabel();
            Text5 = new ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel3 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel4 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel5 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel6 = new ReaLTaiizor.Controls.BigLabel();
            SuspendLayout();
            // 
            // LabelForm
            // 
            LabelForm.AutoSize = true;
            LabelForm.BackColor = System.Drawing.Color.Transparent;
            LabelForm.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            LabelForm.ForeColor = System.Drawing.Color.Azure;
            LabelForm.Location = new System.Drawing.Point(448, 4);
            LabelForm.Name = "LabelForm";
            LabelForm.Size = new System.Drawing.Size(176, 45);
            LabelForm.TabIndex = 14;
            LabelForm.Text = "Запрос #1";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-5, -6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(40, 793);
            panel1.TabIndex = 13;
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
            CloseButton.Location = new System.Drawing.Point(1079, 4);
            CloseButton.Name = "CloseButton";
            CloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.Size = new System.Drawing.Size(28, 27);
            CloseButton.TabIndex = 12;
            CloseButton.Text = "X";
            CloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseButton.Click += CloseButton_Click;
            // 
            // ApplyRequestButton
            // 
            ApplyRequestButton.Alpha = 20;
            ApplyRequestButton.BackColor = System.Drawing.Color.Transparent;
            ApplyRequestButton.Background = true;
            ApplyRequestButton.Background_WidthPen = 4F;
            ApplyRequestButton.BackgroundPen = true;
            ApplyRequestButton.ColorBackground = System.Drawing.Color.Transparent;
            ApplyRequestButton.ColorBackground_1 = System.Drawing.Color.White;
            ApplyRequestButton.ColorBackground_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ApplyRequestButton.ColorBackground_Pen = System.Drawing.Color.LightSlateGray;
            ApplyRequestButton.ColorLighting = System.Drawing.Color.FromArgb(255, 224, 192);
            ApplyRequestButton.ColorPen_1 = System.Drawing.Color.FromArgb(37, 52, 68);
            ApplyRequestButton.ColorPen_2 = System.Drawing.Color.FromArgb(41, 63, 86);
            ApplyRequestButton.CyberButtonStyle = ReaLTaiizor.Enum.Cyber.StateStyle.Custom;
            ApplyRequestButton.Effect_1 = true;
            ApplyRequestButton.Effect_1_ColorBackground = System.Drawing.Color.FromArgb(29, 200, 238);
            ApplyRequestButton.Effect_1_Transparency = 25;
            ApplyRequestButton.Effect_2 = true;
            ApplyRequestButton.Effect_2_ColorBackground = System.Drawing.Color.White;
            ApplyRequestButton.Effect_2_Transparency = 20;
            ApplyRequestButton.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            ApplyRequestButton.ForeColor = System.Drawing.Color.Tan;
            ApplyRequestButton.Lighting = false;
            ApplyRequestButton.LinearGradient_Background = false;
            ApplyRequestButton.LinearGradientPen = false;
            ApplyRequestButton.Location = new System.Drawing.Point(883, 689);
            ApplyRequestButton.Name = "ApplyRequestButton";
            ApplyRequestButton.PenWidth = 15;
            ApplyRequestButton.Rounding = true;
            ApplyRequestButton.RoundingInt = 20;
            ApplyRequestButton.Size = new System.Drawing.Size(224, 72);
            ApplyRequestButton.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            ApplyRequestButton.TabIndex = 43;
            ApplyRequestButton.Tag = "Cyber";
            ApplyRequestButton.TextButton = "Принять запрос";
            ApplyRequestButton.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            ApplyRequestButton.Timer_Effect_1 = 5;
            ApplyRequestButton.Timer_RGB = 300;
            ApplyRequestButton.Click += ApplyRequestButton_Click;
            // 
            // Text1
            // 
            Text1.AutoSize = true;
            Text1.BackColor = System.Drawing.Color.Transparent;
            Text1.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Text1.ForeColor = System.Drawing.Color.Tan;
            Text1.Location = new System.Drawing.Point(281, 86);
            Text1.Name = "Text1";
            Text1.Size = new System.Drawing.Size(150, 40);
            Text1.TabIndex = 44;
            Text1.Text = "Запрос #1";
            // 
            // Text2
            // 
            Text2.AutoSize = true;
            Text2.BackColor = System.Drawing.Color.Transparent;
            Text2.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Text2.ForeColor = System.Drawing.Color.Tan;
            Text2.Location = new System.Drawing.Point(281, 135);
            Text2.Name = "Text2";
            Text2.Size = new System.Drawing.Size(150, 40);
            Text2.TabIndex = 45;
            Text2.Text = "Запрос #1";
            // 
            // Text3
            // 
            Text3.AutoSize = true;
            Text3.BackColor = System.Drawing.Color.Transparent;
            Text3.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Text3.ForeColor = System.Drawing.Color.Tan;
            Text3.Location = new System.Drawing.Point(281, 187);
            Text3.Name = "Text3";
            Text3.Size = new System.Drawing.Size(150, 40);
            Text3.TabIndex = 46;
            Text3.Text = "Запрос #1";
            // 
            // Text6
            // 
            Text6.AutoSize = true;
            Text6.BackColor = System.Drawing.Color.Transparent;
            Text6.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Text6.ForeColor = System.Drawing.Color.Tan;
            Text6.Location = new System.Drawing.Point(234, 723);
            Text6.Name = "Text6";
            Text6.Size = new System.Drawing.Size(150, 40);
            Text6.TabIndex = 49;
            Text6.Text = "Запрос #1";
            // 
            // Text4
            // 
            Text4.AutoSize = true;
            Text4.BackColor = System.Drawing.Color.Transparent;
            Text4.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Text4.ForeColor = System.Drawing.Color.Tan;
            Text4.Location = new System.Drawing.Point(281, 239);
            Text4.Name = "Text4";
            Text4.Size = new System.Drawing.Size(150, 40);
            Text4.TabIndex = 47;
            Text4.Text = "Запрос #1";
            // 
            // Text5
            // 
            Text5.AnimateReadOnly = true;
            Text5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            Text5.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            Text5.Depth = 0;
            Text5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            Text5.HideSelection = true;
            Text5.Location = new System.Drawing.Point(41, 362);
            Text5.MaxLength = 32767;
            Text5.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            Text5.Name = "Text5";
            Text5.PasswordChar = '\0';
            Text5.ReadOnly = true;
            Text5.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            Text5.SelectedText = "";
            Text5.SelectionLength = 0;
            Text5.SelectionStart = 0;
            Text5.ShortcutsEnabled = true;
            Text5.Size = new System.Drawing.Size(1066, 313);
            Text5.TabIndex = 50;
            Text5.TabStop = false;
            Text5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            Text5.UseSystemPasswordChar = false;
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel1.ForeColor = System.Drawing.Color.Azure;
            bigLabel1.Location = new System.Drawing.Point(41, 319);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(327, 40);
            bigLabel1.TabIndex = 51;
            bigLabel1.Text = "Комментарий клиента:";
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel2.ForeColor = System.Drawing.Color.Azure;
            bigLabel2.Location = new System.Drawing.Point(42, 239);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(231, 40);
            bigLabel2.TabIndex = 55;
            bigLabel2.Text = "Номер клиента:";
            // 
            // bigLabel3
            // 
            bigLabel3.AutoSize = true;
            bigLabel3.BackColor = System.Drawing.Color.Transparent;
            bigLabel3.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel3.ForeColor = System.Drawing.Color.Azure;
            bigLabel3.Location = new System.Drawing.Point(42, 187);
            bigLabel3.Name = "bigLabel3";
            bigLabel3.Size = new System.Drawing.Size(233, 40);
            bigLabel3.TabIndex = 54;
            bigLabel3.Text = "Новый депозит:";
            // 
            // bigLabel4
            // 
            bigLabel4.AutoSize = true;
            bigLabel4.BackColor = System.Drawing.Color.Transparent;
            bigLabel4.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel4.ForeColor = System.Drawing.Color.Azure;
            bigLabel4.Location = new System.Drawing.Point(42, 135);
            bigLabel4.Name = "bigLabel4";
            bigLabel4.Size = new System.Drawing.Size(101, 40);
            bigLabel4.TabIndex = 53;
            bigLabel4.Text = "Карта:";
            // 
            // bigLabel5
            // 
            bigLabel5.AutoSize = true;
            bigLabel5.BackColor = System.Drawing.Color.Transparent;
            bigLabel5.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel5.ForeColor = System.Drawing.Color.Azure;
            bigLabel5.Location = new System.Drawing.Point(42, 86);
            bigLabel5.Name = "bigLabel5";
            bigLabel5.Size = new System.Drawing.Size(188, 40);
            bigLabel5.TabIndex = 52;
            bigLabel5.Text = "Тип запроса:";
            // 
            // bigLabel6
            // 
            bigLabel6.AutoSize = true;
            bigLabel6.BackColor = System.Drawing.Color.Transparent;
            bigLabel6.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel6.ForeColor = System.Drawing.Color.Azure;
            bigLabel6.Location = new System.Drawing.Point(42, 723);
            bigLabel6.Name = "bigLabel6";
            bigLabel6.Size = new System.Drawing.Size(186, 40);
            bigLabel6.TabIndex = 56;
            bigLabel6.Text = "Дата заявки:";
            // 
            // RequestExplore
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlDarkDark;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(1131, 776);
            Controls.Add(bigLabel6);
            Controls.Add(bigLabel2);
            Controls.Add(bigLabel3);
            Controls.Add(bigLabel4);
            Controls.Add(bigLabel5);
            Controls.Add(bigLabel1);
            Controls.Add(Text5);
            Controls.Add(Text6);
            Controls.Add(Text4);
            Controls.Add(Text3);
            Controls.Add(Text2);
            Controls.Add(Text1);
            Controls.Add(ApplyRequestButton);
            Controls.Add(LabelForm);
            Controls.Add(panel1);
            Controls.Add(CloseButton);
            Name = "RequestExplore";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            DoubleClick += RequestExplore_DoubleClick;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.BigLabel LabelForm;
        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private ReaLTaiizor.Controls.CyberButton ApplyRequestButton;
        private ReaLTaiizor.Controls.BigLabel Text1;
        private ReaLTaiizor.Controls.BigLabel Text2;
        private ReaLTaiizor.Controls.BigLabel Text3;
        private ReaLTaiizor.Controls.BigLabel Text6;
        private ReaLTaiizor.Controls.BigLabel Text4;
        private ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit Text5;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
        private ReaLTaiizor.Controls.BigLabel bigLabel3;
        private ReaLTaiizor.Controls.BigLabel bigLabel4;
        private ReaLTaiizor.Controls.BigLabel bigLabel5;
        private ReaLTaiizor.Controls.BigLabel bigLabel6;
    }
}