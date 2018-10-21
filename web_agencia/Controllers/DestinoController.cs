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
    [RoutePrefix("destinos")]
    public class DestinoController : Controller
    {
        // GET: Destino
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutAdmin");
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> AllDestinosAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var salida = await col.ListaDestinos();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("nuevo")]
        public ActionResult NuevoAsync()
        {
            return View("Nuevo", "_LayoutAdmin");
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> CrearAsync(Destino_Web destino)
        {
            bool retorno = await destino.Create();
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Destino Creado",
                    Mensaje = "El destino ha sido creado exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Destino",
                    LinkTexto = "Volver a la lista de destinos"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }

            return View();
        }

        [Route("{id:int}")]
        public async Task<ActionResult> EditarAsync(int id)
        {
            Destino_Web destino = new Destino_Web();
            await destino.Read(id);
            return View("Editar", "_LayoutAdmin", destino);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ActionResult> ActualizarAsync(Destino_Web destino)
        {
            bool retorno = await destino.Update();
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Destino Actualizado",
                    Mensaje = "El destino ha sido actualizado exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Destino",
                    LinkTexto = "Volver a la lista de destinos"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("borrar/{id:int}")]
        public async Task<ActionResult> BorrarAsync(int id)
        {
            Destino_Web destino_borrar = new Destino_Web() { Id = id };
            return Json(await destino_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}