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
    public class SolicitarPapelController : Controller
    {

        // GET: TipoPapel
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {

                SolicitarPapel aux = new SolicitarPapel();
                List<SolicitarPapel> list = new List<SolicitarPapel>();
                List<TipoPapel> TipoPapel = new List<TipoPapel>();


                list = aux.GetSolicitarPapel();
                TipoPapel = aux.GetTipoPapel();

                if (TipoPapel.Count == 0)
                {
                    ViewData["errorMsg"] = "É necessário o cadastro de ao menos um tipo de papel para realizar o cadastro de uma solicitação";
                }
                else
                {
                    ViewData["errorMsg"] = "";
                }
                ViewData["TipoPapel"] = TipoPapel;

                ViewData["list"] = list;
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                SolicitarPapel aux = new SolicitarPapel();
                aux = aux.selectById(int.Parse(collection[0]));
                List<TipoPapel> TipoPapel = new List<TipoPapel>();

                TipoPapel = aux.GetTipoPapel();

                ViewData["TipoPapel"] = TipoPapel;
                ViewData["SolicitarPapel"] = aux;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
                SolicitarPapel aux = new SolicitarPapel();
                bool result;
                result = aux.DeleteSolicitarPapel(idToExclude);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                SolicitarPapel aux = new SolicitarPapel();
                aux.IdTipo = int.Parse(collection[1]);
                aux.Ids = int.Parse(Regex.Replace(collection[0], " ", ""));

                bool result = aux.Update(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Insert(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                SolicitarPapel aux = new SolicitarPapel();

                aux.IdTipo = int.Parse(collection[1]);

                bool result;
                result = aux.Insert(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                string tipoPapel = "";

                if (collection[0] != "")
                {
                    tipoPapel = collection[0];
                }
                SolicitarPapel aux = new SolicitarPapel();
                List<SolicitarPapel> list = new List<SolicitarPapel>();
                List<TipoPapel> TipoPapel = new List<TipoPapel>();

                list = aux.GetSolicitarPapel(tipoPapel);
                TipoPapel = aux.GetTipoPapel();

                if (TipoPapel.Count == 0)
                {
                    ViewData["errorMsg"] = "É necessário o cadastro de ao menos um tipo de papel para realizar o cadastro de uma solicitação";
                }
                else
                {
                    ViewData["errorMsg"] = "";
                }
                ViewData["TipoPapel"] = TipoPapel;

                ViewBag.List = list;
                return View(list);
            }
        }
    }
}