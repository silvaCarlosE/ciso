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
            JArray names = new JArray();
            JArray quantities = new JArray();
            foreach (var item in list)
            {
                auxFunc = auxFunc.selectById(item.Id_funcionario);
                names.Add(auxFunc.Nome);
                quantities.Add(item.Quantidade);                
            }

            JObject namesList = new JObject();
            namesList["names"] = names;

            MessageBox.Show(namesList.ToString());

            JObject quantityList = new JObject();
            quantityList["quantities"] = quantities;

            ViewData["listQuantity"] = quantityList.ToString();
            ViewData["listNames"] = namesList.ToString();
            return View();
        }
    }
}