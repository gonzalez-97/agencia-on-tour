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
    public class Servicio_Asociado_Api: Servicio_Asociado
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("ContratoID", this.Contrato.Id);
                p.Add("ServicioID", this.Servicio.Id);
                Db.Execute(Procs.Servicio_Asociado_Crear, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.QuerySingle<dynamic>(Procs.Servicio_Asociado_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)result.CONTRATOID);

                Servicio_Api servicio = new Servicio_Api();
                servicio.Read((int)result.SERVICIOID);

                Id = (int)result.ID;
                Contrato = new Contrato()
                {
                    Id = contrato.Id,
                    Curso = contrato.Curso,
                    Nombre = contrato.Nombre,
                    Descripcion = contrato.Descripcion,
                    Fecha_Viaje = contrato.Fecha_Viaje,
                    Valor = contrato.Valor
                };
                Servicio = new Servicio()
                {
                    Id = servicio.Id,
                    Nombre = servicio.Nombre,
                    Descripcion = servicio.Descripcion,
                    Valor = servicio.Valor
                };
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", this.Id);
                p.Add("ContratoID", this.Contrato.Id);
                p.Add("ServicioID", this.Servicio.Id);
                Db.Execute(Procs.Servicio_Asociado_Actualizar, p, commandType: CommandType.StoredProcedure);
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
                Db.Execute(Procs.Servicio_Asociado_Borrar, p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}