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
    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Devuelve una lista de usuarios con sus perfiles asociados...
        /// </summary>
        [Route]
        public IEnumerable<Usuario> Get()
        {
            return col.ListaUsuarios();
        }

        /// <summary>
        /// Busca coincidencias de usuario segun Rut y Password se envia un json similar a { "Rut": 1234, "Password" 1234  }
        /// PD: no se requieren los demas parametros...
        /// </summary>

        //POST: api/usuario/existe
        [Route("existe")]
        [HttpPost]
        public bool Existe([FromBody] Usuario_Api m)
        {
            return m.ExisteUsuario(m.Rut, m.Password);
        }

        /// <summary>
        /// Busca un usuario, se envia SOLO el rut
        /// </summary>

        //POST: api/usuario/por-rut
        [Route("por-rut/{rut:int}")]
        [HttpGet]
        public Usuario_Api GetByRut(int rut)
        {
            Usuario_Api user = new Usuario_Api();
            user.Read(rut);
            return user;
        }

        /// <summary>
        /// Crea un usuario, se deben enviar todos los datos con formato JSON serializando el objeto usuario...
        /// </summary>

        [Route("crear")]
        public IHttpActionResult NuevoUsuario(Usuario_Api user_crear)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if(user_crear.Create()) return Ok();

            return BadRequest("No se ha podido crear el usuario.");
        }

        /// <summary>
        /// Actualiza el usuario, se deben enviar todos los datos con formato JSON serializando el objeto usuario...
        /// </summary>

        [Route("actualizar")]
        [HttpPut]
        public IHttpActionResult EditarUsuario(Usuario_Api user_editar)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos invalidos.");

            if (user_editar.Update()) return Ok();

            return BadRequest("No se ha podido actualizar el usuario.");
        }

        /// <summary>
        /// Borra el usuario segun rut (debe ser consumido con la propiedad para borrar en .net seria DeleteAsync)
        /// (Recordar que se realiza borrado logico, update a campo eliminado...)
        /// OJO: NO SE CONSUME POR POST
        /// </summary>
        [Route("borrar/{rut:int}")]
        [HttpDelete]
        public IHttpActionResult BorrarUsuario(int rut)
        {
            Usuario_Api user_borrar = new Usuario_Api() { Rut = rut };
            if (user_borrar.Delete()) return Ok();

            return BadRequest("No se ha podido borrar el usuario.");
        }
    }
}
