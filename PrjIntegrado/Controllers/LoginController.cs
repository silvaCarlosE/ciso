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
    public class LoginController : Controller
    {

        // GET: Manutencao
        public ActionResult Index()
        {
            return View();
        }

        // GET:Login
        [HttpPost]
        public ActionResult VerificaUsuarios(System.Web.Mvc.FormCollection collection)
        {
            String UserName = (collection[0]);
            String Senha = (collection[1]);
            Login aux = new Login();
            bool result;
            result = aux.VerificaUsuarios(UserName, Senha);


            if (result == false)
            {
                MessageBox.Show("Acesso negado. Por favor verifique seu Login e senha");
                return RedirectToAction("Index", "Login");

            }
            else
            {
                Session["UsersOnline"] = UserName;
                return RedirectToAction("Index", "RankingPerda", new { UserName = UserName });
            }
        }
    }
}
