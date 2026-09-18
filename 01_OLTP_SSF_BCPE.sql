/* ============================================================================
   PORTAL BI - REPORTES SSF (BCPE)
   Script: 01_OLTP_SSF_BCPE.sql
   Motor:  SQL Server
   Descripcion: Modelo transaccional (OLTP) generado a partir de los XSD:
                ssf_bcpe_afiliado, ssf_bcpe_empleador, ssf_bcpe_cotizante,
                ssf_bcpe_mora_empleador, ssf_bcpe_prestacion,
                ssf_bcpe_pago_beneficio, ssf_bcpe_reintegro_anticipo
   Notas de diseno:
     - Toda tabla transaccional/maestra tiene FK a ctl.carga_archivo para
       trazabilidad total (que archivo origino cada registro).
     - Los catalogos (cat.*) se siembran con los codigos que YA vienen
       enumerados en los XSD. La columna descripcion se deja NULL para
       actualizarla cuando se compartan los catalogos oficiales de la SSF.
     - Campos sin enumeration en el XSD (tipo_beneficio, requisito_legal,
       codigo_pais, estado_afiliado, codigo_centro_trabajo, etc.) se dejan
       como columna simple (sin FK) hasta contar con el catalogo oficial.
   ============================================================================ */

SET NOCOUNT ON;
GO

/* ----------------------------------------------------------------------------
   CREACION DE BASE DE DATOS - LOCALDB (mismo patron del proyecto Mundialito FC)
   Servidor destino: (localdb)\MSSQLLocalDB
   ---------------------------------------------------------------------------- */
IF DB_ID('SSF_BCPE_BI') IS NULL
BEGIN
    CREATE DATABASE SSF_BCPE_BI;
END
GO

USE SSF_BCPE_BI;
GO

/* ============================================================================
   ESQUEMAS
   ============================================================================ */
IF SCHEMA_ID('seg') IS NULL EXEC('CREATE SCHEMA seg');
IF SCHEMA_ID('cat') IS NULL EXEC('CREATE SCHEMA cat');
IF SCHEMA_ID('ctl') IS NULL EXEC('CREATE SCHEMA ctl');
IF SCHEMA_ID('bcpe') IS NULL EXEC('CREATE SCHEMA bcpe');
GO

/* ============================================================================
   ESQUEMA seg  -  SEGURIDAD / LOGIN
   ============================================================================ */
CREATE TABLE seg.rol
(
    id_rol          INT IDENTITY(1,1)  CONSTRAINT PK_seg_rol PRIMARY KEY,
    nombre_rol      VARCHAR(50)        NOT NULL,
    descripcion     VARCHAR(200)       NULL,
    activo          BIT                NOT NULL CONSTRAINT DF_seg_rol_activo DEFAULT (1),
    CONSTRAINT UQ_seg_rol_nombre UNIQUE (nombre_rol)
);
GO

CREATE TABLE seg.usuario
(
    id_usuario          INT IDENTITY(1,1) CONSTRAINT PK_seg_usuario PRIMARY KEY,
    nombre_usuario      VARCHAR(50)       NOT NULL,
    correo              VARCHAR(150)      NOT NULL,
    nombre_completo     VARCHAR(150)      NOT NULL,
    clave_hash          VARBINARY(256)    NOT NULL,
    clave_salt          VARBINARY(128)    NOT NULL,
    activo              BIT               NOT NULL CONSTRAINT DF_seg_usuario_activo DEFAULT (1),
    fecha_creacion      DATETIME          NOT NULL CONSTRAINT DF_seg_usuario_fecha_creacion DEFAULT (GETDATE()),
    fecha_ultimo_acceso DATETIME          NULL,
    intentos_fallidos   INT               NOT NULL CONSTRAINT DF_seg_usuario_intentos DEFAULT (0),
    bloqueado           BIT               NOT NULL CONSTRAINT DF_seg_usuario_bloqueado DEFAULT (0),
    CONSTRAINT UQ_seg_usuario_nombre_usuario UNIQUE (nombre_usuario),
    CONSTRAINT UQ_seg_usuario_correo UNIQUE (correo)
);
GO

