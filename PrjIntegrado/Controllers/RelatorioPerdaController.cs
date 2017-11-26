using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrjIntegrado.Controllers
{
    public class RelatorioPerdaController : Controller
    {
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
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

        public ActionResult Filter(System.Web.Mvc.FormCollection collection)
        {
            RelatorioPerda aux = new RelatorioPerda();
            List<Perda> list = new List<Perda>();
            int type = 0;

            if (collection[0] == "")
            {
                type = 0;
                list = aux.GetRanking(0, "");

            }
            else if (collection[0] != "")
            {
                list = aux.GetRanking(1, collection[0]);
                type = 1;
            }

            Funcionario auxImp = new Funcionario();
            List<string> names = new List<string>();
            List<double> quantities = new List<double>();

            foreach (var item in list)
            {
                auxImp = auxImp.selectById(item.Id_funcionario);
                names.Add(auxImp.Nome);
                quantities.Add(item.Quantidade);
                if (type == 1)
                {
                    ViewData["msg"] = "A busca foi feita com os parâmetros solicitados.";

                }

            }

            ViewData["listQuantity"] = quantities;
            ViewData["listNames"] = names;
            return View("Index");
        }
    }
}