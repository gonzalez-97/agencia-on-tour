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
        public IEnumerable<Models.SEGURO> Get()
        {
            return ConexionDB.Instancia.SEGURO.ToList();
        }

        [Route("{id:int}")]
        [HttpGet]
        public Models.SEGURO GetById(int id)
        {
            if (ConexionDB.Instancia.SEGURO.Where(aux => aux.ID == id).Any())
                return ConexionDB.Instancia.SEGURO.Single(n => n.ID == id);
            else return new SEGURO();
        }
    }
}
