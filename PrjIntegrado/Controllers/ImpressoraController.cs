using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;

namespace PrjIntegrado.Controllers
{
    public class ImpressoraController : Controller
    {
        // GET: Impressora
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                Impressora aux = new Impressora();
                List<Impressora> list = new List<Impressora>();
                List<int> ids = new List<int>();
                list = aux.getImpressoras();
                ids = aux.GetLojas();
                ViewData["lojas"] = ids;
                ViewBag.List = list;
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
        {
            Impressora aux = new Impressora();
            aux = aux.selectById(int.Parse(collection[0]));
            List<int> ids = new List<int>();
            ids = aux.GetLojas();
            ViewData["lojas"] = ids;
            ViewData["Impressora"] = aux;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
            Impressora aux = new Impressora();
            bool result;
            result = aux.DeleteImpressora(idToExclude);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            Impressora aux = new Impressora();
            aux.Id = int.Parse(collection[0]);
            aux.Nome = collection[1];
            aux.Marca = collection[2];
            aux.Tipo_tinta = collection[3];
            aux.Id_loja = int.Parse(collection[4]);
            bool result = aux.Update(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Insert(System.Web.Mvc.FormCollection collection)
        {
            Impressora aux = new Impressora();
            aux.Nome = collection[1];
            aux.Marca = collection[2];
            aux.Tipo_tinta = collection[3];
            aux.Id_loja = int.Parse(collection[4]);
            bool result;
            result = aux.Insert(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            string nome = collection[0];
            Impressora aux = new Impressora();
            List<Impressora> list = new List<Impressora>();
            list = aux.getImpressoras(nome);
            ViewBag.List = list;
            return View(list);
        }
    }
}