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
    [RoutePrefix("cursos")]
    public class CursoController : Controller
    {
        // GET: Curso
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
            var salida = await col.ListaCursos();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("nuevo")]
        public async Task<ActionResult> NuevoAsync()
        {
            Colecciones col = new Colecciones();
            var colegios = await col.ListaColegios();
            CursoViewModel cursoModel = new CursoViewModel()
            {
                ColegiosDisponibles = colegios.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Nombre }).ToList()
            };
            return View("Nuevo", "_LayoutAdmin", cursoModel);
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> CrearAsync(CursoViewModel curso)
        {
            Curso_Web curso_crear = new Curso_Web();
            bool retorno = await curso_crear.CreateFromViewAsync(curso);
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Curso Creado",
                    Mensaje = "El curso ha sido creado exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Curso",
                    LinkTexto = "Volver a la lista de cursos"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }

            return View();
        }

        [Route("{id:int}")]
        public async Task<ActionResult> EditarAsync(int id)
        {
            Curso_Web curso = new Curso_Web();
            await curso.Read(id);

            Colecciones col = new Colecciones();
            var colegios = await col.ListaColegios();

            CursoViewModel cursoModel = new CursoViewModel()
            {
                Id = curso.Id,
                Nombre = curso.Nombre,
                TotalReunido = curso.TotalReunido,
                Colegio = curso.Colegio,
                ColegiosDisponibles = colegios.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Nombre }).ToList()
            };
            return View("Editar", "_LayoutAdmin", cursoModel);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ActionResult> ActualizarAsync(CursoViewModel curso)
        {
            Curso_Web curso_editar = new Curso_Web();
            bool retorno = await curso_editar.UpdateFromViewAsync(curso);
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Curso Actualizado",
                    Mensaje = "El curso ha sido actualizado exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Curso",
                    LinkTexto = "Volver a la lista de cursos"
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
            Curso_Web curso_borrar = new Curso_Web() { Id = id };
            return Json(await curso_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}