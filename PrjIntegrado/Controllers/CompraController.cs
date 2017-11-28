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

                SolicitarPapel auxSol = new SolicitarPapel();
                List<SolicitarPapel> solicitacoes = new List<SolicitarPapel>();
                solicitacoes = auxSol.GetSolicitarPapel();
                ViewData["solicitacoes"] = solicitacoes;

                return View(list);
            }
        }

            [HttpPost]
            public ActionResult Create(System.Web.Mvc.FormCollection collection)
            {
                Compra compra = new Compra();
                compra.SolicitacaoID = int.Parse(collection[1]);
                compra.Quantidade = int.Parse(collection[2]);
                compra.IdTipoPapel = int.Parse(collection[3]);
                compra.Data = collection[4];
                compra.Valor = float.Parse(collection[5].ToString());
                bool result = compra.Insert(compra);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
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

            SolicitarPapel solicitar = new SolicitarPapel();
            List<SolicitarPapel> solicitacoes = new List<SolicitarPapel>();
            solicitacoes = solicitar.GetSolicitarPapel();

            ViewData["solicitacoes"] = solicitacoes;
            ViewData["tiposPapel"] = tiposPapel;
            ViewData["aux"] = aux;
            return View();
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            Compra aux = new Compra();
            aux.CompraID = int.Parse(collection[0]);
            aux.SolicitacaoID = int.Parse(collection[1]);
            aux.Quantidade = int.Parse(collection[2]);
            aux.IdTipoPapel = int.Parse(collection[3]);
            aux.Data = collection[4];
            aux.Valor = float.Parse(collection[5]);
            bool result = aux.Save(aux);
            if (result == true)
            {
                TempData["notice"] = "inserted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
            Compra aux = new Compra();
            bool result;
            result = aux.Delete(idToExclude);
            if (result == true)
            {
                TempData["notice"] = "inserted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            string data = "";
            if (collection[0] != "")
            {
                data = collection[0];
            }
            Compra aux = new Compra();
            List<Compra> list = new List<Compra>();
            list = aux.getCompra(data);
            ViewData["list"] = list;

            TipoPapel auxTipo = new TipoPapel();

            List<TipoPapel> tiposPapel = new List<TipoPapel>();
            tiposPapel = auxTipo.getTipoPapel();
            ViewData["tiposPapel"] = tiposPapel;

            SolicitarPapel auxSol = new SolicitarPapel();
            List<SolicitarPapel> solicitacoes = new List<SolicitarPapel>();
            solicitacoes = auxSol.GetSolicitarPapel();
            ViewData["solicitacoes"] = solicitacoes;

            return View(list);
        }
    }
}