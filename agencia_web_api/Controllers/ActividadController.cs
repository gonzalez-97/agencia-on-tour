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
    [RoutePrefix("api/actividad")]
    public class ActividadController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de actividades :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Actividad> Get()
        {
            return col.ListaActividad();
        }


        /// <summary>
        /// Filtro de actividad por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Actividad_Api GetById(int id)
        {
            Actividad_Api actividad = new Actividad_Api();
            actividad.Read(id);
            return actividad;
        }

        /// <summary>
        /// Crea la actividad (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult NuevaActividad(Actividad_Api actividad_crear)
        {
            if (actividad_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear la actividad.");
        }

        /// <summary>
        /// Actualiza la actividad (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarActividad(Actividad_Api actividad_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (actividad_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar la actividad.");
        }

        /// <summary>
        /// Borra la actividad (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarActividad(int id)
        {
            Actividad_Api actividad_borrar = new Actividad_Api() { Id = id };
            if (actividad_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar la actividad.");
        }


    }
}
