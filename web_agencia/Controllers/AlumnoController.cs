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

        [HttpPost]
        [Route("crear-ajax")]
        public async Task<ActionResult> CrearAlumnoAsync(Alumno_Web alumno)
        {
            var apoderado = new Apoderado_Web();
            //Se busca al apoderado del alumno según el rut de usuario...
            if (await apoderado.ReadPorRut(alumno.Apoderado.Usuario.Rut))
                alumno.Apoderado = apoderado;
            else return Json("No se pudo encontrar al apoderado asociado.", JsonRequestBehavior.AllowGet);
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