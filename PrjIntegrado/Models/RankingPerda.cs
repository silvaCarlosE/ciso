using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class RankingPerda
    {
        public List<Perda> GetRanking()
        {
            DbConnection dbConnection = new DbConnection();
            List<Perda> perdas = new List<Perda>();
            string stmt = " SELECT ID_FUNC, SUM(QUANTIDADE) FROM PERDAS GROUP BY ID_FUNC";
            var result = dbConnection.GenericQuery(stmt);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Perda aux = new Perda();
                    aux.Id_funcionario = result.GetInt32(0);
                    aux.Quantidade = result.GetInt32(1);
                    perdas.Add(aux);
                }
            }
            return perdas;
        }
    }
}