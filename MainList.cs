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
    public partial class MainList : Form
    {
        public static MainList instance;
        
        private Form previousForm;
        public User currentUser;
        public User GetCurrentUser()
        {
            return currentUser;
        }

        public MainList(Form previousForm,Login loginForm, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            currentUser = user;
            this.loginForm = loginForm;

            lblCurrentUser.Text = "Zalogowany użytkownik: " + currentUser.Login; 
        }
        private Form loginForm;

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            Add add = new Add(this);
            add.Show();
            this.Hide();
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=Users.db;Version=3;";
            string selectQuery = "SELECT * FROM Users";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        string usersData = "";
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string login = reader.GetString(1);
                            string password = reader.GetString(2);
                            usersData += $"ID: {id}, Login: {login}, Password: {password}\n";
                        }
                        MessageBox.Show(usersData);
                    }
                }
            }
        }

        private void LogOutbtn_Click(object sender, EventArgs e)
        {
            TextBox txtPassword = (TextBox)loginForm.Controls["txtPassword"];
            txtPassword.Text = string.Empty;
            this.Close();
            previousForm.Show();
             
        }

        private void lblCurrentUser_Text(object sender, EventArgs e)
        {
            lblCurrentUser.Text = "current User: " + currentUser.Login;

        }
    }

}
    