using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class Compra
    {
        public int CompraID { get; set; }
        public int Quantidade { get; set; }
        public int IdTipoPapel { get; set; }
        public string Data { get; set; }
        public float Valor { get; set; }

        public List<Compra> getCompra()
        {
            DbConnection dbConnection = new DbConnection();
            List<Compra> compras = new List<Compra>();
            string tableName = "papel_comprado";
            string fields = " id_compra, quantidade, id_tipo_papel, data_compra, valor_gasto";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Compra aux = new Compra();
                    aux.CompraID = result.GetInt32(0);
                    aux.Quantidade = result.GetInt32(1);
                    aux.IdTipoPapel = result.GetInt32(2);
                    aux.Data = result.GetString(3);
                    aux.Valor = result.GetFloat(4);
                    compras.Add(aux);
                }
            }
            return compras;
        }

        internal bool Insert(Compra aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "papel_comprado";
            string fields = "quantidade, id_tipo_papel, data_compra, valor_gasto";
            string values = "'" + aux.Quantidade + "'" + ", " + "'" + aux.IdTipoPapel + "', '" + aux.Data + "', " + aux.Valor;
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public Compra selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "papel_comprado";
            string fields = " id_compra, quantidade, id_tipo_papel, data_compra, valor_gasto ";
            string id_field = "id_compra";
            int id_compra = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_compra);
            Compra aux = new Compra();
            if (result.HasRows)
            {
                result.Read();
                aux.CompraID = result.GetInt32(0);
                aux.Quantidade = result.GetInt32(1);
                aux.IdTipoPapel = result.GetInt32(2);
                aux.Data = result.GetString(3);
                aux.Valor = result.GetFloat(4);
            }
            return aux;
        }

        public bool Save(Compra aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "papel_comprado";
            string fields = "quantidade = '" + aux.Quantidade + "', id_tipo_papel = '" + aux.IdTipoPapel + "', data_compra = '" + aux.Data + "', valor_gasto = " + aux.Valor;
            string condition = "id_compra = " + (aux.CompraID.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }

        public bool Delete(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string tableName = "papel_comprado";
            string condition = " id_compra = " + (id.ToString());
            dbConnection.Delete(tableName, condition);
            return true;
        }
    }
}