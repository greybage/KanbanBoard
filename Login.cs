﻿using System;
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

namespace WindowsFormsApp
{
    public partial class Login : Form
    {

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

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainList mainList = new MainList(this);
            mainList.Show();
            this.Hide();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Text;

            // połączenie z bazą danych SQLite
            string connectionString = "Data Source=users.db;Version=3;";
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            // zapytanie SQL w celu pobrania danych użytkownika o podanym loginie i haśle
            string query = $"SELECT * FROM users WHERE login='{login}' AND password='{password}'";
            SQLiteCommand command = new SQLiteCommand(query, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            // jeśli istnieje użytkownik o podanym loginie i haśle, otwórz okno "MainList"
            if (reader.HasRows)
            {
                MainList mainList = new MainList(this);
                mainList.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Nieprawidłowy login lub hasło");
            }

            connection.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
