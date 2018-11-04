﻿using agencia_lib;
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
    public class Contrato_Api: Contrato
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Curso", this.Curso.Id);
                p.Add("Nombre", this.Nombre);
                p.Add("Descripcion", this.Descripcion);
                p.Add("Fecha_Viaje", this.Fecha_Viaje);
                p.Add("Total", this.Valor);
                Db.Execute(Procs.Contrato_Crear, p, commandType: CommandType.StoredProcedure);
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

                var result = Db.QuerySingle<dynamic>(Procs.Contrato_Por_Id, param: p, commandType: CommandType.StoredProcedure);

                Curso_Api curso = new Curso_Api();
                curso.Read((int)result.CURSOID);

                Id = (int)result.ID;
                Fecha_Viaje = (DateTime)result.FECHA_VIAJE;
                Nombre = result.NOMBRE;
                Descripcion = result.DESCRIPCION;
                Curso = new Curso() { Id = curso.Id, Nombre = curso.Nombre, TotalReunido = curso.TotalReunido, Colegio = curso.Colegio };

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
                p.Add("Curso", this.Curso.Id);
                p.Add("Nombre", this.Nombre);
                p.Add("Descripcion", this.Descripcion);
                p.Add("Fecha_Viaje", this.Fecha_Viaje);
                p.Add("Valor", this.Valor);
                Db.Execute(Procs.Contrato_Actualizar, p, commandType: CommandType.StoredProcedure);
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
                Db.Execute(Procs.Contrato_Borrar, p, commandType: CommandType.StoredProcedure);
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