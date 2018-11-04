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
    [RoutePrefix("api/contrato")]
    public class ContratoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de contratos :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Contrato> Get()
        {
            return col.ListaContrato();
        }

        /// <summary>
        /// Filtro de contrato por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Contrato_Api GetById(int id)
        {
            Contrato_Api contrato = new Contrato_Api();
            contrato.Read(id);
            return contrato;
        }
        /// <summary>
        /// Crea un nuevo contrato (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult Nuevo(Contrato_Api contrato_crear)
        {
            if (contrato_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el contrato.");
        }

        /// <summary>
        /// Actualiza el contrato (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult Editar(Contrato_Api contrato_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (contrato_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el contrato.");
        }

        /// <summary>
        /// Borra el contrato (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Contrato_Api contrato_borrar = new Contrato_Api() { Id = id };
            if (contrato_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el contrato.");
        }
    }
}
