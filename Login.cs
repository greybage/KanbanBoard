using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp;
using System.Data.SQLite;
using System.IO;



namespace WindowsFormsApp
{
    public partial class Login : Form
    {
        public TextBox TxtPassword;///{ get { return txtPassword; } }

        private Form previousForm;
        public Login(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
        }



        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void password_Click(object sender, EventArgs e)
        {

        }

        private void Registerbtn_Click(object sender, EventArgs e)
        {
            Register register = new Register (this);
            register.Show();
            this.Hide();
        }



        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            // połączenie z bazą danych SQLite
            string connectionString = "Data Source=DataBase.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // zapytanie SQL w celu pobrania id użytkownika o podanym loginie i haśle
                string query = "SELECT id FROM users WHERE login=@login AND password=@password";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    command.Parameters.AddWithValue("@password", password);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        // jeśli istnieje użytkownik o podanym loginie i haśle, pobierz jego id i utwórz obiekt użytkownika
                        if (reader.HasRows)
                        {
                            reader.Read();
                            int id = reader.GetInt32(0);
                            User user = new User(id, login, password);

                            MainList mainList = new MainList(this,this, user)
                            {
                                currentUser = user
                            };
                            mainList.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Nieprawidłowy login lub hasło");
                        }
                    }
                }
            }
        }
       



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
