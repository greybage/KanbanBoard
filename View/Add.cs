﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using WindowsFormsApp;


namespace WindowsFormsApp
{
    public partial class Add : Form
    {
        public static Add instance;
        private Form previousForm;
        public User currentUser;
        public User GetCurrentUser()
        {
            return currentUser;
        }



        public Add(Form previousForm, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string query = "SELECT CategoryName FROM categories";
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryComboBox.Items.Add(reader.GetValue(0));
                        }
                    }
                }
            }

            string[] priorities = { "important", "urgent", "less important", "super urgent" };
            priorityComboBox.Items.AddRange(priorities);

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        public ComboBox categoryCombobox;

        private void Addbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=DataBase.db;Version=3;";
                string name = txtName.Text;
                string date = dateTimePicker.Text;
                string description = txtDescription.Text;
                string priority = priorityComboBox.SelectedItem.ToString();
                string category = categoryComboBox.SelectedItem.ToString();
                int userID = currentUser.Id;


                using (DatabaseManager dbManager = new DatabaseManager(connectionString))
                {
                    dbManager.CategoryCombobox = categoryCombobox;
                    string categoryName = categoryComboBox.SelectedItem.ToString();
                    int categoryId = dbManager.GetCategoryId(categoryName);
                    
                    if (categoryId != -1)
                    {
                        Task task = new Task(userID, name, date, description, priority, categoryId);

                        dbManager.AddTask(task, categoryId);
                        
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowa kategoria.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            previousForm.Show();
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        private void priorityComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void txtDate_TextChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}