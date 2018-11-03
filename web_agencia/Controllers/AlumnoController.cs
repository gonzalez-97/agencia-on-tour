using agencia_lib;
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
    [RoutePrefix("alumno")]
    public class AlumnoController : Controller
    {
        // GET: Alumno
        public ActionResult Index()
        {
            return View();
        }

        // GET: alumno/mis-pupilos
        [Route("mis-pupilos")]
        public ActionResult MisPupilos()
        {
            return View("MisPupilos", "_LayoutApoderado");
        }

        // GET: alumno/mis-pupilos
        [HttpGet]
        [Route("mis-pupilos-ajax")]
        public async Task<ActionResult> MisPupilosAjaxAsync()
        {
            var sesion = new SessionUser();
            var apoderado = new Apoderado_Web();
            //Se busca al apoderado del alumno según el rut de usuario...
            await apoderado.ReadPorRut(sesion.SesionWeb.Rut);

            //Si no existe no traemos nada...
            if (apoderado.Id == 0) return Json(new List<Alumno>(), JsonRequestBehavior.AllowGet);

            Colecciones col = new Colecciones();
            var misPupilos = await col.ListaAlumnos();

            return Json(misPupilos.Where(n => n.Apoderado.Id == apoderado.Id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("crear-ajax")]
        public async Task<ActionResult> CrearAlumnoAsync(Alumno_Web alumno)
        {
            var sesion = new SessionUser();
            var apoderado = new Apoderado_Web();
            //Se busca al apoderado del alumno según el rut de usuario...
            await apoderado.ReadPorRut(sesion.SesionWeb.Rut);

            if(apoderado.Id == 0)
                return Json("No se pudo encontrar al apoderado asociado.", JsonRequestBehavior.AllowGet);

            alumno.Apoderado = apoderado;
            //Se envia a crear el alumno a la web-api
            return Json(await alumno.Create(), JsonRequestBehavior.AllowGet);
        }

        [Route("registro")]
        public ActionResult Registro()
        {
            return View("Registro", "_LayoutApoderado");
        }
    }
}