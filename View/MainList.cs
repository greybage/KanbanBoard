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
using System.Net.NetworkInformation;
using WindowsFormsApp.Model;

namespace WindowsFormsApp
{
    public partial class MainList : Form
    {
        public static MainList instance;
        private Form loginForm;
        private Form previousForm;
        public User currentUser;
        
        public User GetCurrentUser()
        {
            return currentUser;
        }
        private DatabaseManager databaseManager;

        public MainList(Form previousForm, Login loginForm, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            currentUser = user;
            this.loginForm = loginForm;

            lblCurrentUser.Text = "Current user: " + currentUser.Login;
            

            AddViewButtonColumn(dataGridViewToDo);
            AddViewButtonColumn(dataGridViewInProgress);
            AddViewButtonColumn(dataGridViewSuspended);
            AddViewButtonColumn(dataGridViewDone);

            databaseManager = new DatabaseManager("Data Source=DataBase.db");
        }

        private void MainList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();

            databaseManager = new DatabaseManager("Data Source=DataBase.db");

            var categoriesComboboxItems = databaseManager.GetCategories().Select(x => new ComboItemViewModel
            {
                Key = x.CategoryID.ToString(),
                Value = x.CategoryName
            }).ToList();
            categoryComboBox.DataSource = categoriesComboboxItems;
            categoryComboBox.DisplayMember = "Value";
            categoryComboBox.ValueMember = "Key";

          

            string[] priorities = { "important", "urgent", "less important", "super urgent" };
            priorityComboBox.Items.AddRange(priorities);

        }
        private void AddViewButtonColumn(DataGridView dataGridView)
        {
            DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
            viewButtonColumn.Name = "Show";
            viewButtonColumn.HeaderText = "Show";
            viewButtonColumn.Text = "Show";
            viewButtonColumn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(viewButtonColumn);

            dataGridView.CellContentClick += DataGridView_CellContentClick;
        }


