using agencia_lib;
using Dapper;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace agencia_web_api.Models.Servicios
{
    public class Colecciones
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public IEnumerable<Usuario> ListaUsuarios()
        {

            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            List<Usuario> users_db = Db.Query<Usuario>(Procs.Usuario_Todos, p, commandType: CommandType.StoredProcedure).ToList();

            var usuarios = users_db.Select(n => new Usuario()
            {
                Rut = n.Rut,
                DigitoV = n.DigitoV,
                Correo = n.Correo,
                Password = n.Password,
                Nombre = n.Nombre,
                APaterno = n.APaterno,
                AMaterno = n.AMaterno,
                Lista_Perfiles = ListaPerfilesXUsuario(n.Rut).ToList()
            }).ToList();

            return usuarios;
        }

        public IEnumerable<Perfil> ListaPerfiles()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Perfil>(Procs.Perfil_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Perfil> ListaPerfilesXUsuario(int rut)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            p.Add("Rut", rut);
            return Db.Query<Perfil>(Procs.Perfil_Por_Rut, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Actividad> ListaActividad()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Actividad>(Procs.Actividad_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Destino> ListaDestino()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Destino>(Procs.Destino_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Colegio> ListaColegio()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Colegio>(Procs.Colegio_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Curso> ListaCurso()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var result = Db.Query<Curso, Colegio, Curso>(
                        Procs.Curso_Todos,
                        map: (curso, colegio) =>
                        {
                            curso.Colegio = colegio;
                            return curso;
                        },
                        splitOn: "Id",
                        param: p,
                        commandType: CommandType.StoredProcedure).Distinct();

            return result;

        }

        public IEnumerable<Apoderado> ListaApoderados()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var result = Db.Query<Apoderado, Usuario, Apoderado>(
                        Procs.Apoderado_Todos,
                        map: (apoderado, usuario) =>
                        {
                            apoderado.Usuario = usuario;
                            return apoderado;
                        },
                        splitOn: "Rut",
                        param: p,
                        commandType: CommandType.StoredProcedure).Distinct();

            return result;
        }

        public IEnumerable<Alumno> ListaAlumnos()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var result = Db.Query<dynamic>(Procs.Alumno_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Apoderado_Api apoderado = new Apoderado_Api();
               apoderado.Read((int)n.APOID);

                Curso_Api curso = new Curso_Api();
                curso.Read((int)n.CURID);
                return new Alumno()
                {
                    Rut = (int)n.RUT,
                    DigitoV = n.DIGITOV,
                    Nombre = n.NOMBRE,
                    APaterno = n.APATERNO,
                    AMaterno = n.AMATERNO,
                    Apoderado = new Apoderado() { Id = apoderado.Id, Usuario = apoderado.Usuario },
                    Curso = new Curso(){ Id = curso.Id, Nombre = curso.Nombre, TotalReunido = curso.TotalReunido, Colegio = curso.Colegio }
               };
            });

            return salida;

        }

        public IEnumerable<Seguro> ListaSeguro()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Seguro>(Procs.Seguro_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Contrato> ListaContrato()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var result = Db.Query<dynamic>(Procs.Contrato_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Curso_Api curso = new Curso_Api();
                curso.Read((int)n.CURSOID);
                return new Contrato()
                {
                    Id = (int)n.ID,
                    Nombre = n.NOMBRE,
                    Descripcion = n.DESCRIPCION,
                    Fecha_Viaje = (DateTime)n.FECHA_VIAJE,
                    Curso = new Curso() { Id = curso.Id, Nombre = curso.Nombre, TotalReunido = curso.TotalReunido, Colegio = curso.Colegio }
                };
            });

            return salida;
        }

    }
}