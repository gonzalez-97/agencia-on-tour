using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("contrato")]
    public class ContratoController : Controller
    {
        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        [Route("nuevo")]
        public ActionResult Nuevo()
        {
            return View("Nuevo", "_LayoutEjecutivo");
        }
    }
}