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
        public TextBox TxtPassword;
        private DatabaseManager databaseManager;
        private Form previousForm;
        public Login(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            databaseManager = new DatabaseManager("Data Source=DataBase.db");
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

            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {               
                try
                {
                    int id = databaseManager.SelectUserId(login, password);
                    
                    if(id != 0)
                    {
                        User user = new User(id, login, password);

                        MainList mainList = new MainList(this, this, user)
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
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            databaseManager.Dispose();
        }

        private void textLogin_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            txtLogin.Text = textBox.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            txtPassword.Text = textBox.Text;
        }
    }
}
