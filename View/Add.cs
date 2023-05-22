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
            string query = "SELECT CategoryId FROM categories";
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

        private void Addbtn_Click(object sender, EventArgs e)
        {
            Task task = new Task()
            {
                Name = txtName.Text,
                Date = dateTimePicker.Value.ToString(),
                Description = txtDescription.Text,
                Priority = priorityComboBox.SelectedItem.ToString(),
                CategoryID = int.Parse(categoryComboBox.SelectedItem.ToString()),
                UserID = currentUser.Id
            };

            string query = "INSERT INTO tasks (TaskID, UserID, Name, Date, Description, Priority, CategoryId, Stage) " +
                           "VALUES ((SELECT MAX(TaskID) FROM tasks) + 1, @UserID, @Name, @Date, @Description, @Priority, @CategoryId, 'ToDo')";

            using (SQLiteConnection connection = new SQLiteConnection("Data Source=database.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID", task.UserID);
                    command.Parameters.AddWithValue("@Name", task.Name);
                    command.Parameters.AddWithValue("@Date", task.Date);
                    command.Parameters.AddWithValue("@Description", task.Description);
                    command.Parameters.AddWithValue("@Priority", task.Priority);
                    command.Parameters.AddWithValue("@CategoryId", task.CategoryID);

                    int result = command.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Task added successfully!");
                    }
                    else
                    {
                        MessageBox.Show("An error occurred while adding the task.");
                    }
                }
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
