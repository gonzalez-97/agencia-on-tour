using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("duenio")]
    public class DuenioController : Controller
    {
        // GET: Duenio
        public ActionResult Index()
        {
            return View("Index", "_LayoutDuenio");
        }
    }
}