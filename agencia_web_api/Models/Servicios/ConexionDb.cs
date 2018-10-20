using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace agencia_web_api.Models.Servicios
{
    public static class ConexionDb
    {
        public static IDbConnection GeneraConexion()
        {
            string conexion = WebConfigurationManager.ConnectionStrings["oracle12c"].ConnectionString;
            return new OracleConnection(conexion);
        }
    }
}