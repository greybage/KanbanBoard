using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Data.SQLite;
using WindowsFormsApp.Model;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            currentUser = user;
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            databaseManager = new DatabaseManager("Data Source=DataBase.db");

            Task task = databaseManager.GetTasksById(taskId);

            string[] priorities = { "important", "urgent", "less important", "super urgent" };
            priorityComboBox.Items.AddRange(priorities);
            
            string[] stages = { "ToDo", "InProgress", "Suspended", "Done" };
            stageComboBox.Items.AddRange(stages);

            var categoriesComboboxItems = databaseManager.GetCategories().Select(x => new ComboItemViewModel
            {
                Key = x.CategoryID.ToString(),
                Value = x.CategoryName
            }).ToList();
            categoryComboBox.DataSource = categoriesComboboxItems;
            categoryComboBox.DisplayMember = "Value";
            categoryComboBox.ValueMember = "Key";
        
            
            txtName.Text = task.Name;
            dateTimePicker.Text = task.Date;
            txtDescription.Text = task.Description;
            categoryComboBox.SelectedValue =  task.CategoryId.ToString();
            priorityComboBox.SelectedIndex = priorityComboBox.Items.IndexOf(task.Priority);
            stageComboBox.SelectedIndex = stageComboBox.Items.IndexOf(task.Stage);

            //deleteButton.Click += deleteButton_Click;
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

                    if (priority != null && stage != null)
                    {
                        Task task = new Task
                        {
                            TaskID = taskID,
                            Name = name,
                            Date= date,
                            Description = description,
                            Priority = priority,
                            CategoryId = categoryId,
                            UserID= userID,
                            Stage= stage
                        };
                        dbManager.EditTask(task);
                    }
                    else
                    {
                        MessageBox.Show("Nieprawidłowe dane.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć to zadanie?", "Potwierdzenie usunięcia", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    string connectionString = "Data Source=DataBase.db;Version=3;";

                    using (DatabaseManager dbManager = new DatabaseManager(connectionString))
                    {
                        dbManager.DeleteTask(taskId);
                        MessageBox.Show("Zadanie zostało usunięte.");
                        
                        this.Close(); // Zamyka okno TaskView po usunięciu zadania
                        previousForm.Show();

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd podczas usuwania zadania: {ex.Message}");
                }
            }
        }

    }
}
