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
    [RoutePrefix("api/actividad-asociada")]
    public class ActividadAsociadaController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de actividades :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Actividad_Asociada> Get()
        {
            return col.ListaActividadAsociada();
        }

        /// <summary>
        /// Filtro de actividad por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Actividad_Asociada_Api GetById(int id)
        {
            Actividad_Asociada_Api actividad = new Actividad_Asociada_Api();
            actividad.Read(id);
            return actividad;
        }

        /// <summary>
        /// Crea la actividad (formato JSON)
        /// </summary>
        [Route("crear")]
        public IHttpActionResult NuevaActividadAsociada(Actividad_Asociada_Api actividad_crear)
        {
            if (actividad_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear la actividad asociada.");
        }

        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Actividad_Asociada_Api actividad_borrar = new Actividad_Asociada_Api() { Id = id };
            if (actividad_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar la actividad asociada.");
        }


    }
}
