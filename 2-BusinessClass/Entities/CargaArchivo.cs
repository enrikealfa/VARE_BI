using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class CargaArchivo
{
    public int IdCargaArchivo { get; set; }

    public string NombreArchivo { get; set; } = null!;

    public string TipoEntidad { get; set; } = null!;

    public string? PeriodoReporte { get; set; }

    public string? RutaArchivo { get; set; }

    public string? HashArchivo { get; set; }

    public int IdUsuarioCarga { get; set; }

    public DateTime FechaCarga { get; set; }

    public int? TotalRegistros { get; set; }

    public int? RegistrosCorrectos { get; set; }

    public int? RegistrosError { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaFinProceso { get; set; }

    public virtual ICollection<Afiliado> Afiliados { get; set; } = new List<Afiliado>();

    public virtual ICollection<CargaArchivoDetalle> CargaArchivoDetalles { get; set; } = new List<CargaArchivoDetalle>();

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();

    public virtual ICollection<Empleador> Empleadors { get; set; } = new List<Empleador>();

    public virtual Usuario IdUsuarioCargaNavigation { get; set; } = null!;

    public virtual ICollection<MoraEmpleador> MoraEmpleadors { get; set; } = new List<MoraEmpleador>();

    public virtual ICollection<PagoBeneficio> PagoBeneficios { get; set; } = new List<PagoBeneficio>();

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();

    public virtual ICollection<ReintegroAnticipo> ReintegroAnticipos { get; set; } = new List<ReintegroAnticipo>();
}
