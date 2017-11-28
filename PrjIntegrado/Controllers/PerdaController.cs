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
    public class PerdaController : Controller
    {
        // GET: Perda
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                Perda aux = new Perda();
                List<Perda> list = new List<Perda>();
                List<Funcionario> funcionarios = new List<Funcionario>();
                List<TipoPapel> tiposPapel = new List<TipoPapel>();
                list = aux.getPerdas();
                funcionarios = aux.GetFuncionarios();
                tiposPapel = aux.getTiposPapel();
                if (funcionarios.Count == 0 || tiposPapel.Count == 0)
                {
                    ViewData["errorMsg"] = "É necessário o cadastro de ao menos um funcionário e um tipo de papel para realizar o cadastro de uma perda";
                }
                else
                {
                    ViewData["errorMsg"] = "";
                }
                ViewData["funcionarios"] = funcionarios;
                ViewData["tiposPapel"] = tiposPapel;
                ViewBag.List = list;
                return View(list);
            }
        }

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
        {
            Perda aux = new Perda();
            aux = aux.selectById(int.Parse(collection[0]));
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<TipoPapel> tiposPapel = new List<TipoPapel>();
            funcionarios = aux.GetFuncionarios();
            tiposPapel = aux.getTiposPapel();

            ViewData["tiposPapel"] = tiposPapel;
            ViewData["funcionarios"] = funcionarios;
            ViewData["Perda"] = aux;
            return View();
        }

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
            Perda aux = new Perda();
            bool result;
            result = aux.DeletePerda(idToExclude);
            if (result == true)
            {
                TempData["notice"] = "inserted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            Perda aux = new Perda();
            aux.Id = int.Parse(collection[0]);
            aux.Quantidade = int.Parse(collection[1]);
            aux.Data = collection[2];
            aux.Id_tipo_papel = int.Parse(collection[3]);
            aux.Id_funcionario = int.Parse(collection[4]);
            bool result = aux.Update(aux);
            if (result == true)
            {
                TempData["notice"] = "inserted";
            }
            ViewData["actionResult"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Insert(System.Web.Mvc.FormCollection collection)
        {
            Perda aux = new Perda();
            aux.Quantidade = int.Parse(collection[1]);
            aux.Data = collection[2];
            aux.Id_tipo_papel = int.Parse(collection[3]);
            aux.Id_funcionario = int.Parse(collection[4]);
            bool result;
            result = aux.Insert(aux);
            if (result == true)
            {
                TempData["notice"] = "inserted";
            }
            ViewData["actionResult"] = result;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            string data = collection[0];
            Perda aux = new Perda();
            List<Perda> list = new List<Perda>();
            List<Funcionario> funcionarios = new List<Funcionario>();
            List<TipoPapel> tiposPapel = new List<TipoPapel>();
            list = aux.SearchPerda(data);
            funcionarios = aux.GetFuncionarios();
            tiposPapel = aux.getTiposPapel();
            if (funcionarios.Count == 0 || tiposPapel.Count == 0)
            {
                ViewData["errorMsg"] = "É necessário o cadastro de ao menos um funcionário e um tipo de papel para realizar o cadastro de uma perda";
            }
            else
            {
                ViewData["errorMsg"] = "";
            }
            ViewData["funcionarios"] = funcionarios;
            ViewData["tiposPapel"] = tiposPapel;
            ViewBag.List = list;
            return View(list);
        }
    }
}