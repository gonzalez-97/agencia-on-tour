using agencia_lib;
using agencia_web_api.Models;
using agencia_web_api.Models.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace agencia_web_api.Controllers
{
    [RoutePrefix("api/pago-actividad")]
    public class PagoActividadController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de actividades :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Pago_Actividad> Get()
        {
            return col.ListaPagoActividad();
        }

        /// <summary>
        /// Una lista de actividades por  :) (formato JSON)
        /// </summary>
        [Route("por-actividad-asignada/{id:int}")]
        public IEnumerable<Pago_Actividad> GetByActividadAsignada(int id)
        {
            return col.ListaPagoActividadXActividad(id);
        }

        /// <summary>
        /// Filtro de actividad por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Pago_Actividad_Api GetById(int id)
        {
            Pago_Actividad_Api actividad = new Pago_Actividad_Api();
            actividad.Read(id);
            return actividad;
        }

        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Pago_Actividad_Api pago_borrar = new Pago_Actividad_Api() { Id = id };
            if (pago_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el pago asociado.");
        }
    }
}
