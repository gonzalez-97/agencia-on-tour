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
    [RoutePrefix("api/servicio-asociado")]
    public class ServicioAsociadoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de servicios :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Servicio_Asociado> Get()
        {
            return col.ListaServiciosAsociados();
        }

        /// <summary>
        /// Filtro de servicio por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Servicio_Asociado_Api GetById(int id)
        {
            Servicio_Asociado_Api servicio = new Servicio_Asociado_Api();
            servicio.Read(id);
            return servicio;
        }

        /// <summary>
        /// Crea un servicio asociado (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult Nuevo(Servicio_Asociado_Api servicio_asociado_crear)
        {
            if (servicio_asociado_crear.Create()) return Ok();

            return BadRequest("No se ha podido asociar el servicio.");
        }

        /// <summary>
        /// Actualiza el servicio asociado (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult Editar(Servicio_Asociado_Api servicio_asociado_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (servicio_asociado_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el servicio asociado.");
        }

        /// <summary>
        /// Borra el servicio asociado (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Servicio_Asociado_Api servicio_asociado_borrar = new Servicio_Asociado_Api() { Id = id };
            if (servicio_asociado_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el servicio asociado.");
        }
    }
}
