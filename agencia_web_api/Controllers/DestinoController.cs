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
    [RoutePrefix("api/destino")]
    public class DestinoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de destinos :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Destino> Get()
        {
            return col.ListaDestino();
        }


        /// <summary>
        /// Filtro de destino por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Destino_Api GetById(int id)
        {
            Destino_Api destino = new Destino_Api();
            destino.Read(id);
            return destino;
        }


        /// <summary>
        /// Crea un nuevo destino (formato JSON)
        /// </summary>
        [Route("crear")]
        public IHttpActionResult NuevoDestino(Destino_Api destino_crear)
        {
            if (destino_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el destino.");
        }


        /// <summary>
        /// Actualiza el destino (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarDestino(Destino_Api destino_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (destino_editar.Update()) return Ok();

            return BadRequest("No se ha podido editar el destino.");
        }


        /// <summary>
        /// Borra el destino (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarDestino(int id)
        {
            Destino_Api destino_borrar = new Destino_Api() { Id = id };
            if (destino_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el destino.");
        }
    }
}
