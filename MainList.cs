using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        private void button1_Click(object sender, EventArgs e)
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
    