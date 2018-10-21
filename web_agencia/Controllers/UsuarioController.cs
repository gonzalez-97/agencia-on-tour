using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models;
using web_agencia.Models.Servicios;
using web_agencia.Models.Views;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("usuarios")]
    public class UsuarioController : Controller
    {
        // GET: Usuario
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
            var salida = await col.ListaUsuariosAsync();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("nuevo")]
        public async Task<ActionResult> NuevoAsync()
        {
            Colecciones col = new Colecciones();
            var perfiles = await col.ListaPerfilesAsync();
            UsuarioViewModel userModel = new UsuarioViewModel()
            {
                PerfilesDisponibles = perfiles.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Tipo }).ToList()
            };
            return View("Nuevo", "_LayoutAdmin", userModel);
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> CrearAsync(UsuarioViewModel user)
        {
            Usuario_Web user_crear = new Usuario_Web();
            bool retorno = await user_crear.CreateFromViewAsync(user);
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Usuario Creado",
                    Mensaje = "El usuario ha sido creado exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Usuario",
                    LinkTexto = "Volver a la lista de usuarios"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }
           
            return View();
        }

        [Route("{rut:int}")]
        public async Task<ActionResult> EditarAsync(int rut)
        {
            Usuario_Web user = new Usuario_Web();
            await user.Read(rut);

            Colecciones col = new Colecciones();
            var perfiles = await col.ListaPerfilesAsync();

            UsuarioViewModel userModel = new UsuarioViewModel()
            {
                Rut = user.Rut,
                DigitoV = user.DigitoV,
                Correo = user.Correo,
                Nombre = user.Nombre,
                APaterno = user.APaterno,
                AMaterno = user.AMaterno,
                Password = user.Password,
                Lista_Perfiles = user.Lista_Perfiles,
                PerfilesDisponibles = perfiles.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Tipo }).ToList()
            };
            return View("Editar", "_LayoutAdmin", userModel);
        }

        [HttpPost]
        [Route("actualizar")]
        public async Task<ActionResult> ActualizarAsync(UsuarioViewModel user)
        {
            Usuario_Web user_crear = new Usuario_Web();
            bool retorno = await user_crear.UpdateFromViewAsync(user);
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();

                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Usuario Actualizado",
                    Mensaje = "El usuario ha sido actualizado exitosamente.",
                    ActionName = "Index",
                    ControllerName = "Usuario",
                    LinkTexto = "Volver a la lista de usuarios"
                };

                userSesion.SesionTareaTerminada = task;

                return RedirectToAction("Exito", "Home");
            }

            return View();
        }

        [HttpGet]
        [Route("borrar/{rut:int}")]
        public async Task<ActionResult> BorrarAsync(int rut)
        {
            Usuario_Web user_borrar = new Usuario_Web() { Rut = rut };
            return Json(await user_borrar.DeleteThisAsync(), JsonRequestBehavior.AllowGet);
        }
    }
}