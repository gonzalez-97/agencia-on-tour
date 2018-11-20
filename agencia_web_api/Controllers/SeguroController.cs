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
    [RoutePrefix("api/seguro")]
    public class SeguroController : ApiController
    {
        Colecciones col = new Colecciones();

        /// <summary>
        /// Una lista de seguros :) (formato JSON)
        /// </summary>
        [Route]
        public IEnumerable<Tipo_Seguro> Get()
        {
            return col.ListaTipoSeguro();
        }


        /// <summary>
        /// Filtro de seguro por ID
        /// </summary>
        [Route("{id:int}")]
        [HttpGet]
        public Tipo_Seguro_Api GetById(int id)
        {
            Tipo_Seguro_Api seguro = new Tipo_Seguro_Api();
            seguro.Read(id);
            return seguro;
        }
    }
}
