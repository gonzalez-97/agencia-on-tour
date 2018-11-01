using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("deposito")]
    public class DepositoController : Controller
    {
        // GET: Deposito
        public ActionResult Index()
        {
            return View();
        }

        [Route("nuevo")]
        public async Task<ActionResult> NuevoAsync()
        {
            var salida = await Utiles.ExisteApoderadoFromSesionAsync();
            if (!salida)  return RedirectToAction("Registro", "Apoderado");

            return View();
        }
    }
}