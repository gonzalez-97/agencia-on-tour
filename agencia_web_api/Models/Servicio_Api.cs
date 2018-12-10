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
    public class Servicio_Api: Servicio
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Nombre", this.Nombre);
                p.Add("Descripcion", this.Descripcion);
                p.Add("Valor", this.Valor);
                Db.Execute(Procs.Servicio_Crear, p, commandType: CommandType.StoredProcedure);
                logger.Info("Servicio creado correctamente");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Read(int id)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", id);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var retorno = Db.QuerySingle<Servicio_Api>(Procs.Servicio_Por_Id, p, commandType: CommandType.StoredProcedure);
                MappingThisFromAnother(retorno);
                
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
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
                p.Add("Valor", this.Valor);
                Db.Execute(Procs.Servicio_Actualizar, p, commandType: CommandType.StoredProcedure);
                logger.Info("Servicio N°{0} actualizado correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", this.Id);
                Db.Execute(Procs.Servicio_Borrar, p, commandType: CommandType.StoredProcedure);
                logger.Info("Servicio N°{0} borrado correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Info(ex.Message);
                return false;
            }
        }

        private void MappingThisFromAnother(Servicio_Api objeto)
        {

            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Descripcion = objeto.Descripcion;
            this.Valor = objeto.Valor;
        }
    }
}