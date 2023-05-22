
namespace WindowsFormsApp
{
    partial class Categories
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.AddCategorybtn = new System.Windows.Forms.Button();
            this.DeleteCategoriesbtn = new System.Windows.Forms.Button();
            this.Backbtn = new System.Windows.Forms.Button();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(12, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 25);
            this.label1.TabIndex = 11;
            this.label1.Text = "Add Category:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtCategory
            // 
            this.txtCategory.Location = new System.Drawing.Point(17, 57);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(134, 20);
            this.txtCategory.TabIndex = 12;
            // 
            // AddCategorybtn
            // 
            this.AddCategorybtn.Location = new System.Drawing.Point(17, 83);
            this.AddCategorybtn.Name = "AddCategorybtn";
            this.AddCategorybtn.Size = new System.Drawing.Size(134, 23);
            this.AddCategorybtn.TabIndex = 13;
            this.AddCategorybtn.Text = "Add";
            this.AddCategorybtn.UseVisualStyleBackColor = true;
            this.AddCategorybtn.Click += new System.EventHandler(this.AddCategorybtn_Click);
            // 
            // DeleteCategoriesbtn
            // 
            this.DeleteCategoriesbtn.Location = new System.Drawing.Point(157, 172);
            this.DeleteCategoriesbtn.Name = "DeleteCategoriesbtn";
            this.DeleteCategoriesbtn.Size = new System.Drawing.Size(134, 23);
            this.DeleteCategoriesbtn.TabIndex = 15;
            this.DeleteCategoriesbtn.Text = "Delete";
            this.DeleteCategoriesbtn.UseVisualStyleBackColor = true;
            this.DeleteCategoriesbtn.Click += new System.EventHandler(this.DeleteCategoriesbtn_Click);
            // 
            // Backbtn
            // 
            this.Backbtn.Location = new System.Drawing.Point(263, 12);
            this.Backbtn.Name = "Backbtn";
            this.Backbtn.Size = new System.Drawing.Size(95, 23);
            this.Backbtn.TabIndex = 16;
            this.Backbtn.Text = "Back";
            this.Backbtn.UseVisualStyleBackColor = true;
            this.Backbtn.Click += new System.EventHandler(this.Backbtn_Click);
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Location = new System.Drawing.Point(3, 172);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(148, 21);
            this.categoryComboBox.TabIndex = 24;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            // 
            // Categories
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 200);
            this.Controls.Add(this.categoryComboBox);
            this.Controls.Add(this.Backbtn);
            this.Controls.Add(this.DeleteCategoriesbtn);
            this.Controls.Add(this.AddCategorybtn);
            this.Controls.Add(this.txtCategory);
            this.Controls.Add(this.label1);
            this.Name = "Categories";
            this.Text = "Categories";
            this.Load += new System.EventHandler(this.Categories_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Button AddCategorybtn;
        private System.Windows.Forms.Button DeleteCategoriesbtn;
        private System.Windows.Forms.Button Backbtn;
        private System.Windows.Forms.ComboBox categoryComboBox;
    }
}