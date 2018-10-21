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
    [RoutePrefix("api/colegio")]
    public class ColegioController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de colegio :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Colegio> Get()
        {
            return col.ListaColegio();
        }


        /// <summary>
        /// Filtro de colegio por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Colegio_Api GetById(int id)
        {
            Colegio_Api colegio = new Colegio_Api();
            colegio.Read(id);
            return colegio;
        }


        /// <summary>
        /// Crea un nuevo colegio (formato JSON)
        /// </summary>
        [Route("crear")]
        public IHttpActionResult NuevoColegio(Colegio_Api colegio_crear)
        {
            if (colegio_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el colegio.");
        }


        /// <summary>
        /// Actualiza el colegio (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarColegio(Colegio_Api colegio_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (colegio_editar.Update()) return Ok();

            return BadRequest("No se ha podido editar el colegio.");
        }


        /// <summary>
        /// Borra el colegio (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarColegio(int id)
        {
            Colegio_Api colegio_borrar = new Colegio_Api() { Id = id };
            if (colegio_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el colegio.");
        }
    }
}
