using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace PrjIntegrado.Models
{
    public class RelatorioManutencao
    {
        public int QuantManut { get; set; }
        public int IdImpressora { get; set; }
        public double Valor { get; set; }

        public List<RelatorioManutencao> GetManutencoes(int type, string data)
        {
            DbConnection dbConnection = new DbConnection();
            List<RelatorioManutencao> manutencoes = new List<RelatorioManutencao>();
            string stmt = "";
            DateTime dateTime = DateTime.UtcNow.Date;
            string sysDate = dateTime.ToString("yyyy-MM-dd");
            if (type == 0)
            {
                stmt = " SELECT COUNT(ID_MANUT), ID_IMPRESSORA FROM MANUTENCOES GROUP BY ID_IMPRESSORA";
            }else if (type == 1)
            {
                stmt = " SELECT SUM(VALOR_GASTO), ID_IMPRESSORA FROM MANUTENCOES GROUP BY ID_IMPRESSORA";
            }else if (type == 2)
            {
                stmt = " SELECT COUNT(ID_MANUT), ID_IMPRESSORA FROM MANUTENCOES ";
                string where = "WHERE data_manut BETWEEN '" + data + "' AND '" + sysDate + "' GROUP BY ID_IMPRESSORA";
                stmt += where;
            }else if (type == 3)
            {
                stmt = " SELECT SUM(VALOR_GASTO), ID_IMPRESSORA FROM MANUTENCOES ";
                string where = "WHERE data_manut BETWEEN '" + data + "' AND '" + sysDate + "' GROUP BY ID_IMPRESSORA";
                stmt += where;
            }

            var result = dbConnection.GenericQuery(stmt);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    RelatorioManutencao aux = new RelatorioManutencao();
                    if (type == 0 || type == 2)
                    {
                        aux.QuantManut = result.GetInt32(0);
                        aux.IdImpressora = result.GetInt32(1);
                        manutencoes.Add(aux);
                    }
                    else
                    {
                        aux.Valor = result.GetDouble(0);
                        aux.IdImpressora = result.GetInt32(1);
                        manutencoes.Add(aux);
                    }
                    
                }
            }
            return manutencoes;
        }
    }
}