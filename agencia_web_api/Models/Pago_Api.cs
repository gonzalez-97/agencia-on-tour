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
    public class Pago_Api: Pago
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Alumno_Rut", this.Alumno.Rut);
                p.Add("Valor_Pago", this.Valor_Pago);
                p.Add("Total_Cuenta", this.Total_Cuenta);
                Db.Execute(Procs.Pago_Crear, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.QuerySingle<dynamic>(Procs.Pago_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Alumno_Api alumno = new Alumno_Api();
                alumno.Read((int)result.ALUMNO_RUT);


                Id = (int)result.ID;
                Alumno = new Alumno()
                {
                    Rut = alumno.Rut,
                    DigitoV = alumno.DigitoV,
                    Nombre = alumno.Nombre,
                    APaterno = alumno.APaterno,
                    AMaterno = alumno.AMaterno,
                    TotalReunido = alumno.TotalReunido,
                    TotalPagar = alumno.TotalPagar,
                    Curso = alumno.Curso,
                    Apoderado = alumno.Apoderado
                };

                Valor_Pago = (int)result.VALOR_PAGO;
                Total_Cuenta = (int)result.TOTAL_CUENTA;
                Fecha_Pago = (DateTime)result.FECHA_PAGO;
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
                p.Add("Alumno_Rut", this.Alumno.Rut);
                p.Add("Valor_Pago", this.Valor_Pago);
                p.Add("Total_Cuenta", this.Total_Cuenta);
                p.Add("Fecha", this.Fecha_Pago);
                Db.Execute(Procs.Pago_Actualizar, p, commandType: CommandType.StoredProcedure);
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
                Db.Execute(Procs.Pago_Borrar, p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}