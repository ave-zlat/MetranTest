namespace testApp
{
    partial class Form1
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
            this.btnStartTest = new System.Windows.Forms.Button();
            this.btnStopTest = new System.Windows.Forms.Button();
            this.lblTxtIdentifier = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbTestSelection = new System.Windows.Forms.ComboBox();
            this.txtIdentifier = new System.Windows.Forms.TextBox();
            this.lblChooseTest = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStartTest
            // 
            this.btnStartTest.Location = new System.Drawing.Point(27, 213);
            this.btnStartTest.Name = "btnStartTest";
            this.btnStartTest.Size = new System.Drawing.Size(139, 48);
            this.btnStartTest.TabIndex = 0;
            this.btnStartTest.Text = "Запуск теста";
            this.btnStartTest.UseVisualStyleBackColor = true;
            this.btnStartTest.Click += new System.EventHandler(this.btnStartTest_Click);
            // 
            // btnStopTest
            // 
            this.btnStopTest.Location = new System.Drawing.Point(227, 213);
            this.btnStopTest.Name = "btnStopTest";
            this.btnStopTest.Size = new System.Drawing.Size(155, 58);
            this.btnStopTest.TabIndex = 1;
            this.btnStopTest.Text = "Остановка теста";
            this.btnStopTest.UseVisualStyleBackColor = true;
            this.btnStopTest.Click += new System.EventHandler(this.btnStopTest_Click);
            // 
            // lblTxtIdentifier
            // 
            this.lblTxtIdentifier.AutoSize = true;
            this.lblTxtIdentifier.Location = new System.Drawing.Point(12, 32);
            this.lblTxtIdentifier.Name = "lblTxtIdentifier";
            this.lblTxtIdentifier.Size = new System.Drawing.Size(228, 16);
            this.lblTxtIdentifier.TabIndex = 2;
            this.lblTxtIdentifier.Text = "Введите идентификатор изделия";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(87, 305);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 16);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Статус";
            // 
            // cmbTestSelection
            // 
            this.cmbTestSelection.FormattingEnabled = true;
            this.cmbTestSelection.Location = new System.Drawing.Point(15, 150);
            this.cmbTestSelection.Name = "cmbTestSelection";
            this.cmbTestSelection.Size = new System.Drawing.Size(241, 24);
            this.cmbTestSelection.TabIndex = 4;
            // 
            // txtIdentifier
            // 
            this.txtIdentifier.Location = new System.Drawing.Point(27, 62);
            this.txtIdentifier.Name = "txtIdentifier";
            this.txtIdentifier.Size = new System.Drawing.Size(173, 22);
            this.txtIdentifier.TabIndex = 5;
            // 
            // lblChooseTest
            // 
            this.lblChooseTest.AutoSize = true;
            this.lblChooseTest.Location = new System.Drawing.Point(52, 110);
            this.lblChooseTest.Name = "lblChooseTest";
            this.lblChooseTest.Size = new System.Drawing.Size(104, 16);
            this.lblChooseTest.TabIndex = 6;
            this.lblChooseTest.Text = "Выберите тест";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblChooseTest);
            this.Controls.Add(this.txtIdentifier);
            this.Controls.Add(this.cmbTestSelection);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblTxtIdentifier);
            this.Controls.Add(this.btnStopTest);
            this.Controls.Add(this.btnStartTest);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartTest;
        private System.Windows.Forms.Button btnStopTest;
        private System.Windows.Forms.Label lblTxtIdentifier;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbTestSelection;
        private System.Windows.Forms.TextBox txtIdentifier;
        private System.Windows.Forms.Label lblChooseTest;
    }
}

