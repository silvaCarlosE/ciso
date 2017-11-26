using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrjIntegrado.Controllers
{
    public class RelatorioGastosController : Controller
    {
        public ActionResult Index()
        {
            RelatorioGastos aux = new RelatorioGastos();
            List<RelatorioGastos> list = new List<RelatorioGastos>();
            list = aux.GetGastos(0, "");
            List<string> names = new List<string>();
            List<double> quantities = new List<double>();
            foreach (var item in list)
            {
                names.Add(item.Nome);
                quantities.Add(item.Valor);
            }

            ViewData["listQuantity"] = quantities;
            ViewData["listNames"] = names;
            return View();
        }

        public ActionResult Filter(System.Web.Mvc.FormCollection collection)
        {
            RelatorioGastos aux = new RelatorioGastos();
            List<RelatorioGastos> list = new List<RelatorioGastos>();
            int type = 0;

            if (collection[0] == "")
            {
                type = 0;
                list = aux.GetGastos(0, "");

            }
            else if (collection[0] != "")
            {
                list = aux.GetGastos(1, collection[0]);
                type = 1;
            }

            List<string> names = new List<string>();
            List<double> quantities = new List<double>();

            foreach (var item in list)
            {
                names.Add(item.Nome);
                quantities.Add(item.Valor);
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