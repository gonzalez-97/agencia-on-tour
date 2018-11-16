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
        public async Task<ActionResult> AllAjaxAsync()
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
            if (actividad.ValidarActividad(actividad, true)) {
                bool retorno = await actividad.Create();
                if (retorno)
                {
                    SessionUser userSesion = new SessionUser();

                    Tarea_Terminada task = new Tarea_Terminada()
                    {
                        LayoutNombre = "_LayoutAdmin",
                        Titulo = "Actividad Creada",
                        Mensaje = "La actividad ha sido creada exitosamente.",
                        ActionName = "Index",
                        ControllerName = "Actividad",
                        LinkTexto = "Volver a la lista de actividades"
                    };

                    userSesion.SesionTareaTerminada = task;

                    return RedirectToAction("Exito", "Home");
                }
            }
            foreach (var item in actividad._dictionaryError)
                ModelState.AddModelError(item.Key, item.Value);
            return View("Nuevo", "_LayoutAdmin", actividad);
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
            if (actividad.ValidarActividad(actividad, false)) {
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
            }
            foreach (var item in actividad._dictionaryError)
                ModelState.AddModelError(item.Key, item.Value);
            return View("Editar", "_LayoutAdmin", actividad);
        }

        [HttpGet]
        [Route("borrar/{id:int}")]
        public async Task<ActionResult> BorrarAsync(int id)
        {
            Actividad_Web actividad_borrar = new Actividad_Web() { Id = id };
            return Json(await actividad_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}