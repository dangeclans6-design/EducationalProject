namespace BankApplication.Windows
{
    partial class ShowNoti
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
            MainLabel = new ReaLTaiizor.Controls.BigLabel();
            CloseButton = new ReaLTaiizor.Controls.DungeonButtonLeft();
            Comment = new ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit();
            Text1 = new ReaLTaiizor.Controls.BigLabel();
            bigLabel1 = new ReaLTaiizor.Controls.BigLabel();
            DateAnswer = new ReaLTaiizor.Controls.BigLabel();
            SuspendLayout();
            // 
            // MainLabel
            // 
            MainLabel.AutoSize = true;
            MainLabel.BackColor = System.Drawing.Color.Transparent;
            MainLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
            MainLabel.ForeColor = System.Drawing.Color.Azure;
            MainLabel.Location = new System.Drawing.Point(40, 3);
            MainLabel.Name = "MainLabel";
            MainLabel.Size = new System.Drawing.Size(302, 37);
            MainLabel.TabIndex = 13;
            MainLabel.Text = "Уведомление о *******";
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
            CloseButton.Location = new System.Drawing.Point(841, 11);
            CloseButton.Name = "CloseButton";
            CloseButton.PressedColorA = System.Drawing.Color.FromArgb(226, 226, 226);
            CloseButton.PressedColorB = System.Drawing.Color.FromArgb(237, 237, 237);
            CloseButton.PressedContourColorA = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.PressedContourColorB = System.Drawing.Color.FromArgb(167, 167, 167);
            CloseButton.Size = new System.Drawing.Size(30, 29);
            CloseButton.TabIndex = 12;
            CloseButton.Text = "X";
            CloseButton.TextAlignment = System.Drawing.StringAlignment.Center;
            CloseButton.Click += CloseButton_Click;
            // 
            // Comment
            // 
            Comment.AnimateReadOnly = true;
            Comment.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            Comment.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal;
            Comment.Depth = 0;
            Comment.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
            Comment.HideSelection = true;
            Comment.Location = new System.Drawing.Point(15, 103);
            Comment.MaxLength = 32767;
            Comment.MouseState = ReaLTaiizor.Helper.MaterialDrawHelper.MaterialMouseState.OUT;
            Comment.Name = "Comment";
            Comment.PasswordChar = '\0';
            Comment.ReadOnly = true;
            Comment.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            Comment.SelectedText = "";
            Comment.SelectionLength = 0;
            Comment.SelectionStart = 0;
            Comment.ShortcutsEnabled = true;
            Comment.Size = new System.Drawing.Size(842, 253);
            Comment.TabIndex = 64;
            Comment.TabStop = false;
            Comment.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            Comment.UseSystemPasswordChar = false;
            // 
            // Text1
            // 
            Text1.AutoSize = true;
            Text1.BackColor = System.Drawing.Color.Transparent;
            Text1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            Text1.ForeColor = System.Drawing.Color.Tan;
            Text1.Location = new System.Drawing.Point(15, 70);
            Text1.Name = "Text1";
            Text1.Size = new System.Drawing.Size(327, 30);
            Text1.TabIndex = 63;
            Text1.Text = "Комментарий администратора:";
            Text1.Click += Text1_Click;
            // 
            // bigLabel1
            // 
            bigLabel1.AutoSize = true;
            bigLabel1.BackColor = System.Drawing.Color.Transparent;
            bigLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            bigLabel1.ForeColor = System.Drawing.Color.Linen;
            bigLabel1.Location = new System.Drawing.Point(15, 392);
            bigLabel1.Name = "bigLabel1";
            bigLabel1.Size = new System.Drawing.Size(135, 30);
            bigLabel1.TabIndex = 65;
            bigLabel1.Text = "Дата ответа:";
            // 
            // DateAnswer
            // 
            DateAnswer.AutoSize = true;
            DateAnswer.BackColor = System.Drawing.Color.Transparent;
            DateAnswer.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold);
            DateAnswer.ForeColor = System.Drawing.Color.Tan;
            DateAnswer.Location = new System.Drawing.Point(156, 392);
            DateAnswer.Name = "DateAnswer";
            DateAnswer.Size = new System.Drawing.Size(56, 30);
            DateAnswer.TabIndex = 66;
            DateAnswer.Text = "дата";
            // 
            // ShowNoti
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackgroundColor = System.Drawing.Color.FromArgb(30, 30, 30);
            ClientSize = new System.Drawing.Size(881, 442);
            Controls.Add(DateAnswer);
            Controls.Add(bigLabel1);
            Controls.Add(Comment);
            Controls.Add(Text1);
            Controls.Add(MainLabel);
            Controls.Add(CloseButton);
            Name = "ShowNoti";
            SmallLineColor1 = System.Drawing.Color.Transparent;
            SmallLineColor2 = System.Drawing.Color.Transparent;
            Style = ReaLTaiizor.Enum.Metro.Style.Dark;
            TextColor = System.Drawing.Color.White;
            ThemeName = "MetroDark";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ReaLTaiizor.Controls.BigLabel MainLabel;
        private ReaLTaiizor.Controls.DungeonButtonLeft CloseButton;
        private ReaLTaiizor.Controls.MaterialMultiLineTextBoxEdit Comment;
        private ReaLTaiizor.Controls.BigLabel Text1;
        private ReaLTaiizor.Controls.BigLabel bigLabel1;
        private ReaLTaiizor.Controls.BigLabel DateAnswer;
    }
}