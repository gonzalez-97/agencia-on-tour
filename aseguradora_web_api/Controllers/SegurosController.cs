using aseguradora_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aseguradora_web_api.Controllers
{
    [RoutePrefix("api/seguros")]
    public class SegurosController : ApiController
    {
        [Route]
        public IEnumerable<Models.PRIMA_TIPO_SEGURO> Get()
        {
            return ConexionDB.Instancia.PRIMA_TIPO_SEGURO.ToList();
        }

        [Route("{id:int}")]
        [HttpGet]
        public Models.PRIMA_TIPO_SEGURO GetById(int id)
        {
            if (ConexionDB.Instancia.PRIMA_TIPO_SEGURO.Where(aux => aux.ID_TIPO == id).Any())
                return ConexionDB.Instancia.PRIMA_TIPO_SEGURO.Single(n => n.ID_TIPO == id);
            else return new PRIMA_TIPO_SEGURO();
        }
    }
}
