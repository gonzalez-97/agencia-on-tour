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
    public class Alumno_Api: Alumno
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                p.Add("DigitoV", this.DigitoV);
                p.Add("Nombre", this.Nombre);
                p.Add("APaterno", this.APaterno);
                p.Add("AMaterno", this.AMaterno);
                p.Add("Apoderado_Id", this.Apoderado.Id);
                p.Add("Curso", this.Curso.Id);
                Db.Execute(Procs.Usuario_Crear, p, commandType: CommandType.StoredProcedure);
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
                //Usuario
                var retorno = Db.QuerySingle<Usuario_Api>(Procs.Usuario_Por_Rut, p, commandType: CommandType.StoredProcedure);
                //Perfiles
                retorno.Lista_Perfiles = Db.Query<Perfil>(Procs.Perfil_Por_Rut, p, commandType: CommandType.StoredProcedure).ToList();

                MappingThisFromAnother(retorno);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}