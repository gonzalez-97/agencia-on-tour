using agencia_lib;
using agencia_web_api.Models.Servicios;
using Dapper;
using Dapper.Mapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace agencia_web_api.Models
{
    public class Curso_Api : Curso
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Nombre", this.Nombre);
                p.Add("TotalReunido", this.TotalReunido);
                p.Add("Colegio_Id", this.Colegio.Id);
                Db.Execute(Procs.Curso_Borrar, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.Query<Curso, Colegio, Curso>(
                    Procs.Curso_Borrar,
                    map: (curso, colegio) =>
                    {
                        curso.Colegio = colegio;
                        return curso;
                    },
                    splitOn: "Id",
                    param: p,
                    commandType: CommandType.StoredProcedure)
                .Distinct().Single();

                MappingThisFromCurso(result);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public bool Update()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", this.Id);
                p.Add("Nombre", this.Nombre);
                p.Add("TotalReunido", this.TotalReunido);
                p.Add("Colegio_Id", this.Colegio.Id);
                Db.Execute(Procs.Curso_Actualizar, p, commandType: CommandType.StoredProcedure);
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
                p.Add("Id", this.Id);
                Db.Execute(Procs.Curso_Borrar, p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void MappingThisFromCurso(Curso objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.TotalReunido = objeto.TotalReunido;
            this.Colegio = objeto.Colegio;
        }
    }
}