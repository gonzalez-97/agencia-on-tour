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
    public class Destino_Asociado_Api : Destino_Asociado
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("ContratoID", this.Contrato.Id);
                p.Add("DestinoID", this.Destino.Id);
                Db.Execute(Procs.Destino_Asociado_Crear, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.QuerySingle<dynamic>(Procs.Destino_Asociado_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)result.CONTRATOID);

                Destino_Api destino = new Destino_Api();
                destino.Read((int)result.DESTINOID);

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
                Destino = new Destino()
                {
                    Id = destino.Id,
                    Nombre = destino.Nombre,
                    Valor = destino.Valor
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
                p.Add("DestinoID", this.Destino.Id);
                Db.Execute(Procs.Destino_Asociado_Actualizar, p, commandType: CommandType.StoredProcedure);
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
                Db.Execute(Procs.Destino_Asociado_Borrar, p, commandType: CommandType.StoredProcedure);
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