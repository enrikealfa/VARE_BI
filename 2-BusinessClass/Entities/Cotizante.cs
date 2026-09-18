using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Cotizante
{
    public long IdCotizante { get; set; }

    public string NumeroDocumento { get; set; } = null!;

    public int IdTipoDocumento { get; set; }

    public int? IdAfiliado { get; set; }

    public string NitEmpleador { get; set; } = null!;

    public int? IdEmpleador { get; set; }

    public decimal Ibc { get; set; }

    public int PeriodoDevengue { get; set; }

    public int IdPlanilla { get; set; }

    public int IdPagoPlanilla { get; set; }

    public string SituacionLaboral { get; set; } = null!;

    public string CodigoCentroTrabajo { get; set; } = null!;

    public long NumeroPlanilla { get; set; }

    public int IdTipoPlanilla { get; set; }

    public int IdTipoCotizante { get; set; }

    public int? UltimoPeriodoDevengueCotizado { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Afiliado? IdAfiliadoNavigation { get; set; }

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;

    public virtual Empleador? IdEmpleadorNavigation { get; set; }

    public virtual PagoPlanilla IdPagoPlanillaNavigation { get; set; } = null!;

    public virtual Planilla IdPlanillaNavigation { get; set; } = null!;

    public virtual TipoCotizante IdTipoCotizanteNavigation { get; set; } = null!;

    public virtual TipoDocumento IdTipoDocumentoNavigation { get; set; } = null!;

    public virtual TipoPlanilla IdTipoPlanillaNavigation { get; set; } = null!;

    public virtual SituacionLaboral SituacionLaboralNavigation { get; set; } = null!;
}
