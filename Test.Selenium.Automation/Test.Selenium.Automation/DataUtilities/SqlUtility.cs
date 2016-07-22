using System;
using System.Configuration;
using System.Data.SqlClient;

namespace Test.Selenium.Automation.DataUtilities
{
    public class SqlUtil
    {
        #region Database parameters

        private readonly string _dataSource;
        private readonly string _dataBaseName;
        private readonly string _userId;
        private readonly string _password;

        #endregion

        public SqlUtil()
        {
            _dataSource = ConfigurationManager.AppSettings["Server"];
            _dataBaseName = ConfigurationManager.AppSettings["DatabaseName"];
            _userId = ConfigurationManager.AppSettings["UserId"];
            _password = ConfigurationManager.AppSettings["UserPassword"];
        }

        public bool ExecuteNonQuery(string query)
        {
            var connectionString = string.Format(
                "Data Source={0}; Initial catalog = {1}; User ID = {2}; Password = {3}", _dataSource, _dataBaseName,
                _userId, _password);

            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = query,
                        Connection = connection
                    };
                    connection.Open();

                    command.ExecuteNonQuery();
                    connection.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ex.Message);
                return false;
            }
           
        }

        public object ExecuteScalar(string query)
        {
            var connectionString = string.Format(
                "Data Source={0}; Initial catalog = {1}; User ID = {2}; Password = {3}", _dataSource, _dataBaseName,
                _userId, _password);

            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = query,
                        Connection = connection
                    };
                    connection.Open();

                    var result = command.ExecuteScalar();
                    connection.Close();
                    return result;
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }

        }

        public SqlDataReader ExecuteReader(string query)
        {
            var connectionString = string.Format(
                "Data Source={0}; Initial catalog = {1}; User ID = {2}; Password = {3}", _dataSource, _dataBaseName,
                _userId, _password);

            try
            {
                SqlConnection connection;
                using (connection = new SqlConnection(connectionString))
                {
                    var command = new SqlCommand
                    {
                        CommandText = query,
                        Connection = connection
                    };
                    connection.Open();

                    var reader = command.ExecuteReader();
                    connection.Close();
                    return reader;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: "+ex.Message);
                return null;
            }
           
        }