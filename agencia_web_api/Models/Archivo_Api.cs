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
    public class Archivo_Api : Archivo
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Archivo", this.Nombre);
                p.Add("ContratoID", this.Contrato.Id);
                Db.Execute(Procs.Archivo_Crear, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.QuerySingle<dynamic>(Procs.Archivo_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)result.CONTRATOID);

                Id = (int)result.ID;
                Nombre = result.ARCHIVO;
                Contrato = new Contrato()
                {
                    Id = contrato.Id,
                    Nombre = contrato.Nombre,
                    Descripcion = contrato.Descripcion,
                    Fecha_Viaje = contrato.Fecha_Viaje,
                    Valor = contrato.Valor,
                    Curso = contrato.Curso,
                    Estado = contrato.Estado
                    //ListaSeguroAsociados = contrato.ListaSeguroAsociados,
                    //ListaDestinosAsociados = contrato.ListaDestinosAsociados,
                    //ListaServiciosAsociados = contrato.ListaServiciosAsociados
                };

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
                Db.Execute(Procs.Archivo_Borrar, p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }
    }
}