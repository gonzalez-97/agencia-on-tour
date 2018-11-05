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
    [RoutePrefix("api/seguro-asociado")]
    public class SeguroAsociadoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de seguros asociados al contrato :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Seguro_Asociado> Get()
        {
            return col.ListaSeguroAsociados();
        }

        /// <summary>
        /// Filtro de seguro asociado por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Seguro_Asociado_Api GetById(int id)
        {
            Seguro_Asociado_Api seguro = new Seguro_Asociado_Api();
            seguro.Read(id);
            return seguro;
        }

        /// <summary>
        /// Crea un seguro asociado (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult Nuevo(Seguro_Asociado_Api seguro_asociado_crear)
        {
            if (seguro_asociado_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el seguro asociado.");
        }


        /// <summary>
        /// Actualiza el seguro asociado (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult Editar(Seguro_Asociado_Api seguro_asociado_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (seguro_asociado_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el seguro asociado.");
        }

        /// <summary>
        /// Borra el seguro asociado (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Seguro_Asociado_Api seguro_asociado_borrar = new Seguro_Asociado_Api() { Id = id };
            if (seguro_asociado_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el seguro asociado.");
        }
    }
}
