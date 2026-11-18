namespace BankApplication
{
    partial class Requisites
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
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            MainLabel = new ReaLTaiizor.Controls.BigLabel();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            RequisitesCard = new ReaLTaiizor.Controls.Panel();
            bigLabel2 = new ReaLTaiizor.Controls.BigLabel();
            RequisitesCard.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-7, -6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(47, 470);
            panel1.TabIndex = 99;
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
            CloseButton.Location = new System.Drawing.Point(535, 9);
            CloseButton.Name = "CloseButton";
            CloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.Size = new System.Drawing.Size(25, 25);
            CloseButton.TabIndex = 100;
            CloseButton.Text = "X";
            CloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // MainLabel
            // 
            MainLabel.AutoSize = true;
            MainLabel.BackColor = System.Drawing.Color.Transparent;
            MainLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            MainLabel.ForeColor = System.Drawing.Color.Azure;
            MainLabel.Location = new System.Drawing.Point(58, -6);
            MainLabel.Name = "MainLabel";
            MainLabel.Size = new System.Drawing.Size(160, 40);
            MainLabel.TabIndex = 101;
            MainLabel.Text = "Реквизиты";
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel1.ForeColor = System.Drawing.Color.Tan;
            bigLabel1.Location = new System.Drawing.Point(1, 6);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(91, 32);
            bigLabel1.TabIndex = 107;
            bigLabel1.Text = "Номер";
            // 
            // RequisitesCard
            // 
            RequisitesCard.BackColor = System.Drawing.Color.FromArgb(39, 51, 63);
            RequisitesCard.Controls.Add(bigLabel2);
            RequisitesCard.Controls.Add(bigLabel1);
            RequisitesCard.EdgeColor = System.Drawing.Color.FromArgb(32, 41, 50);
            RequisitesCard.Location = new System.Drawing.Point(57, 64);
            RequisitesCard.Name = "RequisitesCard";
            RequisitesCard.Padding = new System.Windows.Forms.Padding(5);
            RequisitesCard.Size = new System.Drawing.Size(503, 371);
            RequisitesCard.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            RequisitesCard.TabIndex = 108;
            RequisitesCard.Text = "panel2";
            // 
            // bigLabel2
            // 
            bigLabel2.AutoSize = true;
            bigLabel2.BackColor = System.Drawing.Color.Transparent;
            bigLabel2.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            bigLabel2.ForeColor = System.Drawing.Color.Tan;
            bigLabel2.Location = new System.Drawing.Point(206, 169);
            bigLabel2.Name = "bigLabel2";
            bigLabel2.Size = new System.Drawing.Size(91, 32);
            bigLabel2.TabIndex = 108;
            bigLabel2.Text = "Номер";
            // 
            // Requisites
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(568, 450);
            Controls.Add(RequisitesCard);
            Controls.Add(MainLabel);
            Controls.Add(CloseButton);
            Controls.Add(panel1);
            Name = "Requisites";
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            Text = "v";
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            Load += Requisites_Load;
            RequisitesCard.ResumeLayout(false);
            RequisitesCard.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private ReaLTaiizor.Controls.BigLabel MainLabel;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.Panel RequisitesCard;
        private ReaLTaiizor.Controls.BigLabel bigLabel2;
    }
}