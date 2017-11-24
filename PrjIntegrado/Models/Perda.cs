using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class Perda
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public string Data { get; set; }
        public int Id_tipo_papel { get; set; }
        public int Id_funcionario { get; set; }

        public List<Perda> getPerdas()
        {
            DbConnection dbConnection = new DbConnection();
            List<Perda> perdas = new List<Perda>();
            string tableName = "perdas";
            string fields = " id_perda, quantidade, dataperda, id_tipo_papel, id_func ";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                Perda aux = new Perda();
                while (result.Read())
                {                    
                    aux.Id = result.GetInt32(0);
                    aux.Quantidade = result.GetInt32(1);
                    aux.Data = result.GetString(2);
                    aux.Id_tipo_papel = result.GetInt32(3);
                    aux.Id_funcionario = result.GetInt32(4);
                    perdas.Add(aux);
                }
            }
            return perdas;
        }

        public List<Perda> getPerdas(string quantidade)
        {
            DbConnection dbConnection = new DbConnection();
            List<Perda> perdas = new List<Perda>();
            string statement = "SELECT SUM(QUANTIDADE) FROM PERDAS GROUP BY ID_FUNC";
            var result = dbConnection.GenericQuery(statement);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Perda aux = new Perda();
                    aux.Id = result.GetInt32(0);
                    aux.Quantidade = result.GetInt32(1);
                    aux.Data = result.GetString(2);
                    aux.Id_tipo_papel = result.GetInt32(3);
                    aux.Id_funcionario = result.GetInt32(4);
                    perdas.Add(aux);
                }
            }
            return perdas;
        }

        public bool DeletePerda(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string tableName = "perdas";
            string condition = " id_perda = " + (id.ToString());
            dbConnection.Delete(tableName, condition);
            return true;
        }

        public Perda selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "perdas";
            string fields = " id_perda, quantidade, dataperda, id_tipo_papel, id_func ";
            string id_field = "id_perda";
            int id_perda = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_perda);
            Perda aux = new Perda();
            if (result.HasRows)
            {
                result.Read();
                aux.Id = result.GetInt32(0);
                aux.Quantidade = result.GetInt32(1);
                aux.Data = result.GetString(2);
                aux.Id_tipo_papel = result.GetInt32(3);
                aux.Id_funcionario = result.GetInt32(4);
            }
            return aux;
        }

        internal bool Insert(Perda aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "perdas ";
            string fields = "quantidade, dataperda, id_tipo_papel, id_func ";
            string values = aux.Quantidade.ToString();
            values += ", " + "'" + aux.Data + "'";
            values += ", " + aux.Id_tipo_papel + ",";
            values += aux.Id_funcionario;
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public bool Update(Perda aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "perdas";
            string fields = "quantidade = " + aux.Quantidade;
            fields += ", dataperda = '" + aux.Data + "'";
            fields += ", id_tipo_papel = " + aux.Id_tipo_papel.ToString();
            fields += ", id_func = " + aux.Id_funcionario;
            string condition = "id_perda = " + (aux.Id.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }

        public List<Funcionario> GetFuncionarios()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "funcionarios";
            string fields = "id_func, nome, cargo, cpf, id_loja";
            var result = dbConnection.Select(table, fields);
            List<Funcionario> tecnicos = new List<Funcionario>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Funcionario aux = new Funcionario();
                    aux.FuncionarioID = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    aux.Cargo = result.GetString(2);
                    aux.CPF = result.GetString(3);
                    aux.LojaFuncionarioID = result.GetString(4);
                    tecnicos.Add(aux);
                }
            }
            return tecnicos;
        }

        public List<TipoPapel> getTiposPapel()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "tipo_papel";
            string fields = "id_tipo_papel, tipo, tamanho, gramatura";
            var result = dbConnection.Select(table, fields);
            List<TipoPapel> tipos = new List<TipoPapel>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    TipoPapel aux = new TipoPapel();
                    aux.Id = result.GetInt32(0);
                    aux.Tipo = result.GetString(1);
                    aux.Tamanho = result.GetString(2);
                    aux.Gramatura = result.GetInt32(3);
                    tipos.Add(aux);
                }
            }
            return tipos;
        }
    }
}