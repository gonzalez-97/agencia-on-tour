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
    [RoutePrefix("api/perfil")]
    public class PerfilController : ApiController
    {
        Colecciones col = new Colecciones();
        [Route]
        public IEnumerable<Perfil> Get()
        {
            return col.ListaPerfiles();
        }

        [Route("asocia-perfil")]
        public IHttpActionResult AsociaPerfilUsuario(Perfil_Asociado_Api perfil_asociado)
        {
            if (perfil_asociado.Create()) return Ok();
            return BadRequest("No se ha podido asociar el perfil al usuario.");
        }

        [Route("elimina-todo-perfil/{rut:int}")]
        [HttpDelete]
        public IHttpActionResult EliminaTodosPerfilUsuario(int rut)
        {
            Perfil_Asociado_Api perfil_asociado = new Perfil_Asociado_Api() { Rut = rut };
            if (perfil_asociado.BorrarTodosXRut()) return Ok();
            return BadRequest("No se han podido eliminar los perfiles del usuario.");
        }
    }
}
