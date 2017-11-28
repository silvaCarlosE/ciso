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
        public ActionResult Index(String var)
        {
            
            Session.Remove("UsersOnline");
            if (var == null)
            {
                ViewData["errorMsg"] = 2;
            }
            else
            {
                ViewData["errorMsg"] = 1;
            }
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


                return RedirectToAction("Index", "Login", new { var = 1});

            }
            else
            {
                
                Session["UsersOnline"] = UserName;
                return RedirectToAction("Index", "RankingPerda", new { UserName = UserName });
            }
        }
    }
}
