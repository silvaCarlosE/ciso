using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class SolicitarPapel
    {
        public int Ids { get; set; }
        public int IdTipo { get; set; }


        public List<SolicitarPapel> GetSolicitarPapel()
        {
            DbConnection dbConnection = new DbConnection();
            List<SolicitarPapel> SolicitarPapel = new List<SolicitarPapel>();
            string tableName = "solicitacoes_papel";
            string fields = " id_solicitacao, id_tipo_papel";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {

                    SolicitarPapel aux = new SolicitarPapel();

                    aux.Ids = result.GetInt32(0);
                    aux.IdTipo = result.GetInt32(1);

                    SolicitarPapel.Add(aux);
                }
            }
            return SolicitarPapel;
        }


        public List<TipoPapel> GetTipoPapel()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tipo_papel";
            string fields = "id_tipo_papel, tipo, tamanho, gramatura";
            var result = dbConnection.Select(table, fields);
            List<TipoPapel> tipo_papel = new List<TipoPapel>();
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

        public bool DeleteSolicitarPapel(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string tableName = "solicitacoes_papel";
            string condition = " id_solicitacao  = " + (id.ToString());
            dbConnection.Delete(tableName, condition);
            return true;
        }

        public SolicitarPapel selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "solicitacoes_papel";
            string field = "id_solicitacao, id_tipo_papel";
            string id_field = " id_solicitacao";
            int id_solicitacao = id;
            var result = dbConnection.SelectById(table,field,id_field,id_solicitacao);
            SolicitarPapel aux = new SolicitarPapel();
            if (result.HasRows)
            {
                result.Read();
                aux.Ids = result.GetInt32(0);
                aux.IdTipo = result.GetInt32(1);
                

            }
            return aux;
        }



        internal bool Insert(SolicitarPapel aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "solicitacoes_papel";
            string field = " id_tipo_papel";
            string values =""+ aux.IdTipo ;
 
            dbConnection.Insert(table, field, values);
            

            return true;
        }

        public bool Update(SolicitarPapel aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "solicitacoes_papel";
            string fields = "id_solicitacao = '" + aux.Ids;
            fields += "', id_tipo_papel = '" + aux.IdTipo + "'";;
            string condition = "id_solicitacao = " + (aux.Ids.ToString());
            dbConnection.Update(table, fields, condition);
            
            return true;
        }
    }
}


