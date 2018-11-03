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
    [RoutePrefix("apoderado")]
    public class ApoderadoController : Controller
    {
        // GET: Apoderado
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutApoderado");
        }

        [HttpGet]
        [Route("existe-apoderado")]
        public async Task<ActionResult> ExisteApoderadoAsync()
        {
            var salida = await Utiles.ExisteApoderadoFromSesionAsync();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("crear-ajax")]
        public async Task<ActionResult> CrearApoderadoAsync(Apoderado_Web apoderado)
        {
            //Estas dos lineas verifican que el usuario no tenga otro apoderado...
            var existe = await Utiles.ExisteApoderadoFromSesionAsync();
            if (existe) return Json("El usuario ya posee un apoderado registrado.", JsonRequestBehavior.AllowGet);
            //Se obtiene el usuario desde sesion...
            var sesion = new SessionUser();
            Usuario_Web usuarioSesion = sesion.SesionWeb;
            //Si el rut es diferente al ingresado tampoco se crea verifica el rut ingresado debe coincidir!!
            if (usuarioSesion.Rut != apoderado.Usuario.Rut)
                return Json("El rut ingresado no coincide con el usuario actual.", JsonRequestBehavior.AllowGet);
            //Se envia a crear el apoderado a la web-api
            return Json(await apoderado.Create(), JsonRequestBehavior.AllowGet);
        }

        [Route("registro")]
        public ActionResult Registro()
        {
            return View("Registro", "_LayoutApoderado");
        }
    }
}