        private void DataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewToDo.Columns["Show"].Index)
            {
                DataGridView dataGridView = (DataGridView)sender;
                string gridName = dataGridView.Name;

                int taskId = 0;

                switch (gridName)
                {
                    case "dataGridViewToDo":
                        taskId = Convert.ToInt32(dataGridViewToDo.Rows[e.RowIndex].Cells["TaskID"].Value);
                        break;
                    case "dataGridViewInProgress":
                        taskId = Convert.ToInt32(dataGridViewInProgress.Rows[e.RowIndex].Cells["TaskID"].Value);
                        break;
                    case "dataGridViewSuspended":
                        taskId = Convert.ToInt32(dataGridViewSuspended.Rows[e.RowIndex].Cells["TaskID"].Value);
                        break;
                    case "dataGridViewDone":
                        taskId = Convert.ToInt32(dataGridViewDone.Rows[e.RowIndex].Cells["TaskID"].Value);
                        break;
                    default:
                        break;
                }

                TaskView taskView = new TaskView(this, taskId, currentUser)
                {
                    currentUser = currentUser
                };
                taskView.Show();
                this.Hide();
            }
        }


        private void dataGridViewToDo_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridViewToDo.Columns[e.ColumnIndex].Name == "Date") // Zmień "Date" na nazwę kolumny zawierającej datę
            {
                if (e.Value != null && e.Value is DateTime)
                {
                    DateTime date = (DateTime)e.Value;

                    if (date < DateTime.Today) // Sprawdź, czy data jest przeterminowana
                    {
                        e.CellStyle.BackColor = Color.Red; // Ustaw kolor tła na czerwony
                    }
                }
            }
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            Add add = new Add(this, currentUser)
            {
                currentUser = currentUser
            };
            add.Show();
            this.Hide();
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CurrentUserTasks_Click(object sender, EventArgs e)
        {
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                int userID = currentUser.Id; // Pobierz identyfikator zalogowanego użytkownika

                List<Task> tasksToDo = databaseManager.GetTasksByStageForUser("ToDo", userID);
                dataGridViewToDo.DataSource = tasksToDo.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewToDo.Columns["TaskID"].Visible = false;

                List<Task> tasksInProgress = databaseManager.GetTasksByStageForUser("InProgress", userID);
                dataGridViewInProgress.DataSource = tasksInProgress.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewInProgress.Columns["TaskID"].Visible = false;

                List<Task> tasksSuspended = databaseManager.GetTasksByStageForUser("Suspended", userID);
                dataGridViewSuspended.DataSource = tasksSuspended.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewSuspended.Columns["TaskID"].Visible = false;

                List<Task> tasksDone = databaseManager.GetTasksByStageForUser("Done", userID);
                dataGridViewDone.DataSource = tasksDone.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewDone.Columns["TaskID"].Visible = false;
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



        private void PopulateDataGridView()
        {
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                List<Task> tasksToDo = databaseManager.GetTasksByStage("ToDo");
                dataGridViewToDo.DataSource = tasksToDo.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewToDo.Columns["TaskID"].Visible = false;

                List<Task> tasksInProgress = databaseManager.GetTasksByStage("InProgress");
                dataGridViewInProgress.DataSource = tasksInProgress.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewInProgress.Columns["TaskID"].Visible = false;

                List<Task> tasksSuspended = databaseManager.GetTasksByStage("Suspended");
                dataGridViewSuspended.DataSource = tasksSuspended.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewSuspended.Columns["TaskID"].Visible = false;

                List<Task> tasksDone = databaseManager.GetTasksByStage("Done");
                dataGridViewDone.DataSource = tasksDone.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewDone.Columns["TaskID"].Visible = false;
            }
        }

        private void SelectByPrioritybtn_Click(object sender, EventArgs e)
        {
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                string Priority = priorityComboBox.SelectedItem.ToString(); 

                List<Task> tasksToDo = databaseManager.GetTasksByPriority("ToDo", Priority);
                dataGridViewToDo.DataSource = tasksToDo.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewToDo.Columns["TaskID"].Visible = false;

                List<Task> tasksInProgress = databaseManager.GetTasksByPriority("InProgress", Priority);
                dataGridViewInProgress.DataSource = tasksInProgress.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewInProgress.Columns["TaskID"].Visible = false;

                List<Task> tasksSuspended = databaseManager.GetTasksByPriority("Suspended", Priority);
                dataGridViewSuspended.DataSource = tasksSuspended.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewSuspended.Columns["TaskID"].Visible = false;

                List<Task> tasksDone = databaseManager.GetTasksByPriority("Done", Priority);
                dataGridViewDone.DataSource = tasksDone.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewDone.Columns["TaskID"].Visible = false;
            }

        }
        private void SelectByCategorybtn_Click(object sender, EventArgs e)
        {

            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                int categoryId = int.Parse(categoryComboBox.SelectedValue.ToString());

                List<Task> tasksToDo = databaseManager.GetTasksByCategory("ToDo", categoryId);
                dataGridViewToDo.DataSource = tasksToDo.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewToDo.Columns["TaskID"].Visible = false;

                List<Task> tasksInProgress = databaseManager.GetTasksByCategory("InProgress", categoryId);
                dataGridViewInProgress.DataSource = tasksInProgress.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewInProgress.Columns["TaskID"].Visible = false;

                List<Task> tasksSuspended = databaseManager.GetTasksByCategory("Suspended", categoryId);
                dataGridViewSuspended.DataSource = tasksSuspended.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewSuspended.Columns["TaskID"].Visible = false;

                List<Task> tasksDone = databaseManager.GetTasksByCategory("Done", categoryId);
                dataGridViewDone.DataSource = tasksDone.Select(t => new { t.Name, t.Date, t.Priority, t.CategoryId, t.TaskID }).ToList();
                dataGridViewDone.Columns["TaskID"].Visible = false;
            }

        }

        private void Categoriesbtn_Click_1(object sender, EventArgs e)
        {
            Categories categories = new Categories(this);
            categories.Show();
            this.Hide();
        }
       
       public void RefreshDataGridView()
        {
           PopulateDataGridView();
        }
      






     

     

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void dataGridViewInProgress_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewSuspended_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void TeamTasksbtn_Click(object sender, EventArgs e)
        {
            PopulateDataGridView();
        }

        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


    