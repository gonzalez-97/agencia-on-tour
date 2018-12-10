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
    [RoutePrefix("gerencia")]
    public class DuenioController : Controller
    {
        // GET: Duenio
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutDuenio");
        }

        [HttpGet]
        [Route("avance-por-colegio")]
        public async Task<ActionResult> AvanceColegioAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var cursos = await col.ListaCursos();
            var colegiosGroup = cursos.GroupBy(n => n.Colegio.Nombre).Select(n => new
            {
                Colegio = n.First().Colegio.Nombre,
                Total = n.Sum(s => s.TotalReunido)
            });

            return Json(colegiosGroup, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("avance-por-curso")]
        public async Task<ActionResult> AvanceCursoAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var cursos = await col.ListaCursos();
            return Json(cursos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("avance-por-actividades")]
        public async Task<ActionResult> AvanceActividadesAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var actividades = await col.ListaActividadesAsociadas();
            return Json(actividades, JsonRequestBehavior.AllowGet);
        }

        [Route("pagos")]
        public ActionResult TodosLosPagos()
        {
            return View("TodosPagos", "_LayoutDuenio");
        }
    }
}