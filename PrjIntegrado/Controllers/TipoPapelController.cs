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
    public class TipoPapelController : Controller
    {
        // GET: TipoPapel
        public ActionResult Index()
        {
            TipoPapel aux = new TipoPapel();
            List<TipoPapel> list = new List<TipoPapel>();
            list = aux.getTipoPapel();
            ViewBag.List = list;
            return View(list);
        }

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
        {
            TipoPapel aux = new TipoPapel();
            aux = aux.selectById(int.Parse(collection[0]));
            ViewData["TipoPapel"] = aux;
           
            return View();
        }

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
            TipoPapel aux = new TipoPapel();
            bool result;
            result = aux.DeleteTipoPapel(idToExclude);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            TipoPapel aux = new TipoPapel();
            aux.Id = int.Parse(collection[0]);
            aux.Tipo = collection[1];
            aux.Tamanho = collection[2];
            aux.Gramatura= int.Parse(collection[3]);
           
            bool result = aux.Update(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Insert(System.Web.Mvc.FormCollection collection)
        {
            TipoPapel aux = new TipoPapel();
            
            aux.Tipo = collection[1];
            aux.Tamanho = collection[2];
            aux.Gramatura = int.Parse(collection[3]);
            bool result;
            result = aux.Insert(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            TipoPapel aux = new TipoPapel();
            aux = aux.selectById(int.Parse(collection[0]));
            ViewData["TipoPapel"] = aux;

            return View();
        }
    }
}