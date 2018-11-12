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
    public class Seguro_Asociado_Api : Seguro_Asociado
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("ContratoID", this.Contrato.Id);
                p.Add("SeguroID", this.Seguro.Id);
                p.Add("Valor", this.Valor);
                p.Add("Tipo_Seguro", this.Tipo_Seguro);
                p.Add("Total_Dias", this.Total_Dias);
                Db.Execute(Procs.Seguro_Asociado_Crear, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.QuerySingle<dynamic>(Procs.Seguro_Asociado_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)result.CONTRATOID);

                Seguro_Api seguro = new Seguro_Api();
                seguro.Read((int)result.SEGUROID);

                Id = (int)result.ID;
                Valor = (int)result.VALOR;
                Tipo_Seguro = (int)result.TIPO_SEGURO;
                Total_Dias = (int)result.TOTAL_DIAS;
                Contrato = new Contrato() {
                    Id =contrato.Id,
                    Curso = contrato.Curso,
                    Nombre = contrato.Nombre ,
                    Descripcion = contrato.Descripcion,
                    Fecha_Viaje = contrato.Fecha_Viaje,
                    Valor = contrato.Valor
                };
                Seguro = new Seguro()
                {
                    Id =seguro.Id,
                    Nombre = seguro.Nombre,
                    Descripcion = seguro.Descripcion,
                    Dias_Cobertura = seguro.Dias_Cobertura
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
                p.Add("SeguroID", this.Seguro.Id);
                p.Add("Valor", this.Valor);
                p.Add("Tipo_Seguro", this.Tipo_Seguro);
                p.Add("Total_Dias", this.Total_Dias);
                Db.Execute(Procs.Seguro_Asociado_Actualizar, p, commandType: CommandType.StoredProcedure);
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
                Db.Execute(Procs.Seguro_Asociado_Borrar, p, commandType: CommandType.StoredProcedure);
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