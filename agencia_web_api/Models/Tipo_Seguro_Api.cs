using agencia_lib;
using agencia_web_api.Models.Servicios;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace agencia_web_api.Models
{
    public class Tipo_Seguro_Api : Tipo_Seguro
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public bool Read(int id)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", id);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var retorno = Db.QuerySingle<Tipo_Seguro_Api>(Procs.Seguro_Por_Id, p, commandType: CommandType.StoredProcedure);
                MappingThisFromAnother(retorno);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public void MappingThisFromAnother(Tipo_Seguro_Api objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Tipo_Aseguradora = objeto.Tipo_Aseguradora;
        }
    }
}