using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class ReintegroAnticipo
{
    public long IdReintegro { get; set; }

    public string NumeroUnicoPrevisional { get; set; } = null!;

    public string? Dui { get; set; }

    public string NumeroSolicitud { get; set; } = null!;

    public string NumeroExpediente { get; set; } = null!;

    public DateOnly? FechaReintegro { get; set; }

    public decimal? PorcentajeReintegro { get; set; }

    public decimal? NumeroCuotasReintegro { get; set; }

    public decimal? MontoReintegro { get; set; }

    public decimal? ValorCuotaReintegro { get; set; }

    public bool ExencionReintegrar { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;
}
