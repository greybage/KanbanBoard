using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using WindowsFormsApp.Model;

namespace WindowsFormsApp
{
    public class DatabaseManager : IDisposable
    {
        private string connectionString;
        private SQLiteConnection connection;
        private SQLiteCommand command;
        public ComboBox CategoryCombobox { get; set; }

        // Delegat dla funkcji wypełniającej combobox
        public delegate void FillComboBoxDelegate(ComboBox comboBox);

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand(connection);
        }

        public void FillComboBox(ComboBox comboBox, string query, string valueMember, string displayMember)
        {
            try
            {
                connection.Open();
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();

                var items = new List<ComboItemViewModel>();

                while (reader.Read())
                {
                    string value = reader[valueMember].ToString();
                    string name = reader[displayMember].ToString();
                    var selectItem = new ComboItemViewModel()
                    {
                        Key = value,
                        Value = name
                    };
                    items.Add(selectItem);
                }

                comboBox.DataSource = items;
                comboBox.DisplayMember = "Value";
                comboBox.ValueMember = "Key";

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while filling the combobox: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
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

                    return value;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
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
            }
            catch (Exception ex)
            {
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
                    MessageBox.Show("Użytkownik o podanym loginie już istnieje.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public void AddTask(Task task)
        {
            try
            {
                connection.Open();
                AddQueryParameter("@UserId", task.UserID.ToString());
                AddQueryParameter("@Name", task.Name);
                AddQueryParameter("@Date", task.Date);
                AddQueryParameter("@Description", task.Description);
                AddQueryParameter("@Priority", task.Priority);
                AddQueryParameter("@CategoryId", task.CategoryId.ToString());
                AddQueryParameter("@Stage", task.Stage);

                string insertQuery = "INSERT INTO Tasks (UserId, Name, Date, Description, Priority, CategoryId, Stage) " +
                    "VALUES (@UserId, @Name, @Date, @Description, @Priority, @CategoryId, 'ToDo')";

                command.CommandText = insertQuery;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Task added.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred DB: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }

        public List<Task> GetTasksByStage(string stage)
        {
            List<Task> tasks = new List<Task>();
            string query = "SELECT TaskID, Name, Date, Description, Priority, Stage, CategoryId, UserId FROM tasks WHERE Stage=@Stage";

            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Stage", stage);
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int taskId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string date = reader.GetString(2);
                    string description = reader.GetString(3);
                    string priority = reader.GetString(4);
                    string taskStage = reader.GetString(5);
                    int categoryId = int.Parse(reader.GetString(6));
                    int userId = int.Parse(reader.GetInt32(7).ToString());

                    Task task = new Task(taskId, userId, name, date, description, priority, categoryId);
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
                reader = null;
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
