using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace web_agencia.Models.Servicios
{
    public static class Utiles
    {
        public static string RutaWebAPI()
        {
            return ConfigurationManager.AppSettings["web_api_service"];
        }

        public static async Task<bool> ExisteApoderadoFromSesionAsync()
        {
            var usuarioConectado = new SessionUser();
            var apoderado = new Apoderado_Web();
            return await apoderado.ExisteApoderadoAsync(usuarioConectado.SesionWeb.Rut);
        }
    }
}