using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("administrador")]
    public class AdministradorController : Controller
    {
        // GET: Administrador
        [Route]
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            return View("Index", "_LayoutAdmin");
        }
    }
}