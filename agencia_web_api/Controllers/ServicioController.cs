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
    [RoutePrefix("api/servicio")]
    public class ServicioController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de servicios :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Servicio> Get()
        {
            return col.ListaServicios();
        }

        /// <summary>
        /// Filtro de servicio por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Servicio_Api GetById(int id)
        {
            Servicio_Api servicio = new Servicio_Api();
            servicio.Read(id);
            return servicio;
        }

        /// <summary>
        /// Crea un servicio asociado (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult Nuevo(Servicio_Api servicio_crear)
        {
            if (servicio_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el servicio.");
        }

        /// <summary>
        /// Actualiza el servicio asociado (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult Editar(Servicio_Api servicio_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (servicio_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el servicio.");
        }

        /// <summary>
        /// Borra el servicio (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Servicio_Api servicio_borrar = new Servicio_Api() { Id = id };
            if (servicio_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el servicio.");
        }
    }
}
