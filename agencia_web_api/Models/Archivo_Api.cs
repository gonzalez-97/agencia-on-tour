using agencia_lib;
using agencia_web_api.Models.Servicios;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace agencia_web_api.Models
{
    public class Archivo_Api : Archivo
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Archivo", this.Nombre);
                p.Add("ContratoID", this.Contrato.Id);
                Db.Execute(Procs.Archivo_Crear, p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public 
    }
}