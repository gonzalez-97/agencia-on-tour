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
    [RoutePrefix("actividades")]
    public class ActividadController : Controller
    {
        // GET: Actividad
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutAdmin");
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> AllUsersAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var salida = await col.ListaActividades();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("nueva")]
        public ActionResult NuevoAsync()
        {
            return View("Nuevo", "_LayoutAdmin");
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> CrearAsync(Actividad_Web actividad)
        {
            bool retorno = await actividad.Create();
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Actividad Creado",
                    Mensaje = "La actividad ha sido creada exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Actividad",
                    LinkTexto = "Volver a la lista de actividades"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }

            return View();
        }

        [Route("{id:int}")]
        public async Task<ActionResult> EditarAsync(int id)
        {
            Actividad_Web actividad = new Actividad_Web();
            await actividad.Read(id);
            return View("Editar", "_LayoutAdmin", actividad);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ActionResult> ActualizarAsync(Actividad_Web actividad)
        {
            bool retorno = await actividad.Update();
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Actividad Actualizada",
                    Mensaje = "La actividad ha sido actualizada exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Actividad",
                    LinkTexto = "Volver a la lista de actividades"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("borrar/{id:int}")]
        public async Task<ActionResult> BorrarActividadAsync(int id)
        {
            Actividad_Web actividad_borrar = new Actividad_Web() { Id = id };
            return Json(await actividad_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}