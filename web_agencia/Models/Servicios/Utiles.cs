using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace web_agencia.Models.Servicios
{
    public static class Utiles
    {
        public static string RutaWebAPI()
        {
            return ConfigurationManager.AppSettings["web_api_service"];
        }
    }
}