using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("ejecutivo")]
    public class EjecutivoController : Controller
    {
        // GET: Apoderado
        [Route]
        // GET: Ejecutivo
        public ActionResult Index()
        {
            return View("Index", "_LayoutEjecutivo");
        }

    }
}