﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PrjIntegrado.Models;
using System.Data;
using System.Data.Common;
using System.Windows.Forms;

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
            ViewData["list"] = list;

            Loja auxLoja = new Loja();

            List<Loja> listaLojas = new List<Loja>();
            listaLojas = auxLoja.getLojas();            
            ViewData["listaLojas"] = listaLojas;

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
            funcionario.LojaFuncionarioID = collection[4];
            bool result = funcionario.Insert(funcionario);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
        {
            Funcionario aux = new Funcionario();
            aux.FuncionarioID = int.Parse(collection[0]);

            aux = aux.selectById(aux.FuncionarioID);
            ViewData["aux"] = aux;

            Loja loja = new Loja();
            List<Loja> listaLojas = loja.getLojas();

            ViewData["listaLojas"] = listaLojas;

            bool result = aux.Save(aux);
            return View();
        }
    }
}