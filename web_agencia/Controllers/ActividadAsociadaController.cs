using agencia_lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models;
using web_agencia.Models.Servicios;
using web_agencia.Models.Views;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("actividad-asociada")]
    public class ActividadAsociadaController : Controller
    {
        // GET: ActividadAsociada
        public ActionResult Index()
        {
            return View();
        }

        [Route("mis-pagos-cursos")]
        public ActionResult MisPagosCursos()
        {
            return View("MisPagosCursos", "_LayoutEncargado");
        }

        [Route("nuevo")]
        public ActionResult Nuevo()
        {
            return View("Nuevo", "_LayoutEncargado");
        }

        [HttpGet]
        [Route("cursos-ajax")]
        public async Task<ActionResult> CursosFromAlumnosAjax()
        {
            var misPupilos = new List<Alumno>();
            var apoderado = new Apoderado_Web();
            //Se busca al apoderado del alumno según el rut de usuario...
            await apoderado.ReadPorRut(new SessionUser().SesionWeb.Rut);

            Colecciones col = new Colecciones();

            if (apoderado.Id != 0)
            {
                misPupilos = await col.ListaAlumnos();
                misPupilos = misPupilos.Where(n => n.Apoderado.Id == apoderado.Id).ToList();
            }
            return Json(misPupilos.GroupBy(n => new { n.Curso.Id, n.Curso.Nombre })
                        .Select(n => new { n.Key.Id, n.Key.Nombre }).ToList(), 
                        JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("actividades-ajax")]
        public async Task<ActionResult> ActividadesAjax()
        {
            Colecciones col = new Colecciones();
            var actividades = await col.ListaActividades();
            return Json(actividades, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("guardar-ajax")]
        public async Task<ActionResult> GuardarAjax(int Total_Recaudado, int Id_Actividad, int Id_Curso)
        {
            Actividad_Asociada_Web actividad_create = new Actividad_Asociada_Web() {
               Total_Recaudado = Total_Recaudado,
               Actividad = new Actividad() { Id = Id_Actividad },
               Curso = new Curso() { Id = Id_Curso } 
            };
            bool retorno = await actividad_create.Create();
            if (retorno) return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("pagos-actividad-ajax")]
        public async Task<ActionResult> ListaPagosAjax()
        {
            var misPupilos = new List<Alumno>();
            var apoderado = new Apoderado_Web();
            //Se busca al apoderado del alumno según el rut de usuario...
            await apoderado.ReadPorRut(new SessionUser().SesionWeb.Rut);

            Colecciones col = new Colecciones();

            if (apoderado.Id != 0)
            {
                misPupilos = await col.ListaAlumnos();
                misPupilos = misPupilos.Where(n => n.Apoderado.Id == apoderado.Id).ToList();
            }
            var misCursos = misPupilos.GroupBy(n => new { n.Curso.Id, n.Curso.Nombre }).Select(n => n.Key.Id );
            var pagos = await col.ListaPagosActividad();
            pagos = pagos.Where(t => misCursos.Contains(t.Pago.Alumno.Curso.Id)).ToList();
            return Json(pagos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("borrar-pago-actividad/{id:int}")]
        public async Task<ActionResult> BorrarAsync(int id)
        {
            Pago_Actividad_Web pago_borrar = new Pago_Actividad_Web() { Id = id };
            return Json(await pago_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}