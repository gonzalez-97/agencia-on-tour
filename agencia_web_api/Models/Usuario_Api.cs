﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using agencia_lib;
using agencia_web_api.Models.Servicios;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace agencia_web_api.Models
{
    public class Usuario_Api : Usuario
    {
        IDbConnection Db = ConexionDb.GeneraConexion();
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public bool ExisteUsuario(int rut, string pass)
        {
            var p = new OracleDynamicParameters();
            p.Add("Rut", rut);
            p.Add("Password", pass);
            p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
            int retorno = Db.QuerySingle<int>(Procs.Usuario_Existe, p, commandType: CommandType.StoredProcedure);
            return (retorno > 0) ? true:  false;
        }


        public bool Create()
        {
            try
            {
                var p = new OracleDynamicParameters();
                this.Password =  EncodePassword(Password);

                AddParametersThis(p);
                Db.Execute(Procs.Usuario_Crear, p, commandType: CommandType.StoredProcedure);
                logger.Info("Usuario creado correctamente");
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
                var salida = true;
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                p.Add("DigitoV", this.DigitoV);
                p.Add("Correo", this.Correo);
                p.Add("Nombre", this.Nombre);
                p.Add("APaterno", this.APaterno);
                p.Add("AMaterno", this.AMaterno);
                Db.Execute(Procs.Usuario_Actualizar, p, commandType: CommandType.StoredProcedure);

                //Si la contraseña no es vacia se modifica
                if (!string.IsNullOrEmpty(this.Password))
                    salida = UpdateOnlyPassword();

                logger.Info("Usuario N°{0} actualizado correctamente", Rut);
                return salida;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public bool UpdateOnlyPassword()
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", this.Rut);
                p.Add("Password", EncodePassword(this.Password));
                Db.Execute(Procs.Usuario_Actualizar_Password, p, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        private void AddParametersThis(OracleDynamicParameters parameters)
        {
            Type t = GetType();
            foreach (PropertyInfo p in t.GetProperties())
            {
                object valor = p.GetValue(this, null);
                if (!p.Name.Contains("Lista_"))
                    parameters.Add(p.Name, valor);
            }
        }

        public bool Read(int rut)
        {
            try
            {
                var p = new OracleDynamicParameters();
                p.Add("Rut", rut);
                p.Add("c1", dbType: OracleDbType.RefCursor, direction: ParameterDirection.Output);
                //Usuario
                var retorno = Db.QuerySingle<Usuario_Api>(Procs.Usuario_Por_Rut, p, commandType: CommandType.StoredProcedure);
                //Perfiles
                retorno.Lista_Perfiles = Db.Query<Perfil>(Procs.Perfil_Por_Rut, p, commandType: CommandType.StoredProcedure).ToList();

                MappingThisFromAnother(retorno);
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
                Db.Execute(Procs.Usuario_Borrar, p, commandType: CommandType.StoredProcedure);
                logger.Info("Usuario N°{0} borrado correctamente", Rut);
                return true;
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                return false;
            }
        }

        public void MappingThisFromAnother(Usuario_Api objeto)
        {
            Colecciones col = new Colecciones();
            this.Rut = objeto.Rut;
            this.DigitoV = objeto.DigitoV;
            this.Correo = objeto.Correo;
            this.Nombre = objeto.Nombre;
            this.APaterno = objeto.APaterno;
            this.AMaterno = objeto.AMaterno;
            this.Password = objeto.Password;
            this.Lista_Perfiles = objeto.Lista_Perfiles;
        }

    }
}