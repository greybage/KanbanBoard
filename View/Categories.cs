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
    public partial class Categories : Form
    {
        private Form previousForm;
       

        public Categories(Form previousForm)
        {
            InitializeComponent();
            this.previousForm = previousForm;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       

        private void AddCategorybtn_Click(object sender, EventArgs e)
        {
            string category = txtCategory.Text;
            string query = $"INSERT INTO categories (CategoryName) VALUES ('{category}')";

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Category '{category}' added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add category!");
                    }
                }
                LoadCategories();
            }
        }

        private void Backbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            previousForm.Show();
            
        }

       
            private void DeleteCategoriesbtn_Click(object sender, EventArgs e)
            {
                string categoryName = categoryComboBox.Text;
                if (!string.IsNullOrEmpty(categoryName))
                {
                    string connectionString = "Data Source=database.db";
                    string deleteQuery = $"DELETE FROM categories WHERE CategoryName = '{categoryName}'";
                    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();
                        using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                        {
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Category  deleted.");
                                
                            }
                            else
                            {
                                MessageBox.Show($"Something went wrong with deleting .");
                            }
                        }
                    }
                    LoadCategories();
                }
            }

        private void Categories_Load(object sender, EventArgs e)
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
                            categoryComboBox.Items.Add(reader.GetString(0));
                        }
                    }
                }
            }

        }
        private void LoadCategories()
        {
            categoryComboBox.Items.Clear();
            string selectQuery = "SELECT CategoryName FROM categories";
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string categoryName = reader.GetString(0);
                            categoryComboBox.Items.Add(categoryName);
                        }
                    }
                }
            }
        }
    }
}
