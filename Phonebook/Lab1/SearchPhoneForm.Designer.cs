namespace Lab1
{
    partial class SearchPhoneForm
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
            this.PhoneT = new System.Windows.Forms.MaskedTextBox();
            this.ResultsTextBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // PhoneT
            // 
            this.PhoneT.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PhoneT.Location = new System.Drawing.Point(257, 68);
            this.PhoneT.Margin = new System.Windows.Forms.Padding(6);
            this.PhoneT.Mask = "(999) 000-0000";
            this.PhoneT.Name = "PhoneT";
            this.PhoneT.Size = new System.Drawing.Size(380, 44);
            this.PhoneT.TabIndex = 25;
            // 
            // ResultsTextBox
            // 
            this.ResultsTextBox.Location = new System.Drawing.Point(82, 402);
            this.ResultsTextBox.Multiline = true;
            this.ResultsTextBox.Name = "ResultsTextBox";
            this.ResultsTextBox.ReadOnly = true;
            this.ResultsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ResultsTextBox.Size = new System.Drawing.Size(555, 275);
            this.ResultsTextBox.TabIndex = 17;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(162, 316);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(370, 50);
            this.SearchButton.TabIndex = 24;
            this.SearchButton.Text = "Поиск";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(46, 75);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 37);
            this.label1.TabIndex = 18;
            this.label1.Text = "Телефон";
            // 
            // SearchPhoneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 740);
            this.Controls.Add(this.PhoneT);
            this.Controls.Add(this.ResultsTextBox);
            this.Controls.Add(this.SearchButton);
            this.Controls.Add(this.label1);
            this.Name = "SearchPhoneForm";
            this.Text = "SearchPhoneForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox ResultsTextBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox PhoneT;
    }
}