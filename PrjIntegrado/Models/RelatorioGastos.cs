using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrjIntegrado.Models
{
    public class RelatorioGastos
    {
        public String Nome { get; set; }
        public double Valor { get; set; }

        public List<RelatorioGastos> GetGastos(int type, string data)
        {
            DbConnection dbConnection = new DbConnection();
            List<RelatorioGastos> gastos = new List<RelatorioGastos>();
            var lastMonth = DateTime.Today.AddMonths(-1);
            string sysDate = lastMonth.ToString("yyyy-MM-dd");
            string stmt = "", stmt1 = "";
            if (type == 0)
            {
                stmt = " SELECT SUM(VALOR_GASTO) FROM PAPEL_COMPRADO WHERE data_compra > '" + sysDate + "'";
                stmt1 = " SELECT SUM(VALOR_GASTO) FROM MANUTENCOES WHERE data_manut > '" + sysDate + "'";
            }
            else
            {
                stmt = " SELECT SUM(VALOR_GASTO) FROM PAPEL_COMPRADO WHERE data_compra BETWEEN '" + data + "' AND '" + sysDate + "'";
                stmt1 = " SELECT SUM(VALOR_GASTO) FROM MANUTENCOES WHERE data_manut BETWEEN '" + data + "' AND '" + sysDate + "'";
            }
            
            var result = dbConnection.GenericQuery(stmt);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    RelatorioGastos aux = new RelatorioGastos();
                    if (!result.IsDBNull(0))
                    {
                        aux.Valor = result.GetDouble(0);                        
                    }
                    else
                    {
                        aux.Valor = 0.0;
                    }            
                    aux.Nome = "Papel Comprado";
                    gastos.Add(aux);
                }
            }

            DbConnection dbConnection1 = new DbConnection();
            
            result = dbConnection1.GenericQuery(stmt1);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    RelatorioGastos aux = new RelatorioGastos();
                    if (!result.IsDBNull(0))
                    {
                        aux.Valor = result.GetDouble(0);
                    }
                    else
                    {
                        aux.Valor = 0.0;
                    }
                    aux.Nome = "Manutenções";
                    gastos.Add(aux);
                }
            }

            return gastos;
        }
    }
}