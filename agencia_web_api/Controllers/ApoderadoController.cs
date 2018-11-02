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
    [RoutePrefix("api/apoderado")]
    public class ApoderadoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de apoderados (con su usuario) :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Apoderado> Get()
        {
            return col.ListaApoderados();
        }

        /// <summary>
        /// Verifica que el apoderado exista por RUT
        /// </summary>
        [Route("existe-por-rut/{rut:int}")]
        [HttpGet]
        public bool ExistePorRut(int rut)
        {
            Apoderado_Api apoderado = new Apoderado_Api();
            return apoderado.ExisteApoderado(rut);
        }

        /// <summary>
        /// Filtro de apoderado por RUT
        /// </summary>
        [Route("{rut:int}")]
        [HttpGet]
        public Apoderado_Api GetById(int rut)
        {
            Apoderado_Api apoderado = new Apoderado_Api();
            apoderado.ReadPorRut(rut);
            return apoderado;
        }

        /// <summary>
        /// Crea un nuevo Apoderado (formato JSON)
        /// </summary>
        [Route("crear")]
        public IHttpActionResult NuevoApoderado(Apoderado_Api apoderado_crear)
        {
            if (apoderado_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el apoderado.");
        }


        /// <summary>
        /// Borra el apoderado (por rut del usuario) (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar-por-rut/{rut:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarApoderado(int rut)
        {
            Apoderado_Api apoderado_borrar = new Apoderado_Api() { Usuario = new Usuario() { Rut = rut } };
            if (apoderado_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el apoderado.");
        }

    }
}
