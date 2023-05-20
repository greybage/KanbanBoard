using System;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Data;

namespace WindowsFormsApp
{
    public class DatabaseManager : IDisposable
    {
        private SQLiteConnection connection;
        private SQLiteCommand command;
        private string connectionString;
        public SQLiteConnection Connection { get; private set; }

        public DatabaseManager(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new SQLiteConnection(connectionString);
            command = new SQLiteCommand();
            command.Connection = connection;
        }

        public int ExecuteScalar(string query)
        {
            int result = 0;
            try
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    result = Convert.ToInt32(command.ExecuteScalar());
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
            return result;
        }

        public int ExecuteNonQuery(string query)
        {
            int rowsAffected = 0;
            try
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    rowsAffected = command.ExecuteNonQuery();
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
            return rowsAffected;
        }

        public SQLiteDataReader ExecuteQuery(string query)
        {
            SQLiteDataReader reader = null;
            try
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    reader = command.ExecuteReader();
                }
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

        public void AddQueryParameter(string parameterName, object value)
        {
            command.Parameters.AddWithValue(parameterName, value);
        }

        public void AddQueryParameter(SQLiteCommand command, string parameterName, object value)
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
    }
}
