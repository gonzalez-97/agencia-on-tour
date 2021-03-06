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
    public class Alumno_Api: Alumno
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        IDbConnection Db = ConexionDb.GeneraConexion();

        public bool ExisteAlumno(int rut)
        {
            var p = new OracleDynamicParameters();
            p.Add("Rut", rut);
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            int retorno = Db.QuerySingle<int>(Procs.Existe_Alumno_Por_Rut, p, commandType: CommandType.StoredProcedure);
            return (retorno > 0) ? true : false;
        }

        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                p.Add("DigitoV", this.DigitoV);
                p.Add("Nombre", this.Nombre);
                p.Add("APaterno", this.APaterno);
                p.Add("AMaterno", this.AMaterno);
                p.Add("Apoderado_Id", this.Apoderado.Id);
                p.Add("Curso", this.Curso.Id);
                Db.Execute(Procs.Alumno_Crear, p, commandType: CommandType.StoredProcedure);

                logger.Info("Alumno N°{0} creado correctamente", Rut);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Read(int rut)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", rut);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);

                var result = Db.QuerySingle<dynamic>(Procs.Alumno_Por_Rut, param: p, commandType: CommandType.StoredProcedure);

                Apoderado_Api apoderado = new Apoderado_Api();
                apoderado.Read((int)result.APOID);

                Curso_Api curso = new Curso_Api();
                curso.Read((int)result.CURID);

                Rut = (int)result.RUT;
                DigitoV = result.DIGITOV;
                Nombre = result.NOMBRE;
                APaterno = result.APATERNO;
                AMaterno = result.AMATERNO;
                TotalReunido = (int?)result.TOTALREUNIDO;
                TotalPagar = (int?)result.TOTALPAGAR;
                Apoderado = new Apoderado() { Id = apoderado.Id, Usuario = apoderado.Usuario };
                Curso = new Curso() { Id = curso.Id, Nombre = curso.Nombre, TotalPagar = curso.TotalPagar, TotalReunido = curso.TotalReunido, Colegio = curso.Colegio };

                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool Update()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                p.Add("DigitoV", this.DigitoV);
                p.Add("Nombre", this.Nombre);
                p.Add("APaterno", this.APaterno);
                p.Add("AMaterno", this.AMaterno);
                p.Add("Apoderado_Id", this.Apoderado.Id);
                p.Add("Curso", this.Curso.Id);
                p.Add("TotalReunido", this.TotalReunido);
                p.Add("TotalPagar", this.TotalPagar);
                Db.Execute(Procs.Alumno_Actualizar, p, commandType: CommandType.StoredProcedure);

                logger.Info("Alumno N°{0} actualizado correctamente", Rut);
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
                p.Add("Rut", this.Rut);
                Db.Execute(Procs.Alumno_Borrar, p, commandType: CommandType.StoredProcedure);
                logger.Info("Alumno N°{0} borrado correctamente", Rut);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        private void MappingThisFromAlumno(Alumno objeto)
        {
            this.Nombre = objeto.Nombre;
            this.Rut = objeto.Rut;
            this.DigitoV = objeto.DigitoV;
            this.APaterno = objeto.APaterno;
            this.AMaterno = objeto.AMaterno;
            this.Apoderado = objeto.Apoderado;
            this.Curso = objeto.Curso;
        }
    }
}