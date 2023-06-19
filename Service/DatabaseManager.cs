using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Threading.Tasks;
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
        public void EditTask(Task task)
        {
            try
            {
                connection.Open();
                AddQueryParameter("@UserId", task.UserID.ToString());
                AddQueryParameter("@taskId", task.TaskID.ToString());
                AddQueryParameter("@Name", task.Name);
                AddQueryParameter("@Date", task.Date);
                AddQueryParameter("@Description", task.Description);
                AddQueryParameter("@Priority", task.Priority);
                AddQueryParameter("@CategoryId", task.CategoryId.ToString());
                AddQueryParameter("@Stage", task.Stage);

                string updateQuery = "UPDATE Tasks SET UserId = @UserId, Name = @Name, Date = @Date, Description = @Description, " +
                    "Priority = @Priority, CategoryId = @CategoryId, Stage = @Stage WHERE TaskID = @taskId";

                command.CommandText = updateQuery;
                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Task updated.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DB edit: {ex.Message}");
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

                    Task task = new Task
                    {
                        TaskID= taskId,
                        Name = name,
                        Date= date,
                        Description = description,
                        Priority = priority,
                        CategoryId = categoryId,
                        UserID= userId,
                        Stage= taskStage
                    };
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

        public List<Task> GetTasksByStageForUser(string stage, int userID)
        {
            List<Task> tasks = new List<Task>();
            string query = "SELECT TaskID, Name, Date, Description, Priority, Stage, CategoryId, UserId FROM tasks WHERE Stage=@Stage AND UserId=@UserID";

            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Stage", stage);
                command.Parameters.AddWithValue("@UserID", userID);
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

                    Task task = new Task
                    {
                        TaskID = taskId,
                        Name = name,
                        Date = date,
                        Description = description,
                        Priority = priority,
                        CategoryId = categoryId,
                        UserID = userId,
                        Stage = taskStage
                    };
                    tasks.Add(task);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DB list: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return tasks;
        }


        public Task GetTasksById(int Id)
        {
            string query = "SELECT TaskID, Name, Date, Description, Priority, Stage, CategoryId, UserId FROM tasks WHERE TaskId=@taskId";
            Task task = null;
            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@taskId", Id);
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

                    task = new Task
                    {
                        TaskID= taskId,
                        Name = name,
                        Date= date,
                        Description = description,
                        Priority = priority,
                        CategoryId = categoryId,
                        UserID = userId,
                        Stage = taskStage
                    };
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
            return task;
        }



        public List<Task> GetTasksByPriority(string stage, string Priority)
        {
            List<Task> tasks = new List<Task>();
            string query = "SELECT TaskID, Name, Date, Description, Priority, Stage, CategoryId, UserId FROM tasks WHERE Stage=@Stage AND Priority=@Priority";

            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Stage", stage);
                command.Parameters.AddWithValue("@Priority", Priority);
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

                    Task task = new Task
                    {
                        TaskID = taskId,
                        Name = name,
                        Date = date,
                        Description = description,
                        Priority = priority,
                        CategoryId = categoryId,
                        UserID = userId,
                        Stage = taskStage
                    };
                    tasks.Add(task);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DB list: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return tasks;
        }

        public List<Task> GetTasksByCategory(string stage, int CategoryId)
        {
            List<Task> tasks = new List<Task>();
            string query = "SELECT TaskID, Name, Date, Description, Priority, Stage, CategoryId, UserId FROM tasks WHERE Stage=@Stage AND categoryId=@CategoryId";

            try
            {
                connection.Open();
                command.CommandText = query;
                command.Parameters.AddWithValue("@Stage", stage);
                command.Parameters.AddWithValue("@CategoryId", CategoryId);
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

                    Task task = new Task
                    {
                        TaskID = taskId,
                        Name = name,
                        Date = date,
                        Description = description,
                        Priority = priority,
                        CategoryId = categoryId,
                        UserID = userId,
                        Stage = taskStage
                    };
                    tasks.Add(task);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during DB list: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }

            return tasks;
        }

        public void DeleteTask(int taskId)
        {
            try
            {
                connection.Open();

                string deleteQuery = "DELETE FROM Tasks WHERE TaskID = @TaskId";

                using (SQLiteCommand command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@TaskId", taskId);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas usuwania zadania: {ex.Message}");
            }
            finally
            {
                connection.Close();
            }
        }




        public List<Category> GetCategories()
        {
            string query = "SELECT * FROM categories";
            List<Category> categories = new List<Category>();
            try
            {
                connection.Open();
                command.CommandText = query;
                SQLiteDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int value = reader.GetInt32(0);
                    string name = reader.GetString(1);

                    var category = new Category
                    {
                        CategoryID = value,
                        CategoryName = name
                    };
                    categories.Add(category);
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
            return categories;
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
