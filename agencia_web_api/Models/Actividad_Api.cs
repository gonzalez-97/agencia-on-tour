﻿using agencia_lib;
using agencia_web_api.Models.Servicios;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace agencia_web_api.Models
{
    public class Actividad_Api : Actividad
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Nombre", this.Nombre);
                p.Add("Descripcion", this.Descripcion);
                Db.Execute("sp_actividad_create", p, commandType: CommandType.StoredProcedure);
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

                var retorno = Db.QuerySingle<Actividad_Api>("sp_actividad_por_id", p, commandType: CommandType.StoredProcedure);
                MappingThisFromAnother(retorno);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }

        }

        public bool Update()
        {
            try
            {
                var p = new OracleDynamicParameters();
                AddParametersThis(p);
                Db.Execute("sp_actividad_update", p, commandType: CommandType.StoredProcedure);
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
                Db.Execute("sp_actividad_delete", p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void AddParametersThis(OracleDynamicParameters parameters)
        {
            Type t = GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                object valor = p.GetValue(this, null);
                parameters.Add(p.Name, valor);
            }
        }

        public void MappingThisFromAnother(Actividad_Api objeto)
        {
            this.Id = objeto.Id;
            this.Nombre = objeto.Nombre;
            this.Descripcion = objeto.Descripcion;
        }
    }
}