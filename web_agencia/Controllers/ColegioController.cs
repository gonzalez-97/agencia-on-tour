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
    [RoutePrefix("colegios")]
    public class ColegioController : Controller
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
            var salida = await col.ListaColegios();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("nuevo")]
        public ActionResult NuevoAsync()
        {
            return View("Nuevo", "_LayoutAdmin");
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> CrearAsync(Colegio_Web colegio)
        {
            Colegio_Web c = new Colegio_Web();
            if (c.ValidarColegio(colegio, true)) {
                bool retorno = await colegio.Create();
                if (retorno)
                {
                    SessionUser userSesion = new SessionUser();

                    Tarea_Terminada task = new Tarea_Terminada()
                    {
                        LayoutNombre = "_LayoutAdmin",
                        Titulo = "Colegio Creado",
                        Mensaje = "El colegio ha sido creado exitosamente.",
                        ActionName = "Index",
                        ControllerName = "Colegio",
                        LinkTexto = "Volver a la lista de colegios"
                    };

                    userSesion.SesionTareaTerminada = task;

                    return RedirectToAction("Exito", "Home");
                }
            }
            foreach(var item in c._dictionaryError)
                ModelState.AddModelError(item.Key, item.Value);

            return View("Nuevo", "_LayoutAdmin", colegio);
        }

        [Route("{id:int}")]
        public async Task<ActionResult> EditarAsync(int id)
        {
            Colegio_Web colegio = new Colegio_Web();
            await colegio.Read(id);
            return View("Editar", "_LayoutAdmin", colegio);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ActionResult> ActualizarAsync(Colegio_Web colegio)
        {
            Colegio_Web c = new Colegio_Web();
            if (c.ValidarColegio(colegio, false)) {
                bool retorno = await colegio.Update();
                if (retorno)
                {
                    SessionUser userSesion = new SessionUser();

                    Tarea_Terminada task = new Tarea_Terminada()
                    {
                        LayoutNombre = "_LayoutAdmin",
                        Titulo = "Colegio Actualizado",
                        Mensaje = "El colegio ha sido actualizado exitosamente.",
                        ActionName = "Index",
                        ControllerName = "Colegio",
                        LinkTexto = "Volver a la lista de colegios"
                    };

                    userSesion.SesionTareaTerminada = task;

                    return RedirectToAction("Exito", "Home");
                }
            }
            foreach (var item in colegio._dictionaryError)
                ModelState.AddModelError(item.Key, item.Value);
            return View();
        }

        [HttpGet]
        [Route("borrar/{id:int}")]
        public async Task<ActionResult> BorrarAsync(int id)
        {
            Colegio_Web colegio_borrar = new Colegio_Web() { Id = id };
            return Json(await colegio_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}