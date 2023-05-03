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
            string connectionString = "Data Source=Users.db;Version=3;";
            string login = LoginSet.Text;
            string password = PasswordSet.Text;

            // Sprawdzenie, czy użytkownik już istnieje w bazie danych
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand("SELECT COUNT(*) FROM Users WHERE login = @login", connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Użytkownik o podanym loginie już istnieje.");
                        return;
                    }
                }

                // Sprawdzenie, jakie jest następne wolne id
                using (SQLiteCommand command = new SQLiteCommand("SELECT MAX(id) FROM Users", connection))
                {
                    var result = command.ExecuteScalar();
                    int nextId = 1;
                    if (result != DBNull.Value)
                    {
                        nextId = Convert.ToInt32(result) + 1;
                    }

                    // Wstawienie nowego użytkownika do bazy danych
                    using (SQLiteCommand insertCommand = new SQLiteCommand("INSERT INTO Users (id, login, password) VALUES (@id, @login, @password)", connection))
                    {
                        insertCommand.Parameters.AddWithValue("@id", nextId);
                        insertCommand.Parameters.AddWithValue("@login", login);
                        insertCommand.Parameters.AddWithValue("@password", password);
                        int rowsAffected = insertCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Użytkownik został zarejestrowany.");
                        }
                    }
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
