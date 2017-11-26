using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace PrjIntegrado.Controllers
{
    public class CompraController : Controller
    {
        // GET: Compra
        public ActionResult Index(String UserName)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
                {

                ViewBag.UserName = UserName;
                Compra aux = new Compra();
                List<Compra> list = new List<Compra>();
                list = aux.getCompra();
                ViewData["list"] = list;

                TipoPapel auxTipo = new TipoPapel();

                List<TipoPapel> tiposPapel = new List<TipoPapel>();
                tiposPapel = auxTipo.getTipoPapel();
                ViewData["tiposPapel"] = tiposPapel;

                return View(list);
            }
        }

            [HttpPost]
            public ActionResult Create(System.Web.Mvc.FormCollection collection)
            {
                Compra compra = new Compra();
                compra.Quantidade = int.Parse(collection[1]);
                compra.IdTipoPapel = int.Parse(collection[2]);
                compra.Data = collection[3];
                compra.Valor = float.Parse(collection[4]);
                bool result = compra.Insert(compra);
                return RedirectToAction("Index");
            }

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
        {
            Compra aux = new Compra();
            aux = aux.selectById(int.Parse(collection[0]));
            List<TipoPapel> tiposPapel = new List<TipoPapel>();

            TipoPapel tipoPapel = new TipoPapel();
            tiposPapel = tipoPapel.getTipoPapel();

            ViewData["tiposPapel"] = tiposPapel;
            ViewData["aux"] = aux;
            return View();
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            Compra aux = new Compra();
            aux.CompraID = int.Parse(collection[0]);
            aux.Quantidade = int.Parse(collection[1]);
            aux.IdTipoPapel = int.Parse(collection[2]);
            aux.Data = collection[3];
            aux.Valor = float.Parse(collection[4]);
            bool result = aux.Save(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
            Compra aux = new Compra();
            bool result;
            result = aux.Delete(idToExclude);
            return RedirectToAction("Index");
        }
    }
}