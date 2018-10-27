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
    public class Apoderado_Api : Apoderado
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Usuario.Rut);
                Db.Execute("sp_apoderado_create", p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool Read(int rut)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", rut);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);


                var result = Db.Query<Apoderado, Usuario, Apoderado>(
                            "sp_apoderado_por_rut",
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
                return false;
                throw;
            }
        }

        public bool Delete()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Usuario.Rut);
                Db.Execute("sp_apoderado_delete_por_rut", p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
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