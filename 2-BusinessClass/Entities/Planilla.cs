using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Planilla
{
    public int IdPlanilla { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();
}