CREATE TABLE seg.usuario_rol
(
    id_usuario  INT NOT NULL,
    id_rol      INT NOT NULL,
    CONSTRAINT PK_seg_usuario_rol PRIMARY KEY (id_usuario, id_rol),
    CONSTRAINT FK_usuario_rol_usuario FOREIGN KEY (id_usuario) REFERENCES seg.usuario (id_usuario),
    CONSTRAINT FK_usuario_rol_rol     FOREIGN KEY (id_rol)     REFERENCES seg.rol (id_rol)
);
GO

/* ============================================================================
   ESQUEMA ctl  -  CONTROL DE CARGA DE ARCHIVOS XML
   ============================================================================ */
CREATE TABLE ctl.carga_archivo
(
    id_carga_archivo     INT IDENTITY(1,1) CONSTRAINT PK_ctl_carga_archivo PRIMARY KEY,
    nombre_archivo       VARCHAR(255)      NOT NULL,
    tipo_entidad         VARCHAR(50)       NOT NULL,   -- afiliado | empleador | cotizante | mora_empleador | prestacion | pago_beneficio | reintegro_anticipo
    periodo_reporte      CHAR(6)           NULL,       -- YYYYMM, se infiere del nombre de archivo o lo indica el usuario al cargar
    ruta_archivo         VARCHAR(500)      NULL,
    hash_archivo         CHAR(64)          NULL,       -- SHA-256 para evitar cargas duplicadas del mismo archivo
    id_usuario_carga     INT               NOT NULL,
    fecha_carga          DATETIME          NOT NULL CONSTRAINT DF_ctl_carga_fecha DEFAULT (GETDATE()),
    total_registros      INT               NULL,
    registros_correctos  INT               NULL,
    registros_error      INT               NULL,
    estado               VARCHAR(25)       NOT NULL CONSTRAINT DF_ctl_carga_estado DEFAULT ('PENDIENTE'),
    fecha_fin_proceso    DATETIME          NULL,
    CONSTRAINT FK_carga_archivo_usuario FOREIGN KEY (id_usuario_carga) REFERENCES seg.usuario (id_usuario),
    CONSTRAINT CK_ctl_carga_estado CHECK (estado IN ('PENDIENTE','PROCESANDO','COMPLETADO','COMPLETADO_CON_ERRORES','ERROR')),
    CONSTRAINT CK_ctl_carga_tipo_entidad CHECK (tipo_entidad IN
        ('afiliado','empleador','cotizante','mora_empleador','prestacion','pago_beneficio','reintegro_anticipo'))
);
GO

CREATE TABLE ctl.carga_archivo_detalle
(
    id_detalle        BIGINT IDENTITY(1,1) CONSTRAINT PK_ctl_carga_detalle PRIMARY KEY,
    id_carga_archivo  INT           NOT NULL,
    numero_linea      INT           NOT NULL,
    estado            VARCHAR(10)   NOT NULL,   -- OK | ERROR
    mensaje_error     VARCHAR(1000) NULL,
    CONSTRAINT FK_carga_detalle_carga FOREIGN KEY (id_carga_archivo) REFERENCES ctl.carga_archivo (id_carga_archivo),
    CONSTRAINT CK_ctl_carga_detalle_estado CHECK (estado IN ('OK','ERROR'))
);
GO

/* ============================================================================
   ESQUEMA cat  -  CATALOGOS (sembrados con codigos de los XSD)
   ============================================================================ */
CREATE TABLE cat.tipo_documento
(   id_tipo_documento INT         CONSTRAINT PK_cat_tipo_documento PRIMARY KEY,
    descripcion       VARCHAR(100) NULL );
INSERT INTO cat.tipo_documento (id_tipo_documento) VALUES (1),(2),(3),(4);
GO

CREATE TABLE cat.genero
(   codigo      CHAR(1)      CONSTRAINT PK_cat_genero PRIMARY KEY,
    descripcion VARCHAR(20)  NULL );
INSERT INTO cat.genero (codigo) VALUES ('F'),('M');
GO

CREATE TABLE cat.estado_familiar
(   codigo      CHAR(1)      CONSTRAINT PK_cat_estado_familiar PRIMARY KEY,
    descripcion VARCHAR(30)  NULL );
INSERT INTO cat.estado_familiar (codigo) VALUES ('S'),('C'),('V'),('U'),('D');
GO

CREATE TABLE cat.tipo_sistema
(   id_tipo_sistema INT        CONSTRAINT PK_cat_tipo_sistema PRIMARY KEY,
    descripcion     VARCHAR(100) NULL );
