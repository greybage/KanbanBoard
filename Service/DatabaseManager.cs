using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Windows.Input;

namespace WindowsFormsApp
{
    public class DatabaseManager : IDisposable
    {
        private string connectionString;
        private SQLiteConnection connection;
        private SQLiteCommand command;

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
/*
        public void AddTask(Task task)
        {
            try
            {

                string insertQuery = "INSERT INTO tasks (TaskID, UserID, Name, Date, Description, Priority, CategoryId, Stage) " +
                               "VALUES ((SELECT MAX(TaskID) FROM tasks) + 1, @UserID, @Name, @Date, @Description, @Priority, @CategoryId, 'ToDo')";

                command.Parameters.AddWithValue("@UserID", task.UserID);
                command.Parameters.AddWithValue("@Name", task.Name);
                command.Parameters.AddWithValue("@Date", task.Date);
                command.Parameters.AddWithValue("@Description", task.Description);
                command.Parameters.AddWithValue("@Priority", task.Priority);
                command.Parameters.AddWithValue("@CategoryId", task.CategoryID);


                int result = command.ExecuteNonQuery();
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
*/

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

        

        //public void AddQueryParameter(SQLiteCommand command, string parameterName, object value)
        //{
        //    command.Parameters.AddWithValue(parameterName, value);
        //}

        
    }
}
