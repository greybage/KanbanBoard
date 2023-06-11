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
using WindowsFormsApp;
using WindowsFormsApp.Model;

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
        private DatabaseManager databaseManager;

        public Add(Form previousForm, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;

            databaseManager = new DatabaseManager("Data Source=DataBase.db");
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string query = "SELECT * FROM categories";
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        var items = new List<ComboItemViewModel>();

                        while (reader.Read())
                        {
                            string value = reader.GetInt32(0).ToString();
                            string name = reader.GetString(1);
                            var seletcItem = new ComboItemViewModel()
                            {
                                Key = value,
                                Value = name
                            };
                            items.Add(seletcItem);
                        }
                        this.categoryComboBox.DataSource = items;
                        this.categoryComboBox.DisplayMember = "Value";
                        this.categoryComboBox.ValueMember = "Key";
                    }
                }
            }

            //using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            //{
            //    string query = "SELECT * FROM categories";
            //    string valueMember = "id";
            //    string displayMember = "name";
            //    databaseManager.FillComboBox(categoryComboBox, query, valueMember, displayMember);
            //}
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


                using (DatabaseManager dbManager = new DatabaseManager(connectionString))
                {
                    
                    string name = txtName.Text;
                    string date = dateTimePicker.Text;
                    string description = txtDescription.Text;
                    string priority = priorityComboBox.SelectedItem.ToString();
                    int categoryId = int.Parse(categoryComboBox.SelectedValue.ToString());
                    int userID = currentUser.Id;

                    if (categoryId != -1)
                    {
                        Task task = new Task(userID, name, date, description, priority, categoryId);
                        dbManager.AddTask(task);                     
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowa kategoria.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd add: {ex.Message}");
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
