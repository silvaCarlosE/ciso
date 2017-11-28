using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjIntegrado.Models;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PrjIntegrado.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {
                return RedirectToAction("Index", "Login");
            }

            else
            {
                Funcionario aux = new Funcionario();
                List<Funcionario> list = new List<Funcionario>();
                list = aux.getFuncionarios();
                ViewData["list"] = list;

                Loja auxLoja = new Loja();

                List<Loja> listaLojas = new List<Loja>();
                listaLojas = auxLoja.getLojas();
                ViewData["listaLojas"] = listaLojas;

                return View(list);
            }

        }


        public ActionResult Create()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                Funcionario funcionario = new Funcionario();
                funcionario.Nome = collection[1];
                funcionario.CPF = collection[2];
                funcionario.Cargo = collection[3];
                funcionario.LojaFuncionarioID = collection[4];
                bool result = funcionario.Insert(funcionario);
                return RedirectToAction("Index");
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
                Funcionario aux = new Funcionario();
                aux.FuncionarioID = int.Parse(collection[0]);
                aux = aux.selectById(aux.FuncionarioID);
                ViewData["aux"] = aux;
                Loja loja = new Loja();
                List<Loja> listaLojas = loja.getLojas();
                ViewData["listaLojas"] = listaLojas;
                bool result = aux.Save(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                return View();
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
                Funcionario aux = new Funcionario();
                aux.FuncionarioID = int.Parse(collection[0]);
                aux.Nome = collection[1];
                aux.CPF = collection[2];
                aux.Cargo = collection[3];
                aux.LojaFuncionarioID = collection[4];
                TempData["notice"] = "inserted";
                bool result = aux.Save(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                return RedirectToAction("Index");
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
                Funcionario aux = new Funcionario();
                bool result;
                result = aux.Delete(idToExclude);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                return RedirectToAction("Index");
            }
        }

        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                string name = "";
                if (collection[0] != null)
                {
                    name = collection[0];
                }
                Funcionario aux = new Funcionario();
                List<Funcionario> list = new List<Funcionario>();
                list = aux.getFuncionarios(name);
                ViewData["list"] = list;

                Loja auxLoja = new Loja();

                List<Loja> listaLojas = new List<Loja>();
                listaLojas = auxLoja.getLojas();
                ViewData["listaLojas"] = listaLojas;

                return View(list);
            }
        }
    }
}