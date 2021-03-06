﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace agencia_web_api.Models.Servicios
{
    public static class Procs
    {
        #region Colecciones ó lista de datos
        public const string Usuario_Todos = "sp_usuarios_todos";
        public const string Perfil_Todos = "sp_perfiles_todos";
        public const string Perfil_Por_Rut = "sp_perfiles_por_rut";
        public const string Actividad_Todos = "sp_actividad_todas";
        public const string Destino_Todos = "sp_destino_todos";
        public const string Colegio_Todos = "sp_colegio_todos";
        public const string Curso_Todos = "sp_curso_todos";
        public const string Apoderado_Todos = "sp_apoderado_todos";
        public const string Alumno_Todos = "sp_alumno_todos";
        public const string Seguro_Todos = "sp_tipo_seguro_todos";
        public const string Contrato_Todos = "sp_contrato_todos";
        public const string Seguros_Asociado_Todos = "sp_seguros_asignados_todos";
        public const string Servicio_Todos = "sp_servicio_todos";
        public const string Servicios_Asociado_Todos = "sp_servicio_asignado_todos";
        public const string Destinos_Asociado_Todos = "sp_destinos_asignados_todos";
        public const string Archivos_Todos = "sp_archivo_todos";
        public const string Pago_Todos = "sp_pago_todos";
        public const string Actividad_Asociada_Todos = "sp_actividad_asignada_todos";
        public const string Pago_Actividad_Todos = "sp_pago_actividad_todos";
        #endregion

        #region Actividad
        public const string Actividad_Crear = "sp_actividad_create";
        public const string Actividad_Por_Id = "sp_actividad_por_id";
        public const string Actividad_Actualizar = "sp_actividad_update";
        public const string Actividad_Borrar = "sp_actividad_delete";
        #endregion

        #region Apoderado
        public const string Apoderado_Crear = "sp_apoderado_create";
        public const string Existe_Apoderado_Por_Rut = "sp_existe_apoderado_por_rut";
        public const string Apoderado_Por_Rut = "sp_apoderado_por_rut";
        public const string Apoderado_Por_Id = "sp_apoderado_por_id";
        public const string Apoderado_Borrar_Por_Rut = "sp_apoderado_delete_por_rut";
        #endregion

        #region Colegio
        public const string Colegio_Crear = "sp_colegio_create";
        public const string Colegio_Por_Id = "sp_colegio_por_id";
        public const string Colegio_Actualizar = "sp_colegio_update";
        public const string Colegio_Borrar = "sp_colegio_delete";
        #endregion

        #region Curso
        public const string Curso_Crear = "sp_curso_create";
        public const string Curso_Por_Id = "sp_curso_por_id";
        public const string Curso_Actualizar = "sp_curso_update";
        public const string Curso_Borrar = "sp_curso_delete";
        #endregion

        #region Destino
        public const string Destino_Crear = "sp_destino_create";
        public const string Destino_Por_Id = "sp_destino_por_id";
        public const string Destino_Actualizar = "sp_destino_update";
        public const string Destino_Borrar = "sp_destino_delete";
        #endregion

        #region Destino Asociado
        public const string Destino_Asociado_Crear = "sp_destinos_asignados_crear";
        public const string Destino_Asociado_Actualizar = "sp_destinos_asignados_update";
        public const string Destino_Asociado_Borrar = "sp_destinos_asignados_delete";
        public const string Destino_Asociado_Por_Id = "sp_destinos_asignados_por_id";
        #endregion

        #region Perfil Asociado
        public const string Perfil_Asociado_Crear = "sp_perfiles_asignados_crear";
        public const string Perfil_Asociado_Borrar_Por_Rut = "sp_perfiles_asignados_borrar_todos_rut";
        #endregion

        #region Usuario
        public const string Usuario_Existe = "sp_existe_usuario";
        public const string Usuario_Crear = "sp_usuarios_create";
        public const string Usuario_Actualizar = "sp_usuarios_update";
        public const string Usuario_Actualizar_Password = "sp_usuarios_update_password";
        public const string Usuario_Por_Rut = "sp_usuarios_por_rut";
        public const string Usuario_Borrar = "sp_usuarios_delete";
        #endregion

        #region Alumno
        public const string Alumno_Crear = "sp_alumno_create";
        public const string Existe_Alumno_Por_Rut = "sp_existe_alumno_por_rut";
        public const string Alumno_Borrar = "sp_alumno_delete";
        public const string Alumno_Por_Rut = "sp_alumno_por_rut";
        public const string Alumno_Actualizar = "sp_alumno_update";
        #endregion

        #region Seguros
        public const string Seguro_Por_Id = "sp_tipo_seguro_por_id";

        #endregion

        #region Seguros Asociados
        public const string Seguro_Asociado_Crear = "sp_seguros_asignados_crear";
        public const string Seguro_Asociado_Actualizar = "sp_seguros_asignados_update";
        public const string Seguro_Asociado_Borrar = "sp_seguros_asignados_delete";
        public const string Seguro_Asociado_Por_Id = "sp_seguros_asignados_por_id";

        #endregion

        #region Contrato
        public const string Contrato_Crear = "sp_contrato_create";
        public const string Contrato_Por_Id = "sp_contrato_por_id";
        public const string Contrato_Borrar = "sp_contrato_delete";
        public const string Contrato_Actualizar = "sp_contrato_update";
        #endregion

        #region Servicio
        public const string Servicio_Crear = "sp_servicio_create";
        public const string Servicio_Actualizar = "sp_servicio_update";
        public const string Servicio_Borrar = "sp_servicio_delete";
        public const string Servicio_Por_Id = "sp_servicio_por_id";
        #endregion

        #region Servicio Asociado
        public const string Servicio_Asociado_Crear = "sp_servicio_asignado_create";
        public const string Servicio_Asociado_Actualizar = "sp_servicio_asignado_update";
        public const string Servicio_Asociado_Borrar = "sp_servicio_asignado_delete";
        public const string Servicio_Asociado_Por_Id = "sp_servicio_asignado_por_id";
        #endregion

        #region Archivo Contrato
        public const string Archivo_Crear = "sp_archivo_create";
        public const string Archivo_Por_Id = "sp_archivo_por_id";
        public const string Archivo_Borrar = "sp_archivo_delete";
        #endregion

        #region Pagos
        public const string Pago_Crear = "sp_pago_create";
        public const string Pago_Crear_Desde_Actividad = "sp_pago_desde_actividad_create";
        public const string Pago_Por_Id = "sp_pago_por_id";
        public const string Pago_Actualizar = "sp_pago_update";
        public const string Pago_Borrar = "sp_pago_delete";
        #endregion

        #region Actividades Asignadas
        public const string Actividad_Asociada_Crear = "sp_actividad_asignada_create";
        public const string Actividad_Asociada_Por_Id = "sp_actividad_asignada_por_id";
        public const string Actividad_Asociada_Borrar = "sp_actividad_asignada_delete";
        public const string Actividad_Asociada_Actualizar = "sp_actividad_asignada_update";
        #endregion

        #region Pagos Actividades
        public const string Pago_Actividad_Crear = "sp_pago_actividad_create";
        public const string Pago_Actividad_Por_Id = "sp_pago_actividad_por_id";
        public const string Pago_Actividad_Borrar = "sp_pago_actividad_delete";
        #endregion
    }
}