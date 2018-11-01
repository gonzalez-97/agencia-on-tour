using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{ 
    [SessionAuthorize]
    [RoutePrefix("apoderado")]
    public class ApoderadoController : Controller
    {
        // GET: Apoderado
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutApoderado");
        }

        [HttpGet]
        [Route("existe-apoderado")]
        public async Task<ActionResult> ExisteApoderadoAsync()
        {
            var salida = await Utiles.ExisteApoderadoFromSesionAsync();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("registro")]
        public ActionResult Registro()
        {
            return View("Registro", "_LayoutApoderado");
        }
    }
}