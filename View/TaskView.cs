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
    public partial class TaskView : Form
    {
        private Form previousForm;
        private int taskId;
        public User currentUser;
        public User GetCurrentUser()

        {
            return currentUser;
        }
        private DatabaseManager databaseManager;

        public TaskView(Form previousForm, int taskId, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;
          
            this.taskId = taskId;
            label5.Text = "task id: " + taskId;

            databaseManager = new DatabaseManager("Data Source=DataBase.db");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        

        private void Form3_Load(object sender, EventArgs e)
        {
            string[] priorities = { "important", "urgent", "less important", "super urgent" };
            priorityComboBox.Items.AddRange(priorities);
            
            string[] stages = { "ToDo", "InProgress", "Suspended", "Done" };
            stageComboBox.Items.AddRange(stages);

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
        }

        private void backbtn_Click(object sender, EventArgs e)
        {
            this.Close();
            previousForm.Show();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Editbtn_Click_1(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "Data Source=DataBase.db;Version=3;";


                using (DatabaseManager dbManager = new DatabaseManager(connectionString))
                {
                    int taskID = taskId;
                    string name = txtName.Text;
                    string date = dateTimePicker.Text;
                    string description = txtDescription.Text;
                    string priority = priorityComboBox.SelectedItem.ToString();
                    string stage = stageComboBox.SelectedItem.ToString();
                    int categoryId = int.Parse(categoryComboBox.SelectedValue.ToString());
                    int userID = currentUser.Id;

                    if (categoryId != -1)
                    {
                        Task task = new Task(taskId, userID, name,  date,  description,  priority,  stage,  categoryId);
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
    }
}
