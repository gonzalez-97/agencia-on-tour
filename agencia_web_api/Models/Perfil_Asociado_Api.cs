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
    public class Perfil_Asociado_Api : Perfil_Asociado
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Perfil", this.Perfil);
                p.Add("Rut", this.Rut);
                Db.Execute("sp_perfiles_asignados_crear", p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
        public bool BorrarTodosXRut()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                Db.Execute("sp_perfiles_asignados_borrar_todos_rut", p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}