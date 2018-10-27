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
    [RoutePrefix("api/curso")]
    public class CursoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de cursos :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Curso> Get()
        {
            return col.ListaCurso();
        }

        /// <summary>
        /// Filtro de curso por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Curso_Api GetById(int id)
        {
            Curso_Api curso = new Curso_Api();
            curso.Read(id);
            return curso;
        }

        /// <summary>
        /// Crea un nuevo curso (formato JSON)
        /// </summary>
        [Route("crear")]
        public IHttpActionResult NuevoCurso(Curso_Api curso_crear)
        {
            if (curso_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el curso.");
        }

        /// <summary>
        /// Actualiza el curso (formato JSON)
        /// </summary>
        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarCurso(Curso_Api curso_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (curso_editar.Update()) return Ok();

            return BadRequest("No se ha podido editar el curso.");
        }

        /// <summary>
        /// Borra el curso (actualiza el campo eliminado = 0)
        /// </summary>
        [Route("borrar/{id:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarColegio(int id)
        {
            Curso_Api curso_borrar = new Curso_Api() { Id = id };
            if (curso_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el curso.");
        }
    }
}
