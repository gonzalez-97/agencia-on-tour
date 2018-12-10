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
    public class Pago_Actividad_Api : Pago_Actividad
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Pago_ID", this.Pago.Id);
                p.Add("Actividad_Asignada_ID", this.Actividad_Asignada.Id);
                Db.Execute(Procs.Pago_Actividad_Crear, p, commandType: CommandType.StoredProcedure);
                logger.Info("Pago actividad creada correctamente");
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Read(int id)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", id);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var result = Db.QuerySingle<dynamic>(Procs.Pago_Actividad_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Pago_Api pago = new Pago_Api();
                pago.Read((int)result.PAGOID);

                Actividad_Asociada_Api actividad_asociada = new Actividad_Asociada_Api();
                actividad_asociada.Read((int)result.ACTIVIDAD_ASIGNADAID);

                Id = (int)result.ID;
                Pago = new Pago()
                {
                    Id = pago.Id,
                    Alumno = pago.Alumno,
                    Valor_Pago = pago.Valor_Pago,
                    Total_Cuenta = pago.Total_Cuenta,
                    Fecha_Pago = pago.Fecha_Pago
                };
                Actividad_Asignada = new Actividad_Asociada()
                {
                    Id = actividad_asociada.Id,
                    Actividad = actividad_asociada.Actividad,
                    Curso = actividad_asociada.Curso,
                    Total_Recaudado = actividad_asociada.Total_Recaudado,
                    Prorrateo = actividad_asociada.Prorrateo
                };
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Delete()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Id", this.Id);
                Db.Execute(Procs.Pago_Actividad_Borrar, p, commandType: CommandType.StoredProcedure);
                logger.Info("Pago actividad N°{0} borrado correctamente", Id);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }
    }
}