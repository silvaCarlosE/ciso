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

        [HttpPost]
        public ActionResult Update(System.Web.Mvc.FormCollection collection)
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

        [HttpPost]
        public ActionResult Delete(System.Web.Mvc.FormCollection collection)
        {
            int idToExclude = int.Parse(Regex.Replace(collection[0], ",", ""));
            Manutencao aux = new Manutencao();
            bool result;
            result = aux.DeleteManutencao(idToExclude);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Save(System.Web.Mvc.FormCollection collection)
        {
            Manutencao aux = new Manutencao();
            aux.Id = int.Parse(collection[0]);
            aux.Defeito = collection[1];
            aux.Data = collection[2];
            aux.Id_tecnico = int.Parse(collection[3]);
            aux.Id_impressora = int.Parse(collection[4]);
            aux.Valor_gasto = double.Parse(collection[5]);
            bool result = aux.Update(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Insert(System.Web.Mvc.FormCollection collection)
        {
            Manutencao aux = new Manutencao();
            aux.Defeito = collection[1];
            aux.Data = collection[2];
            aux.Id_tecnico = int.Parse(collection[3]);
            aux.Id_impressora = int.Parse(collection[4]);
            aux.Valor_gasto = int.Parse(collection[5]);
            bool result;
            result = aux.Insert(aux);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Search(System.Web.Mvc.FormCollection collection)
        {
            string nome = collection[0];
            Manutencao aux = new Manutencao();
            List<Manutencao> list = new List<Manutencao>();
            list = aux.getManutencoes(nome);
            ViewBag.List = list;
            return View(list);
        }
    }
}