INSERT INTO cat.tipo_sistema (id_tipo_sistema) VALUES (1),(2),(3);
GO

CREATE TABLE cat.tipo_persona
(   id_tipo_persona INT        CONSTRAINT PK_cat_tipo_persona PRIMARY KEY,
    descripcion     VARCHAR(100) NULL );
INSERT INTO cat.tipo_persona (id_tipo_persona) VALUES (1),(2);
GO

CREATE TABLE cat.tipo_empleador
(   id_tipo_empleador INT        CONSTRAINT PK_cat_tipo_empleador PRIMARY KEY,
    descripcion       VARCHAR(100) NULL );
INSERT INTO cat.tipo_empleador (id_tipo_empleador) VALUES (1),(2);
GO

CREATE TABLE cat.planilla
(   id_planilla INT        CONSTRAINT PK_cat_planilla PRIMARY KEY,
    descripcion VARCHAR(100) NULL );
INSERT INTO cat.planilla (id_planilla) VALUES (1),(2),(3);
GO

CREATE TABLE cat.pago_planilla
(   id_pago_planilla INT        CONSTRAINT PK_cat_pago_planilla PRIMARY KEY,
    descripcion      VARCHAR(100) NULL );
INSERT INTO cat.pago_planilla (id_pago_planilla) VALUES (1),(2),(3);
GO

CREATE TABLE cat.situacion_laboral
(   codigo      CHAR(1)      CONSTRAINT PK_cat_situacion_laboral PRIMARY KEY,
    descripcion VARCHAR(50)  NULL );
INSERT INTO cat.situacion_laboral (codigo) VALUES ('D'),('I');
GO

CREATE TABLE cat.tipo_planilla
(   id_tipo_planilla INT        CONSTRAINT PK_cat_tipo_planilla PRIMARY KEY,
    descripcion      VARCHAR(100) NULL );
INSERT INTO cat.tipo_planilla (id_tipo_planilla) VALUES (0),(1);
GO

CREATE TABLE cat.tipo_cotizante
(   id_tipo_cotizante INT        CONSTRAINT PK_cat_tipo_cotizante PRIMARY KEY,
    descripcion       VARCHAR(100) NULL );
INSERT INTO cat.tipo_cotizante (id_tipo_cotizante) VALUES (1),(2),(3);
GO

CREATE TABLE cat.parentesco
(   id_parentesco INT        CONSTRAINT PK_cat_parentesco PRIMARY KEY,
    descripcion   VARCHAR(100) NULL );
INSERT INTO cat.parentesco (id_parentesco) VALUES (1),(2),(3),(4),(5),(6),(7);
GO

CREATE TABLE cat.fuente_fondos
(   id_fuente_fondos INT        CONSTRAINT PK_cat_fuente_fondos PRIMARY KEY,
    descripcion      VARCHAR(100) NULL );
INSERT INTO cat.fuente_fondos (id_fuente_fondos) VALUES (1),(2),(5),(6),(7);
GO

CREATE TABLE cat.numero_anualidad
(   id_numero_anualidad INT        CONSTRAINT PK_cat_numero_anualidad PRIMARY KEY,
    descripcion         VARCHAR(100) NULL );
INSERT INTO cat.numero_anualidad (id_numero_anualidad) VALUES (1),(2),(3),(4),(5),(6);
GO

CREATE TABLE cat.tipo_documento_beneficiario
(   id_tipo_documento_beneficiario INT        CONSTRAINT PK_cat_tipo_doc_benef PRIMARY KEY,
    descripcion                    VARCHAR(100) NULL );
INSERT INTO cat.tipo_documento_beneficiario (id_tipo_documento_beneficiario) VALUES (1),(2),(3),(4),(5),(6);
GO

/* ============================================================================
   ESQUEMA bcpe  -  TABLAS MAESTRAS Y TRANSACCIONALES (segun XSD)
   ============================================================================ */

