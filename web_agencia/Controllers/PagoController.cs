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
    [RoutePrefix("pagos")]
    public class PagoController : Controller
    {
        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }

        // GET: pagos/mis-pupilos
        [Route("mis-pagos")]
        public ActionResult MisPagos()
        {
            return View("MisPagos", "_LayoutApoderado");
        }

        [Route("todos")]
        public ActionResult TodosLosPagos()
        {
            return View("TodosPagos", "_LayoutEjecutivo");
        }

        [Route("mis-pagos/{id:int}")]
        public async Task<ActionResult> EditarMisPagos(int id)
        {
            //Los datos del pago
            Pago_Web pago_editar = new Pago_Web();
            await pago_editar.Read(id);

            List<Alumno> misAlumnos = new List<Alumno>();
            misAlumnos.Add(pago_editar.Alumno);

            return View("EditarMisPagos", "_LayoutApoderado", new PagoViewModel()
            {
                Id = pago_editar.Id,
                Total_Cuenta = pago_editar.Total_Cuenta,
                Valor_Pago = pago_editar.Valor_Pago,
                Fecha_Pago = pago_editar.Fecha_Pago,
                MisAlumnos = misAlumnos.Select(n => new SelectListItem()
                {
                    Value = n.Rut.ToString(),
                    Text = String.Format("{0} {1}", n.Nombre, n.APaterno)
                }).ToList()
            });
        }

        [HttpGet]
        [Route("mis-pagos-ajax")]
        public async Task<ActionResult> MisPagosAjaxAsync()
        {
            var sesion = new SessionUser();
            Colecciones col = new Colecciones();
            var misPagos = await col.ListaPagos();
            
            return Json(misPagos.Where(n => n.Alumno.Apoderado.Usuario.Rut == sesion.SesionWeb.Rut), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("all-pagos-ajax")]
        public async Task<ActionResult> AllPagosAjaxAsync()
        {
            var sesion = new SessionUser();
            Colecciones col = new Colecciones();
            var misPagos = await col.ListaPagos();

            return Json(misPagos, JsonRequestBehavior.AllowGet);
        }

        [Route("nuevo")]
        public async Task<ActionResult> Nuevo()
        {
            var misPupilos = new List<Alumno>();
            var sesion = new SessionUser();
            var apoderado = new Apoderado_Web();
            //Se busca al apoderado del alumno según el rut de usuario...
            await apoderado.ReadPorRut(sesion.SesionWeb.Rut);

            Colecciones col = new Colecciones();

            if (apoderado.Id != 0)
            {
                misPupilos = await col.ListaAlumnos();
                misPupilos = misPupilos.Where(n => n.Apoderado.Id == apoderado.Id).ToList();
            }
            return View("Nuevo", "_LayoutApoderado", new PagoViewModel()
            {
                MisAlumnos = misPupilos.Select(n => new SelectListItem() { Value = n.Rut.ToString(),
                    Text = String.Format("{0} {1}", n.Nombre, n.APaterno) }).ToList()
            });
        }

        [HttpPost]
        [Route("editar")]
        public async Task<ActionResult> EditarAsync(PagoViewModel pago)
        {
            Pago_Web pago_crear = new Pago_Web();
            bool retorno = await pago_crear.UpdateFromViewAsync(pago);
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();
                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Pago Editado",
                    Mensaje = "El pago ha sido actualizado exitosamente.",
                    ActionName = "MisPagos",
                    ControllerName = "Pago",
                    LinkTexto = "Volver a la lista de mis pagos"
                };
                userSesion.SesionTareaTerminada = task;
                return RedirectToAction("Exito", "Home");
            }
            return View("Nuevo", "_LayoutApoderado");
        }

        [HttpPost]
        [Route("crear")]
        public async Task<ActionResult> CrearAsync(PagoViewModel pago)
        {
            Pago_Web pago_crear = new Pago_Web();
            bool retorno = await pago_crear.CreateFromViewAsync(pago);
            if (retorno)
            {
                SessionUser userSesion = new SessionUser();
                Tarea_Terminada task = new Tarea_Terminada()
                {
                    LayoutNombre = "_LayoutAdmin",
                    Titulo = "Pago Creado",
                    Mensaje = "El pago ha sido creado exitosamente.",
                    ActionName = "MisPagos",
                    ControllerName = "Pago",
                    LinkTexto = "Volver a la lista de mis pagos"
                };
                userSesion.SesionTareaTerminada = task;
                return RedirectToAction("Exito", "Home");
            }
            return View("Nuevo", "_LayoutApoderado");
        }

        [HttpGet]
        [Route("borrar/{id:int}")]
        public async Task<ActionResult> BorrarAsync(int id)
        { 
            Pago_Web pago_borrar = new Pago_Web (){ Id = id };
            return Json(await pago_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }
    }
}