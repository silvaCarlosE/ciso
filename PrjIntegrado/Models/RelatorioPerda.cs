using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace PrjIntegrado.Models
{
    public class RelatorioPerda
    {
        public List<Perda> GetRanking(int type, string data)
        {
            DbConnection dbConnection = new DbConnection();
            List<Perda> perdas = new List<Perda>();
            DateTime dateTime = DateTime.UtcNow.Date;
            string sysDate = dateTime.ToString("yyyy-MM-dd");
            string stmt;
            if (type == 0)
            {
                stmt = " SELECT ID_FUNC, SUM(QUANTIDADE) FROM PERDAS GROUP BY ID_FUNC";
            }
            else
            {
                stmt = " SELECT ID_FUNC, SUM(QUANTIDADE) FROM PERDAS ";
                string where = "WHERE dataperda BETWEEN '" + data + "' AND '" + sysDate + "' GROUP BY ID_FUNC";
                stmt += where;
            }
            MessageBox.Show(stmt);
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