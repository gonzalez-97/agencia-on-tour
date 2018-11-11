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
    [RoutePrefix("api/destino-asociado")]
    public class DestinoAsociadoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de destinos asociados al contrato :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Destino_Asociado> Get()
        {
            return col.ListaDestinosAsociados();
        }

        /// <summary>
        /// Filtro de destino asociado por ID
        /// </summary>

        [Route("{id:int}")]
        [HttpGet]
        public Destino_Asociado_Api GetById(int id)
        {
            Destino_Asociado_Api destino = new Destino_Asociado_Api();
            destino.Read(id);
            return destino;
        }

        /// <summary>
        /// Crea un destino asociado (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult Nuevo(Destino_Asociado_Api destino_asociado_crear)
        {
            if (destino_asociado_crear.Create())
                return Ok();
            return BadRequest("No se ha podido crear el destino asociado.");
        }

        /// <summary>
        /// Actualiza el destino asociado (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult Editar(Destino_Asociado_Api destino_asociado_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (destino_asociado_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el destino asociado.");
        }

        /// <summary>
        /// Borra el destino asociado (Lo borra directamente no es necesario borrado logico...)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Destino_Asociado_Api destino_asociado_borrar = new Destino_Asociado_Api() { Id = id };
            if (destino_asociado_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el destino asociado.");
        }
    }
}

