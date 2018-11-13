using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using web_agencia.Models;
using web_agencia.Models.Servicios;

namespace web_agencia.Controllers
{
    [SessionAuthorize]
    [RoutePrefix("contrato")]
    public class ContratoController : Controller
    {
        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        [Route("nuevo")]
        public ActionResult Nuevo()
        {
            RemoveFiles();
            return View("Nuevo", "_LayoutEjecutivo");
        }

        [HttpPost]
        [Route("crear-ajax")]
        public async Task<ActionResult> CrearContratoAsync(Contrato_Web contrato)
        {
            //Se envia a crear el contrato a la web api
            if (!await contrato.Create())
                return Json(false, JsonRequestBehavior.AllowGet);

            //Se crean los servicios relacionados...
            if (contrato.ListaServiciosAsociados != null && contrato.ListaServiciosAsociados.Any())
            {
                foreach (var item in contrato.ListaServiciosAsociados)
                {
                    Servicio_Asociado_Web servicio_create = new Servicio_Asociado_Web() { Contrato = contrato, Servicio = item.Servicio };
                    if (!await servicio_create.Create())
                        return Json("Error al crear un servicio asociado", JsonRequestBehavior.AllowGet);
                }
            }

            //Se crean los destinos relacionados...
            if (contrato.ListaDestinosAsociados != null && contrato.ListaDestinosAsociados.Any())
            {
                foreach (var item in contrato.ListaDestinosAsociados)
                {
                    Destino_Asociado_Web destino_create = new Destino_Asociado_Web() { Contrato = contrato, Destino = item.Destino };
                    if (!await destino_create.Create())
                        return Json("Error al crear un destino asociado", JsonRequestBehavior.AllowGet);
                }
            }

            //Se crean los seguros asociados...
            if (contrato.ListaSeguroAsociados != null && contrato.ListaSeguroAsociados.Any())
            {
                foreach (var item in contrato.ListaSeguroAsociados)
                {
                    Seguro_Asociado_Web seguro_create = new Seguro_Asociado_Web()
                    {
                        Total_Dias = item.Total_Dias,
                        Tipo_Seguro = item.Tipo_Seguro,
                        Valor = item.Valor,
                        Contrato = contrato,
                        Seguro = item.Seguro
                    };

                    if (!await seguro_create.Create())
                        return Json("Error al crear un seguro asociado", JsonRequestBehavior.AllowGet);
                }
            }

            //Se guardar los archivos subidos...
            if (contrato.ListaArchivos != null && contrato.ListaArchivos.Any())
            {
                var archivos_contrato = ArchivosTemporales().Where(p => !contrato.ListaArchivos.Any(p2 => p2.Nombre == p));
                foreach (var item in archivos_contrato)
                {
                    if(!SaveArchivoContrato(contrato.Id, item))
                        return Json("Error al guardar el archivo en carpeta", JsonRequestBehavior.AllowGet);

                    Archivo_Web archivo_create = new Archivo_Web() { Nombre = Path.GetFileName(item), Contrato = contrato };

                    if (!await archivo_create.Create())
                        return Json("Error al registrar el archivo ", JsonRequestBehavior.AllowGet);                                      
                }
                RemoveFiles();
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [Route("subir-archivo-temp")]
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult SaveArchivoTemp(HttpPostedFileBase file)
        {
            try
            {
                string filename = string.Empty;
                if (file != null)
                {
                    filename = DateTime.Now.Ticks + "_" + file.FileName;
                    var path = Path.Combine(Server.MapPath("~/Content/contrato/temp/"), filename);
                    file.SaveAs(path);
                }
                return Json(filename, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private List<string> ArchivosTemporales()
        {
            List<string> salida = new List<string>();
            string sourcePath = Server.MapPath("~/Content/contrato/temp/");
            if (Directory.Exists(sourcePath))
                salida = Directory.GetFiles(sourcePath).ToList();

            return salida;
        }

        private bool SaveArchivoContrato(int IdContrato, string archivoGuardar)
        {
            if (!System.IO.File.Exists(archivoGuardar))
                return false;

            //This method has been copied from here:https://stackoverflow.com/a/15140431/5202777 
            string targetPath = Server.MapPath(String.Format("~/Content/contrato/{0}/", IdContrato));
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            string fileName = Path.GetFileName(archivoGuardar);
            string destFile = Path.Combine(targetPath, fileName);
            System.IO.File.Copy(archivoGuardar, destFile, true);
            return true;
        }

        public void RemoveFiles()
        {
            string sourcePath = Server.MapPath("~/Content/contrato/temp/");
            string[] files = Directory.GetFiles(sourcePath);
            foreach (string file in files)
            {
                if (System.IO.File.Exists(System.IO.Path.Combine(sourcePath, file)))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch (System.IO.IOException e)
                    {
                        return;
                    }
                }
            }
        }

    }
}