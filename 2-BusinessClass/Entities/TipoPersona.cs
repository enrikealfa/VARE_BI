using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class TipoPersona
{
    public int IdTipoPersona { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Empleador> Empleadors { get; set; } = new List<Empleador>();
}