-- ---------------------------------------------------------------------------
-- bcpe.afiliado  (maestro de personas afiliadas)
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.afiliado
(
    id_afiliado                  INT IDENTITY(1,1) CONSTRAINT PK_bcpe_afiliado PRIMARY KEY,
    numero_documento             VARCHAR(12)  NOT NULL,
    id_tipo_documento            INT          NOT NULL,
    primer_nombre                VARCHAR(20)  NOT NULL,
    segundo_nombre                VARCHAR(40)  NULL,
    primer_apellido               VARCHAR(20)  NOT NULL,
    segundo_apellido              VARCHAR(20)  NULL,
    apellido_casada               VARCHAR(20)  NULL,
    conocido_por                  VARCHAR(100) NULL,
    fecha_nacimiento              DATE         NULL,
    genero                        CHAR(1)      NOT NULL,
    estado_familiar               CHAR(1)      NOT NULL,
    nup                           CHAR(12)     NULL,
    dui                           VARCHAR(9)   NULL,
    cip                           VARCHAR(11)  NULL,
    carne_residente               VARCHAR(10)  NULL,
    pasaporte                     VARCHAR(15)  NULL,
    carne_minoridad               VARCHAR(10)  NULL,
    isss                          CHAR(9)      NULL,
    inpep                         VARCHAR(10)  NULL,
    nit                           VARCHAR(14)  NULL,
    tipo_solicitud_afiliacion     CHAR(2)      NULL,
    numero_solicitud_afiliacion   VARCHAR(10)  NULL,
    fecha_docum_afiliacion        DATE         NULL,
    fecha_afiliacion              DATE         NULL,
    estado_afiliado                CHAR(3)      NOT NULL,
    fecha_fallecimiento           DATE         NULL,
    codigo_pais                   CHAR(3)      NOT NULL,
    id_tipo_sistema               INT          NOT NULL,
    tipo_afiliado                 CHAR(2)      NULL,
    id_carga_archivo              INT          NOT NULL,
    fecha_creacion                DATETIME     NOT NULL CONSTRAINT DF_bcpe_afiliado_fc DEFAULT (GETDATE()),
    fecha_actualizacion           DATETIME     NULL,
    CONSTRAINT UQ_bcpe_afiliado_documento UNIQUE (numero_documento, id_tipo_documento),
    CONSTRAINT FK_afiliado_tipo_documento FOREIGN KEY (id_tipo_documento) REFERENCES cat.tipo_documento (id_tipo_documento),
    CONSTRAINT FK_afiliado_genero         FOREIGN KEY (genero)            REFERENCES cat.genero (codigo),
    CONSTRAINT FK_afiliado_estado_familiar FOREIGN KEY (estado_familiar)  REFERENCES cat.estado_familiar (codigo),
    CONSTRAINT FK_afiliado_tipo_sistema   FOREIGN KEY (id_tipo_sistema)   REFERENCES cat.tipo_sistema (id_tipo_sistema),
    CONSTRAINT FK_afiliado_carga_archivo  FOREIGN KEY (id_carga_archivo)  REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

-- ---------------------------------------------------------------------------
-- bcpe.empleador  (maestro de empleadores / centros de trabajo)
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.empleador
(
    id_empleador           INT IDENTITY(1,1) CONSTRAINT PK_bcpe_empleador PRIMARY KEY,
    nit                    VARCHAR(14)  NOT NULL,
    id_tipo_persona        INT          NOT NULL,
    primer_nombre          VARCHAR(20)  NULL,
    segundo_nombre         VARCHAR(40)  NULL,
    primer_apellido        VARCHAR(20)  NULL,
    segundo_apellido       VARCHAR(20)  NULL,
    apellido_casada        VARCHAR(20)  NULL,
    razon_social           VARCHAR(100) NULL,
    numero_patronal        VARCHAR(9)   NULL,
    id_tipo_empleador      INT          NOT NULL,
    codigo_centro_trabajo  VARCHAR(5)   NOT NULL,
    nombre_centro_trabajo  VARCHAR(100) NULL,
    numero_patronal_ct     VARCHAR(9)   NULL,
    id_carga_archivo       INT          NOT NULL,
    fecha_creacion         DATETIME     NOT NULL CONSTRAINT DF_bcpe_empleador_fc DEFAULT (GETDATE()),
    fecha_actualizacion    DATETIME     NULL,
    CONSTRAINT UQ_bcpe_empleador_nit_centro UNIQUE (nit, codigo_centro_trabajo),
    CONSTRAINT FK_empleador_tipo_persona   FOREIGN KEY (id_tipo_persona)   REFERENCES cat.tipo_persona (id_tipo_persona),
    CONSTRAINT FK_empleador_tipo_empleador FOREIGN KEY (id_tipo_empleador) REFERENCES cat.tipo_empleador (id_tipo_empleador),
    CONSTRAINT FK_empleador_carga_archivo  FOREIGN KEY (id_carga_archivo)  REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

-- ---------------------------------------------------------------------------
-- bcpe.cotizante  (transaccional mensual de cotizaciones)
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.cotizante
(
    id_cotizante                       BIGINT IDENTITY(1,1) CONSTRAINT PK_bcpe_cotizante PRIMARY KEY,
    numero_documento                   VARCHAR(12) NOT NULL,
    id_tipo_documento                  INT         NOT NULL,
    id_afiliado                        INT         NULL,       -- resuelto en el proceso de carga
    nit_empleador                      VARCHAR(14) NOT NULL,
    id_empleador                       INT         NULL,       -- resuelto en el proceso de carga
    ibc                                DECIMAL(6,2) NOT NULL,
    periodo_devengue                   INT         NOT NULL,   -- YYYYMM
    id_planilla                        INT         NOT NULL,
    id_pago_planilla                   INT         NOT NULL,
    situacion_laboral                  CHAR(1)     NOT NULL,
    codigo_centro_trabajo              VARCHAR(5)  NOT NULL,
    numero_planilla                    BIGINT      NOT NULL,
    id_tipo_planilla                   INT         NOT NULL,
    id_tipo_cotizante                  INT         NOT NULL,
    ultimo_periodo_devengue_cotizado   INT         NULL,
    id_carga_archivo                   INT         NOT NULL,
    fecha_creacion                     DATETIME    NOT NULL CONSTRAINT DF_bcpe_cotizante_fc DEFAULT (GETDATE()),
    CONSTRAINT UQ_bcpe_cotizante UNIQUE (numero_documento, id_tipo_documento, nit_empleador, periodo_devengue, numero_planilla),
    CONSTRAINT FK_cotizante_tipo_documento FOREIGN KEY (id_tipo_documento) REFERENCES cat.tipo_documento (id_tipo_documento),
    CONSTRAINT FK_cotizante_afiliado       FOREIGN KEY (id_afiliado)       REFERENCES bcpe.afiliado (id_afiliado),
    CONSTRAINT FK_cotizante_empleador      FOREIGN KEY (id_empleador)      REFERENCES bcpe.empleador (id_empleador),
    CONSTRAINT FK_cotizante_planilla       FOREIGN KEY (id_planilla)       REFERENCES cat.planilla (id_planilla),
    CONSTRAINT FK_cotizante_pago_planilla  FOREIGN KEY (id_pago_planilla)  REFERENCES cat.pago_planilla (id_pago_planilla),
    CONSTRAINT FK_cotizante_situacion_lab  FOREIGN KEY (situacion_laboral) REFERENCES cat.situacion_laboral (codigo),
    CONSTRAINT FK_cotizante_tipo_planilla  FOREIGN KEY (id_tipo_planilla)  REFERENCES cat.tipo_planilla (id_tipo_planilla),
    CONSTRAINT FK_cotizante_tipo_cotizante FOREIGN KEY (id_tipo_cotizante) REFERENCES cat.tipo_cotizante (id_tipo_cotizante),
    CONSTRAINT FK_cotizante_carga_archivo  FOREIGN KEY (id_carga_archivo)  REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

-- ---------------------------------------------------------------------------
-- bcpe.mora_empleador  (transaccional)
-- NOTA: el XSD no trae periodo/fecha; periodo_reporte se llena desde
--       metadata de la carga (nombre de archivo) - CONFIRMAR con SSF/negocio.
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.mora_empleador
(
    id_mora           BIGINT IDENTITY(1,1) CONSTRAINT PK_bcpe_mora_empleador PRIMARY KEY,
    nit               VARCHAR(14)   NOT NULL,
    id_empleador      INT           NULL,
    periodo_reporte   CHAR(6)       NULL,
    monto_omis        DECIMAL(14,2) NOT NULL,
    monto_dnp         DECIMAL(14,2) NOT NULL,
    monto_ins         DECIMAL(14,2) NOT NULL,
    total_mora        DECIMAL(17,2) NOT NULL,
    id_carga_archivo  INT           NOT NULL,
    fecha_creacion    DATETIME      NOT NULL CONSTRAINT DF_bcpe_mora_fc DEFAULT (GETDATE()),
    CONSTRAINT FK_mora_empleador_empleador FOREIGN KEY (id_empleador)     REFERENCES bcpe.empleador (id_empleador),
    CONSTRAINT FK_mora_carga_archivo       FOREIGN KEY (id_carga_archivo) REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

-- ---------------------------------------------------------------------------
-- bcpe.prestacion  (transaccional - otorgamiento de beneficios/pensiones)
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.prestacion
(
    id_prestacion                        BIGINT IDENTITY(1,1) CONSTRAINT PK_bcpe_prestacion PRIMARY KEY,
    numero_documento                     VARCHAR(12)  NOT NULL,
    id_tipo_documento                    INT          NOT NULL,
    id_afiliado                          INT          NULL,
    codigo_beneficiario                  VARCHAR(11)  NOT NULL,
    tipo_beneficio                       INT          NOT NULL,
    primer_nombre                        VARCHAR(20)  NOT NULL,
    segundo_nombre                       VARCHAR(40)  NULL,
    primer_apellido                      VARCHAR(20)  NOT NULL,
    segundo_apellido                     VARCHAR(20)  NULL,
    apellido_casada                      VARCHAR(20)  NULL,
    genero                               CHAR(1)      NOT NULL,
    fecha_nacimiento                     DATE         NULL,
    id_parentesco                        INT          NULL,
    numero_solicitud                     VARCHAR(11)  NULL,
    numero_expediente                    VARCHAR(12)  NOT NULL,
    fecha_solicitud                      DATE         NULL,
    requisito_legal                      INT          NOT NULL,
    saldo_ciap                           DECIMAL(9,2) NULL,
    fecha_otorgamiento                   DATE         NOT NULL,
    fecha_inicio_devengue                DATE         NULL,
    monto_pension_mensual                DECIMAL(12,2) NULL,
    monto_total_devolucion_asignacion    DECIMAL(12,2) NULL,
    tiempo_cotizado                      INT          NOT NULL,
    pension_calculada                    DECIMAL(12,2) NULL,
    garantia_estado                      BIT          NULL,
    pension_longevidad                   BIT          NULL,
    pension_sin_hacienda                 DECIMAL(6,2) NULL,
    monto_pension_hacienda               DECIMAL(5,2) NULL,
    numero_documento_beneficiario        VARCHAR(15)  NULL,
    id_tipo_documento_beneficiario       INT          NULL,
    sbr                                  DECIMAL(7,2) NULL,
    pension_referencia                   DECIMAL(7,2) NULL,
    id_carga_archivo                     INT          NOT NULL,
    fecha_creacion                       DATETIME     NOT NULL CONSTRAINT DF_bcpe_prestacion_fc DEFAULT (GETDATE()),
    CONSTRAINT UQ_bcpe_prestacion_expediente UNIQUE (numero_expediente),
    CONSTRAINT FK_prestacion_tipo_documento    FOREIGN KEY (id_tipo_documento)  REFERENCES cat.tipo_documento (id_tipo_documento),
    CONSTRAINT FK_prestacion_afiliado          FOREIGN KEY (id_afiliado)        REFERENCES bcpe.afiliado (id_afiliado),
    CONSTRAINT FK_prestacion_genero            FOREIGN KEY (genero)             REFERENCES cat.genero (codigo),
    CONSTRAINT FK_prestacion_parentesco        FOREIGN KEY (id_parentesco)      REFERENCES cat.parentesco (id_parentesco),
    CONSTRAINT FK_prestacion_tipo_doc_benef    FOREIGN KEY (id_tipo_documento_beneficiario) REFERENCES cat.tipo_documento_beneficiario (id_tipo_documento_beneficiario),
    CONSTRAINT FK_prestacion_carga_archivo     FOREIGN KEY (id_carga_archivo)   REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

-- ---------------------------------------------------------------------------
-- bcpe.pago_beneficio  (transaccional - pagos efectivos de beneficios)
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.pago_beneficio
(
    id_pago_beneficio             BIGINT IDENTITY(1,1) CONSTRAINT PK_bcpe_pago_beneficio PRIMARY KEY,
    numero_documento               VARCHAR(12)   NOT NULL,
    id_tipo_documento              INT           NOT NULL,
    id_afiliado                    INT           NULL,
    codigo_beneficiario             VARCHAR(11)   NOT NULL,
    tipo_beneficio                  INT           NOT NULL,
    fecha_pago                      DATE          NOT NULL,
    monto_pagado                    DECIMAL(12,2) NOT NULL,
    fecha_inicio_pago               DATE          NOT NULL,
    fecha_fin_pago                  DATE          NOT NULL,
    id_fuente_fondos                INT           NOT NULL,
    id_numero_anualidad             INT           NULL,
    numero_cuotas                   DECIMAL(12,8) NULL,
    valor_cuota_resolucion          DECIMAL(12,8) NULL,
    fecha_valor_cuota_resolucion    DATE          NULL,
    id_carga_archivo                INT           NOT NULL,
    fecha_creacion                  DATETIME      NOT NULL CONSTRAINT DF_bcpe_pago_benef_fc DEFAULT (GETDATE()),
    CONSTRAINT UQ_bcpe_pago_beneficio UNIQUE (numero_documento, id_tipo_documento, codigo_beneficiario, tipo_beneficio, fecha_pago),
    CONSTRAINT FK_pago_benef_tipo_documento FOREIGN KEY (id_tipo_documento)  REFERENCES cat.tipo_documento (id_tipo_documento),
    CONSTRAINT FK_pago_benef_afiliado       FOREIGN KEY (id_afiliado)        REFERENCES bcpe.afiliado (id_afiliado),
    CONSTRAINT FK_pago_benef_fuente_fondos  FOREIGN KEY (id_fuente_fondos)   REFERENCES cat.fuente_fondos (id_fuente_fondos),
    CONSTRAINT FK_pago_benef_num_anualidad  FOREIGN KEY (id_numero_anualidad) REFERENCES cat.numero_anualidad (id_numero_anualidad),
    CONSTRAINT FK_pago_benef_carga_archivo  FOREIGN KEY (id_carga_archivo)   REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

-- ---------------------------------------------------------------------------
-- bcpe.reintegro_anticipo  (transaccional)
-- ---------------------------------------------------------------------------
CREATE TABLE bcpe.reintegro_anticipo
(
    id_reintegro                BIGINT IDENTITY(1,1) CONSTRAINT PK_bcpe_reintegro_anticipo PRIMARY KEY,
    numero_unico_previsional    CHAR(12)      NOT NULL,
    dui                         VARCHAR(9)    NULL,
    numero_solicitud            VARCHAR(11)   NOT NULL,
    numero_expediente           VARCHAR(12)   NOT NULL,
    fecha_reintegro              DATE          NULL,
    porcentaje_reintegro         DECIMAL(5,2)  NULL,
    numero_cuotas_reintegro      DECIMAL(12,8) NULL,
    monto_reintegro              DECIMAL(12,2) NULL,
    valor_cuota_reintegro        DECIMAL(12,8) NULL,
    exencion_reintegrar          BIT           NOT NULL,
    id_carga_archivo             INT           NOT NULL,
    fecha_creacion                DATETIME      NOT NULL CONSTRAINT DF_bcpe_reintegro_fc DEFAULT (GETDATE()),
    CONSTRAINT UQ_bcpe_reintegro UNIQUE (numero_expediente, numero_solicitud),
    CONSTRAINT FK_reintegro_carga_archivo FOREIGN KEY (id_carga_archivo) REFERENCES ctl.carga_archivo (id_carga_archivo)
);
GO

/* ============================================================================
   INDICES DE APOYO (busquedas frecuentes para carga y BI)
   ============================================================================ */
CREATE INDEX IX_cotizante_periodo          ON bcpe.cotizante (periodo_devengue);
CREATE INDEX IX_cotizante_nit_empleador    ON bcpe.cotizante (nit_empleador);
CREATE INDEX IX_mora_empleador_nit         ON bcpe.mora_empleador (nit);
CREATE INDEX IX_prestacion_numero_doc      ON bcpe.prestacion (numero_documento, id_tipo_documento);
CREATE INDEX IX_pago_beneficio_fecha_pago  ON bcpe.pago_beneficio (fecha_pago);
CREATE INDEX IX_carga_archivo_tipo_periodo ON ctl.carga_archivo (tipo_entidad, periodo_reporte);
GO

PRINT 'Modelo OLTP SSF_BCPE_BI creado correctamente.';
GO
