using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class Login
    {
        public int IdFuncionario { get; set; }
        public String UserName { get; set; }
        public String Senha { get; set; }


        internal bool VerificaUsuarios(String UserName, String Senha)
        {
            DbConnection dbConnection = new DbConnection();
            string stmt = " SELECT username, senha FROM usuarios where username=" + "'" + (UserName) + "'" + " and senha = " + "'" + (Senha) + "'";
            var result = dbConnection.GenericQuery(stmt);

            Login aux = new Login();
            if (result.HasRows)
            {
                while (result.Read())
                {

                    aux.UserName = result.GetString(0);
                    aux.Senha = result.GetString(1);

                }

            }
            if (aux.UserName == UserName && aux.Senha == Senha)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public List<Funcionario> GetFuncionario()
        {
            DbConnection dbConnection = new DbConnection();
            string table = "funcionarios";
            string fields = "id_func, nome";
            var result = dbConnection.Select(table, fields);
            List<Funcionario> Funcionario = new List<Funcionario>();
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Funcionario aux = new Funcionario();
                    aux.FuncionarioID = result.GetInt32(0);
                    aux.Nome = result.GetString(1);
                    Funcionario.Add(aux);
                }
            }
            return Funcionario;
        }


    }
}