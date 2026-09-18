using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class PagoPlanilla
{
    public int IdPagoPlanilla { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();
}
