--------------------------------------------------------
--  DDL for Table CONTRATO
--------------------------------------------------------

  CREATE TABLE "AGENCIA_USER"."ARCHIVO" 
   (	"ARC_ID" NUMBER(*,0) GENERATED ALWAYS AS IDENTITY MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 START WITH 1 CACHE 20 NOORDER  NOCYCLE  NOKEEP  NOSCALE , 
	"CON_ID" NUMBER(*,0), 
	"ARC_ARCHIVO" CHAR(256 BYTE)
  )
--------------------------------------------------------
--  DDL for Index PK_CONTRATO
--------------------------------------------------------
--------------------------------------------------------
--  Constraints for Table CONTRATO
--------------------------------------------------------

  ALTER TABLE "AGENCIA_USER"."CONTRATO" MODIFY ("ARC_ID" NOT NULL ENABLE);
  ALTER TABLE "AGENCIA_USER"."CONTRATO" MODIFY ("CON_ID" NOT NULL ENABLE);
  ALTER TABLE "AGENCIA_USER"."CONTRATO" MODIFY ("ARC_ARCHIVO" NOT NULL ENABLE);
  ALTER TABLE "AGENCIA_USER"."CONTRATO" ADD CONSTRAINT "PK_ARCHIVO" PRIMARY KEY ("ARC_ID");
