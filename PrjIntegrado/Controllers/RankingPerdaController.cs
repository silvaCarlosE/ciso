using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.Extensions;

namespace PrjIntegrado.Controllers
{
    public class RankingPerdaController : Controller
    {
        // GET: RankingPerda
        public ActionResult Index()
        {
            RankingPerda aux = new RankingPerda();
            List<Perda> list = new List<Perda>();
            list = aux.GetRanking();
            Funcionario auxFunc = new Funcionario();
            List<string> names = new List<string>();
            List<double> quantities = new List<double>();
            foreach (var item in list)
            {
                auxFunc = auxFunc.selectById(item.Id_funcionario);
                names.Add(auxFunc.Nome);
                quantities.Add(double.Parse(item.Quantidade.ToString()));                
            }

            ViewData["listQuantity"] = quantities;
            ViewData["listNames"] = names;
            return View();
        }
    }
}