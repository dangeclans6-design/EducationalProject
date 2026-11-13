namespace BankApplication
{
    partial class CardItem
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            NameTarget = new ReaLTaiizor.Controls.BigLabel();
            panel1 = new ReaLTaiizor.Controls.Panel();
            StatusTarget = new ReaLTaiizor.Controls.BigLabel();
            BlockButton = new ReaLTaiizor.Controls.Button();
            RequisitesButton = new ReaLTaiizor.Controls.Button();
            TransferMoneyButton = new ReaLTaiizor.Controls.Button();
            PayButton = new ReaLTaiizor.Controls.Button();
            ReplenishButton = new ReaLTaiizor.Controls.Button();
            BalanceTarget = new ReaLTaiizor.Controls.BigLabel();
            NumberTarget = new ReaLTaiizor.Controls.BigLabel();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // NameTarget
            // 
            NameTarget.AutoSize = true;
            NameTarget.BackColor = System.Drawing.Color.Transparent;
            NameTarget.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            NameTarget.ForeColor = System.Drawing.Color.Linen;
            NameTarget.Location = new System.Drawing.Point(6, 4);
            NameTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            NameTarget.Name = "NameTarget";
            NameTarget.Size = new System.Drawing.Size(68, 30);
            NameTarget.TabIndex = 0;
            NameTarget.Text = "Карта";
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
            panel1.Controls.Add(StatusTarget);
            panel1.Controls.Add(BlockButton);
            panel1.Controls.Add(RequisitesButton);
            panel1.Controls.Add(TransferMoneyButton);
            panel1.Controls.Add(PayButton);
            panel1.Controls.Add(ReplenishButton);
            panel1.Controls.Add(BalanceTarget);
            panel1.Controls.Add(NumberTarget);
            panel1.Controls.Add(NameTarget);
            panel1.EdgeColor = System.Drawing.Color.FromArgb(32, 41, 50);
            panel1.Location = new System.Drawing.Point(-6, -4);
            panel1.Margin = new System.Windows.Forms.Padding(2);
            panel1.Name = "panel1";
            panel1.Padding = new System.Windows.Forms.Padding(4);
            panel1.Size = new System.Drawing.Size(526, 124);
            panel1.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            panel1.TabIndex = 1;
            panel1.Text = "panel1";
            // 
            // StatusTarget
            // 
            StatusTarget.AutoSize = true;
            StatusTarget.BackColor = System.Drawing.Color.Transparent;
            StatusTarget.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            StatusTarget.ForeColor = System.Drawing.Color.Linen;
            StatusTarget.Location = new System.Drawing.Point(312, 45);
            StatusTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            StatusTarget.Name = "StatusTarget";
            StatusTarget.Size = new System.Drawing.Size(75, 30);
            StatusTarget.TabIndex = 8;
            StatusTarget.Text = "Статус";
            // 
            // BlockButton
            // 
            BlockButton.BackColor = System.Drawing.Color.DimGray;
            BlockButton.BorderColor = System.Drawing.Color.FromArgb(32, 34, 37);
            BlockButton.EnteredBorderColor = System.Drawing.Color.Maroon;
            BlockButton.EnteredColor = System.Drawing.Color.FromArgb(32, 34, 37);
            BlockButton.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            BlockButton.Image = null;
            BlockButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            BlockButton.InactiveColor = System.Drawing.Color.FromArgb(32, 34, 37);
            BlockButton.Location = new System.Drawing.Point(396, 88);
            BlockButton.Margin = new System.Windows.Forms.Padding(2);
            BlockButton.Name = "BlockButton";
            BlockButton.PressedBorderColor = System.Drawing.Color.Maroon;
            BlockButton.PressedColor = System.Drawing.Color.Maroon;
            BlockButton.Size = new System.Drawing.Size(115, 30);
            BlockButton.TabIndex = 7;
            BlockButton.Text = "Заблокировать";
            BlockButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // RequisitesButton
            // 
            RequisitesButton.BackColor = System.Drawing.Color.DimGray;
            RequisitesButton.BorderColor = System.Drawing.Color.FromArgb(32, 34, 37);
            RequisitesButton.EnteredBorderColor = System.Drawing.Color.LightGray;
            RequisitesButton.EnteredColor = System.Drawing.Color.FromArgb(32, 34, 37);
            RequisitesButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            RequisitesButton.Image = null;
            RequisitesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            RequisitesButton.InactiveColor = System.Drawing.Color.FromArgb(32, 34, 37);
            RequisitesButton.Location = new System.Drawing.Point(299, 88);
            RequisitesButton.Margin = new System.Windows.Forms.Padding(2);
            RequisitesButton.Name = "RequisitesButton";
            RequisitesButton.PressedBorderColor = System.Drawing.Color.LightGray;
            RequisitesButton.PressedColor = System.Drawing.Color.LightGray;
            RequisitesButton.Size = new System.Drawing.Size(93, 30);
            RequisitesButton.TabIndex = 6;
            RequisitesButton.Text = "Реквизиты";
            RequisitesButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // TransferMoneyButton
            // 
            TransferMoneyButton.BackColor = System.Drawing.Color.DimGray;
            TransferMoneyButton.BorderColor = System.Drawing.Color.FromArgb(32, 34, 37);
            TransferMoneyButton.EnteredBorderColor = System.Drawing.Color.Tan;
            TransferMoneyButton.EnteredColor = System.Drawing.Color.FromArgb(32, 34, 37);
            TransferMoneyButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            TransferMoneyButton.Image = null;
            TransferMoneyButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            TransferMoneyButton.InactiveColor = System.Drawing.Color.FromArgb(32, 34, 37);
            TransferMoneyButton.Location = new System.Drawing.Point(199, 88);
            TransferMoneyButton.Margin = new System.Windows.Forms.Padding(2);
            TransferMoneyButton.Name = "TransferMoneyButton";
            TransferMoneyButton.PressedBorderColor = System.Drawing.Color.Tan;
            TransferMoneyButton.PressedColor = System.Drawing.Color.Tan;
            TransferMoneyButton.Size = new System.Drawing.Size(96, 30);
            TransferMoneyButton.TabIndex = 5;
            TransferMoneyButton.Text = "Перевести";
            TransferMoneyButton.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // PayButton
            // 
            PayButton.BackColor = System.Drawing.Color.DimGray;
            PayButton.BorderColor = System.Drawing.Color.FromArgb(32, 34, 37);
            PayButton.EnteredBorderColor = System.Drawing.Color.Tan;
            PayButton.EnteredColor = System.Drawing.Color.FromArgb(32, 34, 37);
            PayButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            PayButton.Image = null;
            PayButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            PayButton.InactiveColor = System.Drawing.Color.FromArgb(32, 34, 37);
            PayButton.Location = new System.Drawing.Point(113, 88);
            PayButton.Margin = new System.Windows.Forms.Padding(2);
            PayButton.Name = "PayButton";
            PayButton.PressedBorderColor = System.Drawing.Color.Tan;
            PayButton.PressedColor = System.Drawing.Color.Tan;
            PayButton.Size = new System.Drawing.Size(82, 30);
            PayButton.TabIndex = 4;
            PayButton.Text = "Оплатить";
            PayButton.TextAlignment = System.Drawing.StringAlignment.Center;
            PayButton.Click += PayButton_Click;
            // 
            // ReplenishButton
            // 
            ReplenishButton.BackColor = System.Drawing.Color.DimGray;
            ReplenishButton.BorderColor = System.Drawing.Color.FromArgb(32, 34, 37);
            ReplenishButton.EnteredBorderColor = System.Drawing.Color.Tan;
            ReplenishButton.EnteredColor = System.Drawing.Color.FromArgb(32, 34, 37);
            ReplenishButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            ReplenishButton.Image = null;
            ReplenishButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            ReplenishButton.InactiveColor = System.Drawing.Color.FromArgb(32, 34, 37);
            ReplenishButton.Location = new System.Drawing.Point(15, 88);
            ReplenishButton.Margin = new System.Windows.Forms.Padding(2);
            ReplenishButton.Name = "ReplenishButton";
            ReplenishButton.PressedBorderColor = System.Drawing.Color.Tan;
            ReplenishButton.PressedColor = System.Drawing.Color.Tan;
            ReplenishButton.Size = new System.Drawing.Size(94, 30);
            ReplenishButton.TabIndex = 3;
            ReplenishButton.Text = "Пополнить";
            ReplenishButton.TextAlignment = System.Drawing.StringAlignment.Center;
            ReplenishButton.Click += ReplenishButton_Click;
            // 
            // BalanceTarget
            // 
            BalanceTarget.AutoSize = true;
            BalanceTarget.BackColor = System.Drawing.Color.Transparent;
            BalanceTarget.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            BalanceTarget.ForeColor = System.Drawing.Color.Linen;
            BalanceTarget.Location = new System.Drawing.Point(312, 4);
            BalanceTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            BalanceTarget.Name = "BalanceTarget";
            BalanceTarget.Size = new System.Drawing.Size(80, 30);
            BalanceTarget.TabIndex = 2;
            BalanceTarget.Text = "Баланс";
            // 
            // NumberTarget
            // 
            NumberTarget.AutoSize = true;
            NumberTarget.BackColor = System.Drawing.Color.Transparent;
            NumberTarget.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            NumberTarget.ForeColor = System.Drawing.Color.Linen;
            NumberTarget.Location = new System.Drawing.Point(6, 34);
            NumberTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            NumberTarget.Name = "NumberTarget";
            NumberTarget.Size = new System.Drawing.Size(89, 30);
            NumberTarget.TabIndex = 1;
            NumberTarget.Text = "Номер: ";
            // 
            // CardItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(panel1);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "CardItem";
            Size = new System.Drawing.Size(520, 120);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.BigLabel BalanceTarget;
        private ReaLTaiizor.Controls.Panel panel1;
        private ReaLTaiizor.Controls.BigLabel NumberTarget;
        private ReaLTaiizor.Controls.BigLabel NameTarget;
        private ReaLTaiizor.Controls.Button BlockButton;
        private ReaLTaiizor.Controls.Button RequisitesButton;
        private ReaLTaiizor.Controls.Button TransferMoneyButton;
        private ReaLTaiizor.Controls.Button ReplenishButton;
        private ReaLTaiizor.Controls.Button PayButton;
        private ReaLTaiizor.Controls.BigLabel StatusTarget;
    }
}
