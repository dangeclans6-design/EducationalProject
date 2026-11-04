namespace BankApplication
{
    partial class ToPayDebt
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
            this.debt_info = new System.Windows.Forms.ListBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.debt_pay_amount = new System.Windows.Forms.NumericUpDown();
            this.replenish_text = new System.Windows.Forms.ListBox();
            this.pay = new System.Windows.Forms.Button();
            this.back = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.listBox3 = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.debt_pay_amount)).BeginInit();
            this.SuspendLayout();
            // 
            // debt_info
            // 
            this.debt_info.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.debt_info.FormattingEnabled = true;
            this.debt_info.ItemHeight = 36;
            this.debt_info.Location = new System.Drawing.Point(242, 22);
            this.debt_info.Name = "debt_info";
            this.debt_info.Size = new System.Drawing.Size(354, 40);
            this.debt_info.TabIndex = 20;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.ForeColor = System.Drawing.Color.Black;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 42;
            this.listBox1.Items.AddRange(new object[] {
            "Ваш долг:"});
            this.listBox1.Location = new System.Drawing.Point(46, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(190, 42);
            this.listBox1.TabIndex = 19;
            // 
            // debt_pay_amount
            // 
            this.debt_pay_amount.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.debt_pay_amount.Location = new System.Drawing.Point(296, 122);
            this.debt_pay_amount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.debt_pay_amount.Name = "debt_pay_amount";
            this.debt_pay_amount.Size = new System.Drawing.Size(300, 39);
            this.debt_pay_amount.TabIndex = 18;
            this.debt_pay_amount.Enter += new System.EventHandler(this.replenish_amount_Enter);
            this.debt_pay_amount.Leave += new System.EventHandler(this.debt_pay_amount_Leave);
            // 
            // replenish_text
            // 
            this.replenish_text.BackColor = System.Drawing.Color.WhiteSmoke;
            this.replenish_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.replenish_text.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.replenish_text.ForeColor = System.Drawing.Color.Black;
            this.replenish_text.FormattingEnabled = true;
            this.replenish_text.ItemHeight = 42;
            this.replenish_text.Items.AddRange(new object[] {
            "Введите сумму:"});
            this.replenish_text.Location = new System.Drawing.Point(24, 118);
            this.replenish_text.Name = "replenish_text";
            this.replenish_text.Size = new System.Drawing.Size(274, 42);
            this.replenish_text.TabIndex = 17;
            // 
            // pay
            // 
            this.pay.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.pay.Location = new System.Drawing.Point(466, 307);
            this.pay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pay.Name = "pay";
            this.pay.Size = new System.Drawing.Size(241, 81);
            this.pay.TabIndex = 16;
            this.pay.Text = "Оплатить";
            this.pay.UseVisualStyleBackColor = true;
            this.pay.Click += new System.EventHandler(this.pay_Click);
            // 
            // back
            // 
            this.back.Font = new System.Drawing.Font("Times New Roman", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.back.Location = new System.Drawing.Point(46, 307);
            this.back.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.back.Name = "back";
            this.back.Size = new System.Drawing.Size(241, 81);
            this.back.TabIndex = 15;
            this.back.Text = "Назад";
            this.back.UseVisualStyleBackColor = true;
            this.back.Click += new System.EventHandler(this.back_Click);
            // 
            // listBox2
            // 
            this.listBox2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox2.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox2.ForeColor = System.Drawing.Color.Black;
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 42;
            this.listBox2.Items.AddRange(new object[] {
            "рублей."});
            this.listBox2.Location = new System.Drawing.Point(602, 22);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(144, 42);
            this.listBox2.TabIndex = 21;
            // 
            // listBox3
            // 
            this.listBox3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox3.Font = new System.Drawing.Font("Times New Roman", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox3.ForeColor = System.Drawing.Color.Black;
            this.listBox3.FormattingEnabled = true;
            this.listBox3.ItemHeight = 42;
            this.listBox3.Items.AddRange(new object[] {
            "рублей."});
            this.listBox3.Location = new System.Drawing.Point(602, 122);
            this.listBox3.Name = "listBox3";
            this.listBox3.Size = new System.Drawing.Size(144, 42);
            this.listBox3.TabIndex = 22;
            // 
            // ToPayDebt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 416);
            this.Controls.Add(this.listBox3);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.debt_info);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.debt_pay_amount);
            this.Controls.Add(this.replenish_text);
            this.Controls.Add(this.pay);
            this.Controls.Add(this.back);
            this.Name = "ToPayDebt";
            this.Text = "Оплата задолженности";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ToPayDebt_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.debt_pay_amount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox debt_info;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.NumericUpDown debt_pay_amount;
        private System.Windows.Forms.ListBox replenish_text;
        private System.Windows.Forms.Button pay;
        private System.Windows.Forms.Button back;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.ListBox listBox3;
    }
}