namespace FC12.SupportExtensions
{
    partial class SupportMessageForm
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
            this.SendButton = new System.Windows.Forms.Button();
            this.PriorityLowRadioButton = new System.Windows.Forms.RadioButton();
            this.PriorityMediumRadioButton = new System.Windows.Forms.RadioButton();
            this.PriorityHighRadioButton = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.PriorityUrgentRadioButton = new System.Windows.Forms.RadioButton();
            this.CategoriesGroup = new System.Windows.Forms.GroupBox();
            this.CategoryAnotherCheckBox = new System.Windows.Forms.CheckBox();
            this.CategoryAttributeCheckBox = new System.Windows.Forms.CheckBox();
            this.CategoryExceptionCheckBox = new System.Windows.Forms.CheckBox();
            this.CategoryLayOutCheckBox = new System.Windows.Forms.CheckBox();
            this.CommentLabel = new System.Windows.Forms.Label();
            this.CommentValueTextBox = new System.Windows.Forms.TextBox();
            this.BatchNameLabel = new System.Windows.Forms.Label();
            this.BatchNameValueLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.CategoriesGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendButton
            // 
            this.SendButton.BackColor = System.Drawing.SystemColors.Control;
            this.SendButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SendButton.Location = new System.Drawing.Point(18, 409);
            this.SendButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(733, 74);
            this.SendButton.TabIndex = 0;
            this.SendButton.Text = "Отправить запрос в техническую поддержку";
            this.SendButton.UseVisualStyleBackColor = false;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // PriorityLowRadioButton
            // 
            this.PriorityLowRadioButton.AutoSize = true;
            this.PriorityLowRadioButton.Checked = true;
            this.PriorityLowRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PriorityLowRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.PriorityLowRadioButton.Location = new System.Drawing.Point(8, 37);
            this.PriorityLowRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PriorityLowRadioButton.Name = "PriorityLowRadioButton";
            this.PriorityLowRadioButton.Size = new System.Drawing.Size(74, 21);
            this.PriorityLowRadioButton.TabIndex = 3;
            this.PriorityLowRadioButton.TabStop = true;
            this.PriorityLowRadioButton.Text = "Низкий";
            this.PriorityLowRadioButton.UseVisualStyleBackColor = true;
            this.PriorityLowRadioButton.CheckedChanged += new System.EventHandler(this.PriorityLowRadioButton_CheckedChanged);
            // 
            // PriorityMediumRadioButton
            // 
            this.PriorityMediumRadioButton.AutoSize = true;
            this.PriorityMediumRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PriorityMediumRadioButton.ForeColor = System.Drawing.Color.Goldenrod;
            this.PriorityMediumRadioButton.Location = new System.Drawing.Point(8, 66);
            this.PriorityMediumRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PriorityMediumRadioButton.Name = "PriorityMediumRadioButton";
            this.PriorityMediumRadioButton.Size = new System.Drawing.Size(83, 21);
            this.PriorityMediumRadioButton.TabIndex = 4;
            this.PriorityMediumRadioButton.Text = "Средний";
            this.PriorityMediumRadioButton.UseVisualStyleBackColor = true;
            this.PriorityMediumRadioButton.CheckedChanged += new System.EventHandler(this.PriorityMediumRadioButton_CheckedChanged);
            // 
            // PriorityHighRadioButton
            // 
            this.PriorityHighRadioButton.AutoSize = true;
            this.PriorityHighRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PriorityHighRadioButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.PriorityHighRadioButton.Location = new System.Drawing.Point(8, 97);
            this.PriorityHighRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PriorityHighRadioButton.Name = "PriorityHighRadioButton";
            this.PriorityHighRadioButton.Size = new System.Drawing.Size(83, 21);
            this.PriorityHighRadioButton.TabIndex = 5;
            this.PriorityHighRadioButton.Text = "Высокий";
            this.PriorityHighRadioButton.UseVisualStyleBackColor = true;
            this.PriorityHighRadioButton.CheckedChanged += new System.EventHandler(this.PriorityHighRadioButton_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.PriorityUrgentRadioButton);
            this.groupBox1.Controls.Add(this.PriorityHighRadioButton);
            this.groupBox1.Controls.Add(this.PriorityLowRadioButton);
            this.groupBox1.Controls.Add(this.PriorityMediumRadioButton);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(18, 18);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(129, 163);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Приоритет";
            // 
            // PriorityUrgentRadioButton
            // 
            this.PriorityUrgentRadioButton.AutoSize = true;
            this.PriorityUrgentRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PriorityUrgentRadioButton.ForeColor = System.Drawing.Color.Maroon;
            this.PriorityUrgentRadioButton.Location = new System.Drawing.Point(8, 128);
            this.PriorityUrgentRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PriorityUrgentRadioButton.Name = "PriorityUrgentRadioButton";
            this.PriorityUrgentRadioButton.Size = new System.Drawing.Size(85, 21);
            this.PriorityUrgentRadioButton.TabIndex = 7;
            this.PriorityUrgentRadioButton.Text = "Срочный";
            this.PriorityUrgentRadioButton.UseVisualStyleBackColor = true;
            this.PriorityUrgentRadioButton.CheckedChanged += new System.EventHandler(this.PriorityUrgentRadioButton_CheckedChanged);
            // 
            // CategoriesGroup
            // 
            this.CategoriesGroup.Controls.Add(this.CategoryAnotherCheckBox);
            this.CategoriesGroup.Controls.Add(this.CategoryAttributeCheckBox);
            this.CategoriesGroup.Controls.Add(this.CategoryExceptionCheckBox);
            this.CategoriesGroup.Controls.Add(this.CategoryLayOutCheckBox);
            this.CategoriesGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoriesGroup.Location = new System.Drawing.Point(155, 18);
            this.CategoriesGroup.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CategoriesGroup.Name = "CategoriesGroup";
            this.CategoriesGroup.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CategoriesGroup.Size = new System.Drawing.Size(200, 163);
            this.CategoriesGroup.TabIndex = 7;
            this.CategoriesGroup.TabStop = false;
            this.CategoriesGroup.Text = "Категории проблемы";
            // 
            // CategoryAnotherCheckBox
            // 
            this.CategoryAnotherCheckBox.AutoSize = true;
            this.CategoryAnotherCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryAnotherCheckBox.Location = new System.Drawing.Point(10, 128);
            this.CategoryAnotherCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CategoryAnotherCheckBox.Name = "CategoryAnotherCheckBox";
            this.CategoryAnotherCheckBox.Size = new System.Drawing.Size(74, 21);
            this.CategoryAnotherCheckBox.TabIndex = 9;
            this.CategoryAnotherCheckBox.Text = "Другое";
            this.CategoryAnotherCheckBox.UseVisualStyleBackColor = true;
            this.CategoryAnotherCheckBox.CheckedChanged += new System.EventHandler(this.CategoryAnotherCheckBox_CheckedChanged);
            // 
            // CategoryAttributeCheckBox
            // 
            this.CategoryAttributeCheckBox.AutoSize = true;
            this.CategoryAttributeCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryAttributeCheckBox.Location = new System.Drawing.Point(10, 66);
            this.CategoryAttributeCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CategoryAttributeCheckBox.Name = "CategoryAttributeCheckBox";
            this.CategoryAttributeCheckBox.Size = new System.Drawing.Size(186, 21);
            this.CategoryAttributeCheckBox.TabIndex = 9;
            this.CategoryAttributeCheckBox.Text = "Извлечение аттрибутов";
            this.CategoryAttributeCheckBox.UseVisualStyleBackColor = true;
            this.CategoryAttributeCheckBox.CheckedChanged += new System.EventHandler(this.CategoryAttributeCheckBox_CheckedChanged);
            // 
            // CategoryExceptionCheckBox
            // 
            this.CategoryExceptionCheckBox.AutoSize = true;
            this.CategoryExceptionCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryExceptionCheckBox.Location = new System.Drawing.Point(10, 97);
            this.CategoryExceptionCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CategoryExceptionCheckBox.Name = "CategoryExceptionCheckBox";
            this.CategoryExceptionCheckBox.Size = new System.Drawing.Size(109, 21);
            this.CategoryExceptionCheckBox.TabIndex = 8;
            this.CategoryExceptionCheckBox.Text = "Исключение";
            this.CategoryExceptionCheckBox.UseVisualStyleBackColor = true;
            this.CategoryExceptionCheckBox.CheckedChanged += new System.EventHandler(this.CategoryExceptionCheckBox_CheckedChanged);
            // 
            // CategoryLayOutCheckBox
            // 
            this.CategoryLayOutCheckBox.AutoSize = true;
            this.CategoryLayOutCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CategoryLayOutCheckBox.Location = new System.Drawing.Point(10, 37);
            this.CategoryLayOutCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.CategoryLayOutCheckBox.Name = "CategoryLayOutCheckBox";
            this.CategoryLayOutCheckBox.Size = new System.Drawing.Size(172, 21);
            this.CategoryLayOutCheckBox.TabIndex = 0;
            this.CategoryLayOutCheckBox.Text = "Наложение шаблонов";
            this.CategoryLayOutCheckBox.UseVisualStyleBackColor = true;
            this.CategoryLayOutCheckBox.CheckedChanged += new System.EventHandler(this.CategoryLayOutCheckBox_CheckedChanged);
            // 
            // CommentLabel
            // 
            this.CommentLabel.AutoSize = true;
            this.CommentLabel.Location = new System.Drawing.Point(14, 213);
            this.CommentLabel.Name = "CommentLabel";
            this.CommentLabel.Size = new System.Drawing.Size(113, 20);
            this.CommentLabel.TabIndex = 9;
            this.CommentLabel.Text = "Комментарий";
            // 
            // CommentValueTextBox
            // 
            this.CommentValueTextBox.Location = new System.Drawing.Point(18, 236);
            this.CommentValueTextBox.Multiline = true;
            this.CommentValueTextBox.Name = "CommentValueTextBox";
            this.CommentValueTextBox.Size = new System.Drawing.Size(733, 155);
            this.CommentValueTextBox.TabIndex = 10;
            this.CommentValueTextBox.TextChanged += new System.EventHandler(this.CommentValueTextBox_TextChanged);
            // 
            // BatchNameLabel
            // 
            this.BatchNameLabel.AutoSize = true;
            this.BatchNameLabel.Location = new System.Drawing.Point(384, 18);
            this.BatchNameLabel.Name = "BatchNameLabel";
            this.BatchNameLabel.Size = new System.Drawing.Size(179, 20);
            this.BatchNameLabel.TabIndex = 11;
            this.BatchNameLabel.Text = "Наименование пакета";
            // 
            // BatchNameValueLabel
            // 
            this.BatchNameValueLabel.AutoSize = true;
            this.BatchNameValueLabel.Location = new System.Drawing.Point(388, 55);
            this.BatchNameValueLabel.Name = "BatchNameValueLabel";
            this.BatchNameValueLabel.Size = new System.Drawing.Size(106, 20);
            this.BatchNameValueLabel.TabIndex = 12;
            this.BatchNameValueLabel.Text = "(ИмяПакета)";
            // 
            // SupportMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 504);
            this.Controls.Add(this.BatchNameValueLabel);
            this.Controls.Add(this.BatchNameLabel);
            this.Controls.Add(this.CommentValueTextBox);
            this.Controls.Add(this.CommentLabel);
            this.Controls.Add(this.CategoriesGroup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SendButton);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SupportMessageForm";
            this.Text = "Запрос в техническую поддержку";
            this.Load += new System.EventHandler(this.SupportMessageForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.CategoriesGroup.ResumeLayout(false);
            this.CategoriesGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SendButton;
        private System.Windows.Forms.RadioButton PriorityLowRadioButton;
        private System.Windows.Forms.RadioButton PriorityMediumRadioButton;
        private System.Windows.Forms.RadioButton PriorityHighRadioButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton PriorityUrgentRadioButton;
        private System.Windows.Forms.GroupBox CategoriesGroup;
        private System.Windows.Forms.CheckBox CategoryExceptionCheckBox;
        private System.Windows.Forms.CheckBox CategoryLayOutCheckBox;
        private System.Windows.Forms.CheckBox CategoryAnotherCheckBox;
        private System.Windows.Forms.CheckBox CategoryAttributeCheckBox;
        private System.Windows.Forms.Label CommentLabel;
        private System.Windows.Forms.TextBox CommentValueTextBox;
        private System.Windows.Forms.Label BatchNameLabel;
        private System.Windows.Forms.Label BatchNameValueLabel;
    }
}