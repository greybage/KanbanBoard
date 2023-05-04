namespace WindowsFormsApp
{
    partial class MainList
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.to = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Addbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.MyTasksbtn = new System.Windows.Forms.Button();
            this.TeamTasksbtn = new System.Windows.Forms.Button();
            this.LogOutbtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lblCurrentUser = new System.Windows.Forms.Label();
            this.dataGridViewToDo = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToDo)).BeginInit();
            this.SuspendLayout();
            // 
            // to
            // 
            this.to.AutoSize = true;
            this.to.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.to.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.to.Location = new System.Drawing.Point(9, 265);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(95, 31);
            this.to.TabIndex = 0;
            this.to.Text = "To Do ";
            this.to.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(179, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "In Progress";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(495, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 31);
            this.label2.TabIndex = 2;
            this.label2.Text = "Done";
            // 
            // Addbtn
            // 
            this.Addbtn.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Addbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Addbtn.Location = new System.Drawing.Point(724, 27);
            this.Addbtn.Name = "Addbtn";
            this.Addbtn.Size = new System.Drawing.Size(167, 53);
            this.Addbtn.TabIndex = 6;
            this.Addbtn.Text = "Add Task";
            this.Addbtn.UseVisualStyleBackColor = false;
            this.Addbtn.Click += new System.EventHandler(this.Addbtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(338, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(151, 31);
            this.label3.TabIndex = 9;
            this.label3.Text = "Suspended";
            // 
            // MyTasksbtn
            // 
            this.MyTasksbtn.Location = new System.Drawing.Point(631, 27);
            this.MyTasksbtn.Name = "MyTasksbtn";
            this.MyTasksbtn.Size = new System.Drawing.Size(75, 23);
            this.MyTasksbtn.TabIndex = 14;
            this.MyTasksbtn.Text = "My tasks";
            this.MyTasksbtn.UseVisualStyleBackColor = true;
            this.MyTasksbtn.Click += new System.EventHandler(this.button2_Click);
            // 
            // TeamTasksbtn
            // 
            this.TeamTasksbtn.Location = new System.Drawing.Point(631, 57);
            this.TeamTasksbtn.Name = "TeamTasksbtn";
            this.TeamTasksbtn.Size = new System.Drawing.Size(75, 23);
            this.TeamTasksbtn.TabIndex = 15;
            this.TeamTasksbtn.Text = "Teams tasks";
            this.TeamTasksbtn.UseVisualStyleBackColor = true;
            // 
            // LogOutbtn
            // 
            this.LogOutbtn.Location = new System.Drawing.Point(12, 12);
            this.LogOutbtn.Name = "LogOutbtn";
            this.LogOutbtn.Size = new System.Drawing.Size(75, 23);
            this.LogOutbtn.TabIndex = 16;
            this.LogOutbtn.Text = "Log Out";
            this.LogOutbtn.UseVisualStyleBackColor = true;
            this.LogOutbtn.Click += new System.EventHandler(this.LogOutbtn_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.button2.Location = new System.Drawing.Point(724, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(167, 35);
            this.button2.TabIndex = 17;
            this.button2.Text = " Categories";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.AutoSize = true;
            this.lblCurrentUser.Location = new System.Drawing.Point(12, 42);
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(0, 13);
            this.lblCurrentUser.TabIndex = 18;
            this.lblCurrentUser.Click += new System.EventHandler(this.lblCurrentUser_Text);
            // 
            // dataGridViewToDo
            // 
            this.dataGridViewToDo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewToDo.Location = new System.Drawing.Point(12, 319);
            this.dataGridViewToDo.Name = "dataGridViewToDo";
            this.dataGridViewToDo.Size = new System.Drawing.Size(105, 176);
            this.dataGridViewToDo.TabIndex = 19;
            this.dataGridViewToDo.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewToDo_CellContentClick);
            // 
            // MainList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(931, 519);
            this.Controls.Add(this.dataGridViewToDo);
            this.Controls.Add(this.lblCurrentUser);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.LogOutbtn);
            this.Controls.Add(this.TeamTasksbtn);
            this.Controls.Add(this.MyTasksbtn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Addbtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.to);
            this.Name = "MainList";
            this.Text = "MainList";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewToDo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label to;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Addbtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button MyTasksbtn;
        private System.Windows.Forms.Button TeamTasksbtn;
        private System.Windows.Forms.Button LogOutbtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label lblCurrentUser;
        private System.Windows.Forms.DataGridView dataGridViewToDo;
    }
}

