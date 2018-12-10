using agencia_lib;
using agencia_web_api.Models.Servicios;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace agencia_web_api.Models
{
    public class Actividad_Api : Actividad
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Nombre", this.Nombre);
                p.Add("Descripcion", this.Descripcion);
                Db.Execute(Procs.Actividad_Crear, p, commandType: CommandType.StoredProcedure);

                logger.Info("Actividad creada correctamente");
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool Read(int id)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", id);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var retorno = Db.QuerySingle<Actividad_Api>(Procs.Actividad_Por_Id, p, commandType: CommandType.StoredProcedure);
                MappingThisFromAnother(retorno);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }

        }

        public bool Update()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", this.Id);
                p.Add("Nombre", this.Nombre);
                p.Add("Descripcion", this.Descripcion);
                Db.Execute(Procs.Actividad_Actualizar, p, commandType: CommandType.StoredProcedure);

                logger.Info("Actividad N°{0} actualizada correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", this.Id);
                Db.Execute(Procs.Actividad_Borrar, p, commandType: CommandType.StoredProcedure);

                logger.Info("Actividad N°{0} borrada correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public void MappingThisFromAnother(Actividad_Api objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Descripcion = objeto.Descripcion;
        }
    }
}