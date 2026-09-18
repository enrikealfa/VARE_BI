using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class SituacionLaboral
{
    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();
}
