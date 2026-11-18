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
            MainPanel = new ReaLTaiizor.Controls.Panel();
            StatusTarget = new ReaLTaiizor.Controls.BigLabel();
            BalanceTarget = new ReaLTaiizor.Controls.BigLabel();
            NumberTarget = new ReaLTaiizor.Controls.BigLabel();
            MainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // NameTarget
            // 
            NameTarget.AutoSize = true;
            NameTarget.BackColor = System.Drawing.Color.Transparent;
            NameTarget.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            NameTarget.ForeColor = System.Drawing.Color.Linen;
            NameTarget.Location = new System.Drawing.Point(6, 4);
            NameTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            NameTarget.Name = "NameTarget";
            NameTarget.Size = new System.Drawing.Size(62, 25);
            NameTarget.TabIndex = 0;
            NameTarget.Text = "Карта";
            // 
            // MainPanel
            // 
            MainPanel.BackColor = System.Drawing.SystemColors.WindowFrame;
            MainPanel.Controls.Add(StatusTarget);
            MainPanel.Controls.Add(BalanceTarget);
            MainPanel.Controls.Add(NumberTarget);
            MainPanel.Controls.Add(NameTarget);
            MainPanel.EdgeColor = System.Drawing.Color.FromArgb(32, 41, 50);
            MainPanel.Location = new System.Drawing.Point(0, 0);
            MainPanel.Margin = new System.Windows.Forms.Padding(2);
            MainPanel.Name = "MainPanel";
            MainPanel.Padding = new System.Windows.Forms.Padding(4);
            MainPanel.Size = new System.Drawing.Size(461, 120);
            MainPanel.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            MainPanel.TabIndex = 1;
            MainPanel.Text = "panel1";
            // 
            // StatusTarget
            // 
            StatusTarget.AutoSize = true;
            StatusTarget.BackColor = System.Drawing.Color.Transparent;
            StatusTarget.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            StatusTarget.ForeColor = System.Drawing.Color.Linen;
            StatusTarget.Location = new System.Drawing.Point(352, 34);
            StatusTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            StatusTarget.Name = "StatusTarget";
            StatusTarget.Size = new System.Drawing.Size(68, 25);
            StatusTarget.TabIndex = 8;
            StatusTarget.Text = "Статус";
            // 
            // BalanceTarget
            // 
            BalanceTarget.AutoSize = true;
            BalanceTarget.BackColor = System.Drawing.Color.Transparent;
            BalanceTarget.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            BalanceTarget.ForeColor = System.Drawing.Color.Linen;
            BalanceTarget.Location = new System.Drawing.Point(264, 4);
            BalanceTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            BalanceTarget.Name = "BalanceTarget";
            BalanceTarget.Size = new System.Drawing.Size(73, 25);
            BalanceTarget.TabIndex = 2;
            BalanceTarget.Text = "Баланс";
            // 
            // NumberTarget
            // 
            NumberTarget.AutoSize = true;
            NumberTarget.BackColor = System.Drawing.Color.Transparent;
            NumberTarget.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            NumberTarget.ForeColor = System.Drawing.Color.Linen;
            NumberTarget.Location = new System.Drawing.Point(6, 34);
            NumberTarget.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            NumberTarget.Name = "NumberTarget";
            NumberTarget.Size = new System.Drawing.Size(79, 25);
            NumberTarget.TabIndex = 1;
            NumberTarget.Text = "Номер: ";
            // 
            // CardItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(MainPanel);
            Margin = new System.Windows.Forms.Padding(2);
            Name = "CardItem";
            Size = new System.Drawing.Size(461, 120);
            MainPanel.ResumeLayout(false);
            MainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ReaLTaiizor.Controls.BigLabel BalanceTarget;
        private ReaLTaiizor.Controls.Panel MainPanel;
        private ReaLTaiizor.Controls.BigLabel NumberTarget;
        private ReaLTaiizor.Controls.BigLabel NameTarget;
        private ReaLTaiizor.Controls.BigLabel StatusTarget;
    }
}
