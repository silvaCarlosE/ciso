using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjIntegrado.Models;

namespace PrjIntegrado.Controllers
{
    public class FuncionarioController : Controller
    {
        // GET: Funcionario
        public ActionResult Index()
        {
            Funcionario aux = new Funcionario();
            List<Funcionario> list = new List<Funcionario>();
            list = aux.getFuncionarios();
            //ViewData["list"] = list;
            ViewBag.Func = list;

            Loja auxLoja = new Loja();

            List<Loja> listaLojas = new List<Loja>();
            listaLojas = auxLoja.getLojas();

            //Descobrir como passar o parametro
            ViewBag.List = listaLojas;

            return View(list);


        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(System.Web.Mvc.FormCollection collection)
        {


            Funcionario funcionario = new Funcionario();
            funcionario.Nome = collection[1];
            funcionario.CPF = collection[2];
            funcionario.Cargo = collection[3];
            funcionario.LojaFuncionarioID = "7";
            bool result = funcionario.Insert(funcionario);
            return RedirectToAction("Index");
        }
    }
}