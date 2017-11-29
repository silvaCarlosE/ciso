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
    public class ManutencaoController : Controller
    {
        // GET: Manutencao
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                Manutencao aux = new Manutencao();
                List<Manutencao> list = new List<Manutencao>();
                List<Tecnico> tecnicos = new List<Tecnico>();
                List<Impressora> impressoras = new List<Impressora>();

                list = aux.getManutencoes();
                tecnicos = aux.GetTecnicos();
                impressoras = aux.GetImpressoras();
                if (tecnicos.Count == 0 || impressoras.Count == 0)
                {
                    ViewData["errorMsg"] = "É necessário o cadastro de ao menos um técnico e uma impressora para realizar o cadastro de uma manutenção";
                }
                else
                {
                    ViewData["errorMsg"] = "";
                }
                ViewData["tecnicos"] = tecnicos;
                ViewData["impressoras"] = impressoras;
                ViewBag.List = list;
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
                Manutencao aux = new Manutencao();
                aux = aux.selectById(int.Parse(collection[0]));
                List<Tecnico> tecnicos = new List<Tecnico>();
                List<Impressora> impressoras = new List<Impressora>();
                tecnicos = aux.GetTecnicos();
                impressoras = aux.GetImpressoras();

                ViewData["impressoras"] = impressoras;
                ViewData["tecnicos"] = tecnicos;
                ViewData["Manutencao"] = aux;
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
                Manutencao aux = new Manutencao();
                bool result;
                result = aux.DeleteManutencao(idToExclude);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                else
                {
                    TempData["notice"] = "error";
                }
                ViewData["actionResult"] = result;
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
                Manutencao aux = new Manutencao();
                aux.Id = int.Parse(collection[0]);
                aux.Defeito = collection[1];
                aux.Data = collection[2];
                aux.Id_tecnico = int.Parse(collection[3]);
                aux.Id_impressora = int.Parse(collection[4]);
                aux.Valor_gasto = double.Parse(collection[5]);
                bool result = aux.Update(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                ViewData["actionResult"] = result;
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
                Manutencao aux = new Manutencao();
                aux.Defeito = collection[1];
                aux.Data = collection[2];
                aux.Id_tecnico = int.Parse(collection[3]);
                aux.Id_impressora = int.Parse(collection[4]);
                aux.Valor_gasto = int.Parse(collection[5]);
                bool result;
                result = aux.Insert(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                ViewData["actionResult"] = result;
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
                string data = collection[0];
                Manutencao aux = new Manutencao();
                List<Manutencao> list = new List<Manutencao>();
                List<Tecnico> tecnicos = new List<Tecnico>();
                List<Impressora> impressoras = new List<Impressora>();

                list = aux.getManutencoes(data);
                tecnicos = aux.GetTecnicos();
                impressoras = aux.GetImpressoras();
                ViewData["tecnicos"] = tecnicos;
                ViewData["impressoras"] = impressoras;
                ViewBag.List = list;
                return View(list);
            }
        }
    }
}