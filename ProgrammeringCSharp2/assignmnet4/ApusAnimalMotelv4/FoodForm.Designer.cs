namespace ApusAnimalMotel
{
    partial class FoodForm
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
            this.qualificationsGroupBox = new System.Windows.Forms.GroupBox();
            this.DeleteIngredientButton = new System.Windows.Forms.Button();
            this.ChangeIngredientButton = new System.Windows.Forms.Button();
            this.AddIngredientButton = new System.Windows.Forms.Button();
            this.AddedIngredientsListBox = new System.Windows.Forms.ListBox();
            this.NameOfIngredientLabel = new System.Windows.Forms.Label();
            this.NameOfIngredientTextBox = new System.Windows.Forms.TextBox();
            this.NameOfRecipeLabel = new System.Windows.Forms.Label();
            this.NameOfRecipeTextBox = new System.Windows.Forms.TextBox();
            this.CancelRecipeButton = new System.Windows.Forms.Button();
            this.AddRecipeButton = new System.Windows.Forms.Button();
            this.qualificationsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // qualificationsGroupBox
            // 
            this.qualificationsGroupBox.Controls.Add(this.DeleteIngredientButton);
            this.qualificationsGroupBox.Controls.Add(this.ChangeIngredientButton);
            this.qualificationsGroupBox.Controls.Add(this.AddIngredientButton);
            this.qualificationsGroupBox.Controls.Add(this.AddedIngredientsListBox);
            this.qualificationsGroupBox.Controls.Add(this.NameOfIngredientLabel);
            this.qualificationsGroupBox.Controls.Add(this.NameOfIngredientTextBox);
            this.qualificationsGroupBox.Location = new System.Drawing.Point(19, 86);
            this.qualificationsGroupBox.Name = "qualificationsGroupBox";
            this.qualificationsGroupBox.Size = new System.Drawing.Size(607, 285);
            this.qualificationsGroupBox.TabIndex = 10;
            this.qualificationsGroupBox.TabStop = false;
            this.qualificationsGroupBox.Text = "Qualifications";
            // 
            // DeleteIngredientButton
            // 
            this.DeleteIngredientButton.Location = new System.Drawing.Point(438, 244);
            this.DeleteIngredientButton.Name = "DeleteIngredientButton";
            this.DeleteIngredientButton.Size = new System.Drawing.Size(146, 23);
            this.DeleteIngredientButton.TabIndex = 14;
            this.DeleteIngredientButton.Text = "Delete ingredient";
            this.DeleteIngredientButton.UseVisualStyleBackColor = true;
            this.DeleteIngredientButton.Click += new System.EventHandler(this.DeleteIngredientButton_Click);
            // 
            // ChangeIngredientButton
            // 
            this.ChangeIngredientButton.Location = new System.Drawing.Point(251, 244);
            this.ChangeIngredientButton.Name = "ChangeIngredientButton";
            this.ChangeIngredientButton.Size = new System.Drawing.Size(146, 23);
            this.ChangeIngredientButton.TabIndex = 13;
            this.ChangeIngredientButton.Text = "Change ingredient";
            this.ChangeIngredientButton.UseVisualStyleBackColor = true;
            this.ChangeIngredientButton.Click += new System.EventHandler(this.ChangeIngredientButton_Click);
            // 
            // AddIngredientButton
            // 
            this.AddIngredientButton.Location = new System.Drawing.Point(72, 244);
            this.AddIngredientButton.Name = "AddIngredientButton";
            this.AddIngredientButton.Size = new System.Drawing.Size(146, 23);
            this.AddIngredientButton.TabIndex = 5;
            this.AddIngredientButton.Text = "Add ingredient";
            this.AddIngredientButton.UseVisualStyleBackColor = true;
            this.AddIngredientButton.Click += new System.EventHandler(this.AddIngredientButton_Click);
            // 
            // AddedIngredientsListBox
            // 
            this.AddedIngredientsListBox.FormattingEnabled = true;
            this.AddedIngredientsListBox.Location = new System.Drawing.Point(72, 82);
            this.AddedIngredientsListBox.Name = "AddedIngredientsListBox";
            this.AddedIngredientsListBox.Size = new System.Drawing.Size(512, 147);
            this.AddedIngredientsListBox.TabIndex = 4;
            // 
            // NameOfIngredientLabel
            // 
            this.NameOfIngredientLabel.AutoSize = true;
            this.NameOfIngredientLabel.Location = new System.Drawing.Point(16, 42);
            this.NameOfIngredientLabel.Name = "NameOfIngredientLabel";
            this.NameOfIngredientLabel.Size = new System.Drawing.Size(35, 13);
            this.NameOfIngredientLabel.TabIndex = 3;
            this.NameOfIngredientLabel.Text = "Name";
            // 
            // NameOfIngredientTextBox
            // 
            this.NameOfIngredientTextBox.Location = new System.Drawing.Point(72, 39);
            this.NameOfIngredientTextBox.Name = "NameOfIngredientTextBox";
            this.NameOfIngredientTextBox.Size = new System.Drawing.Size(512, 20);
            this.NameOfIngredientTextBox.TabIndex = 2;
            // 
            // NameOfRecipeLabel
            // 
            this.NameOfRecipeLabel.AutoSize = true;
            this.NameOfRecipeLabel.Location = new System.Drawing.Point(16, 27);
            this.NameOfRecipeLabel.Name = "NameOfRecipeLabel";
            this.NameOfRecipeLabel.Size = new System.Drawing.Size(35, 13);
            this.NameOfRecipeLabel.TabIndex = 9;
            this.NameOfRecipeLabel.Text = "Name";
            // 
            // NameOfRecipeTextBox
            // 
            this.NameOfRecipeTextBox.Location = new System.Drawing.Point(72, 27);
            this.NameOfRecipeTextBox.Name = "NameOfRecipeTextBox";
            this.NameOfRecipeTextBox.Size = new System.Drawing.Size(531, 20);
            this.NameOfRecipeTextBox.TabIndex = 8;
            // 
            // CancelRecipeButton
            // 
            this.CancelRecipeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelRecipeButton.Location = new System.Drawing.Point(457, 401);
            this.CancelRecipeButton.Name = "CancelRecipeButton";
            this.CancelRecipeButton.Size = new System.Drawing.Size(146, 23);
            this.CancelRecipeButton.TabIndex = 12;
            this.CancelRecipeButton.Text = "Cancel new recipe";
            this.CancelRecipeButton.UseVisualStyleBackColor = true;
            this.CancelRecipeButton.Click += new System.EventHandler(this.CancelRecipeButton_Click);
            // 
            // AddRecipeButton
            // 
            this.AddRecipeButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AddRecipeButton.Location = new System.Drawing.Point(91, 401);
            this.AddRecipeButton.Name = "AddRecipeButton";
            this.AddRecipeButton.Size = new System.Drawing.Size(146, 23);
            this.AddRecipeButton.TabIndex = 11;
            this.AddRecipeButton.Text = "Add new recipe";
            this.AddRecipeButton.UseVisualStyleBackColor = true;
            this.AddRecipeButton.Click += new System.EventHandler(this.AddRecipeButton_Click);
            // 
            // FoodForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 450);
            this.Controls.Add(this.qualificationsGroupBox);
            this.Controls.Add(this.NameOfRecipeLabel);
            this.Controls.Add(this.NameOfRecipeTextBox);
            this.Controls.Add(this.CancelRecipeButton);
            this.Controls.Add(this.AddRecipeButton);
            this.Name = "FoodForm";
            this.Text = "Recipe";
            this.qualificationsGroupBox.ResumeLayout(false);
            this.qualificationsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox qualificationsGroupBox;
        private System.Windows.Forms.Button AddIngredientButton;
        private System.Windows.Forms.ListBox AddedIngredientsListBox;
        private System.Windows.Forms.Label NameOfIngredientLabel;
        private System.Windows.Forms.TextBox NameOfIngredientTextBox;
        private System.Windows.Forms.Label NameOfRecipeLabel;
        private System.Windows.Forms.TextBox NameOfRecipeTextBox;
        private System.Windows.Forms.Button CancelRecipeButton;
        private System.Windows.Forms.Button AddRecipeButton;
        private System.Windows.Forms.Button DeleteIngredientButton;
        private System.Windows.Forms.Button ChangeIngredientButton;
    }
}