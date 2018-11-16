using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models;
using web_agencia.Models.Servicios;
using web_agencia.Models.Views;

namespace web_agencia.Controllers
{
    [RoutePrefix("")]
    public class HomeController : Controller
    {

        [Route]
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }

        [Route("cerrar-sesion")]
        [HttpPost]
        public ActionResult Logout()
        {
            Session.Abandon();
            return Json(new { status = "done" });
        }

        [Route("ingreso")]
        [HttpPost]
        public async Task<ActionResult> LoginAsync(UsuarioViewModel user)
        {
            Usuario_Web user_web = new Usuario_Web();
            bool retorno = await user_web.ExisteUsuarioAsync(user);

            if (!retorno) return RedirectToAction("Index");

            SessionUser user_sesion = new SessionUser();
            Mantenedor_Ruta mantenedoDefault = user_sesion.SesionWeb.MantenedoresRutas.FirstOrDefault();
            return RedirectToAction(mantenedoDefault.ActionName, mantenedoDefault.ControllerName);
        }


        [Route("inicio")]
        [SessionAuthorize]
        public  ActionResult Inicio()
        {
            return View();
        }

        [Route("exito")]
        public ActionResult Exito()
        {
            SessionUser userSesion = new SessionUser();
            if (userSesion == null) return RedirectToAction("Inicio");
            Tarea_Terminada modelView = userSesion.SesionTareaTerminada;
            return View("Exito", modelView.LayoutNombre, modelView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}