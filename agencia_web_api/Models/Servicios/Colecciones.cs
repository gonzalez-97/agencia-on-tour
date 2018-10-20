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

            List<Usuario> users_db = Db.Query<Usuario>("sp_usuarios_todos", p, commandType: CommandType.StoredProcedure).ToList();

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
            return Db.Query<Perfil>("sp_perfiles_todos", p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Perfil> ListaPerfilesXUsuario(int rut)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            p.Add("Rut", rut);
            return Db.Query<Perfil>("sp_perfiles_por_rut", p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Actividad> ListaActividad()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Actividad>("sp_actividad_todas", p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Destino> ListaDestino()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Destino>("sp_destino_todos", p, commandType: CommandType.StoredProcedure);
        }
    }
}