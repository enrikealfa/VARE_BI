using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class MoraEmpleador
{
    public long IdMora { get; set; }

    public string Nit { get; set; } = null!;

    public int? IdEmpleador { get; set; }

    public string? PeriodoReporte { get; set; }

    public decimal MontoOmis { get; set; }

    public decimal MontoDnp { get; set; }

    public decimal MontoIns { get; set; }

    public decimal TotalMora { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;

    public virtual Empleador? IdEmpleadorNavigation { get; set; }
}
