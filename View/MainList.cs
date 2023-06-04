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
    public partial class MainList : Form
    {
        public static MainList instance;
        private Form loginForm;
        private Form previousForm;
        public User currentUser;
        private DatabaseManager databaseManager;
        public User GetCurrentUser()
        {
            return currentUser;
        }

        public MainList(Form previousForm, Login loginForm, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            currentUser = user;
            this.loginForm = loginForm;

            lblCurrentUser.Text = "Current user: " + currentUser.Login;

            //string connectionString = "Data Source=DataBase.db;Version=3;";
            databaseManager = new DatabaseManager("Data Source=DataBase.db");
        }

        private void MainList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
            RefreshDataGridView();
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

        private void dataGridViewToDo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        // W metodzie PopulateDataGridView w klasie MainList
        private void PopulateDataGridView()
        {
            // ...

            List<Task> tasksToDo = databaseManager.GetTasksByStage("ToDo");
            dataGridViewToDo.DataSource = tasksToDo;

            List<Task> tasksInProgress = databaseManager.GetTasksByStage("InProgress");
            dataGridViewInProgress.DataSource = tasksInProgress;

            List<Task> tasksSuspended = databaseManager.GetTasksByStage("Suspended");
            dataGridViewSuspended.DataSource = tasksSuspended;

            List<Task> tasksDone = databaseManager.GetTasksByStage("Done");
            dataGridViewDone.DataSource = tasksDone;

            // ...
        }


        private void Categoriesbtn_Click_1(object sender, EventArgs e)
        {
            Categories categories = new Categories(this);
            categories.Show();
            this.Hide();
        }
        private void MainList_Activated(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
        public void RefreshDataGridView()
        {
            PopulateDataGridView(dataGridViewToDo, "ToDo");
            PopulateDataGridView(dataGridViewInProgress, "InProgress");
            PopulateDataGridView(dataGridViewSuspended, "Suspended");
            PopulateDataGridView(dataGridViewDone, "Done");
        }
        private void PopulateDataGridView(DataGridView dataGridView, string stage)
        {
            string query = $"SELECT Name, CategoryId, Priority FROM tasks WHERE Stage='{stage}'";
            DataTable dataTable = new DataTable();
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(query, databaseManager.Connection))
                {
                    dataAdapter.Fill(dataTable);
                }
            }
            dataGridView.DataSource = dataTable;
        }






        private void MainList_Show(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainList_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                
                RefreshDataGridView();
            }
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
    }
}


    