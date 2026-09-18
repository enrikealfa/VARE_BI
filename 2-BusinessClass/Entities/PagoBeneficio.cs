using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class PagoBeneficio
{
    public long IdPagoBeneficio { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public int IdTipoDocumento { get; set; }

    public int? IdAfiliado { get; set; }

    public string CodigoBeneficiario { get; set; } = null!;

    public int TipoBeneficio { get; set; }

    public DateOnly FechaPago { get; set; }

    public decimal MontoPagado { get; set; }

    public DateOnly FechaInicioPago { get; set; }

    public DateOnly FechaFinPago { get; set; }

    public int IdFuenteFondos { get; set; }

    public int? IdNumeroAnualidad { get; set; }

    public decimal? NumeroCuotas { get; set; }

    public decimal? ValorCuotaResolucion { get; set; }

    public DateOnly? FechaValorCuotaResolucion { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Afiliado? IdAfiliadoNavigation { get; set; }

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;

    public virtual FuenteFondo IdFuenteFondosNavigation { get; set; } = null!;

    public virtual NumeroAnualidad? IdNumeroAnualidadNavigation { get; set; }

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;
}
