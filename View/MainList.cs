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
            string connectionString = "Data Source=DataBase.db;Version=3;";
            string selectQuery = "SELECT * FROM Users";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        string usersData = "";
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string login = reader.GetString(1);
                            string password = reader.GetString(2);
                            usersData += $"ID: {id}, Login: {login}, Password: {password}\n";
                        }
                        MessageBox.Show(usersData);
                    }
                }
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

        private void dataGridViewToDo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void PopulateDataGridView()
        {
            string queryToDo = "SELECT Name, CategoryId, Priority FROM tasks WHERE Stage='ToDo'";
            DataTable dataTableToDo = new DataTable();
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(queryToDo, databaseManager.Connection))
                {
                    dataAdapter.Fill(dataTableToDo);
                }
            }
            dataGridViewToDo.DataSource = dataTableToDo;

            string queryInProgress = "SELECT Name,CategoryId, Priority FROM tasks WHERE Stage='InProgress'";
            DataTable dataTableInProgress = new DataTable();
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(queryInProgress, databaseManager.Connection))
                {
                    dataAdapter.Fill(dataTableInProgress);
                }
            }
            dataGridViewInProgress.DataSource = dataTableInProgress;

            string querySuspended = "SELECT Name, CategoryId, Priority FROM tasks WHERE Stage='Suspended'";
            DataTable dataTableSuspended = new DataTable();
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(querySuspended, databaseManager.Connection))
                {
                    dataAdapter.Fill(dataTableSuspended);
                }
            }
            dataGridViewSuspended.DataSource = dataTableSuspended;

            string queryDone = "SELECT Name, CategoryId, Priority FROM tasks WHERE Stage='Done'";
            DataTable dataTableDone = new DataTable();
            using (DatabaseManager databaseManager = new DatabaseManager("Data Source=DataBase.db"))
            {
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(queryDone, databaseManager.Connection))
                {
                    dataAdapter.Fill(dataTableDone);
                }
            }
            dataGridViewDone.DataSource = dataTableDone;
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


    