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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Perfil", this.Perfil);
                p.Add("Rut", this.Rut);
                Db.Execute(Procs.Perfil_Asociado_Crear, p, commandType: CommandType.StoredProcedure);
                logger.Info("Perfil Asociado creado correctamente");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }
        public bool BorrarTodosXRut()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                Db.Execute(Procs.Perfil_Asociado_Borrar_Por_Rut, p, commandType: CommandType.StoredProcedure);
                logger.Info("Perfiles Asociado por rut borrados correctamente");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }
    }
}