using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Windows.Forms;

namespace PrjIntegrado.Models
{
    public class Manutencao
    {
        public int Id { get; set; }
        public string Defeito { get; set; }
        public string Data { get; set; }
        public int Id_tecnico { get; set; }
        public int Id_impressora { get; set; }
        public double Valor_gasto { get; set; }

        public List<Manutencao> getManutencoes()
        {
            DbConnection dbConnection = new DbConnection();
            List<Manutencao> manutencao = new List<Manutencao>();
            string tableName = "manutencoes";
            string fields = " id_manut, defeito, DATE_FORMAT(data_manut, '%Y-%m-%d'), id_tecnico, Id_impressora, valor_gasto ";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Manutencao aux = new Manutencao();
                    aux.Id = result.GetInt32(0);
                    aux.Defeito = result.GetString(1);
                    aux.Data = result.GetString(2);
                    aux.Id_tecnico = result.GetInt32(3);
                    aux.Id_impressora = result.GetInt32(4);
                    aux.Valor_gasto = result.GetDouble(5);
                    manutencao.Add(aux);
                }
            }
            return manutencao;
        }

        public List<Manutencao> getManutencoes(string defeito)
        {
            DbConnection dbConnection = new DbConnection();
            List<Manutencao> manutencao = new List<Manutencao>();
            string table = "manutencoes";
            string like = " defeito LIKE '%" + defeito + "%'";
            var result = dbConnection.Search(table, like);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Manutencao aux = new Manutencao();
                    aux.Id = result.GetInt32(0);
                    aux.Defeito = result.GetString(1);
                    aux.Data = result.GetString(2);
                    aux.Id_tecnico = result.GetInt32(3);
                    aux.Id_impressora = result.GetInt32(4);
                    aux.Valor_gasto = result.GetDouble(5);
                    manutencao.Add(aux);
                }
            }
            return manutencao;
        }

        public bool DeleteManutencao(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string tableName = "manutencoes";
            string condition = " id_manut = " + (id.ToString());
            dbConnection.Delete(tableName, condition);
            return true;
        }

        public Manutencao selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "manutencoes";
            string fields = " id_manut, defeito, DATE_FORMAT(data_manut, '%Y-%m-%d'), id_tecnico, id_impressora, valor_gasto ";
            string id_field = "id_manut";
            int id_manut = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_manut);
            Manutencao aux = new Manutencao();
            if (result.HasRows)
            {
                result.Read();
                aux.Id = result.GetInt32(0);
                aux.Defeito = result.GetString(1);
                aux.Data = result.GetString(2);
                aux.Id_tecnico = result.GetInt32(3);
                aux.Id_impressora = result.GetInt32(4);
                aux.Valor_gasto = result.GetDouble(5);
            }
            return aux;
        }

        internal bool Insert(Manutencao aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "manutencoes ";
            string fields = "defeito, data_manut, id_tecnico, id_impressora, valor_gasto ";
            string values = "'" + aux.Defeito + "'";
                    values += ", " + "'" + aux.Data + "'";
                    values += ", " + aux.Id_tecnico + ",";
                    values += aux.Id_impressora + ", ";
                    values += aux.Valor_gasto;
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public bool Update(Manutencao aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "manutencoes";
            string fields = "defeito = '" + aux.Defeito;
                    fields += "', data_manut = '" + aux.Data + "'";
                    fields += ", id_tecnico = " + aux.Id_tecnico.ToString();
                    fields += ", id_impressora = " + aux.Id_impressora;
                    fields += ", valor_gasto =" + aux.Valor_gasto.ToString();
            string condition = "id_manut = " + (aux.Id.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }

        public List<Tecnico> GetTecnicos()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tecnicos";
            string fields = "id_tecnico, nome";
            var result = dbConnection.Select(table, fields);
            List<Tecnico> tecnicos = new List<Tecnico>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Tecnico aux = new Tecnico();
                    aux.Id = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    tecnicos.Add(aux);
                }
            }
            return tecnicos;
        }

        public List<Impressora> GetImpressoras()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "impressoras";
            string fields = "id_impressora, nome";
            var result = dbConnection.Select(table, fields);
            List<Impressora> impressoras = new List<Impressora>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Impressora aux = new Impressora();
                    aux.Id = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    impressoras.Add(aux);
                }
            }
            return impressoras;
        }
    }
}