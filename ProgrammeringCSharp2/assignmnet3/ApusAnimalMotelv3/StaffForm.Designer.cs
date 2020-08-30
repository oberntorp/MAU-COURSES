namespace ApusAnimalMotel
{
    partial class StaffForm
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
            this.NameOfStaffTextBox = new System.Windows.Forms.TextBox();
            this.NameOfStaffLabel = new System.Windows.Forms.Label();
            this.QualificationsGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteQualificationButton = new System.Windows.Forms.Button();
            this.ChangeQualificationButton = new System.Windows.Forms.Button();
            this.AddQualificationButton = new System.Windows.Forms.Button();
            this.AddedQualificationsListBox = new System.Windows.Forms.ListBox();
            this.QualificationOfStaffLabel = new System.Windows.Forms.Label();
            this.NameOfQualificationOfStaffTextBox = new System.Windows.Forms.TextBox();
            this.AddStaffButton = new System.Windows.Forms.Button();
            this.CancelStaffButton = new System.Windows.Forms.Button();
            this.QualificationsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // NameOfStaffTextBox
            // 
            this.NameOfStaffTextBox.Location = new System.Drawing.Point(68, 25);
            this.NameOfStaffTextBox.Name = "NameOfStaffTextBox";
            this.NameOfStaffTextBox.Size = new System.Drawing.Size(531, 20);
            this.NameOfStaffTextBox.TabIndex = 0;
            // 
            // NameOfStaffLabel
            // 
            this.NameOfStaffLabel.AutoSize = true;
            this.NameOfStaffLabel.Location = new System.Drawing.Point(12, 25);
            this.NameOfStaffLabel.Name = "NameOfStaffLabel";
            this.NameOfStaffLabel.Size = new System.Drawing.Size(35, 13);
            this.NameOfStaffLabel.TabIndex = 1;
            this.NameOfStaffLabel.Text = "Name";
            // 
            // QualificationsGroupBox
            // 
            this.QualificationsGroupBox.Controls.Add(this.DeleteQualificationButton);
            this.QualificationsGroupBox.Controls.Add(this.ChangeQualificationButton);
            this.QualificationsGroupBox.Controls.Add(this.AddQualificationButton);
            this.QualificationsGroupBox.Controls.Add(this.AddedQualificationsListBox);
            this.QualificationsGroupBox.Controls.Add(this.QualificationOfStaffLabel);
            this.QualificationsGroupBox.Controls.Add(this.NameOfQualificationOfStaffTextBox);
            this.QualificationsGroupBox.Location = new System.Drawing.Point(15, 84);
            this.QualificationsGroupBox.Name = "QualificationsGroupBox";
            this.QualificationsGroupBox.Size = new System.Drawing.Size(693, 285);
            this.QualificationsGroupBox.TabIndex = 2;
            this.QualificationsGroupBox.TabStop = false;
            this.QualificationsGroupBox.Text = "Qualifications";
            // 
            // DeleteQualificationButton
            // 
            this.DeleteQualificationButton.Location = new System.Drawing.Point(466, 235);
            this.DeleteQualificationButton.Name = "DeleteQualificationButton";
            this.DeleteQualificationButton.Size = new System.Drawing.Size(146, 23);
            this.DeleteQualificationButton.TabIndex = 7;
            this.DeleteQualificationButton.Text = "Delete qualification";
            this.DeleteQualificationButton.UseVisualStyleBackColor = true;
            this.DeleteQualificationButton.Click += new System.EventHandler(this.DeleteQualificationButton_Click);
            // 
            // ChangeQualificationButton
            // 
            this.ChangeQualificationButton.Location = new System.Drawing.Point(281, 235);
            this.ChangeQualificationButton.Name = "ChangeQualificationButton";
            this.ChangeQualificationButton.Size = new System.Drawing.Size(146, 23);
            this.ChangeQualificationButton.TabIndex = 6;
            this.ChangeQualificationButton.Text = "Change qualification";
            this.ChangeQualificationButton.UseVisualStyleBackColor = true;
            this.ChangeQualificationButton.Click += new System.EventHandler(this.ChangeQualificationButton_Click);
            // 
            // AddQualificationButton
            // 
            this.AddQualificationButton.Location = new System.Drawing.Point(100, 235);
            this.AddQualificationButton.Name = "AddQualificationButton";
            this.AddQualificationButton.Size = new System.Drawing.Size(146, 23);
            this.AddQualificationButton.TabIndex = 5;
            this.AddQualificationButton.Text = "Add qualification";
            this.AddQualificationButton.UseVisualStyleBackColor = true;
            this.AddQualificationButton.Click += new System.EventHandler(this.AddQualificationButton_Click);
            // 
            // AddedQualificationsListBox
            // 
            this.AddedQualificationsListBox.FormattingEnabled = true;
            this.AddedQualificationsListBox.Location = new System.Drawing.Point(100, 82);
            this.AddedQualificationsListBox.Name = "AddedQualificationsListBox";
            this.AddedQualificationsListBox.Size = new System.Drawing.Size(512, 147);
            this.AddedQualificationsListBox.TabIndex = 4;
            // 
            // QualificationOfStaffLabel
            // 
            this.QualificationOfStaffLabel.AutoSize = true;
            this.QualificationOfStaffLabel.Location = new System.Drawing.Point(16, 42);
            this.QualificationOfStaffLabel.Name = "QualificationOfStaffLabel";
            this.QualificationOfStaffLabel.Size = new System.Drawing.Size(65, 13);
            this.QualificationOfStaffLabel.TabIndex = 3;
            this.QualificationOfStaffLabel.Text = "Qualification";
            // 
            // NameOfQualificationOfStaffTextBox
            // 
            this.NameOfQualificationOfStaffTextBox.Location = new System.Drawing.Point(100, 39);
            this.NameOfQualificationOfStaffTextBox.Name = "NameOfQualificationOfStaffTextBox";
            this.NameOfQualificationOfStaffTextBox.Size = new System.Drawing.Size(512, 20);
            this.NameOfQualificationOfStaffTextBox.TabIndex = 2;
            // 
            // AddStaffButton
            // 
            this.AddStaffButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddStaffButton.Location = new System.Drawing.Point(115, 399);
            this.AddStaffButton.Name = "AddStaffButton";
            this.AddStaffButton.Size = new System.Drawing.Size(146, 23);
            this.AddStaffButton.TabIndex = 6;
            this.AddStaffButton.Text = "Add new staff";
            this.AddStaffButton.UseVisualStyleBackColor = true;
            this.AddStaffButton.Click += new System.EventHandler(this.AddStaffButton_Click);
            // 
            // CancelStaffButton
            // 
            this.CancelStaffButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelStaffButton.Location = new System.Drawing.Point(477, 399);
            this.CancelStaffButton.Name = "CancelStaffButton";
            this.CancelStaffButton.Size = new System.Drawing.Size(146, 23);
            this.CancelStaffButton.TabIndex = 7;
            this.CancelStaffButton.Text = "Cancel new staff";
            this.CancelStaffButton.UseVisualStyleBackColor = true;
            this.CancelStaffButton.Click += new System.EventHandler(this.CancelStaffButton_Click);
            // 
            // StaffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 450);
            this.Controls.Add(this.CancelStaffButton);
            this.Controls.Add(this.AddStaffButton);
            this.Controls.Add(this.QualificationsGroupBox);
            this.Controls.Add(this.NameOfStaffLabel);
            this.Controls.Add(this.NameOfStaffTextBox);
            this.Name = "StaffForm";
            this.Text = "Staff Planning";
            this.QualificationsGroupBox.ResumeLayout(false);
            this.QualificationsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox NameOfStaffTextBox;
        private System.Windows.Forms.Label NameOfStaffLabel;
        private System.Windows.Forms.GroupBox QualificationsGroupBox;
        private System.Windows.Forms.Label QualificationOfStaffLabel;
        private System.Windows.Forms.TextBox NameOfQualificationOfStaffTextBox;
        private System.Windows.Forms.Button AddQualificationButton;
        private System.Windows.Forms.ListBox AddedQualificationsListBox;
        private System.Windows.Forms.Button AddStaffButton;
        private System.Windows.Forms.Button CancelStaffButton;
        private System.Windows.Forms.Button DeleteQualificationButton;
        private System.Windows.Forms.Button ChangeQualificationButton;
    }
}