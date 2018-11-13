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
    [RoutePrefix("api/archivo")]
    public class ArchivoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de archivos del contrato :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Archivo> Get()
        {
            return col.ListaArchivos();
        }

        /// <summary>
        /// Filtro de contrato por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Archivo_Api GetById(int id)
        {
            Archivo_Api archivo = new Archivo_Api();
            archivo.Read(id);
            return archivo;
        }

        /// <summary>
        /// Crea un nuevo archivo (formato JSON)
        /// </summary>

        [Route("crear")]
        public IHttpActionResult Nuevo(Archivo_Api archivo_crear)
        {
            if (archivo_crear.Create())
                return Ok();
            return BadRequest("No se ha podido crear el archivo.");
        }

        /// <summary>
        /// Borra el archivo  (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult Borrar(int id)
        {
            Archivo_Api archivo_borrar = new Archivo_Api() { Id = id };
            if (archivo_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el archivo.");
        }
    }
}
