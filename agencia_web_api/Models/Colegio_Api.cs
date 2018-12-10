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
    public class Colegio_Api: Colegio
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Nombre", this.Nombre);
                Db.Execute(Procs.Colegio_Crear, p, commandType: CommandType.StoredProcedure);
                logger.Info("Colegio creado correctamente");
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

                var retorno = Db.QuerySingle<Colegio_Api>(Procs.Colegio_Por_Id, p, commandType: CommandType.StoredProcedure);
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
                AddParametersThis(p);
                Db.Execute(Procs.Colegio_Actualizar,p, commandType: CommandType.StoredProcedure);
                logger.Info("Colegio N°{0} actualizado correctamente", Id);
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
                Db.Execute(Procs.Colegio_Borrar, p, commandType: CommandType.StoredProcedure);
                logger.Info("Colegio N°{0} borrado correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        private void AddParametersThis(OracleDynamicParameters parameters)
        {
            Type t = GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                object valor = p.GetValue(this, null);
                parameters.Add(p.Name, valor);
            }
        }

        public void MappingThisFromAnother(Colegio_Api objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
        }
    }
}