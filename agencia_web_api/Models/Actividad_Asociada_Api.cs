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
    public class Actividad_Asociada_Api : Actividad_Asociada
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {

                Colecciones col = new Colecciones();
                List<Alumno> alumnos_from_curso = col.ListaAlumnos().Where(n => n.Curso.Id == Curso.Id).ToList();
                //Divide el total recaudado entre el total de alumnos...
                Prorrateo = Total_Recaudado / ((alumnos_from_curso.Count > 0) ? alumnos_from_curso.Count : 1);

                var p = new OracleDynamicParameters();
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                p.Add("ActividadID", this.Actividad.Id);
                p.Add("CursoID", this.Curso.Id);
                p.Add("Total_Recaudado", this.Total_Recaudado);
                p.Add("Prorrateo", this.Prorrateo);
                var retorno = Db.QuerySingle<Actividad_Asociada_Api>(Procs.Actividad_Asociada_Por_Id, param: p, commandType: CommandType.StoredProcedure);
                this.Id = retorno.Id;


                foreach (Alumno alu in alumnos_from_curso)
                {
                    var parametrosPago = new OracleDynamicParameters();
                    parametrosPago.Add("Alumno_Rut", alu.Rut);
                    parametrosPago.Add("Valor_Pago", Prorrateo);
                    parametrosPago.Add("Total_Cuenta", alu.TotalReunido);
                    parametrosPago.Add("Id_Actividad_Asignada", Id);
                    Db.Execute(Procs.Pago_Crear_Desde_Actividad, parametrosPago, commandType: CommandType.StoredProcedure);
                }

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

                var result = Db.QuerySingle<dynamic>(Procs.Actividad_Asociada_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Curso_Api curso = new Curso_Api();
                curso.Read((int)result.CURSOID);

                Actividad_Api actividad = new Actividad_Api();
                actividad.Read((int)result.ACTIVIDADID);

                Id = (int)result.ID;
                Actividad = new Actividad()
                {
                    Id = actividad.Id,
                    Nombre = actividad.Nombre,
                    Descripcion = actividad.Descripcion
                };
                Curso = new Curso()
                {
                    Id = curso.Id,
                    Nombre = curso.Nombre,
                    TotalReunido = curso.TotalReunido,
                    Colegio = curso.Colegio
                };
                Total_Recaudado = (int)result.TOTAL_RECAUDADO;
                Prorrateo = (int)result.PRORRATEO;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}