﻿using System;
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
    [RoutePrefix("contrato")]
    public class ContratoController : Controller
    {
        public object Contrato_Api { get; private set; }

        // GET: Contrato
        public ActionResult Index()
        {
            return View();
        }

        [Route("nuevo")]
        public ActionResult Nuevo()
        {
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

            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}