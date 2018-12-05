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
    [RoutePrefix("api/pago")]
    public class PagoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de pagos :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Pago> Get()
        {
            return col.ListaPagos();
        }


        /// <summary>
        /// Filtro de pago por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Pago_Api GetById(int id)
        {
            Pago_Api pago = new Pago_Api();
            pago.Read(id);
            return pago;
        }

        /// <summary>
        /// Crea un nuevo pago (formato JSON)
        /// </summary>
        [Route("crear")]
        public IHttpActionResult NuevoPago(Pago_Api pago_crear)
        {
            if (pago_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el pago.");
        }

        /// <summary>
        /// Actualiza el pago (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarPago(Pago_Api pago_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (pago_editar.Update()) return Ok();

            return BadRequest("No se ha podido editar el pago.");
        }

        /// <summary>
        /// Borra el pago (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarPago(int id)
        {
            Pago_Api pago_borrar = new Pago_Api() { Id = id };
            if (pago_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el pago.");
        }
    }
}
