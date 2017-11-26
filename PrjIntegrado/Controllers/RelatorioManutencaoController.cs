using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace PrjIntegrado.Controllers
{
    public class RelatorioManutencaoController : Controller
    {
        // GET: RelatorioManutencao
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                RelatorioManutencao aux = new RelatorioManutencao();
                List<RelatorioManutencao> list = new List<RelatorioManutencao>();
                list = aux.GetManutencoes(0, "");

                Impressora auxImp = new Impressora();
                List<string> names = new List<string>();

                List<double> quantities = new List<double>();

                foreach (var item in list)
                {
                    auxImp = auxImp.selectById(item.IdImpressora);
                    names.Add(auxImp.Nome);
                    quantities.Add(double.Parse(item.QuantManut.ToString()));
                }

                ViewData["listQuantity"] = quantities;
                ViewData["listNames"] = names;
                return View();
            }
        }

        public ActionResult Filter(System.Web.Mvc.FormCollection collection)
        {
            RelatorioManutencao aux = new RelatorioManutencao();
            List<RelatorioManutencao> list = new List<RelatorioManutencao>();
            int type = 0;

            if (collection[0] == "" && collection[1] == "0")
            {
                type = 0;
                list = aux.GetManutencoes(0, "");
                
            }else if (collection[0] == "" && collection[1] == "1")
            {
                list = aux.GetManutencoes(1, "");
                type = 1;
            }
            else if (collection[0] != "" && collection[1] == "0")
            {
                list = aux.GetManutencoes(2, collection[0]);
                type = 2;
            }
            else if (collection[0] != "" && collection[1] == "1")
            {
                list = aux.GetManutencoes(3, collection[0]);
                type = 3;
            }          
            

            Impressora auxImp = new Impressora();
            List<string> names = new List<string>();

            List<double> quantities = new List<double>();

            foreach (var item in list)
            {
                auxImp = auxImp.selectById(item.IdImpressora);
                names.Add(auxImp.Nome);
                if (type == 0 || type == 2)
                {
                    quantities.Add(item.QuantManut);
                    ViewData["msg"] = "A busca foi feita com os parâmetros solicitados.";
                    if (type == 2)
                    {
                        ViewData["msg"] = "A busca foi feita com a data solicitada.";
                    }
                }else if (type == 1 || type == 3)
                {
                    quantities.Add(item.Valor);
                    ViewData["msg"] = "A busca foi feita com os parâmetros solicitados.";
                    if (type == 3)
                    {
                        ViewData["msg"] = "A busca foi feita com a data solicitada.";
                    }
                }
                
            }

            ViewData["listQuantity"] = quantities;
            ViewData["listNames"] = names;
            return View("Index");
        }
    }
}