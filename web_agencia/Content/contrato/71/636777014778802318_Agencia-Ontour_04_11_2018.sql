--------------------------------------------------------
--  File created - Sunday-November-04-2018   
--------------------------------------------------------
--------------------------------------------------------
--  DDL for Table ACTIVIDAD
--------------------------------------------------------

  CREATE TABLE "ACTIVIDAD" 
   (	"ACT_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"ACT_NOMBRE" CHAR(256 BYTE), 
	"ACT_DESCRIPCION" CHAR(256 BYTE), 
	"ACT_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table ACTIVIDAD_ASIGNADA
--------------------------------------------------------

  CREATE TABLE "ACTIVIDAD_ASIGNADA" 
   (	"ASIGACT_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"ACT_ID" NUMBER(*,0), 
	"CUR_ID" NUMBER(*,0), 
	"ASIGACT_TOTAL_RECAUDADO" NUMBER(*,0), 
	"ASIGACT_PRORRATEO" NUMBER, 
	"ASIGACT_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table ALUMNO
--------------------------------------------------------

  CREATE TABLE "ALUMNO" 
   (	"ALU_RUT" NUMBER(*,0), 
	"ALU_DIGITO_VERIFICADO" CHAR(1 BYTE), 
	"APO_ID" NUMBER(*,0), 
	"CUR_ID" NUMBER(*,0), 
	"ALU_NOMBRE" CHAR(50 BYTE), 
	"ALU_APATERNO" CHAR(50 BYTE), 
	"ALU_AMATERNO" CHAR(50 BYTE), 
	"ALU_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table APODERADO
--------------------------------------------------------

  CREATE TABLE "APODERADO" 
   (	"APO_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"USU_RUT" NUMBER(*,0), 
	"APO_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table COLEGIO
--------------------------------------------------------

  CREATE TABLE "COLEGIO" 
   (	"COL_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"COL_NOMBRE" CHAR(256 BYTE), 
	"COL_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table CONTRATO
--------------------------------------------------------

  CREATE TABLE "CONTRATO" 
   (	"CON_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"CUR_ID" NUMBER(*,0), 
	"CON_DESCRIPCION" CHAR(256 BYTE), 
	"CON_NOMBRE" CHAR(256 BYTE), 
	"CON_FECHA_VIAJE" DATE, 
	"CON_TOTAL_VIAJE" NUMBER(*,0), 
	"CON_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table CUENTA_CORRIENTE
--------------------------------------------------------

  CREATE TABLE "CUENTA_CORRIENTE" 
   (	"CUE_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"ALU_RUT" NUMBER(*,0), 
	"CUE_TOTAL_REUNIDO" NUMBER(*,0), 
	"CUE_MONTO_A_PAGAR" NUMBER(*,0), 
	"CUE_TOTAL_PAGADO" NUMBER(*,0), 
	"CUE_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table CURSO
--------------------------------------------------------

  CREATE TABLE "CURSO" 
   (	"CUR_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"COL_ID" NUMBER(*,0), 
	"CUR_NOMBRE" CHAR(256 BYTE), 
	"CUR_TOTAL_REUNIDO" NUMBER(*,0), 
	"CUR_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table DEPOSITO
--------------------------------------------------------

  CREATE TABLE "DEPOSITO" 
   (	"DEP_ID" NUMBER(*,0), 
	"DEP_TIPODEP_ID" NUMBER(*,0), 
	"DEP_FECHA_DEPOSITO" DATE, 
	"DEP_VALOR_DEPOSITO" NUMBER(*,0), 
	"DEP_DESCRIPCION" CHAR(256 BYTE)
   ) ;
--------------------------------------------------------
--  DDL for Table DESTINO
--------------------------------------------------------

  CREATE TABLE "DESTINO" 
   (	"DES_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"DES_NOMBRE" CHAR(256 BYTE), 
	"DES_VALOR" FLOAT(126), 
	"DES_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table DESTINO_ASIGNADO
--------------------------------------------------------

  CREATE TABLE "DESTINO_ASIGNADO" 
   (	"ASIGDES_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"CON_ID" NUMBER(*,0), 
	"DES_ID" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table PAGO
--------------------------------------------------------

  CREATE TABLE "PAGO" 
   (	"PAG_ID" NUMBER(*,0), 
	"CUE_ID" NUMBER(*,0), 
	"DEP_ID" NUMBER(*,0), 
	"PAG_VALOR_PAGO" NUMBER(*,0), 
	"PAG_MONTO_A_PAGAR" NUMBER(*,0), 
	"PAG_TOTAL_PAGADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table PERFIL_ASIGNADO
--------------------------------------------------------

  CREATE TABLE "PERFIL_ASIGNADO" 
   (	"ASIGPER_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"PER_ID" NUMBER(*,0), 
	"USU_RUT" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table PERFIL_USUARIO
--------------------------------------------------------

  CREATE TABLE "PERFIL_USUARIO" 
   (	"PER_ID" NUMBER(*,0), 
	"PER_TIPO" CHAR(30 BYTE)
   ) ;
--------------------------------------------------------
--  DDL for Table SEGURO
--------------------------------------------------------

  CREATE TABLE "SEGURO" 
   (	"SEG_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"SEG_NOMBRE" CHAR(50 BYTE), 
	"SEG_DESCRIPCION" CHAR(256 BYTE), 
	"SEG_DIAS_COBERTURA" NUMBER(*,0), 
	"SEG_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table SEGURO_ASIGNADO
--------------------------------------------------------

  CREATE TABLE "SEGURO_ASIGNADO" 
   (	"ASIGSEG_ID" NUMBER(*,0), 
	"CON_ID" NUMBER(*,0), 
	"SEG_ID" NUMBER(*,0), 
	"ASIGSEG_VALOR" NUMBER(*,0), 
	"ASIGSEG_ID_TIPO_SEGURO" NUMBER(*,0), 
	"ASIGSEG_ELIMINADO" NUMBER(*,0)
   ) ;
--------------------------------------------------------
--  DDL for Table TIPO_DEPOSITO
--------------------------------------------------------

  CREATE TABLE "TIPO_DEPOSITO" 
   (	"TIPODEP_ID" NUMBER(*,0), 
	"TIPODEP_DESCRIPCION" CHAR(256 BYTE)
   ) ;
--------------------------------------------------------
--  DDL for Table USUARIO
--------------------------------------------------------

  CREATE TABLE "USUARIO" 
   (	"USU_RUT" NUMBER(*,0), 
	"USU_DIGITO_VERIFICADO" CHAR(1 BYTE), 
	"USU_CORREO_ELECTRONICO" CHAR(30 BYTE), 
	"USU_PASSWORD" CHAR(30 BYTE), 
	"USU_NOMBRE" CHAR(30 BYTE), 
	"USU_APATERNO" CHAR(30 BYTE), 
	"USU_AMANTERNO" CHAR(30 BYTE), 
	"USU_ELIMINADO" NUMBER(*,0)
   ) ;
REM INSERTING into ACTIVIDAD
SET DEFINE OFF;
Insert into ACTIVIDAD (ACT_ID,ACT_NOMBRE,ACT_DESCRIPCION,ACT_ELIMINADO) values (1,'Liceo 1                                                                                                                                                                                                                                                         ','Liceo 1 de providencia                                                                                                                                                                                                                                          ',1);
Insert into ACTIVIDAD (ACT_ID,ACT_NOMBRE,ACT_DESCRIPCION,ACT_ELIMINADO) values (2,'Liceo 2                                                                                                                                                                                                                                                         ','Liceo 2 de Providencia                                                                                                                                                                                                                                          ',1);
REM INSERTING into ACTIVIDAD_ASIGNADA
SET DEFINE OFF;
REM INSERTING into ALUMNO
SET DEFINE OFF;
Insert into ALUMNO (ALU_RUT,ALU_DIGITO_VERIFICADO,APO_ID,CUR_ID,ALU_NOMBRE,ALU_APATERNO,ALU_AMATERNO,ALU_ELIMINADO) values (1,'9',65,1,'Juanito                                           ','Perez                                             ','Perez                                             ',1);
REM INSERTING into APODERADO
SET DEFINE OFF;
Insert into APODERADO (APO_ID,USU_RUT,APO_ELIMINADO) values (65,195709297,1);
REM INSERTING into COLEGIO
SET DEFINE OFF;
Insert into COLEGIO (COL_ID,COL_NOMBRE,COL_ELIMINADO) values (1,'Cumbre de Condores                                                                                                                                                                                                                                              ',1);
Insert into COLEGIO (COL_ID,COL_NOMBRE,COL_ELIMINADO) values (2,'San José                                                                                                                                                                                                                                                       ',1);
REM INSERTING into CONTRATO
SET DEFINE OFF;
Insert into CONTRATO (CON_ID,CUR_ID,CON_DESCRIPCION,CON_NOMBRE,CON_FECHA_VIAJE,CON_TOTAL_VIAJE,CON_ELIMINADO) values (1,1,'Viajes a la costa :)                                                                                                                                                                                                                                            ','Viaje a Fin de año 3°C                                                                                                                                                                                                                                        ',to_date('03-NOV-18','DD-MON-RR'),100000,1);
REM INSERTING into CUENTA_CORRIENTE
SET DEFINE OFF;
REM INSERTING into CURSO
SET DEFINE OFF;
Insert into CURSO (CUR_ID,COL_ID,CUR_NOMBRE,CUR_TOTAL_REUNIDO,CUR_ELIMINADO) values (1,2,'3°B                                                                                                                                                                                                                                                            ',0,1);
Insert into CURSO (CUR_ID,COL_ID,CUR_NOMBRE,CUR_TOTAL_REUNIDO,CUR_ELIMINADO) values (2,1,'4°B                                                                                                                                                                                                                                                            ',0,1);
REM INSERTING into DEPOSITO
SET DEFINE OFF;
REM INSERTING into DESTINO
SET DEFINE OFF;
Insert into DESTINO (DES_ID,DES_NOMBRE,DES_VALOR,DES_ELIMINADO) values (1,'Cartagena                                                                                                                                                                                                                                                       ',15000,1);
Insert into DESTINO (DES_ID,DES_NOMBRE,DES_VALOR,DES_ELIMINADO) values (2,'El Quisco                                                                                                                                                                                                                                                       ',200000,1);
Insert into DESTINO (DES_ID,DES_NOMBRE,DES_VALOR,DES_ELIMINADO) values (3,'Viña del Mar                                                                                                                                                                                                                                                   ',300000,1);
REM INSERTING into DESTINO_ASIGNADO
SET DEFINE OFF;
REM INSERTING into PAGO
SET DEFINE OFF;
REM INSERTING into PERFIL_ASIGNADO
SET DEFINE OFF;
Insert into PERFIL_ASIGNADO (ASIGPER_ID,PER_ID,USU_RUT) values (4,1,195709297);
Insert into PERFIL_ASIGNADO (ASIGPER_ID,PER_ID,USU_RUT) values (5,5,195709297);
REM INSERTING into PERFIL_USUARIO
SET DEFINE OFF;
Insert into PERFIL_USUARIO (PER_ID,PER_TIPO) values (1,'Administrador                 ');
Insert into PERFIL_USUARIO (PER_ID,PER_TIPO) values (2,'Ejecutivo                     ');
Insert into PERFIL_USUARIO (PER_ID,PER_TIPO) values (3,'Dueño                        ');
Insert into PERFIL_USUARIO (PER_ID,PER_TIPO) values (4,'Encargado Curso               ');
Insert into PERFIL_USUARIO (PER_ID,PER_TIPO) values (5,'Apoderado                     ');
REM INSERTING into SEGURO
SET DEFINE OFF;
Insert into SEGURO (SEG_ID,SEG_NOMBRE,SEG_DESCRIPCION,SEG_DIAS_COBERTURA,SEG_ELIMINADO) values (1,'Seguro salud 2018                                 ','Seguro de salud temporada 2018                                                                                                                                                                                                                                  ',60,1);
REM INSERTING into SEGURO_ASIGNADO
SET DEFINE OFF;
REM INSERTING into TIPO_DEPOSITO
SET DEFINE OFF;
REM INSERTING into USUARIO
SET DEFINE OFF;
Insert into USUARIO (USU_RUT,USU_DIGITO_VERIFICADO,USU_CORREO_ELECTRONICO,USU_PASSWORD,USU_NOMBRE,USU_APATERNO,USU_AMANTERNO,USU_ELIMINADO) values (195709297,'7','david@gmail.com               ','1234                          ','David                         ','Gonzalez                      ','Rebeco                        ',1);
--------------------------------------------------------
--  DDL for Index PK_ACTIVIDAD
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_ACTIVIDAD" ON "ACTIVIDAD" ("ACT_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_ACTIVIDAD_ASIGNADA
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_ACTIVIDAD_ASIGNADA" ON "ACTIVIDAD_ASIGNADA" ("ACT_ID", "CUR_ID", "ASIGACT_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_ALUMNO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_ALUMNO" ON "ALUMNO" ("ALU_RUT") 
  ;
--------------------------------------------------------
--  DDL for Index PK_APODERADO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_APODERADO" ON "APODERADO" ("APO_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_COLEGIO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_COLEGIO" ON "COLEGIO" ("COL_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_CONTRATO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_CONTRATO" ON "CONTRATO" ("CON_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_CUENTA_CORRIENTE
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_CUENTA_CORRIENTE" ON "CUENTA_CORRIENTE" ("CUE_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_CURSO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_CURSO" ON "CURSO" ("CUR_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_DEPOSITO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_DEPOSITO" ON "DEPOSITO" ("DEP_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_DESTINO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_DESTINO" ON "DESTINO" ("DES_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_DESTINO_ASIGNADO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_DESTINO_ASIGNADO" ON "DESTINO_ASIGNADO" ("CON_ID", "DES_ID", "ASIGDES_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_PAGO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_PAGO" ON "PAGO" ("PAG_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_PERFIL_ASIGNADO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_PERFIL_ASIGNADO" ON "PERFIL_ASIGNADO" ("PER_ID", "USU_RUT", "ASIGPER_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_PERFIL_USUARIO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_PERFIL_USUARIO" ON "PERFIL_USUARIO" ("PER_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_SEGURO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_SEGURO" ON "SEGURO" ("SEG_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_SEGURO_ASIGNADO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_SEGURO_ASIGNADO" ON "SEGURO_ASIGNADO" ("CON_ID", "SEG_ID", "ASIGSEG_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_TIPO_DEPOSITO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_TIPO_DEPOSITO" ON "TIPO_DEPOSITO" ("TIPODEP_ID") 
  ;
--------------------------------------------------------
--  DDL for Index PK_USUARIO
--------------------------------------------------------

  CREATE UNIQUE INDEX "PK_USUARIO" ON "USUARIO" ("USU_RUT") 
  ;
--------------------------------------------------------
--  DDL for Index RELATIONSHIP_2_FK
--------------------------------------------------------

  CREATE INDEX "RELATIONSHIP_2_FK" ON "APODERADO" ("USU_RUT") 
  ;
--------------------------------------------------------
--  DDL for Procedure SP_ACTIVIDAD_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_ACTIVIDAD_CREATE" 
(Nombre VARCHAR2,Descripcion VARCHAR2) as 
begin
  INSERT INTO ACTIVIDAD
  (ACT_NOMBRE,ACT_DESCRIPCION, ACT_ELIMINADO)
  values (Nombre, Descripcion, 1);
  commit;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_ACTIVIDAD_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_ACTIVIDAD_DELETE" 
(Id IN NUMBER) AS 
BEGIN
 UPDATE ACTIVIDAD SET ACT_ELIMINADO = 0 WHERE ACT_ID= Id;  
 COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ACTIVIDAD_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_ACTIVIDAD_POR_ID" 
(Id IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
begin
  open c1 for
    SELECT ACTIVIDAD.ACT_ID Id,
    ACTIVIDAD.ACT_NOMBRE Nombre,
    ACTIVIDAD.ACT_DESCRIPCION Descripcion
    FROM ACTIVIDAD
    WHERE ACT_ELIMINADO > 0 AND  ACTIVIDAD.ACT_ID = Id;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ACTIVIDAD_TODAS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_ACTIVIDAD_TODAS" 
(c1 out sys_refcursor) as
begin
  open c1 for
    SELECT ACTIVIDAD.ACT_ID Id,
    ACTIVIDAD.ACT_NOMBRE Nombre,
    ACTIVIDAD.ACT_DESCRIPCION Descripcion
    FROM ACTIVIDAD
    WHERE ACT_ELIMINADO > 0;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ACTIVIDAD_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_ACTIVIDAD_UPDATE" 
(Id IN NUMBER, Nombre VARCHAR, Descripcion VARCHAR2) IS  
BEGIN  
 UPDATE ACTIVIDAD SET ACT_NOMBRE = Nombre,ACT_DESCRIPCION = Descripcion WHERE ACT_ID= Id;  
COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ALUMNO_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_ALUMNO_CREATE" 
(Rut IN NUMBER, DigitoV CHAR, Nombre CHAR, APaterno CHAR, AMaterno CHAR, Apoderado_Id IN NUMBER, Curso IN NUMBER) AS 
BEGIN

  INSERT INTO ALUMNO 
  (ALU_RUT,ALU_DIGITO_VERIFICADO, ALU_NOMBRE, ALU_APATERNO, ALU_AMATERNO, APO_ID, CUR_ID, ALU_ELIMINADO) 
  VALUES (Rut, DigitoV, Nombre, APaterno, AMaterno, Apoderado_Id, Curso, 1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ALUMNO_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_ALUMNO_DELETE" 
(Rut IN NUMBER) AS 
BEGIN
  
  UPDATE ALUMNO 
  SET 
  ALU_ELIMINADO = 0
  WHERE ALU_RUT = Rut;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ALUMNO_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_ALUMNO_POR_RUT" 
(Rut IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT
    Alu.ALU_RUT Rut,
    Alu.ALU_DIGITO_VERIFICADO DigitoV,
    Alu.ALU_NOMBRE Nombre,
    Alu.ALU_APATERNO APaterno,
    Alu.ALU_AMATERNO AMaterno,
    Alu.APO_ID ApoId,
    Alu.CUR_ID CurId
    FROM Alumno Alu
    WHERE  Alu.ALU_RUT = Rut;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ALUMNO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_ALUMNO_TODOS" 
(c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT
    Alu.ALU_RUT Rut,
    Alu.ALU_DIGITO_VERIFICADO DigitoV,
    Alu.ALU_NOMBRE Nombre,
    Alu.ALU_APATERNO APaterno,
    Alu.ALU_AMATERNO AMaterno,
    Alu.APO_ID ApoId,
    Alu.CUR_ID CurId
    FROM Alumno Alu
    WHERE Alu.ALU_ELIMINADO > 0 ;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_ALUMNO_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_ALUMNO_UPDATE" 
(Rut IN NUMBER, DigitoV CHAR, Nombre CHAR, APaterno CHAR, AMaterno CHAR, Apoderado_Id IN NUMBER, Curso IN NUMBER) AS 
BEGIN
  
  UPDATE ALUMNO 
  SET ALU_DIGITO_VERIFICADO = DigitoV, 
  ALU_NOMBRE = Nombre,
  ALU_AMATERNO = AMaterno, 
  ALU_APATERNO = APaterno,
  APO_ID = Apoderado_Id, 
  CUR_ID = Curso
  WHERE ALU_RUT = Rut;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_APODERADO_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_APODERADO_CREATE" 
(Rut IN NUMBER) AS 
BEGIN

  INSERT INTO Apoderado (USU_RUT, APO_ELIMINADO) 
  VALUES (Rut,1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_APODERADO_DELETE_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_APODERADO_DELETE_POR_RUT" 
(Rut IN NUMBER) AS 
BEGIN
  
  UPDATE Apoderado
  SET 
  APO_ELIMINADO = 0
  WHERE USU_RUT = Rut;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_APODERADO_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_APODERADO_POR_ID" 
(Id IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT  APO_ID Id,
    Usuario.USU_RUT Rut,
    Usuario.USU_DIGITO_VERIFICADO DigitoV,
    Usuario.USU_CORREO_ELECTRONICO Correo,
    Usuario.USU_PASSWORD Password,
    Usuario.USU_NOMBRE Nombre,
    Usuario.USU_APATERNO APaterno,
    Usuario.USU_AMANTERNO AMaterno
    FROM Apoderado
    JOIN Usuario ON Usuario.USU_RUT = Apoderado.USU_RUT
    WHERE Usuario.USU_ELIMINADO > 0 and Apoderado.APO_ELIMINADO > 0
    AND Apoderado.APO_ID = Id;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_APODERADO_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_APODERADO_POR_RUT" 
(Rut IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT  APO_ID Id,
    Usuario.USU_RUT Rut,
    Usuario.USU_DIGITO_VERIFICADO DigitoV,
    Usuario.USU_CORREO_ELECTRONICO Correo,
    Usuario.USU_PASSWORD Password,
    Usuario.USU_NOMBRE Nombre,
    Usuario.USU_APATERNO APaterno,
    Usuario.USU_AMANTERNO AMaterno
    FROM Apoderado
    JOIN Usuario ON Usuario.USU_RUT = Apoderado.USU_RUT
    WHERE Usuario.USU_ELIMINADO > 0 and Apoderado.APO_ELIMINADO > 0
    AND Apoderado.USU_RUT = Rut;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_APODERADO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_APODERADO_TODOS" 
(c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT  APO.APO_ID Id,
    USU.USU_RUT Rut,
    USU.USU_DIGITO_VERIFICADO DigitoV,
    USU.USU_CORREO_ELECTRONICO Correo,
    USU.USU_PASSWORD Password,
    USU.USU_NOMBRE Nombre,
    USU.USU_APATERNO APaterno,
    USU.USU_AMANTERNO AMaterno
    FROM Apoderado APO
    JOIN Usuario USU ON USU.USU_RUT = APO.USU_RUT
    WHERE USU.USU_ELIMINADO > 0 and APO.APO_ELIMINADO > 0;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_COLEGIO_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_COLEGIO_CREATE" 
(Nombre VARCHAR2) as 
BEGIN
  INSERT INTO COLEGIO (COL_NOMBRE,COL_ELIMINADO) values (Nombre,1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_COLEGIO_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_COLEGIO_DELETE" 
(Id IN NUMBER) IS  
BEGIN  
 UPDATE COLEGIO 
 SET COL_ELIMINADO = 0
 WHERE COL_ID= Id;  
COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_COLEGIO_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_COLEGIO_POR_ID" 
(Id IN NUMBER, c1 out sys_refcursor) as
begin
  open c1 for
    select
    COL_ID Id,
    COL_NOMBRE Nombre
    from COLEGIO
    where COL_ELIMINADO > 0 and COL_ID = Id;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_COLEGIO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_COLEGIO_TODOS" 
(c1 out sys_refcursor) as
begin
  open c1 for
    select
    COL_ID Id,
    COL_NOMBRE Nombre
    from COLEGIO
    where COL_ELIMINADO > 0;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_COLEGIO_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_COLEGIO_UPDATE" 
(Id IN NUMBER, Nombre VARCHAR2) IS  
BEGIN  
 UPDATE COLEGIO 
 SET COL_NOMBRE = Nombre
 WHERE COL_ID= Id;  
COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CONTRATO_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_CONTRATO_CREATE" 
(Curso INTEGER, Nombre VARCHAR2, Descripcion VARCHAR2, Fecha_Viaje DATE, Total INTEGER) as 
BEGIN
  INSERT INTO CONTRATO (CUR_ID,CON_NOMBRE,CON_DESCRIPCION,CON_FECHA_VIAJE, CON_TOTAL_VIAJE, CON_ELIMINADO) 
  VALUES (Curso, Nombre, Descripcion,Fecha_Viaje, Total,1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CONTRATO_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_CONTRATO_DELETE" 
(Id INTEGER) as 
BEGIN
  UPDATE CONTRATO
  SET CON_ELIMINADO = 0
  WHERE CON_ID = Id;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CONTRATO_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_CONTRATO_POR_ID" 
(Id INTEGER, c1 out sys_refcursor) as 
BEGIN
  open c1 for
  SELECT 
  CON.CON_ID Id,
  CON.CUR_ID CursoID,
  CON.CON_NOMBRE Nombre,
  CON.CON_DESCRIPCION Descripcion,
  CON.CON_FECHA_VIAJE Fecha_Viaje,
  CON.CON_TOTAL_VIAJE Total
  FROM 
  CONTRATO CON
  WHERE CON_ID = Id;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CONTRATO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_CONTRATO_TODOS" 
(c1 out sys_refcursor) as 
BEGIN
  open c1 for
  SELECT 
  CON.CON_ID Id,
  CON.CUR_ID CursoID,
  CON.CON_NOMBRE Nombre,
  CON.CON_DESCRIPCION Descripcion,
  CON.CON_FECHA_VIAJE Fecha_Viaje,
  CON.CON_TOTAL_VIAJE Total
  FROM 
  CONTRATO CON
  WHERE CON.CON_ELIMINADO > 0;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CONTRATO_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_CONTRATO_UPDATE" 
(Id INTEGER, Curso INTEGER, Nombre VARCHAR2, Descripcion VARCHAR2, Fecha_Viaje DATE, Valor INTEGER) as 
BEGIN

  UPDATE CONTRATO
  SET CUR_ID = Curso,
  CON_NOMBRE = Nombre,
  CON_DESCRIPCION = Descripcion,
  CON_FECHA_VIAJE = Fecha_Viaje,
  CON_TOTAL_VIAJE = Valor 
  WHERE CON_ID = Id;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CURSO_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_CURSO_CREATE" 
(Colegio_Id IN NUMBER, Nombre VARCHAR2, TotalReunido IN NUMBER) as 
begin
  INSERT INTO CURSO (COL_ID,CUR_NOMBRE,CUR_TOTAL_REUNIDO, CUR_ELIMINADO)
  VALUES (Colegio_Id,Nombre,TotalReunido,1);
  COMMIT ;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_CURSO_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_CURSO_DELETE" 
(Id IN NUMBER) AS 
BEGIN
 UPDATE CURSO SET CUR_ELIMINADO = 0 WHERE CUR_ID= Id;  
 COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_CURSO_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_CURSO_POR_ID" 
(Id IN NUMBER,c1 out sys_refcursor) AS
BEGIN
  OPEN c1 FOR
    SELECT CU.CUR_ID Id,
    CU.CUR_NOMBRE Nombre,
    CU.CUR_TOTAL_REUNIDO TotalReunido,
    CO.COL_ID Id,
    CO.COL_NOMBRE Nombre
    FROM CURSO CU
    JOIN COLEGIO CO ON CU.COL_ID = CO.COL_ID
    WHERE CU.CUR_ELIMINADO > 0 and CU.CUR_ID = Id;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_CURSO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_CURSO_TODOS" 
(c1 out sys_refcursor) AS
BEGIN
  OPEN c1 FOR
    SELECT CU.CUR_ID Id,
    CU.CUR_NOMBRE Nombre,
    CU.CUR_TOTAL_REUNIDO TotalReunido,
    CO.COL_ID Id,
    CO.COL_NOMBRE Nombre
    FROM CURSO CU
    JOIN COLEGIO CO ON CU.COL_ID = CO.COL_ID
    WHERE CU.CUR_ELIMINADO > 0;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_CURSO_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_CURSO_UPDATE" 
(Id number, Colegio_Id IN NUMBER, Nombre varchar2 ,TotalReunido IN NUMBER) IS  
BEGIN  
 UPDATE CURSO 
 SET CUR_NOMBRE = Nombre,
 CUR_TOTAL_REUNIDO = TotalReunido,
 COL_ID = Colegio_Id
 WHERE CUR_ID=Id;  
COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_DESTINO_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_DESTINO_CREATE" 
(Nombre VARCHAR2, Valor INTEGER) as 
BEGIN
  INSERT INTO destino (DES_NOMBRE,DES_VALOR, DES_ELIMINADO) values (Nombre,Valor,1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_DESTINO_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_DESTINO_DELETE" 
(Id IN NUMBER) IS  
BEGIN  
 UPDATE DESTINO 
 SET DES_ELIMINADO = 0
 WHERE DES_ID= Id;  
COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_DESTINO_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_DESTINO_POR_ID" 
(Id IN NUMBER, c1 out sys_refcursor) as
begin
  open c1 for
    SELECT DESTINO.DES_ID Id, 
    DESTINO.DES_NOMBRE Nombre,
    DESTINO.DES_VALOR Valor
    FROM DESTINO
    WHERE DES_ELIMINADO > 0 AND DES_ID = Id;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_DESTINO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_DESTINO_TODOS" 
(c1 out sys_refcursor) as
begin
  open c1 for
    SELECT DESTINO.DES_ID Id, 
    DESTINO.DES_NOMBRE Nombre,
    DESTINO.DES_VALOR Valor
    FROM DESTINO
    WHERE DES_ELIMINADO > 0;
end;

/
--------------------------------------------------------
--  DDL for Procedure SP_DESTINO_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_DESTINO_UPDATE" 
(Id IN NUMBER, Nombre VARCHAR2, Valor INTEGER) IS  
BEGIN  
 UPDATE DESTINO 
 SET DES_NOMBRE = Nombre, 
 DES_VALOR = Valor 
 WHERE DES_ID= Id;  
COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_EXISTE_ALUMNO_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_EXISTE_ALUMNO_POR_RUT" 
(Rut IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT COUNT(*) FROM ALUMNO 
    WHERE ALU_RUT = Rut;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_EXISTE_APODERADO_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_EXISTE_APODERADO_POR_RUT" 
(Rut IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT COUNT(*) FROM APODERADO 
    WHERE USU_RUT = Rut;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_EXISTE_USUARIO
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_EXISTE_USUARIO" 
(Rut IN NUMBER, Password CHAR, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT COUNT(*) FROM USUARIO 
    WHERE USU_ELIMINADO > 0 AND USU_RUT = Rut AND USU_PASSWORD = Password;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_PERFILES_ASIGNADOS_BORRAR_TODOS_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_PERFILES_ASIGNADOS_BORRAR_TODOS_RUT" 
(Rut IN NUMBER ) AS 
BEGIN

  DELETE FROM PERFIL_ASIGNADO WHERE USU_RUT = Rut;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_PERFILES_ASIGNADOS_CREAR
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_PERFILES_ASIGNADOS_CREAR" 
(Perfil IN NUMBER, Rut IN NUMBER ) AS 
BEGIN

  INSERT INTO PERFIL_ASIGNADO 
  (PER_ID,USU_RUT) 
  VALUES (Perfil, Rut);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_PERFILES_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_PERFILES_POR_RUT" 
(Rut IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT 
    PERFIL_USUARIO.PER_ID Id,
    PERFIL_USUARIO.PER_TIPO Tipo
    FROM PERFIL_ASIGNADO 
    JOIN PERFIL_USUARIO ON PERFIL_ASIGNADO.PER_ID = PERFIL_USUARIO.PER_ID
    WHERE PERFIL_ASIGNADO.USU_RUT = Rut;

END;

/
--------------------------------------------------------
--  DDL for Procedure SP_PERFILES_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_PERFILES_TODOS" 
(c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT
    PERFIL_USUARIO.PER_ID Id,
    PERFIL_USUARIO.PER_TIPO Tipo
    FROM PERFIL_USUARIO;
   
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGUROS_ASIGNADOS_CREAR
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGUROS_ASIGNADOS_CREAR" 
(ContratoID IN NUMBER, SeguroID IN NUMBER, Valor IN NUMBER, Tipo_Seguro IN NUMBER) AS 
BEGIN

  INSERT INTO SEGURO_ASIGNADO 
  (CON_ID,SEG_ID, ASIGSEG_VALOR, ASIGSEG_ID_TIPO_SEGURO, ASIGSEG_ELIMINADO) 
  VALUES (ContratoID, SeguroID, Valor, Tipo_Seguro, 1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGUROS_ASIGNADOS_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGUROS_ASIGNADOS_DELETE" 
(Id IN NUMBER) AS 
BEGIN
 UPDATE SEGURO_ASIGNADO 
 SET ASIGSEG_ELIMINADO = 0 
 WHERE ASIGSEG_ID = Id;  
 COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGUROS_ASIGNADOS_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGUROS_ASIGNADOS_POR_ID" 
(Id IN NUMBER,C1 OUT SYS_REFCURSOR) AS 
BEGIN

  OPEN C1 FOR
    SELECT
    ASIGSEG.ASIGSEG_ID Id,
    ASIGSEG.CON_ID ContratoID,
    ASIGSEG.SEG_ID SeguroID,
    ASIGSEG.ASIGSEG_VALOR Valor,
    ASIGSEG.ASIGSEG_ID_TIPO_SEGURO Tipo_Seguro
    FROM SEGURO_ASIGNADO ASIGSEG
    WHERE ASIGSEG.ASIGSEG_ELIMINADO > 0 and ASIGSEG.ASIGSEG_ID = Id;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGUROS_ASIGNADOS_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGUROS_ASIGNADOS_TODOS" 
(C1 OUT SYS_REFCURSOR) AS 
BEGIN

  OPEN C1 FOR
    SELECT
    ASIGSEG.ASIGSEG_ID Id,
    ASIGSEG.CON_ID ContratoID,
    ASIGSEG.SEG_ID SeguroID,
    ASIGSEG.ASIGSEG_VALOR Valor,
    ASIGSEG.ASIGSEG_ID_TIPO_SEGURO Tipo_Seguro
    FROM SEGURO_ASIGNADO ASIGSEG
    WHERE ASIGSEG.ASIGSEG_ELIMINADO > 0;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGUROS_ASIGNADOS_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGUROS_ASIGNADOS_UPDATE" 
(Id IN NUMBER, ContratoID IN NUMBER, SeguroID IN NUMBER, Valor IN NUMBER, Tipo_Seguro IN NUMBER) AS 
BEGIN
   UPDATE SEGURO_ASIGNADO 
   SET CON_ID = ContratoID,
   SEG_ID = SeguroID,
   ASIGSEG_VALOR = Valor,
   ASIGSEG_ID_TIPO_SEGURO = Tipo_Seguro
   WHERE ASIGSEG_ID = Id;  
   COMMIT;  
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGURO_POR_ID
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGURO_POR_ID" 
(Id IN NUMBER,c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT  SEG.SEG_ID Id,
    SEG.SEG_NOMBRE Nombre,
    SEG.SEG_DESCRIPCION Descripcion,
    SEG.SEG_DIAS_COBERTURA Dias_Cobertura
    FROM SEGURO SEG
    WHERE SEG.SEG_ELIMINADO > 0 AND SEG.SEG_ID = Id;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_SEGURO_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE NONEDITIONABLE PROCEDURE "SP_SEGURO_TODOS" 
(c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT  SEG.SEG_ID Id,
    SEG.SEG_NOMBRE Nombre,
    SEG.SEG_DESCRIPCION Descripcion,
    SEG.SEG_DIAS_COBERTURA Dias_Cobertura
    FROM SEGURO SEG
    WHERE SEG.SEG_ELIMINADO > 0;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_USUARIOS_CREATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_USUARIOS_CREATE" 
(Rut IN NUMBER, DigitoV CHAR, Nombre CHAR, APaterno CHAR, AMaterno CHAR, Correo CHAR, Password CHAR) AS 
BEGIN

  INSERT INTO Usuario 
  (USU_RUT,USU_DIGITO_VERIFICADO, USU_NOMBRE, USU_APATERNO, USU_AMANTERNO, USU_CORREO_ELECTRONICO, USU_PASSWORD, USU_ELIMINADO) 
  VALUES (Rut, DigitoV, Nombre, APaterno, AMaterno, Correo, Password, 1);
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_USUARIOS_DELETE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_USUARIOS_DELETE" 
(Rut IN NUMBER) AS 
BEGIN
  
  UPDATE USUARIO
  SET 
  USU_ELIMINADO = 0
  WHERE USU_RUT = Rut;
  COMMIT;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_USUARIOS_POR_RUT
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_USUARIOS_POR_RUT" 
(Rut IN NUMBER, c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT Usuario.USU_RUT Rut,
    Usuario.USU_DIGITO_VERIFICADO DigitoV,
    Usuario.USU_CORREO_ELECTRONICO Correo,
    Usuario.USU_PASSWORD Password,
    Usuario.USU_NOMBRE Nombre,
    Usuario.USU_APATERNO APaterno,
    Usuario.USU_AMANTERNO AMaterno
    FROM Usuario
    WHERE  USU_ELIMINADO > 0 AND  Usuario.USU_RUT = Rut;
   
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_USUARIOS_TODOS
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_USUARIOS_TODOS" 
(c1 OUT SYS_REFCURSOR) AS 
BEGIN

  open c1 for
    SELECT Usuario.USU_RUT Rut,
    Usuario.USU_DIGITO_VERIFICADO DigitoV,
    Usuario.USU_CORREO_ELECTRONICO Correo,
    Usuario.USU_PASSWORD Password,
    Usuario.USU_NOMBRE Nombre,
    Usuario.USU_APATERNO APaterno,
    Usuario.USU_AMANTERNO AMaterno
    FROM Usuario
    WHERE USU_ELIMINADO > 0;
END;

/
--------------------------------------------------------
--  DDL for Procedure SP_USUARIOS_UPDATE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE EDITIONABLE PROCEDURE "SP_USUARIOS_UPDATE" 
(Rut IN NUMBER, DigitoV CHAR, Nombre CHAR, APaterno CHAR, AMaterno CHAR, Correo CHAR, Password CHAR) AS 
BEGIN
  
  UPDATE USUARIO
  SET USU_NOMBRE =  Nombre,
  USU_APATERNO = APaterno,
  USU_AMANTERNO = AMaterno,
  USU_CORREO_ELECTRONICO = Correo,
  USU_PASSWORD = Password
  WHERE USU_RUT = Rut;
  COMMIT;
END;

/
--------------------------------------------------------
--  Constraints for Table CUENTA_CORRIENTE
--------------------------------------------------------

  ALTER TABLE "CUENTA_CORRIENTE" MODIFY ("CUE_ID" NOT NULL ENABLE);
  ALTER TABLE "CUENTA_CORRIENTE" MODIFY ("ALU_RUT" NOT NULL ENABLE);
  ALTER TABLE "CUENTA_CORRIENTE" MODIFY ("CUE_TOTAL_REUNIDO" NOT NULL ENABLE);
  ALTER TABLE "CUENTA_CORRIENTE" MODIFY ("CUE_MONTO_A_PAGAR" NOT NULL ENABLE);
  ALTER TABLE "CUENTA_CORRIENTE" MODIFY ("CUE_TOTAL_PAGADO" NOT NULL ENABLE);
  ALTER TABLE "CUENTA_CORRIENTE" MODIFY ("CUE_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "CUENTA_CORRIENTE" ADD CONSTRAINT "PK_CUENTA_CORRIENTE" PRIMARY KEY ("CUE_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table DEPOSITO
--------------------------------------------------------

  ALTER TABLE "DEPOSITO" MODIFY ("DEP_ID" NOT NULL ENABLE);
  ALTER TABLE "DEPOSITO" MODIFY ("DEP_FECHA_DEPOSITO" NOT NULL ENABLE);
  ALTER TABLE "DEPOSITO" MODIFY ("DEP_VALOR_DEPOSITO" NOT NULL ENABLE);
  ALTER TABLE "DEPOSITO" MODIFY ("DEP_DESCRIPCION" NOT NULL ENABLE);
  ALTER TABLE "DEPOSITO" ADD CONSTRAINT "PK_DEPOSITO" PRIMARY KEY ("DEP_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table DESTINO_ASIGNADO
--------------------------------------------------------

  ALTER TABLE "DESTINO_ASIGNADO" MODIFY ("ASIGDES_ID" NOT NULL ENABLE);
  ALTER TABLE "DESTINO_ASIGNADO" MODIFY ("CON_ID" NOT NULL ENABLE);
  ALTER TABLE "DESTINO_ASIGNADO" MODIFY ("DES_ID" NOT NULL ENABLE);
  ALTER TABLE "DESTINO_ASIGNADO" ADD CONSTRAINT "PK_DESTINO_ASIGNADO" PRIMARY KEY ("CON_ID", "DES_ID", "ASIGDES_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table PAGO
--------------------------------------------------------

  ALTER TABLE "PAGO" MODIFY ("PAG_ID" NOT NULL ENABLE);
  ALTER TABLE "PAGO" MODIFY ("PAG_VALOR_PAGO" NOT NULL ENABLE);
  ALTER TABLE "PAGO" MODIFY ("PAG_MONTO_A_PAGAR" NOT NULL ENABLE);
  ALTER TABLE "PAGO" MODIFY ("PAG_TOTAL_PAGADO" NOT NULL ENABLE);
  ALTER TABLE "PAGO" ADD CONSTRAINT "PK_PAGO" PRIMARY KEY ("PAG_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table CONTRATO
--------------------------------------------------------

  ALTER TABLE "CONTRATO" MODIFY ("CON_ID" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" MODIFY ("CUR_ID" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" MODIFY ("CON_DESCRIPCION" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" MODIFY ("CON_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" MODIFY ("CON_FECHA_VIAJE" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" MODIFY ("CON_TOTAL_VIAJE" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" MODIFY ("CON_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "CONTRATO" ADD CONSTRAINT "PK_CONTRATO" PRIMARY KEY ("CON_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table DESTINO
--------------------------------------------------------

  ALTER TABLE "DESTINO" MODIFY ("DES_ID" NOT NULL ENABLE);
  ALTER TABLE "DESTINO" MODIFY ("DES_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "DESTINO" MODIFY ("DES_VALOR" NOT NULL ENABLE);
  ALTER TABLE "DESTINO" MODIFY ("DES_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "DESTINO" ADD CONSTRAINT "PK_DESTINO" PRIMARY KEY ("DES_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table PERFIL_USUARIO
--------------------------------------------------------

  ALTER TABLE "PERFIL_USUARIO" MODIFY ("PER_ID" NOT NULL ENABLE);
  ALTER TABLE "PERFIL_USUARIO" MODIFY ("PER_TIPO" NOT NULL ENABLE);
  ALTER TABLE "PERFIL_USUARIO" ADD CONSTRAINT "PK_PERFIL_USUARIO" PRIMARY KEY ("PER_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table SEGURO
--------------------------------------------------------

  ALTER TABLE "SEGURO" MODIFY ("SEG_ID" NOT NULL ENABLE);
  ALTER TABLE "SEGURO" MODIFY ("SEG_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "SEGURO" MODIFY ("SEG_DESCRIPCION" NOT NULL ENABLE);
  ALTER TABLE "SEGURO" MODIFY ("SEG_DIAS_COBERTURA" NOT NULL ENABLE);
  ALTER TABLE "SEGURO" MODIFY ("SEG_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "SEGURO" ADD CONSTRAINT "PK_SEGURO" PRIMARY KEY ("SEG_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table COLEGIO
--------------------------------------------------------

  ALTER TABLE "COLEGIO" MODIFY ("COL_ID" NOT NULL ENABLE);
  ALTER TABLE "COLEGIO" ADD CONSTRAINT "PK_COLEGIO" PRIMARY KEY ("COL_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table CURSO
--------------------------------------------------------

  ALTER TABLE "CURSO" MODIFY ("CUR_ID" NOT NULL ENABLE);
  ALTER TABLE "CURSO" ADD CONSTRAINT "PK_CURSO" PRIMARY KEY ("CUR_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table USUARIO
--------------------------------------------------------

  ALTER TABLE "USUARIO" MODIFY ("USU_RUT" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_DIGITO_VERIFICADO" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_CORREO_ELECTRONICO" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_PASSWORD" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_APATERNO" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_AMANTERNO" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" MODIFY ("USU_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "USUARIO" ADD CONSTRAINT "PK_USUARIO" PRIMARY KEY ("USU_RUT")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table SEGURO_ASIGNADO
--------------------------------------------------------

  ALTER TABLE "SEGURO_ASIGNADO" MODIFY ("ASIGSEG_ID" NOT NULL ENABLE);
  ALTER TABLE "SEGURO_ASIGNADO" MODIFY ("CON_ID" NOT NULL ENABLE);
  ALTER TABLE "SEGURO_ASIGNADO" MODIFY ("SEG_ID" NOT NULL ENABLE);
  ALTER TABLE "SEGURO_ASIGNADO" MODIFY ("ASIGSEG_VALOR" NOT NULL ENABLE);
  ALTER TABLE "SEGURO_ASIGNADO" MODIFY ("ASIGSEG_ID_TIPO_SEGURO" NOT NULL ENABLE);
  ALTER TABLE "SEGURO_ASIGNADO" MODIFY ("ASIGSEG_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "SEGURO_ASIGNADO" ADD CONSTRAINT "PK_SEGURO_ASIGNADO" PRIMARY KEY ("CON_ID", "SEG_ID", "ASIGSEG_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table ACTIVIDAD_ASIGNADA
--------------------------------------------------------

  ALTER TABLE "ACTIVIDAD_ASIGNADA" MODIFY ("ASIGACT_ID" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD_ASIGNADA" MODIFY ("ACT_ID" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD_ASIGNADA" MODIFY ("CUR_ID" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD_ASIGNADA" MODIFY ("ASIGACT_TOTAL_RECAUDADO" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD_ASIGNADA" MODIFY ("ASIGACT_PRORRATEO" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD_ASIGNADA" MODIFY ("ASIGACT_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD_ASIGNADA" ADD CONSTRAINT "PK_ACTIVIDAD_ASIGNADA" PRIMARY KEY ("ACT_ID", "CUR_ID", "ASIGACT_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table TIPO_DEPOSITO
--------------------------------------------------------

  ALTER TABLE "TIPO_DEPOSITO" MODIFY ("TIPODEP_ID" NOT NULL ENABLE);
  ALTER TABLE "TIPO_DEPOSITO" MODIFY ("TIPODEP_DESCRIPCION" NOT NULL ENABLE);
  ALTER TABLE "TIPO_DEPOSITO" ADD CONSTRAINT "PK_TIPO_DEPOSITO" PRIMARY KEY ("TIPODEP_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table ALUMNO
--------------------------------------------------------

  ALTER TABLE "ALUMNO" MODIFY ("ALU_RUT" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("ALU_DIGITO_VERIFICADO" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("APO_ID" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("CUR_ID" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("ALU_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("ALU_APATERNO" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("ALU_AMATERNO" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" MODIFY ("ALU_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "ALUMNO" ADD CONSTRAINT "PK_ALUMNO" PRIMARY KEY ("ALU_RUT")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table ACTIVIDAD
--------------------------------------------------------

  ALTER TABLE "ACTIVIDAD" MODIFY ("ACT_ID" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD" MODIFY ("ACT_NOMBRE" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD" MODIFY ("ACT_DESCRIPCION" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD" MODIFY ("ACT_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "ACTIVIDAD" ADD CONSTRAINT "PK_ACTIVIDAD" PRIMARY KEY ("ACT_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table PERFIL_ASIGNADO
--------------------------------------------------------

  ALTER TABLE "PERFIL_ASIGNADO" MODIFY ("ASIGPER_ID" NOT NULL ENABLE);
  ALTER TABLE "PERFIL_ASIGNADO" MODIFY ("PER_ID" NOT NULL ENABLE);
  ALTER TABLE "PERFIL_ASIGNADO" MODIFY ("USU_RUT" NOT NULL ENABLE);
  ALTER TABLE "PERFIL_ASIGNADO" ADD CONSTRAINT "PK_PERFIL_ASIGNADO" PRIMARY KEY ("PER_ID", "USU_RUT", "ASIGPER_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Constraints for Table APODERADO
--------------------------------------------------------

  ALTER TABLE "APODERADO" MODIFY ("APO_ID" NOT NULL ENABLE);
  ALTER TABLE "APODERADO" MODIFY ("APO_ELIMINADO" NOT NULL ENABLE);
  ALTER TABLE "APODERADO" ADD CONSTRAINT "PK_APODERADO" PRIMARY KEY ("APO_ID")
  USING INDEX  ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table ACTIVIDAD_ASIGNADA
--------------------------------------------------------

  ALTER TABLE "ACTIVIDAD_ASIGNADA" ADD CONSTRAINT "FK_ASIACT_ACT" FOREIGN KEY ("ACT_ID")
	  REFERENCES "ACTIVIDAD" ("ACT_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table ALUMNO
--------------------------------------------------------

  ALTER TABLE "ALUMNO" ADD CONSTRAINT "FK_ALU_APO" FOREIGN KEY ("APO_ID")
	  REFERENCES "APODERADO" ("APO_ID") ENABLE;
  ALTER TABLE "ALUMNO" ADD CONSTRAINT "FK_ALU_CUR" FOREIGN KEY ("CUR_ID")
	  REFERENCES "CURSO" ("CUR_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table APODERADO
--------------------------------------------------------

  ALTER TABLE "APODERADO" ADD CONSTRAINT "FK_APO_USU" FOREIGN KEY ("USU_RUT")
	  REFERENCES "USUARIO" ("USU_RUT") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table CUENTA_CORRIENTE
--------------------------------------------------------

  ALTER TABLE "CUENTA_CORRIENTE" ADD CONSTRAINT "FK_CUE_ALU" FOREIGN KEY ("ALU_RUT")
	  REFERENCES "ALUMNO" ("ALU_RUT") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table CURSO
--------------------------------------------------------

  ALTER TABLE "CURSO" ADD CONSTRAINT "FK_CUR_COL" FOREIGN KEY ("COL_ID")
	  REFERENCES "COLEGIO" ("COL_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DEPOSITO
--------------------------------------------------------

  ALTER TABLE "DEPOSITO" ADD CONSTRAINT "FK_DEP_TIPODEP" FOREIGN KEY ("DEP_TIPODEP_ID")
	  REFERENCES "TIPO_DEPOSITO" ("TIPODEP_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table DESTINO_ASIGNADO
--------------------------------------------------------

  ALTER TABLE "DESTINO_ASIGNADO" ADD CONSTRAINT "FK_ASIGDES_DES" FOREIGN KEY ("DES_ID")
	  REFERENCES "DESTINO" ("DES_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PAGO
--------------------------------------------------------

  ALTER TABLE "PAGO" ADD CONSTRAINT "FK_PAG_CUE" FOREIGN KEY ("CUE_ID")
	  REFERENCES "CUENTA_CORRIENTE" ("CUE_ID") ENABLE;
  ALTER TABLE "PAGO" ADD CONSTRAINT "FK_PAG_DEP" FOREIGN KEY ("DEP_ID")
	  REFERENCES "DEPOSITO" ("DEP_ID") ENABLE;
--------------------------------------------------------
--  Ref Constraints for Table PERFIL_ASIGNADO
--------------------------------------------------------

  ALTER TABLE "PERFIL_ASIGNADO" ADD CONSTRAINT "FK_ASIGPER_PER" FOREIGN KEY ("PER_ID")
	  REFERENCES "PERFIL_USUARIO" ("PER_ID") ENABLE;
  ALTER TABLE "PERFIL_ASIGNADO" ADD CONSTRAINT "FK_ASIPER_USU" FOREIGN KEY ("USU_RUT")
	  REFERENCES "USUARIO" ("USU_RUT") ENABLE;
