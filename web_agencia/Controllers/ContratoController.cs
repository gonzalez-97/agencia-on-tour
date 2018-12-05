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
        [Route]
        public ActionResult Index()
        {
            return View("Index", "_LayoutEjecutivo");
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> AllAjaxAsync()
        {
            Colecciones col = new Colecciones();
            var salida = await col.ListaContratos();
            return Json(salida, JsonRequestBehavior.AllowGet);
        }

        [Route("{id:int}")]
        public ActionResult Editar(int id)
        {
            return View("Editar", "_LayoutEjecutivo");
        }

        [HttpGet]
        [Route("borrar/{id:int}")]
        public async Task<ActionResult> BorrarAsync(int id)
        {
            Contrato_Web contrato_borrar = new Contrato_Web() { Id = id };
            return Json(await contrato_borrar.Delete(), JsonRequestBehavior.AllowGet);
        }

        [Route("nuevo")]
        public ActionResult Nuevo()
        {
            RemoveFiles();
            return View("Nuevo", "_LayoutEjecutivo");
        }

        [HttpPost]
        [Route("cargar-ajax")]
        public async Task<ActionResult> CargarContratoAsync(Contrato_Web contrato)
        {
            await contrato.Read(contrato.Id);
            return Json(contrato, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("editar-ajax")]
        public async Task<ActionResult> EditarContratoAjax(Contrato_Web contrato)
        {
            //Otro Objeto tipo contrato... para que?
            //Para obtener los hijos que faltan y?
            //eliminarlos o dejarlos en estado muerto...
            Contrato_Web contrato_comparar = new Contrato_Web();
            await contrato_comparar.Read(contrato.Id);

             //Se envia a actualizar el contrato a la web api
             if (!await contrato.Update())
                return Json(false, JsonRequestBehavior.AllowGet);

            #region Seccion Seguros
            //SEGUROS
            var seguros_eliminar = contrato_comparar.ListaSeguroAsociados;

            if (contrato.ListaSeguroAsociados != null && contrato.ListaSeguroAsociados.Any())
            {
                //Se actualizan los seguros asociados... siempre que tengan Id
                var seguros_actualizar = contrato.ListaSeguroAsociados.Where(n => n.Id != 0);
                foreach (var item in seguros_actualizar)
                {
                    Seguro_Asociado_Web seguro_update = new Seguro_Asociado_Web()
                    {
                        Id = item.Id,
                        Total_Dias = item.Total_Dias,
                        Tipo_Seguro = item.Tipo_Seguro,
                        Valor = item.Valor,
                        Contrato = contrato,
                        Seguro = item.Seguro
                    };

                    if (!await seguro_update.Update()) return Json("Error al actualizar un seguro asociado", JsonRequestBehavior.AllowGet);
                }

                foreach (var item in contrato.ListaSeguroAsociados.Where(n => n.Id == 0))
                {
                    Seguro_Asociado_Web seguro_create = new Seguro_Asociado_Web()
                    {
                        Total_Dias = item.Total_Dias,
                        Tipo_Seguro = item.Tipo_Seguro,
                        Valor = item.Valor,
                        Contrato = contrato,
                        Seguro = item.Seguro
                    };

                    if (!await seguro_create.Create()) return Json("Error al crear un seguro asociado", JsonRequestBehavior.AllowGet);
                }
                //Se quitan los registros actualizados...
                seguros_eliminar = seguros_eliminar.Where(p => !seguros_actualizar.Any(p2 => p2.Id == p.Id)).ToList();
            }

            //Se borran los seguros que no existan...
            foreach (var item in seguros_eliminar)
            {
                Seguro_Asociado_Web seguro_delete = new Seguro_Asociado_Web() { Id = item.Id };
                if (!await seguro_delete.Delete()) return Json("Error al borrar un seguro asociado", JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region Seccion de servicios
            //SERVICIOS
            var servicios_eliminar = contrato_comparar.ListaServiciosAsociados;
            if (contrato.ListaServiciosAsociados != null && contrato.ListaServiciosAsociados.Any())
            {
                //Se actualizan los servicios asociados... siempre que tengan Id
                var servicios_actualizar = contrato.ListaServiciosAsociados.Where(n => n.Id != 0);
                foreach (var item in servicios_actualizar)
                {
                    Servicio_Asociado_Web servicio_update = new Servicio_Asociado_Web() { Id = item.Id, Contrato = contrato, Servicio = item.Servicio };
                    if (!await servicio_update.Update()) return Json("Error al actualizar un servicio asociado", JsonRequestBehavior.AllowGet);
                }

                //Se crean los servicios asociados si no tienen Id XD
                foreach (var item in contrato.ListaServiciosAsociados.Where(n => n.Id == 0))
                {
                    Servicio_Asociado_Web servicio_create = new Servicio_Asociado_Web() { Contrato = contrato, Servicio = item.Servicio };
                    if (!await servicio_create.Create()) return Json("Error al crear un servicio asociado", JsonRequestBehavior.AllowGet);
                }

                //Se quitan los registros actualizados...
                servicios_eliminar = servicios_eliminar.Where(p => !servicios_actualizar.Any(p2 => p2.Id == p.Id)).ToList();
            }
            
            //Se borran los servicios que no existan...
            foreach (var item in servicios_eliminar)
            {
                Servicio_Asociado_Web servicio_delete = new Servicio_Asociado_Web() { Id = item.Id };
                if (!await servicio_delete.Delete()) return Json("Error al borrar un servicio asociado", JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region Seccion de destinos

            //DESTINOS
            var destinos_eliminar = contrato_comparar.ListaDestinosAsociados;
            if (contrato.ListaDestinosAsociados != null && contrato.ListaDestinosAsociados.Any())
            {
                //Se actualizan los destinos asociados... siempre que tengan Id
                var destinos_actualizar = contrato.ListaDestinosAsociados.Where(n => n.Id != 0);
                foreach (var item in destinos_actualizar)
                {
                    Destino_Asociado_Web servicio_update = new Destino_Asociado_Web() { Id = item.Id, Contrato = contrato, Destino = item.Destino };
                    if (!await servicio_update.Update()) return Json("Error al actualizar un destino asociado", JsonRequestBehavior.AllowGet);
                }

                //Se crean los destinos asociados si no tienen Id XD
                foreach (var item in contrato.ListaDestinosAsociados.Where(n => n.Id == 0))
                {
                    Destino_Asociado_Web destino_create = new Destino_Asociado_Web() { Contrato = contrato, Destino = item.Destino };
                    if (!await destino_create.Create()) return Json("Error al crear un destino asociado", JsonRequestBehavior.AllowGet);
                }

                //Se quitan los registros actualizados...
                destinos_eliminar = destinos_eliminar.Where(p => !destinos_actualizar.Any(p2 => p2.Id == p.Id)).ToList();
            }
            

            //Se borran los destinos que no existan...
            foreach (var item in destinos_eliminar)
            {
                Destino_Asociado_Web destino_delete = new Destino_Asociado_Web() { Id = item.Id };
                if (!await destino_delete.Delete()) return Json("Error al borrar un destino asociado", JsonRequestBehavior.AllowGet);
            }
            #endregion

            #region Seccion Archivos
            //ARCHIVOS
            var archivos_eliminar = contrato_comparar.ListaArchivos;
            if (contrato.ListaArchivos != null && contrato.ListaArchivos.Any())
            {
                var archivos_nuevos = contrato.ListaArchivos.Where(n => n.Id == 0);
                var archivos_contrato = ArchivosTemporales().Where(p => !archivos_nuevos.Any(p2 => p2.Nombre == p));
                foreach (var item in archivos_contrato)
                {
                    if (!SaveArchivoContrato(contrato.Id, item))
                        return Json("Error al guardar el archivo en carpeta", JsonRequestBehavior.AllowGet);

                    Archivo_Web archivo_create = new Archivo_Web() { Nombre = Path.GetFileName(item), Contrato = contrato };

                    if (!await archivo_create.Create())
                        return Json("Error al registrar el archivo ", JsonRequestBehavior.AllowGet);
                }
                RemoveFiles();

                //Se quitan los archivos viejos que no se eliminaron
                var archivos_viejos = contrato.ListaArchivos.Where(n => n.Id != 0);
                archivos_eliminar = archivos_eliminar.Where(p => !archivos_viejos.Any(p2 => p2.Id == p.Id)).ToList();
            }

            //Se borran los archivos que no existan...
            foreach (var item in archivos_eliminar)
            {
                Archivo_Web archivo_delete = new Archivo_Web() { Id = item.Id };
                if (!await archivo_delete.Delete()) return Json("Error al borrar un archivo", JsonRequestBehavior.AllowGet);
            }
            #endregion

            return Json(true, JsonRequestBehavior.AllowGet);
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