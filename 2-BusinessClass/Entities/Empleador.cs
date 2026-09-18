using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Empleador
{
    public int IdEmpleador { get; set; }

    public string Nit { get; set; } = null!;

    public int IdTipoPersona { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? ApellidoCasada { get; set; }

    public string? RazonSocial { get; set; }

    public string? NumeroPatronal { get; set; }

    public int IdTipoEmpleador { get; set; }

    public string CodigoCentroTrabajo { get; set; } = null!;

    public string? NombreCentroTrabajo { get; set; }

    public string? NumeroPatronalCt { get; set; }

    public int IdCargaArchivo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;

    public virtual TipoEmpleador IdTipoEmpleadorNavigation { get; set; } = null!;

    public virtual TipoPersona IdTipoPersonaNavigation { get; set; } = null!;

    public virtual ICollection<MoraEmpleador> MoraEmpleadors { get; set; } = new List<MoraEmpleador>();
}
