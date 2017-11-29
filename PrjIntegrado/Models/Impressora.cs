using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class Impressora
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Tipo_tinta { get; set; }
        public int Id_loja { get; set; }

        public List<Impressora> getImpressoras()
        {
            DbConnection dbConnection = new DbConnection();
            List<Impressora> impressoras = new List<Impressora>();
            string tableName = "impressoras";
            string fields = " id_impressora, nome, marca, tipo_tinta, id_loja ";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Impressora aux = new Impressora();
                    aux.Id = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    aux.Marca = result.GetString(2);
                    aux.Tipo_tinta = result.GetString(3);
                    aux.Id_loja = result.GetInt32(4);
                    impressoras.Add(aux);
                }
            }
            return impressoras;
        }

        public List<Impressora> getImpressoras(string nome)
        {
            DbConnection dbConnection = new DbConnection();
            List<Impressora> impressoras = new List<Impressora>();
            string table = "impressoras";
            string like = " nome LIKE '%" + nome + "%'";
            var result = dbConnection.Search(table, like);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Impressora aux = new Impressora();
                    aux.Id = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    aux.Marca = result.GetString(2);
                    aux.Tipo_tinta = result.GetString(3);
                    aux.Id_loja = result.GetInt32(4);
                    impressoras.Add(aux);
                }
            }
            return impressoras;
        }

        public bool DeleteImpressora(int id)
        {
            DbConnection dbConnection1 = new DbConnection();
            string stmt = "SELECT * FROM manutencoes WHERE id_impressora = " + id;
            var result = dbConnection1.GenericQuery(stmt);
            if (result.HasRows)
            {
                return false;
            }
            DbConnection dbConnection = new DbConnection();
            string tableName = "impressoras";
            string condition = " id_impressora = " + (id.ToString());
            dbConnection.Delete(tableName, condition);
            return true;
        }

        public Impressora selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "impressoras";
            string fields = " id_impressora, nome, marca, tipo_tinta, id_loja ";
            string id_field = "id_impressora";
            int id_impressora = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_impressora);
            Impressora aux = new Impressora();
            if (result.HasRows)
            {
                result.Read();
                aux.Id = result.GetInt32(0);
                aux.Nome = result.GetString(1);
                aux.Marca = result.GetString(2);
                aux.Tipo_tinta = result.GetString(3);
                aux.Id_loja = result.GetInt32(4);
            }
            return aux;
        }

        internal bool Insert(Impressora aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "impressoras ";
            string fields = "nome, marca, tipo_tinta, id_loja";
            string values = "'" + aux.Nome + "'" + ", " + "'" + aux.Marca + "', '" + aux.Tipo_tinta + "', " + aux.Id_loja + "";
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public bool Update(Impressora aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "impressoras";
            string fields = "nome = '" + aux.Nome + "', marca = '" + aux.Marca + "'" + ", tipo_tinta = '" + aux.Tipo_tinta + "'" + ", id_loja = " + aux.Id_loja;
            string condition = "id_impressora = " + (aux.Id.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }

        public List<int> GetLojas()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "unidades";
            string fields = "id_loja";
            var result = dbConnection.Search(table, fields);
            List < int > ids = new List<int>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Impressora aux = new Impressora();
                    int id = result.GetInt32(0);
                    ids.Add(id);
                }
            }
            return ids;
        }
    }
}