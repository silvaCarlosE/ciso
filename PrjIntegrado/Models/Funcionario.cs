using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace PrjIntegrado.Models
{
    public class Funcionario
    {
        public int FuncionarioID { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cargo { get; set; }

        public string LojaFuncionarioID { get; set; }

        public List<Funcionario> getFuncionarios()
        {
            DbConnection dbConnection = new DbConnection();
            List<Funcionario> funcionarios = new List<Funcionario>();
            string tableName = "funcionarios";
            string fields = " id_func, nome, cargo, cpf, id_loja ";
            var result = dbConnection.Select(tableName, fields);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Funcionario aux = new Funcionario();
                    aux.FuncionarioID = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    aux.Cargo = result.GetString(2);
                    aux.CPF = result.GetString(3);

                    //Arrumar
                    aux.LojaFuncionarioID = result.GetString(4);
                    funcionarios.Add(aux);
                }
            }
            return funcionarios;
        }

        internal bool Insert(Funcionario aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "funcionarios";
            string fields = "nome, cpf, cargo, id_loja";
            string values = "'" + aux.Nome + "'" + ", " + "'" + aux.CPF + "', '" + aux.Cargo + "', " + aux.LojaFuncionarioID;
            dbConnection.Insert(table, fields, values);
            return true;
        }

        public bool Save(Funcionario aux)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "funcionarios";
            string fields = "nome = '" + aux.Nome + "', cpf = '" + aux.CPF + "', cargo = '" + aux.Cargo + "', id_loja = " + aux.LojaFuncionarioID;
            string condition = "id_func = " + (aux.FuncionarioID.ToString());
            dbConnection.Update(table, fields, condition);
            return true;
        }

        public Funcionario selectById(int id)
        {
            DbConnection dbConnection = new DbConnection();
            string table = "funcionarios";
            string fields = " id_func, nome, cpf, cargo, id_loja ";
            string id_field = "id_func";
            int id_func = id;
            var result = dbConnection.SelectById(table, fields, id_field, id_func);
            Funcionario aux = new Funcionario();
            if (result.HasRows)
            {
                result.Read();
                aux.FuncionarioID = result.GetInt32(0);
                aux.Nome = result.GetString(1);
                aux.CPF = result.GetString(2);
                aux.Cargo = result.GetString(3);
                aux.LojaFuncionarioID = result.GetString(4);
            }
            return aux;
        }
    }
}