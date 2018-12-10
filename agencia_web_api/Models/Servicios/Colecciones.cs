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
                    TotalReunido = (int?)n.TOTALREUNIDO,
                    TotalPagar = (int?)n.TOTALPAGAR,
                    Apoderado = new Apoderado() { Id = apoderado.Id, Usuario = apoderado.Usuario },
                    Curso = new Curso() { Id = curso.Id, Nombre = curso.Nombre, TotalReunido = curso.TotalReunido, TotalPagar = curso.TotalPagar, Colegio = curso.Colegio }
                };
            });

            return salida;

        }

        public IEnumerable<Tipo_Seguro> ListaTipoSeguro()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Tipo_Seguro>(Procs.Seguro_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Seguro_Asociado> ListaSeguroAsociados()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);


            var result = Db.Query<dynamic>(Procs.Seguros_Asociado_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)n.CONTRATOID);

                Tipo_Seguro_Api seguro = new Tipo_Seguro_Api();
                seguro.Read((int)n.TIPO_SEGUROID);

                return new Seguro_Asociado()
                {
                    Id = (int)n.ID,
                    Valor = (int)n.VALOR,
                    Total_Dias = (int)n.TOTAL_DIAS,
                    Seguro = (int)n.SEGUROID,
                    Contrato = new Contrato()
                    {
                        Id = contrato.Id,
                        Curso = contrato.Curso,
                        Nombre = contrato.Nombre,
                        Descripcion = contrato.Descripcion,
                        Fecha_Viaje = contrato.Fecha_Viaje,
                        Valor = contrato.Valor
                    },
                    Tipo_Seguro = new Tipo_Seguro()
                    {
                        Id = seguro.Id,
                        Nombre = seguro.Nombre,
                        Tipo_Aseguradora = seguro.Tipo_Aseguradora
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Seguro_Asociado> ListaSeguroAsociadosXContrato(int Id)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);


            var result = Db.Query<dynamic>(Procs.Seguros_Asociado_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Where(aux => (int)aux.CONTRATOID == Id).Select(n =>
            {

                Tipo_Seguro_Api seguro = new Tipo_Seguro_Api();
                seguro.Read((int)n.TIPO_SEGUROID);

                return new Seguro_Asociado()
                {
                    Id = (int)n.ID,
                    Valor = (int)n.VALOR,
                    Seguro = (int)n.SEGUROID,
                    Total_Dias = (int)n.TOTAL_DIAS,
                    Tipo_Seguro = new Tipo_Seguro()
                    {
                        Id = seguro.Id,
                        Nombre = seguro.Nombre,
                        Tipo_Aseguradora = seguro.Tipo_Aseguradora
                    }
                };
            });

            return salida;
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
                    Estado = ((int)n.ESTADO > 0) ? true : false,
                    Valor = (int)n.TOTAL,
                    Curso = new Curso() { Id = curso.Id, Nombre = curso.Nombre, TotalReunido = curso.TotalReunido, TotalPagar = curso.TotalPagar, Colegio = curso.Colegio },
                    ListaSeguroAsociados = ListaSeguroAsociadosXContrato((int)n.ID).ToList(),
                    ListaServiciosAsociados = ListaServiciosAsociadosXContrato((int)n.ID).ToList(),
                    ListaDestinosAsociados = ListaDestinosAsociadosXContrato((int)n.ID).ToList(),
                    ListaArchivos = ListaArchivosXContrato((int)n.ID).ToList()
                };
            });

            return salida;
        }

        public IEnumerable<Servicio> ListaServicios()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            return Db.Query<Servicio>(Procs.Servicio_Todos, p, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Servicio_Asociado> ListaServiciosAsociadosXContrato(int Id)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Servicios_Asociado_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Where(aux => (int)aux.CONTRATOID == Id).Select(n =>
            {
                Servicio_Api servicio = new Servicio_Api();
                servicio.Read((int)n.SERVICIOID);
                return new Servicio_Asociado()
                {
                    Id = (int)n.ID,
                    Servicio = new Servicio()
                    {
                        Id = servicio.Id,
                        Nombre = servicio.Nombre,
                        Descripcion = servicio.Descripcion,
                        Valor = servicio.Valor
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Servicio_Asociado> ListaServiciosAsociados()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Servicios_Asociado_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)n.CONTRATOID);

                Servicio_Api servicio = new Servicio_Api();
                servicio.Read((int)n.SERVICIOID);
                return new Servicio_Asociado()
                {
                    Id = (int)n.ID,
                    Contrato = new Contrato()
                    {
                        Id = contrato.Id,
                        Curso = contrato.Curso,
                        Nombre = contrato.Nombre,
                        Descripcion = contrato.Descripcion,
                        Fecha_Viaje = contrato.Fecha_Viaje,
                        Valor = contrato.Valor
                    },
                    Servicio = new Servicio()
                    {
                        Id = servicio.Id,
                        Nombre = servicio.Nombre,
                        Descripcion = servicio.Descripcion,
                        Valor = servicio.Valor
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Destino_Asociado> ListaDestinosAsociados()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Destinos_Asociado_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)n.CONTRATOID);

                Destino_Api destino = new Destino_Api();
                destino.Read((int)n.DESTINOID);
                return new Destino_Asociado()
                {
                    Id = (int)n.ID,
                    Contrato = new Contrato()
                    {
                        Id = contrato.Id,
                        Curso = contrato.Curso,
                        Nombre = contrato.Nombre,
                        Descripcion = contrato.Descripcion,
                        Fecha_Viaje = contrato.Fecha_Viaje,
                        Valor = contrato.Valor
                    },
                    Destino = new Destino()
                    {
                        Id = destino.Id,
                        Nombre = destino.Nombre,
                        Valor = destino.Valor
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Destino_Asociado> ListaDestinosAsociadosXContrato(int IdContrato)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Destinos_Asociado_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Where(n => (int)n.CONTRATOID == IdContrato).Select(n =>
            {
                Destino_Api destino = new Destino_Api();
                destino.Read((int)n.DESTINOID);
                return new Destino_Asociado()
                {
                    Id = (int)n.ID,
                    Destino = new Destino()
                    {
                        Id = destino.Id,
                        Nombre = destino.Nombre,
                        Valor = destino.Valor
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Archivo> ListaArchivos()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Archivos_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Contrato_Api contrato = new Contrato_Api();
                contrato.Read((int)n.CONTRATOID);
                return new Archivo()
                {
                    Id = (int)n.ID,
                    Nombre = n.ARCHIVO,
                    Contrato = new Contrato()
                    {
                        Id = contrato.Id,
                        Nombre = contrato.Nombre,
                        Descripcion = contrato.Descripcion,
                        Fecha_Viaje = contrato.Fecha_Viaje,
                        Valor = contrato.Valor,
                        Curso = contrato.Curso,
                        Estado = contrato.Estado
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Archivo> ListaArchivosXContrato(int Id)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Archivos_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Where(n => (int)n.CONTRATOID == Id).Select(n =>
            {
                return new Archivo()
                {
                    Id = (int)n.ID,
                    Nombre = n.ARCHIVO
                };
            });

            return salida;
        }

        public IEnumerable<Pago> ListaPagos()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            var result = Db.Query<dynamic>(Procs.Pago_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Alumno_Api alumno = new Alumno_Api();
                alumno.Read((int)n.ALUMNO_RUT);

                return new Pago()
                {
                    Id = (int)n.ID,
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
                    },
                    Valor_Pago = (int)n.VALOR_PAGO,
                    Total_Cuenta = (int)n.TOTAL_CUENTA,
                    Fecha_Pago = (DateTime)n.FECHA_PAGO
                };
            });

            return salida;
        }

        public IEnumerable<Actividad_Asociada> ListaActividadAsociada()
        {
             var p = new OracleDynamicParameters();
             p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

             var result = Db.Query<dynamic>(Procs.Actividad_Asociada_Todos, param: p, commandType: CommandType.StoredProcedure);

             var salida = result.Select(n =>
             {
                  Curso_Api curso = new Curso_Api();
                  curso.Read((int)n.CURSOID);

                  Actividad_Api actividad = new Actividad_Api();
                  actividad.Read((int)n.ACTIVIDADID);

                  return new Actividad_Asociada()
                  {
                      Id = (int)n.ID,
                      Actividad = new Actividad()
                      {
                          Id = actividad.Id,
                          Nombre = actividad.Nombre,
                          Descripcion = actividad.Descripcion
                      },
                      Curso = new Curso()
                      {
                          Id = curso.Id,
                          Nombre = curso.Nombre,
                          TotalReunido = curso.TotalReunido,
                          TotalPagar = curso.TotalPagar,
                          Colegio = curso.Colegio
                      },
                      Total_Recaudado = (int)n.TOTAL_RECAUDADO,
                      Prorrateo = (int)n.PRORRATEO
                  };
             });

            return salida;
        }

        public IEnumerable<Pago_Actividad> ListaPagoActividad()
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var result = Db.Query<dynamic>(Procs.Pago_Actividad_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Select(n =>
            {
                Pago_Api pago = new Pago_Api();
                pago.Read((int)n.PAGOID);

                Actividad_Asociada_Api actividad_asociada = new Actividad_Asociada_Api();
                actividad_asociada.Read((int)n.ACTIVIDAD_ASIGNADAID);

                return new Pago_Actividad()
                {
                    Id = (int)n.ID,
                    Pago = new Pago()
                    {
                        Id = pago.Id,
                        Alumno = pago.Alumno,
                        Valor_Pago = pago.Valor_Pago,
                        Total_Cuenta = pago.Total_Cuenta,
                        Fecha_Pago = pago.Fecha_Pago
                    },
                    Actividad_Asignada = new Actividad_Asociada()
                    {
                        Id = actividad_asociada.Id,
                        Actividad = actividad_asociada.Actividad,
                        Curso = actividad_asociada.Curso,
                        Total_Recaudado = actividad_asociada.Total_Recaudado,
                        Prorrateo = actividad_asociada.Prorrateo
                    }
                };
            });

            return salida;
        }

        public IEnumerable<Pago_Actividad> ListaPagoActividadXActividad(int id)
        {
            var p = new OracleDynamicParameters();
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

            var result = Db.Query<dynamic>(Procs.Pago_Actividad_Todos, param: p, commandType: CommandType.StoredProcedure);

            var salida = result.Where(n => (int)n.ACTIVIDAD_ASIGNADAID == id).Select(n =>
            {
                Pago_Api pago = new Pago_Api();
                pago.Read((int)n.PAGOID);

                return new Pago_Actividad()
                {
                    Id = (int)n.ID,
                    Pago = new Pago()
                    {
                        Id = pago.Id,
                        Alumno = pago.Alumno,
                        Valor_Pago = pago.Valor_Pago,
                        Total_Cuenta = pago.Total_Cuenta,
                        Fecha_Pago = pago.Fecha_Pago
                    }
                };
            });

            return salida;
        }

    }
}