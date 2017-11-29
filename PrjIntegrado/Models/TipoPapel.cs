using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class TipoPapel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Campo Tipo é obrigatório")]
        public string Tipo { get; set; }
        [Required(ErrorMessage = "Campo Tamanho é obrigatório")]
        public string Tamanho { get; set; }
        [Required(ErrorMessage = "Campo Gramatura é obrigatório")]
        public int Gramatura { get; set; }

        public List<TipoPapel> getTipoPapel()
        {
            DbConnection dbConnection = new DbConnection();
            List<TipoPapel> tipo_papel = new List<TipoPapel>();
            string tableName = "tipo_papel";
            string fields = " id_tipo_papel, tipo, tamanho, gramatura";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    TipoPapel aux = new TipoPapel();
                    aux.Id = result.GetInt32(0);
                    aux.Tipo = result.GetString(1);
                    aux.Tamanho = result.GetString(2);
                    aux.Gramatura = result.GetInt32(3);
                    tipo_papel.Add(aux);
                }
            }
            return tipo_papel;
        }

        public List<TipoPapel> getTipoPapel(string tipo)
        {
            DbConnection dbConnection = new DbConnection();
            List<TipoPapel> tipo_papel = new List<TipoPapel>();
            string table = "tipo_papel";
            string like = " tipo LIKE '%" + tipo + "%'";
            var result = dbConnection.Search(table, like);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    TipoPapel aux = new TipoPapel();
                    aux.Id = result.GetInt32(0);
                    aux.Tipo = result.GetString(1);
                    aux.Tamanho = result.GetString(2);
                    aux.Gramatura = result.GetInt32(3);
                    tipo_papel.Add(aux);
                }
            }
            return tipo_papel;
        }

        public bool DeleteTipoPapel(int id)
        {
            DbConnection dbConnectionX = new DbConnection();
            string tableName = "tipo_papel";
            string condition = " id_tipo_papel = " + (id.ToString());
            string stmt = "SELECT * FROM papel_comprado WHERE id_tipo_papel = " + id;
            var result = dbConnectionX.GenericQuery(stmt);            
            stmt = "SELECT * FROM perdas WHERE id_tipo_papel = " + id;
            DbConnection dbConnection1 = new DbConnection();
            var result1 = dbConnection1.GenericQuery(stmt);
            stmt = "SELECT * FROM solicitacoes_papel WHERE id_tipo_papel = " + id;
            DbConnection dbConnection2 = new DbConnection();
            var result2 = dbConnection2.GenericQuery(stmt);
            DbConnection dbConnection = new DbConnection();
            if (result.HasRows || result1.HasRows || result2.HasRows)
            {
                return false;
            }
            dbConnection.Delete(tableName, condition);
            return true;
        }

        public TipoPapel selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tipo_papel";
            string fields = " id_tipo_papel, tipo, tamanho, gramatura";
            string id_field = "id_tipo_papel";
            int id_tipo_papel = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_tipo_papel);
            TipoPapel aux = new TipoPapel();
            if (result.HasRows)
            {
                result.Read();
                aux.Id = result.GetInt32(0);
                aux.Tipo = result.GetString(1);
                aux.Tamanho = result.GetString(2);
                aux.Gramatura = result.GetInt32(3);

            }
            return aux;
        }

        

        internal bool Insert( TipoPapel aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tipo_papel ";
            string fields = "tipo, tamanho,gramatura ";
            string values = "'" + aux.Tipo + "'";
                   values += ", " + "'" + aux.Tamanho + "'";
                   values += ", " + aux.Gramatura;
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public bool Update(TipoPapel aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tipo_papel ";
            
            string fields = "tipo = '" + aux.Tipo;
            fields += "', tamanho = '" + aux.Tamanho + "'";
            fields += ", gramatura = " + aux.Gramatura;
           
            string condition = "id_tipo_papel = " + (aux.Id.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }
    }
}