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
            AccountNumber = new ReaLTaiizor.Controls.BigLabel();
            RequisitesCard = new ReaLTaiizor.Controls.Panel();
            OpenedAt = new ReaLTaiizor.Controls.BigLabel();
            Reason = new ReaLTaiizor.Controls.BigLabel();
            Status = new ReaLTaiizor.Controls.BigLabel();
            Currency = new ReaLTaiizor.Controls.BigLabel();
            CorAccount = new ReaLTaiizor.Controls.BigLabel();
            Bic = new ReaLTaiizor.Controls.BigLabel();
            RequisitesCard.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.Tan;
            panel1.Location = new System.Drawing.Point(-7, -6);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(47, 523);
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
            CloseButton.Click += CloseButton_Click;
            // 
            // MainLabel
            // 
            MainLabel.AutoSize = true;
            MainLabel.BackColor = System.Drawing.Color.Transparent;
            MainLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            MainLabel.ForeColor = System.Drawing.Color.Azure;
            MainLabel.Location = new System.Drawing.Point(57, 9);
            MainLabel.Name = "MainLabel";
            MainLabel.Size = new System.Drawing.Size(133, 32);
            MainLabel.TabIndex = 101;
            MainLabel.Text = "Реквизиты";
            // 
            // AccountNumber
            // 
            AccountNumber.AutoSize = true;
            AccountNumber.BackColor = System.Drawing.Color.Transparent;
            AccountNumber.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            AccountNumber.ForeColor = System.Drawing.Color.Tan;
            AccountNumber.Location = new System.Drawing.Point(1, 6);
            AccountNumber.Name = "AccountNumber";
            AccountNumber.Size = new System.Drawing.Size(199, 32);
            AccountNumber.TabIndex = 107;
            AccountNumber.Text = "Номер аккаунта";
            // 
            // RequisitesCard
            // 
            RequisitesCard.BackColor = System.Drawing.Color.FromArgb(30, 30, 30);
            RequisitesCard.Controls.Add(OpenedAt);
            RequisitesCard.Controls.Add(Reason);
            RequisitesCard.Controls.Add(Status);
            RequisitesCard.Controls.Add(Currency);
            RequisitesCard.Controls.Add(CorAccount);
            RequisitesCard.Controls.Add(Bic);
            RequisitesCard.Controls.Add(AccountNumber);
            RequisitesCard.EdgeColor = System.Drawing.Color.FromArgb(30, 30, 30);
            RequisitesCard.Location = new System.Drawing.Point(57, 64);
            RequisitesCard.Name = "RequisitesCard";
            RequisitesCard.Padding = new System.Windows.Forms.Padding(5);
            RequisitesCard.Size = new System.Drawing.Size(503, 434);
            RequisitesCard.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            RequisitesCard.TabIndex = 108;
            RequisitesCard.Text = "panel2";
            // 
            // OpenedAt
            // 
            OpenedAt.AutoSize = true;
            OpenedAt.BackColor = System.Drawing.Color.Transparent;
            OpenedAt.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            OpenedAt.ForeColor = System.Drawing.Color.Tan;
            OpenedAt.Location = new System.Drawing.Point(8, 397);
            OpenedAt.Name = "OpenedAt";
            OpenedAt.Size = new System.Drawing.Size(95, 32);
            OpenedAt.TabIndex = 115;
            OpenedAt.Text = "Создан";
            // 
            // Reason
            // 
            Reason.AutoSize = true;
            Reason.BackColor = System.Drawing.Color.Transparent;
            Reason.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Reason.ForeColor = System.Drawing.Color.Tan;
            Reason.Location = new System.Drawing.Point(1, 242);
            Reason.Name = "Reason";
            Reason.Size = new System.Drawing.Size(115, 32);
            Reason.TabIndex = 114;
            Reason.Text = "Причина";
            // 
            // Status
            // 
            Status.AutoSize = true;
            Status.BackColor = System.Drawing.Color.Transparent;
            Status.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Status.ForeColor = System.Drawing.Color.Tan;
            Status.Location = new System.Drawing.Point(1, 193);
            Status.Name = "Status";
            Status.Size = new System.Drawing.Size(85, 32);
            Status.TabIndex = 113;
            Status.Text = "Статус";
            // 
            // Currency
            // 
            Currency.AutoSize = true;
            Currency.BackColor = System.Drawing.Color.Transparent;
            Currency.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Currency.ForeColor = System.Drawing.Color.Tan;
            Currency.Location = new System.Drawing.Point(1, 146);
            Currency.Name = "Currency";
            Currency.Size = new System.Drawing.Size(98, 32);
            Currency.TabIndex = 111;
            Currency.Text = "Валюта";
            // 
            // CorAccount
            // 
            CorAccount.AutoSize = true;
            CorAccount.BackColor = System.Drawing.Color.Transparent;
            CorAccount.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            CorAccount.ForeColor = System.Drawing.Color.Tan;
            CorAccount.Location = new System.Drawing.Point(1, 100);
            CorAccount.Name = "CorAccount";
            CorAccount.Size = new System.Drawing.Size(171, 32);
            CorAccount.TabIndex = 109;
            CorAccount.Text = "Кор. аккаунта";
            // 
            // Bic
            // 
            Bic.AutoSize = true;
            Bic.BackColor = System.Drawing.Color.Transparent;
            Bic.Font = new System.Drawing.Font("Segoe UI Semibold", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            Bic.ForeColor = System.Drawing.Color.Tan;
            Bic.Location = new System.Drawing.Point(1, 54);
            Bic.Name = "Bic";
            Bic.Size = new System.Drawing.Size(55, 32);
            Bic.TabIndex = 108;
            Bic.Text = "Бик";
            // 
            // Requisites
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(568, 513);
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
        private ReaLTaiizor.Controls.BigLabel AccountNumber;
        private ReaLTaiizor.Controls.Panel RequisitesCard;
        private ReaLTaiizor.Controls.BigLabel Bic;
        private ReaLTaiizor.Controls.BigLabel OpenedAt;
        private ReaLTaiizor.Controls.BigLabel Reason;
        private ReaLTaiizor.Controls.BigLabel Status;
        private ReaLTaiizor.Controls.BigLabel Currency;
        private ReaLTaiizor.Controls.BigLabel CorAccount;
    }
}