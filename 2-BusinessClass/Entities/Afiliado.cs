using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Afiliado
{
    public int IdAfiliado { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public int IdTipoDocumento { get; set; }

    public string PrimerNombre { get; set; } = null!;

    public string? SegundoNombre { get; set; }

    public string PrimerApellido { get; set; } = null!;

    public string? SegundoApellido { get; set; }

    public string? ApellidoCasada { get; set; }

    public string? ConocidoPor { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string Genero { get; set; } = null!;

    public string EstadoFamiliar { get; set; } = null!;

    public string? Nup { get; set; }

    public string? Dui { get; set; }

    public string? Cip { get; set; }

    public string? CarneResidente { get; set; }

    public string? Pasaporte { get; set; }

    public string? CarneMinoridad { get; set; }

    public string? Isss { get; set; }

    public string? Inpep { get; set; }

    public string? Nit { get; set; }

    public string? TipoSolicitudAfiliacion { get; set; }

    public string? NumeroSolicitudAfiliacion { get; set; }

    public DateOnly? FechaDocumAfiliacion { get; set; }

    public DateOnly? FechaAfiliacion { get; set; }

    public string EstadoAfiliado { get; set; } = null!;

    public DateOnly? FechaFallecimiento { get; set; }

    public string CodigoPais { get; set; } = null!;

    public int IdTipoSistema { get; set; }

    public string? TipoAfiliado { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();

    public virtual EstadoFamiliar EstadoFamiliarNavigation { get; set; } = null!;

    public virtual Genero GeneroNavigation { get; set; } = null!;

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;

    public virtual TipoSistema IdTipoSistemaNavigation { get; set; } = null!;

    public virtual ICollection<PagoBeneficio> PagoBeneficios { get; set; } = new List<PagoBeneficio>();

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();
}
