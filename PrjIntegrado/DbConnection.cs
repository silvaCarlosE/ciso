using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace PrjIntegrado
{
    public class DbConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user_id;
        private string password;

        public DbConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "copiadora";
            user_id = "root";
            password = "root";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + user_id + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private bool estabilishConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro de conexão com o banco de dados", ex);
                return false;
            }
        }

        private bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível fechar a conexão com o banco de dados", ex);
                return false;
            }
        }

        public MySqlDataReader Select(string table, string fields)
        {
            if (this.estabilishConnection() == true)
            {
                string query = "SELECT " + fields + " FROM " + table;
                try
                {
                    MySqlCommand stmt = new MySqlCommand(query, connection);
                    MySqlDataReader reader = stmt.ExecuteReader();
                    return reader;
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível selecionar os dados.", ex);
                }
            }
            else
            {
                string query = "SELECT " + fields + " FROM " + table;
                MySqlCommand stmt = new MySqlCommand(query, connection);
                MySqlDataReader reader = stmt.ExecuteReader();
                return reader;
            }

        }

        public void Insert(string table, string fields, string values)
        {
            if (estabilishConnection() == true)
            {
                try
                {
                    string insert = "insert into " + table + "(" + fields + ") values(" + values + ")";
                    MySqlCommand cmdInsert = new MySqlCommand(insert, connection);
                    cmdInsert.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível inserir os dados", ex);
                }
            }
        }

        public void Update(string table, string fields, string condition)
        {
            if (estabilishConnection() == true)
            {
                string update = "update " + table + " set " + fields + " where " + condition;
                try
                {
                    MySqlCommand cmdUpdate = new MySqlCommand(update, connection);
                    cmdUpdate.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível atualizar o registro. " + ex);
                }
            }
        }

        public void Delete(string table, string condition)
        {
            if (estabilishConnection() == true)
            {
                try
                {
                    string delete = "delete from " + table + " where " + condition;
                    MySqlCommand cmdDelete = new MySqlCommand(delete, connection);
                    cmdDelete.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível deletar o registro. " + ex);
                }
            }
        }

        public MySqlDataReader SelectById(string table, string fields, string id_field, int id)
        {
            if (this.estabilishConnection() == true)
            {
                try
                {
                    string query = "SELECT " + fields + " FROM " + table + " WHERE " + id_field + " = " + id;
                    MySqlCommand stmt = new MySqlCommand(query, connection);
                    MySqlDataReader reader = stmt.ExecuteReader();
                    return reader;
                }
                catch (Exception ex)
                {

                    throw new Exception("Não foi possível localizar nenhum registro", ex);
                }
            }
            else
            {
                string query = "SELECT " + fields + " FROM " + table + " WHERE " + id_field + " = " + id;
                MySqlCommand stmt = new MySqlCommand(query, connection);
                MySqlDataReader reader = stmt.ExecuteReader();
                return reader;
            }
        }

        public MySqlDataReader Search(string table, string like)
        {
            if (this.estabilishConnection() == true)
            {
                try
                {
                    string query = "SELECT * " + " FROM " + table + " WHERE " + like;
                    MessageBox.Show(query);
                    MySqlCommand stmt = new MySqlCommand(query, connection);
                    MySqlDataReader reader = stmt.ExecuteReader();
                    return reader;
                }
                catch (Exception ex)
                {

                    throw new Exception("Não foi possível localizar nenhum registro", ex);
                }
            }
            else
            {
                string query = "SELECT * " + " FROM " + table + " WHERE " + like;
                MySqlCommand stmt = new MySqlCommand(query, connection);
                MySqlDataReader reader = stmt.ExecuteReader();
                return reader;
            }
        }
    }
}