
namespace WindowsFormsApp
{
    partial class Register
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
            this.LoginSet = new System.Windows.Forms.TextBox();
            this.PasswordSet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.RegisterSetbtn = new System.Windows.Forms.Button();
            this.BackToLoginbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LoginSet
            // 
            this.LoginSet.Location = new System.Drawing.Point(326, 165);
            this.LoginSet.Name = "LoginSet";
            this.LoginSet.Size = new System.Drawing.Size(100, 20);
            this.LoginSet.TabIndex = 0;
            this.LoginSet.TextChanged += new System.EventHandler(this.LoginSet_TextChanged);
            // 
            // PasswordSet
            // 
            this.PasswordSet.Location = new System.Drawing.Point(326, 204);
            this.PasswordSet.Name = "PasswordSet";
            this.PasswordSet.Size = new System.Drawing.Size(100, 20);
            this.PasswordSet.TabIndex = 1;
            this.PasswordSet.UseSystemPasswordChar = true;
            this.PasswordSet.TextChanged += new System.EventHandler(this.PasswordSet_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(326, 149);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Login";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(326, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // RegisterSetbtn
            // 
            this.RegisterSetbtn.Location = new System.Drawing.Point(326, 230);
            this.RegisterSetbtn.Name = "RegisterSetbtn";
            this.RegisterSetbtn.Size = new System.Drawing.Size(100, 23);
            this.RegisterSetbtn.TabIndex = 6;
            this.RegisterSetbtn.Text = "Register";
            this.RegisterSetbtn.UseVisualStyleBackColor = true;
            this.RegisterSetbtn.Click += new System.EventHandler(this.RegisterSetbtn_Click);
            // 
            // BackToLoginbtn
            // 
            this.BackToLoginbtn.Location = new System.Drawing.Point(713, 12);
            this.BackToLoginbtn.Name = "BackToLoginbtn";
            this.BackToLoginbtn.Size = new System.Drawing.Size(75, 23);
            this.BackToLoginbtn.TabIndex = 7;
            this.BackToLoginbtn.Text = "back";
            this.BackToLoginbtn.UseVisualStyleBackColor = true;
            this.BackToLoginbtn.Click += new System.EventHandler(this.BackToLoginbtn_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BackToLoginbtn);
            this.Controls.Add(this.RegisterSetbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PasswordSet);
            this.Controls.Add(this.LoginSet);
            this.Name = "Register";
            this.Text = "Register";
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox LoginSet;
        private System.Windows.Forms.TextBox PasswordSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button RegisterSetbtn;
        private System.Windows.Forms.Button BackToLoginbtn;
    }
}