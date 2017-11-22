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
    }
}