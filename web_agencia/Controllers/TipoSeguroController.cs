﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("tipos-seguros")]
    public class TipoSeguroController : Controller
    {
        // GET: Seguro
        [Route]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> AllAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var salida = await col.ListaTiposSeguros();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }
    }
}