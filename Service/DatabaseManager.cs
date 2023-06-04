using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Windows.Input;
using System.Collections.Generic;



namespace WindowsFormsApp
{
    public class DatabaseManager : IDisposable
    {
        private string connectionString;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        public ComboBox CategoryCombobox { get; set; }

        //DO USUNIĘCIA
        public SQLiteConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }
        //DO USUNIĘCIA
        public SQLiteCommand Command
        {
            get { return command; }
            set { command = value; }
        }

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);

        }

        public int SelectUserId(string login, string password)
        {
            string query = "SELECT id FROM users WHERE login=@login AND password=@password";

            AddQueryParameter("@login", login);
            AddQueryParameter("@password", password);

            try
            {
                connection.Open();
                command.CommandText = query;

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    int value = reader.GetInt32(0);

                    connection.Close();

                    return value;
                }                   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            return 0;
        }

       

        public void InsertUser(string login, string password)
        {
            try
            {
                string insertQuery = "INSERT INTO Users (login, password) VALUES (@login, @password)";
                ClearParameters();

                AddQueryParameter("@login", login);
                AddQueryParameter("@password", password);

                command.CommandText = insertQuery;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Użytkownik został zarejestrowany.");
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show($"An error occurred: {ex.Message}");
            }           
        }

        public void RegisterUser(string login, string password)
        {
            // Sprawdzenie, czy użytkownik już istnieje w bazie danych
            AddQueryParameter("@login", login);
            string countQuery = "SELECT COUNT(*) FROM Users WHERE login = @login";

            try
            {
                connection.Open();
                command.CommandText = countQuery;

                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                reader?.Close();

                if (count == 0)
                {
                    InsertUser(login, password);
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Użytkownik o podanym loginie już istnieje.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        public int GetCategoryId(string categoryName)
        {
            AddQueryParameter("@CategoryName", categoryName);
            string selectQuery = "SELECT CategoryId FROM Categories WHERE CategoryName = @CategoryName"; // Sprawdzanie, czy nazwa kategorii istnieje w bazie danych
            try
            {
                connection.Open();
                command.CommandText = selectQuery;

                SQLiteDataReader reader = command.ExecuteReader();

                if (reader != null && reader.HasRows)
                {
                    reader.Read();
                    int value = reader.GetInt32(0);

                    reader.Close(); // Zamknięcie SQLiteDataReader przed zmianą CommandText
                    connection.Close();

                    return value;
                }
                else
                {
                    reader?.Close(); // Zamknięcie SQLiteDataReader przed zmianą CommandText
                    connection.Close();

                    MessageBox.Show("brak takiej kategorii");
                }
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show($"DB An error occurred: {ex.Message}");
            }

            return -1; // Jeśli nie znaleziono kategorii
        }



        public void AddTask(Task task, int categoryId)
        {
            try
            {
                connection.Open();         
                AddQueryParameter("@UserId", task.UserID.ToString());
                AddQueryParameter("@Name", task.Name);
                AddQueryParameter("@Date", task.Date);
                AddQueryParameter("@Description", task.Description);
                AddQueryParameter("@Priority", task.Priority);
                AddQueryParameter("@CategoryId", categoryId.ToString());
                AddQueryParameter("@Stage", task.Stage);

                string insertQuery = "INSERT INTO Tasks (UserId, Name, Date, Description, Priority, CategoryId, Stage) " +
                    "VALUES (@UserId, @Name, @Date, @Description, @Priority, @CategoryId, 'ToDo')";

                command.CommandText = insertQuery;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Task added.");
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd DB: {ex.Message}");
            }
        }


        public List<Task> GetTasksByStage(string stage)
        {
            List<Task> tasks = new List<Task>();
            string query = $"SELECT TaskID, Name, Date, Description, Priority, Stage, CategoryId, UserId FROM tasks WHERE Stage='{stage}'";

            try
            {
                connection.Open();
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int TaskID = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string date = reader.GetString(2);
                    string description = reader.GetString(3);
                    string priority = reader.GetString(4);
                    string taskStage = reader.GetString(5);
                    int categoryId = reader.GetInt32(6);
                    int userId = reader.GetInt32(7);

                    Task task = new Task(userId, name, date, description, priority, categoryId);
                    tasks.Add(task);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred DB list: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return tasks;
        }










        public void AddQueryParameter(string parameterName, string value)
        {
            command.Parameters.AddWithValue(parameterName, value);
        }

        





        public void ClearParameters()
        {
            command.Parameters.Clear();
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        /// //////////////////////



        public int ExecuteScalar(string query)
        {
            int result = 0;
            try
            {
                connection.Open();
                command.CommandText = query;
                result = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            try
            {
                connection.Open();
                command.CommandText = query;
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
            return rowsAffected;
        }

        public SQLiteDataReader ExecuteQuery(string query)
        {
            SQLiteDataReader reader = null;
            try
            {
                connection.Open();
                command.CommandText = query;
                reader = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                reader = null; // Ustawienie readera na null w przypadku błędu
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return reader;
        }

        

      

        
    }
}
