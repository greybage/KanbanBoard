using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace WindowsFormsApp
{
    public partial class Register : Form

    {
      

        private Form previousForm;

        public Register(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RegisterSetbtn_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DataBase.db;Version=3;";
            string login = LoginSet.Text;
            string password = PasswordSet.Text;

            using (DatabaseManager dbManager = new DatabaseManager(connectionString))
            {               
                dbManager.RegisterUser(login, password);
            }
        }


        private void BackToLoginbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            previousForm.Show();
        }

        private void LoginSet_TextChanged(object sender, EventArgs e)
        {

        }

        private void PasswordSet_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
