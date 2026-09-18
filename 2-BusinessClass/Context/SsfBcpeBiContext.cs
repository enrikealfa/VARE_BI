using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SSF.PortalBI.BusinessClass.Entities;

namespace SSF.PortalBI.BusinessClass.Context;

public partial class SsfBcpeBiContext : DbContext
{
    public SsfBcpeBiContext(DbContextOptions<SsfBcpeBiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Afiliado> Afiliados { get; set; }

    public virtual DbSet<CargaArchivo> CargaArchivos { get; set; }

    public virtual DbSet<CargaArchivoDetalle> CargaArchivoDetalles { get; set; }

    public virtual DbSet<Cotizante> Cotizantes { get; set; }

    public virtual DbSet<Empleador> Empleadors { get; set; }

    public virtual DbSet<EstadoFamiliar> EstadoFamiliars { get; set; }

    public virtual DbSet<FuenteFondo> FuenteFondos { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<MoraEmpleador> MoraEmpleadors { get; set; }

    public virtual DbSet<NumeroAnualidad> NumeroAnualidads { get; set; }

    public virtual DbSet<PagoBeneficio> PagoBeneficios { get; set; }

    public virtual DbSet<PagoPlanilla> PagoPlanillas { get; set; }

    public virtual DbSet<Parentesco> Parentescos { get; set; }

    public virtual DbSet<Planilla> Planillas { get; set; }

    public virtual DbSet<Prestacion> Prestacions { get; set; }

    public virtual DbSet<ReintegroAnticipo> ReintegroAnticipos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<SituacionLaboral> SituacionLaborals { get; set; }

    public virtual DbSet<TipoCotizante> TipoCotizantes { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoDocumentoBeneficiario> TipoDocumentoBeneficiarios { get; set; }

    public virtual DbSet<TipoEmpleador> TipoEmpleadors { get; set; }

    public virtual DbSet<TipoPersona> TipoPersonas { get; set; }

    public virtual DbSet<TipoPlanilla> TipoPlanillas { get; set; }

    public virtual DbSet<TipoSistema> TipoSistemas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Afiliado>(entity =>
        {
            entity.HasKey(e => e.IdAfiliado).HasName("PK_bcpe_afiliado");

            entity.ToTable("afiliado", "bcpe");

            entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento }, "UQ_bcpe_afiliado_documento").IsUnique();

            entity.Property(e => e.IdAfiliado).HasColumnName("id_afiliado");
            entity.Property(e => e.ApellidoCasada)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_casada");
            entity.Property(e => e.CarneMinoridad)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("carne_minoridad");
            entity.Property(e => e.CarneResidente)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("carne_residente");
            entity.Property(e => e.Cip)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cip");
            entity.Property(e => e.CodigoPais)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo_pais");
            entity.Property(e => e.ConocidoPor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("conocido_por");
            entity.Property(e => e.Dui)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("dui");
            entity.Property(e => e.EstadoAfiliado)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado_afiliado");
            entity.Property(e => e.EstadoFamiliar)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado_familiar");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaAfiliacion).HasColumnName("fecha_afiliacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_afiliado_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaDocumAfiliacion).HasColumnName("fecha_docum_afiliacion");
            entity.Property(e => e.FechaFallecimiento).HasColumnName("fecha_fallecimiento");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");
            entity.Property(e => e.IdTipoSistema).HasColumnName("id_tipo_sistema");
            entity.Property(e => e.Inpep)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("inpep");
            entity.Property(e => e.Isss)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("isss");
            entity.Property(e => e.Nit)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("nit");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.NumeroSolicitudAfiliacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("numero_solicitud_afiliacion");
            entity.Property(e => e.Nup)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("nup");
            entity.Property(e => e.Pasaporte)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("pasaporte");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("segundo_nombre");
            entity.Property(e => e.TipoAfiliado)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_afiliado");
            entity.Property(e => e.TipoSolicitudAfiliacion)
                .HasMaxLength(2)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_solicitud_afiliacion");

            entity.HasOne(d => d.EstadoFamiliarNavigation).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.EstadoFamiliar)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_afiliado_estado_familiar");

            entity.HasOne(d => d.GeneroNavigation).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.Genero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_afiliado_genero");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_afiliado_carga_archivo");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_afiliado_tipo_documento");

            entity.HasOne(d => d.IdTipoSistemaNavigation).WithMany(p => p.Afiliados)
                .HasForeignKey(d => d.IdTipoSistema)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_afiliado_tipo_sistema");
        });

        modelBuilder.Entity<CargaArchivo>(entity =>
        {
            entity.HasKey(e => e.IdCargaArchivo).HasName("PK_ctl_carga_archivo");

            entity.ToTable("carga_archivo", "ctl");

            entity.HasIndex(e => new { e.TipoEntidad, e.PeriodoReporte }, "IX_carga_archivo_tipo_periodo");

            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.Estado)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasDefaultValue("PENDIENTE", "DF_ctl_carga_estado")
                .HasColumnName("estado");
            entity.Property(e => e.FechaCarga)
                .HasDefaultValueSql("(getdate())", "DF_ctl_carga_fecha")
                .HasColumnType("datetime")
                .HasColumnName("fecha_carga");
            entity.Property(e => e.FechaFinProceso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_fin_proceso");
            entity.Property(e => e.HashArchivo)
                .HasMaxLength(64)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("hash_archivo");
            entity.Property(e => e.IdUsuarioCarga).HasColumnName("id_usuario_carga");
            entity.Property(e => e.NombreArchivo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre_archivo");
            entity.Property(e => e.PeriodoReporte)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("periodo_reporte");
            entity.Property(e => e.RegistrosCorrectos).HasColumnName("registros_correctos");
            entity.Property(e => e.RegistrosError).HasColumnName("registros_error");
            entity.Property(e => e.RutaArchivo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("ruta_archivo");
            entity.Property(e => e.TipoEntidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_entidad");
            entity.Property(e => e.TotalRegistros).HasColumnName("total_registros");

            entity.HasOne(d => d.IdUsuarioCargaNavigation).WithMany(p => p.CargaArchivos)
                .HasForeignKey(d => d.IdUsuarioCarga)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_carga_archivo_usuario");
        });

        modelBuilder.Entity<CargaArchivoDetalle>(entity =>
        {
            entity.HasKey(e => e.IdDetalle).HasName("PK_ctl_carga_detalle");

            entity.ToTable("carga_archivo_detalle", "ctl");

            entity.Property(e => e.IdDetalle).HasColumnName("id_detalle");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.MensajeError)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("mensaje_error");
            entity.Property(e => e.NumeroLinea).HasColumnName("numero_linea");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.CargaArchivoDetalles)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_carga_detalle_carga");
        });

        modelBuilder.Entity<Cotizante>(entity =>
        {
            entity.HasKey(e => e.IdCotizante).HasName("PK_bcpe_cotizante");

            entity.ToTable("cotizante", "bcpe");

            entity.HasIndex(e => e.NitEmpleador, "IX_cotizante_nit_empleador");

            entity.HasIndex(e => e.PeriodoDevengue, "IX_cotizante_periodo");

            entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento, e.NitEmpleador, e.PeriodoDevengue, e.NumeroPlanilla }, "UQ_bcpe_cotizante").IsUnique();

            entity.Property(e => e.IdCotizante).HasColumnName("id_cotizante");
            entity.Property(e => e.CodigoCentroTrabajo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_centro_trabajo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_cotizante_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Ibc)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("ibc");
            entity.Property(e => e.IdAfiliado).HasColumnName("id_afiliado");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.IdEmpleador).HasColumnName("id_empleador");
            entity.Property(e => e.IdPagoPlanilla).HasColumnName("id_pago_planilla");
            entity.Property(e => e.IdPlanilla).HasColumnName("id_planilla");
            entity.Property(e => e.IdTipoCotizante).HasColumnName("id_tipo_cotizante");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");
            entity.Property(e => e.IdTipoPlanilla).HasColumnName("id_tipo_planilla");
            entity.Property(e => e.NitEmpleador)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("nit_empleador");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.NumeroPlanilla).HasColumnName("numero_planilla");
            entity.Property(e => e.PeriodoDevengue).HasColumnName("periodo_devengue");
            entity.Property(e => e.SituacionLaboral)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("situacion_laboral");
            entity.Property(e => e.UltimoPeriodoDevengueCotizado).HasColumnName("ultimo_periodo_devengue_cotizado");

            entity.HasOne(d => d.IdAfiliadoNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdAfiliado)
                .HasConstraintName("FK_cotizante_afiliado");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_carga_archivo");

            entity.HasOne(d => d.IdEmpleadorNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdEmpleador)
                .HasConstraintName("FK_cotizante_empleador");

            entity.HasOne(d => d.IdPagoPlanillaNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdPagoPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_pago_planilla");

            entity.HasOne(d => d.IdPlanillaNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_planilla");

            entity.HasOne(d => d.IdTipoCotizanteNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdTipoCotizante)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_tipo_cotizante");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_tipo_documento");

            entity.HasOne(d => d.IdTipoPlanillaNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.IdTipoPlanilla)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_tipo_planilla");

            entity.HasOne(d => d.SituacionLaboralNavigation).WithMany(p => p.Cotizantes)
                .HasForeignKey(d => d.SituacionLaboral)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cotizante_situacion_lab");
        });

        modelBuilder.Entity<Empleador>(entity =>
        {
            entity.HasKey(e => e.IdEmpleador).HasName("PK_bcpe_empleador");

            entity.ToTable("empleador", "bcpe");

            entity.HasIndex(e => new { e.Nit, e.CodigoCentroTrabajo }, "UQ_bcpe_empleador_nit_centro").IsUnique();

            entity.Property(e => e.IdEmpleador).HasColumnName("id_empleador");
            entity.Property(e => e.ApellidoCasada)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_casada");
            entity.Property(e => e.CodigoCentroTrabajo)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("codigo_centro_trabajo");
            entity.Property(e => e.FechaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("fecha_actualizacion");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_empleador_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.IdTipoEmpleador).HasColumnName("id_tipo_empleador");
            entity.Property(e => e.IdTipoPersona).HasColumnName("id_tipo_persona");
            entity.Property(e => e.Nit)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("nit");
            entity.Property(e => e.NombreCentroTrabajo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_centro_trabajo");
            entity.Property(e => e.NumeroPatronal)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero_patronal");
            entity.Property(e => e.NumeroPatronalCt)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("numero_patronal_ct");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razon_social");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("segundo_nombre");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.Empleadors)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleador_carga_archivo");

            entity.HasOne(d => d.IdTipoEmpleadorNavigation).WithMany(p => p.Empleadors)
                .HasForeignKey(d => d.IdTipoEmpleador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleador_tipo_empleador");

            entity.HasOne(d => d.IdTipoPersonaNavigation).WithMany(p => p.Empleadors)
                .HasForeignKey(d => d.IdTipoPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_empleador_tipo_persona");
        });

        modelBuilder.Entity<EstadoFamiliar>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_cat_estado_familiar");

            entity.ToTable("estado_familiar", "cat");

            entity.Property(e => e.Codigo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<FuenteFondo>(entity =>
        {
            entity.HasKey(e => e.IdFuenteFondos).HasName("PK_cat_fuente_fondos");

            entity.ToTable("fuente_fondos", "cat");

            entity.Property(e => e.IdFuenteFondos)
                .ValueGeneratedNever()
                .HasColumnName("id_fuente_fondos");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_cat_genero");

            entity.ToTable("genero", "cat");

            entity.Property(e => e.Codigo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<MoraEmpleador>(entity =>
        {
            entity.HasKey(e => e.IdMora).HasName("PK_bcpe_mora_empleador");

            entity.ToTable("mora_empleador", "bcpe");

            entity.HasIndex(e => e.Nit, "IX_mora_empleador_nit");

            entity.Property(e => e.IdMora).HasColumnName("id_mora");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_mora_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.IdEmpleador).HasColumnName("id_empleador");
            entity.Property(e => e.MontoDnp)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("monto_dnp");
            entity.Property(e => e.MontoIns)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("monto_ins");
            entity.Property(e => e.MontoOmis)
                .HasColumnType("decimal(14, 2)")
                .HasColumnName("monto_omis");
            entity.Property(e => e.Nit)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("nit");
            entity.Property(e => e.PeriodoReporte)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("periodo_reporte");
            entity.Property(e => e.TotalMora)
                .HasColumnType("decimal(17, 2)")
                .HasColumnName("total_mora");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.MoraEmpleadors)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_mora_carga_archivo");

            entity.HasOne(d => d.IdEmpleadorNavigation).WithMany(p => p.MoraEmpleadors)
                .HasForeignKey(d => d.IdEmpleador)
                .HasConstraintName("FK_mora_empleador_empleador");
        });

        modelBuilder.Entity<NumeroAnualidad>(entity =>
        {
            entity.HasKey(e => e.IdNumeroAnualidad).HasName("PK_cat_numero_anualidad");

            entity.ToTable("numero_anualidad", "cat");

            entity.Property(e => e.IdNumeroAnualidad)
                .ValueGeneratedNever()
                .HasColumnName("id_numero_anualidad");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<PagoBeneficio>(entity =>
        {
            entity.HasKey(e => e.IdPagoBeneficio).HasName("PK_bcpe_pago_beneficio");

            entity.ToTable("pago_beneficio", "bcpe");

            entity.HasIndex(e => e.FechaPago, "IX_pago_beneficio_fecha_pago");

            entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento, e.CodigoBeneficiario, e.TipoBeneficio, e.FechaPago }, "UQ_bcpe_pago_beneficio").IsUnique();

            entity.Property(e => e.IdPagoBeneficio).HasColumnName("id_pago_beneficio");
            entity.Property(e => e.CodigoBeneficiario)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("codigo_beneficiario");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_pago_benef_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaFinPago).HasColumnName("fecha_fin_pago");
            entity.Property(e => e.FechaInicioPago).HasColumnName("fecha_inicio_pago");
            entity.Property(e => e.FechaPago).HasColumnName("fecha_pago");
            entity.Property(e => e.FechaValorCuotaResolucion).HasColumnName("fecha_valor_cuota_resolucion");
            entity.Property(e => e.IdAfiliado).HasColumnName("id_afiliado");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.IdFuenteFondos).HasColumnName("id_fuente_fondos");
            entity.Property(e => e.IdNumeroAnualidad).HasColumnName("id_numero_anualidad");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");
            entity.Property(e => e.MontoPagado)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("monto_pagado");
            entity.Property(e => e.NumeroCuotas)
                .HasColumnType("decimal(12, 8)")
                .HasColumnName("numero_cuotas");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.TipoBeneficio).HasColumnName("tipo_beneficio");
            entity.Property(e => e.ValorCuotaResolucion)
                .HasColumnType("decimal(12, 8)")
                .HasColumnName("valor_cuota_resolucion");

            entity.HasOne(d => d.IdAfiliadoNavigation).WithMany(p => p.PagoBeneficios)
                .HasForeignKey(d => d.IdAfiliado)
                .HasConstraintName("FK_pago_benef_afiliado");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.PagoBeneficios)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_benef_carga_archivo");

            entity.HasOne(d => d.IdFuenteFondosNavigation).WithMany(p => p.PagoBeneficios)
                .HasForeignKey(d => d.IdFuenteFondos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_benef_fuente_fondos");

            entity.HasOne(d => d.IdNumeroAnualidadNavigation).WithMany(p => p.PagoBeneficios)
                .HasForeignKey(d => d.IdNumeroAnualidad)
                .HasConstraintName("FK_pago_benef_num_anualidad");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.PagoBeneficios)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_pago_benef_tipo_documento");
        });

        modelBuilder.Entity<PagoPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdPagoPlanilla).HasName("PK_cat_pago_planilla");

            entity.ToTable("pago_planilla", "cat");

            entity.Property(e => e.IdPagoPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("id_pago_planilla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Parentesco>(entity =>
        {
            entity.HasKey(e => e.IdParentesco).HasName("PK_cat_parentesco");

            entity.ToTable("parentesco", "cat");

            entity.Property(e => e.IdParentesco)
                .ValueGeneratedNever()
                .HasColumnName("id_parentesco");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Planilla>(entity =>
        {
            entity.HasKey(e => e.IdPlanilla).HasName("PK_cat_planilla");

            entity.ToTable("planilla", "cat");

            entity.Property(e => e.IdPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("id_planilla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Prestacion>(entity =>
        {
            entity.HasKey(e => e.IdPrestacion).HasName("PK_bcpe_prestacion");

            entity.ToTable("prestacion", "bcpe");

            entity.HasIndex(e => new { e.NumeroDocumento, e.IdTipoDocumento }, "IX_prestacion_numero_doc");

            entity.HasIndex(e => e.NumeroExpediente, "UQ_bcpe_prestacion_expediente").IsUnique();

            entity.Property(e => e.IdPrestacion).HasColumnName("id_prestacion");
            entity.Property(e => e.ApellidoCasada)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("apellido_casada");
            entity.Property(e => e.CodigoBeneficiario)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("codigo_beneficiario");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_prestacion_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaInicioDevengue).HasColumnName("fecha_inicio_devengue");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.FechaOtorgamiento).HasColumnName("fecha_otorgamiento");
            entity.Property(e => e.FechaSolicitud).HasColumnName("fecha_solicitud");
            entity.Property(e => e.GarantiaEstado).HasColumnName("garantia_estado");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("genero");
            entity.Property(e => e.IdAfiliado).HasColumnName("id_afiliado");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.IdParentesco).HasColumnName("id_parentesco");
            entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");
            entity.Property(e => e.IdTipoDocumentoBeneficiario).HasColumnName("id_tipo_documento_beneficiario");
            entity.Property(e => e.MontoPensionHacienda)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("monto_pension_hacienda");
            entity.Property(e => e.MontoPensionMensual)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("monto_pension_mensual");
            entity.Property(e => e.MontoTotalDevolucionAsignacion)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("monto_total_devolucion_asignacion");
            entity.Property(e => e.NumeroDocumento)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("numero_documento");
            entity.Property(e => e.NumeroDocumentoBeneficiario)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("numero_documento_beneficiario");
            entity.Property(e => e.NumeroExpediente)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("numero_expediente");
            entity.Property(e => e.NumeroSolicitud)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("numero_solicitud");
            entity.Property(e => e.PensionCalculada)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("pension_calculada");
            entity.Property(e => e.PensionLongevidad).HasColumnName("pension_longevidad");
            entity.Property(e => e.PensionReferencia)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("pension_referencia");
            entity.Property(e => e.PensionSinHacienda)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("pension_sin_hacienda");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.RequisitoLegal).HasColumnName("requisito_legal");
            entity.Property(e => e.SaldoCiap)
                .HasColumnType("decimal(9, 2)")
                .HasColumnName("saldo_ciap");
            entity.Property(e => e.Sbr)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("sbr");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("segundo_nombre");
            entity.Property(e => e.TiempoCotizado).HasColumnName("tiempo_cotizado");
            entity.Property(e => e.TipoBeneficio).HasColumnName("tipo_beneficio");

            entity.HasOne(d => d.GeneroNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.Genero)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_prestacion_genero");

            entity.HasOne(d => d.IdAfiliadoNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdAfiliado)
                .HasConstraintName("FK_prestacion_afiliado");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_prestacion_carga_archivo");

            entity.HasOne(d => d.IdParentescoNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdParentesco)
                .HasConstraintName("FK_prestacion_parentesco");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_prestacion_tipo_documento");

            entity.HasOne(d => d.IdTipoDocumentoBeneficiarioNavigation).WithMany(p => p.Prestacions)
                .HasForeignKey(d => d.IdTipoDocumentoBeneficiario)
                .HasConstraintName("FK_prestacion_tipo_doc_benef");
        });

        modelBuilder.Entity<ReintegroAnticipo>(entity =>
        {
            entity.HasKey(e => e.IdReintegro).HasName("PK_bcpe_reintegro_anticipo");

            entity.ToTable("reintegro_anticipo", "bcpe");

            entity.HasIndex(e => new { e.NumeroExpediente, e.NumeroSolicitud }, "UQ_bcpe_reintegro").IsUnique();

            entity.Property(e => e.IdReintegro).HasColumnName("id_reintegro");
            entity.Property(e => e.Dui)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("dui");
            entity.Property(e => e.ExencionReintegrar).HasColumnName("exencion_reintegrar");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_bcpe_reintegro_fc")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaReintegro).HasColumnName("fecha_reintegro");
            entity.Property(e => e.IdCargaArchivo).HasColumnName("id_carga_archivo");
            entity.Property(e => e.MontoReintegro)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("monto_reintegro");
            entity.Property(e => e.NumeroCuotasReintegro)
                .HasColumnType("decimal(12, 8)")
                .HasColumnName("numero_cuotas_reintegro");
            entity.Property(e => e.NumeroExpediente)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("numero_expediente");
            entity.Property(e => e.NumeroSolicitud)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("numero_solicitud");
            entity.Property(e => e.NumeroUnicoPrevisional)
                .HasMaxLength(12)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("numero_unico_previsional");
            entity.Property(e => e.PorcentajeReintegro)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("porcentaje_reintegro");
            entity.Property(e => e.ValorCuotaReintegro)
                .HasColumnType("decimal(12, 8)")
                .HasColumnName("valor_cuota_reintegro");

            entity.HasOne(d => d.IdCargaArchivoNavigation).WithMany(p => p.ReintegroAnticipos)
                .HasForeignKey(d => d.IdCargaArchivo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_reintegro_carga_archivo");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK_seg_rol");

            entity.ToTable("rol", "seg");

            entity.HasIndex(e => e.NombreRol, "UQ_seg_rol_nombre").IsUnique();

            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true, "DF_seg_rol_activo")
                .HasColumnName("activo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.NombreRol)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_rol");
        });

        modelBuilder.Entity<SituacionLaboral>(entity =>
        {
            entity.HasKey(e => e.Codigo).HasName("PK_cat_situacion_laboral");

            entity.ToTable("situacion_laboral", "cat");

            entity.Property(e => e.Codigo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("codigo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoCotizante>(entity =>
        {
            entity.HasKey(e => e.IdTipoCotizante).HasName("PK_cat_tipo_cotizante");

            entity.ToTable("tipo_cotizante", "cat");

            entity.Property(e => e.IdTipoCotizante)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_cotizante");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK_cat_tipo_documento");

            entity.ToTable("tipo_documento", "cat");

            entity.Property(e => e.IdTipoDocumento)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_documento");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoDocumentoBeneficiario>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumentoBeneficiario).HasName("PK_cat_tipo_doc_benef");

            entity.ToTable("tipo_documento_beneficiario", "cat");

            entity.Property(e => e.IdTipoDocumentoBeneficiario)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_documento_beneficiario");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoEmpleador>(entity =>
        {
            entity.HasKey(e => e.IdTipoEmpleador).HasName("PK_cat_tipo_empleador");

            entity.ToTable("tipo_empleador", "cat");

            entity.Property(e => e.IdTipoEmpleador)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_empleador");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoPersona>(entity =>
        {
            entity.HasKey(e => e.IdTipoPersona).HasName("PK_cat_tipo_persona");

            entity.ToTable("tipo_persona", "cat");

            entity.Property(e => e.IdTipoPersona)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_persona");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoPlanilla>(entity =>
        {
            entity.HasKey(e => e.IdTipoPlanilla).HasName("PK_cat_tipo_planilla");

            entity.ToTable("tipo_planilla", "cat");

            entity.Property(e => e.IdTipoPlanilla)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_planilla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TipoSistema>(entity =>
        {
            entity.HasKey(e => e.IdTipoSistema).HasName("PK_cat_tipo_sistema");

            entity.ToTable("tipo_sistema", "cat");

            entity.Property(e => e.IdTipoSistema)
                .ValueGeneratedNever()
                .HasColumnName("id_tipo_sistema");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK_seg_usuario");

            entity.ToTable("usuario", "seg");

            entity.HasIndex(e => e.Correo, "UQ_seg_usuario_correo").IsUnique();

            entity.HasIndex(e => e.NombreUsuario, "UQ_seg_usuario_nombre_usuario").IsUnique();

            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Activo)
                .HasDefaultValue(true, "DF_seg_usuario_activo")
                .HasColumnName("activo");
            entity.Property(e => e.Bloqueado).HasColumnName("bloqueado");
            entity.Property(e => e.ClaveHash)
                .HasMaxLength(256)
                .HasColumnName("clave_hash");
            entity.Property(e => e.ClaveSalt)
                .HasMaxLength(128)
                .HasColumnName("clave_salt");
            entity.Property(e => e.Correo)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())", "DF_seg_usuario_fecha_creacion")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.FechaUltimoAcceso)
                .HasColumnType("datetime")
                .HasColumnName("fecha_ultimo_acceso");
            entity.Property(e => e.IntentosFallidos).HasColumnName("intentos_fallidos");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");

            entity.HasMany(d => d.IdRols).WithMany(p => p.IdUsuarios)
                .UsingEntity<Dictionary<string, object>>(
                    "UsuarioRol",
                    r => r.HasOne<Rol>().WithMany()
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_usuario_rol_rol"),
                    l => l.HasOne<Usuario>().WithMany()
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_usuario_rol_usuario"),
                    j =>
                    {
                        j.HasKey("IdUsuario", "IdRol").HasName("PK_seg_usuario_rol");
                        j.ToTable("usuario_rol", "seg");
                        j.IndexerProperty<int>("IdUsuario").HasColumnName("id_usuario");
                        j.IndexerProperty<int>("IdRol").HasColumnName("id_rol");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
