﻿using agencia_lib;
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
    public class Apoderado_Api : Apoderado
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IDbConnection Db = ConexionDb.GeneraConexion();

        public bool ExisteApoderado(int rut)
        {
            var p = new OracleDynamicParameters();
            p.Add("Rut", rut);
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            int retorno = Db.QuerySingle<int>(Procs.Existe_Apoderado_Por_Rut, p, commandType: CommandType.StoredProcedure);
            return (retorno > 0) ? true : false;
        }

        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Usuario.Rut);
                Db.Execute(Procs.Apoderado_Crear, p, commandType: CommandType.StoredProcedure);
                logger.Info("Apoderado creado correctamente");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Read(int Id)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", Id);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var result = Db.Query<Apoderado, Usuario, Apoderado>(
                            Procs.Apoderado_Por_Id,
                            map: (apoderado, usuario) =>
                            {
                                apoderado.Usuario = usuario;
                                return apoderado;
                            },
                            splitOn: "Rut",
                            param: p,
                            commandType: CommandType.StoredProcedure).Distinct().FirstOrDefault();

                MappingThisFromApoderado(result);

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool ReadPorRut(int rut)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", rut);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);


                var result = Db.Query<Apoderado, Usuario, Apoderado>(
                            Procs.Apoderado_Por_Rut,
                            map: (apoderado, usuario) =>
                            {
                                apoderado.Usuario = usuario;
                                return apoderado;
                            },
                            splitOn: "Rut",
                            param: p,
                            commandType: CommandType.StoredProcedure).Distinct().FirstOrDefault();

                MappingThisFromApoderado(result);

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
                p.Add("Rut", this.Usuario.Rut);
                Db.Execute(Procs.Apoderado_Borrar_Por_Rut, p, commandType: CommandType.StoredProcedure);
                logger.Info("Apoderado N°{0} borrado correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public void MappingThisFromApoderado(Apoderado objeto)
        {
            this.Id = objeto.Id;
            this.Usuario = objeto.Usuario;
        }

    }
}