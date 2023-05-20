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
                // Sprawdzenie, czy użytkownik już istnieje w bazie danych
                string countQuery = "SELECT COUNT(*) FROM Users WHERE login = @login";
                dbManager.ClearParameters();
                dbManager.AddQueryParameter("@login", login);
                object countObj = dbManager.ExecuteScalar(countQuery);
                int count = Convert.ToInt32(countObj);
                if (count > 0)
                {
                    MessageBox.Show("Użytkownik o podanym loginie już istnieje.");
                    return;
                }

                // Sprawdzenie, jakie jest następne wolne id
                string maxIdQuery = "SELECT MAX(id) FROM Users";
                object nextIdObj = dbManager.ExecuteScalar(maxIdQuery);
                int nextId = Convert.ToInt32(nextIdObj) + 1;

                // Wstawienie nowego użytkownika do bazy danych
                string insertQuery = "INSERT INTO Users (id, login, password) VALUES (@id, @login, @password)";
                dbManager.ClearParameters();
                dbManager.AddQueryParameter("@id", nextId);
                dbManager.AddQueryParameter("@login", login);
                dbManager.AddQueryParameter("@password", password);
                int rowsAffected = dbManager.ExecuteNonQuery(insertQuery);
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Użytkownik został zarejestrowany.");
                }
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
