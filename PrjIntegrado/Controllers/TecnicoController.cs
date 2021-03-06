﻿using PrjIntegrado.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Windows.Forms;

namespace PrjIntegrado.Controllers
{
    public class TecnicoController : Controller
    {
        // GET: Tecnico
        public ActionResult Index()
        {
            string auxsessao = (string)(Session["UsersOnline"]);
            if (auxsessao == null)
            {

                return RedirectToAction("Index", "Login");

            }

            else
            {
                Tecnico aux = new Tecnico();
                List<Tecnico> list = new List<Tecnico>();
                list = aux.getTecnicos();
                ViewBag.List = list;
                ViewData["msg"] = "Index";
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
                Tecnico aux = new Tecnico();
                aux = aux.selectById(int.Parse(collection[0]));
                ViewData["Tecnico"] = aux;
                return View(aux);
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
                Tecnico aux = new Tecnico();
                bool result;
                result = aux.DeleteTecnico(idToExclude);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                else
                {
                    TempData["notice"] = "error";
                }
                ViewData["msg"] = result;
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
                Tecnico aux = new Tecnico();
                aux.Id = int.Parse(collection[0]);
                aux.Nome = collection[1];
                aux.Cpf = collection[2];
                bool result = aux.Update(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                if (result == true)
                {
                    ViewData["msg"] = "Dados inseridos com sucesso.";
                }
                else
                {
                    ViewData["msg"] = "Erro";
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

                Tecnico aux = new Tecnico();
                aux.Nome = collection[1];
                aux.Cpf = collection[2];
                bool result;
                result = aux.Insert(aux);
                if (result == true)
                {
                    TempData["notice"] = "inserted";
                }
                ViewData["msg"] = result;
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
                string nome = collection[0];
                Tecnico aux = new Tecnico();
                List<Tecnico> list = new List<Tecnico>();
                list = aux.getTecnicos(nome);
                ViewBag.List = list;
                ViewData["tecnicos"] = list;
                ViewData["msg"] = "A busca foi feita com os parâmetros solicitados.";
                return View(list);
            }
        }
    }
}