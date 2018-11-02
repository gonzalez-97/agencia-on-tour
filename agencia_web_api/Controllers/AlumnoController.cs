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
    [RoutePrefix("api/alumno")]
    public class AlumnoController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Devuelve una lista da alumnos...
        /// </summary>
        [Route]
        public IEnumerable<Alumno> Get()
        {
            return col.ListaAlumnos();
        }

        /// <summary>
        /// Verifica que el alumno exista por RUT
        /// </summary>
        [Route("existe-por-rut/{rut:int}")]
        [HttpGet]
        public bool ExistePorRut(int rut)
        {
            Alumno_Api alumno = new Alumno_Api();
            return alumno.ExisteAlumno(rut);
        }

        /// <summary>
        /// Busca un alumno, se envia SOLO el rut
        /// </summary>

        //GET api/alumno/por-rut
        [Route("por-rut/{rut:int}")]
        [HttpGet]
        public Alumno_Api GetByRut(int rut)
        {
            Alumno_Api alumno = new Alumno_Api();
            alumno.Read(rut);
            return alumno;
        }

        /// <summary>
        /// Crea un alumno, se deben enviar todos los datos con formato JSON serializando el objeto alumno...
        /// </summary>

        [Route("crear")]
        public IHttpActionResult NuevoAlumno(Alumno_Api alumno_crear)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (alumno_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el alumno.");
        }

        /// <summary>
        /// Actualiza el alumno, se deben enviar todos los datos con formato JSON serializando el objeto usuario...
        /// </summary>

        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarAlumno(Alumno_Api alumno_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (alumno_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el alumno.");
        }

        /// <summary>
        /// Borra el usuario segun rut 
        /// </summary>
        [Route("borrar/{rut:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarAlumno(int rut)
        {
            Alumno_Api alumno_borrar = new Alumno_Api() { Rut = rut };
            if (alumno_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el alumno.");
        }
    }
}
