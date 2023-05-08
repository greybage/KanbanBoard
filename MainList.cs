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
        
        private Form previousForm;
        public User currentUser;
        public User GetCurrentUser()
        {
            return currentUser;
        }

        public MainList(Form previousForm,Login loginForm, User user)
        {
            InitializeComponent();
            this.previousForm = previousForm;
            currentUser = user;
            this.loginForm = loginForm;
            
            lblCurrentUser.Text = "Zalogowany użytkownik: " + currentUser.Login; 
        }

        private void MainList_Load(object sender, EventArgs e)
        {
            PopulateDataGridView();
            RefreshDataGridView();
        }

        private Form loginForm;

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
            
            string queryToDo = "SELECT Name, Category, Priority FROM tasks WHERE Stage='ToDo'";
            DataTable dataTableToDo = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(queryToDo, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTableToDo);
                    }
                }
            }
            dataGridViewToDo.DataSource = dataTableToDo;

            
            string queryInProgress = "SELECT Name, Category, Priority FROM tasks WHERE Stage='InProgress'";
            DataTable dataTableInProgress = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(queryInProgress, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTableInProgress);
                    }
                }
            }
            dataGridViewInProgress.DataSource = dataTableInProgress;

            
            string querySuspended = "SELECT Name, Category, Priority FROM tasks WHERE Stage='Suspended'";
            DataTable dataTableSuspended = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(querySuspended, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTableSuspended);
                    }
                }
            }
            dataGridViewSuspended.DataSource = dataTableSuspended;

            
            string queryDone = "SELECT Name, Category, Priority FROM tasks WHERE Stage='Done'";
            DataTable dataTableDone = new DataTable();
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(queryDone, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        adapter.Fill(dataTableDone);
                    }
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
        string query = $"SELECT * FROM tasks WHERE Stage='{stage}'";
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=DataBase.db"))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView.DataSource = dataTable;
                    }
                }
            }
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

    }
}


    