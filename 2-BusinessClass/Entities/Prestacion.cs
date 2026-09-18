using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Prestacion
{
    public long IdPrestacion { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public int IdTipoDocumento { get; set; }

    public int? IdAfiliado { get; set; }

    public string CodigoBeneficiario { get; set; } = null!;

    public int TipoBeneficio { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string? ApellidoCasada { get; set; }

    public string Genero { get; set; } = null!;

    public DateOnly? FechaNacimiento { get; set; }

    public int? IdParentesco { get; set; }

    public string? NumeroSolicitud { get; set; }

    public string NumeroExpediente { get; set; } = null!;

    public DateOnly? FechaSolicitud { get; set; }

    public int RequisitoLegal { get; set; }

    public decimal? SaldoCiap { get; set; }

    public DateOnly FechaOtorgamiento { get; set; }

    public DateOnly? FechaInicioDevengue { get; set; }

    public decimal? MontoPensionMensual { get; set; }

    public decimal? MontoTotalDevolucionAsignacion { get; set; }

    public int TiempoCotizado { get; set; }

    public decimal? PensionCalculada { get; set; }

    public bool? GarantiaEstado { get; set; }

    public bool? PensionLongevidad { get; set; }

    public decimal? PensionSinHacienda { get; set; }

    public decimal? MontoPensionHacienda { get; set; }

    public string? NumeroDocumentoBeneficiario { get; set; }

    public int? IdTipoDocumentoBeneficiario { get; set; }

    public decimal? Sbr { get; set; }

    public decimal? PensionReferencia { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Genero GeneroNavigation { get; set; } = null!;

    public virtual Afiliado? IdAfiliadoNavigation { get; set; }

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;

    public virtual Parentesco? IdParentescoNavigation { get; set; }

    public virtual TipoDocumentoBeneficiario? IdTipoDocumentoBeneficiarioNavigation { get; set; }

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;
}
