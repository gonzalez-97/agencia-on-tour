using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("encargado")]
    public class EncargadoController : Controller
    {
        // GET: Encargado
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutEncargado");
        }
    }
}