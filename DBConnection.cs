using MySql.Data.MySqlClient;
using System.Runtime.InteropServices.ComTypes;

namespace PhoneSpravachnik
{
    public class DBConnection
    {
        private MySqlConnection connection;

        private const string host = "mysql11.hostland.ru";
        private const string database = "host1323541_itstep3";
        private const string port = "3306";
        private const string username = "host1323541_itstep";
        private const string pass = "269f43dc";
        private const string ConnString = "Server=" + host + ";Database=" + database + ";port=" + port + ";User Id=" + username + ";password=" + pass;

        public DBConnection()
        {
            connection = new MySqlConnection(ConnString);
            connection.Open();
        }

        public MySqlDataReader SelectQuery(string sql)
        {
            var command = new MySqlCommand { Connection = connection, CommandText = sql };
            var result = command.ExecuteReader();
            return result;
        }

        public int UpdateQuery(string sql)
        {
            var command = new MySqlCommand { Connection = connection, CommandText = sql };
            var result = command.ExecuteNonQuery();
            return result;
        }

        public int InsertQuery(string sql)
        {
            var command = new MySqlCommand { Connection = connection, CommandText = sql };
            var result = command.ExecuteNonQuery();
            return result;
        }

        public void Close()
        {
            connection.Close();
        }
        public void AddAccount(string login, string password)
        {
            string sql = $"INSERT INTO Account (login, pass) VALUES ('{login}', '{password}')";
            var command = new MySqlCommand { Connection = connection, CommandText = sql };
            command.ExecuteNonQuery();
        }
    }
}
