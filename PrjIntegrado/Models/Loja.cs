using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class Loja
    {
        public string LojaID { get; set; }
        public string CNPJ { get; set; }
        public string Endereco { get; set; }

        public List<Loja> getLojas()
        {
            DbConnection dbConnection = new DbConnection();
            List<Loja> lojas = new List<Loja>();
            string tableName = "unidades";
            string fields = " id_loja, endereco, cnpj ";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Loja aux = new Loja();
                    aux.LojaID = result.GetString(0);
                    aux.Endereco = result.GetString(1);
                    aux.CNPJ = result.GetString(2);
                    lojas.Add(aux);
                }
            }
            return lojas;
        }
    }
}