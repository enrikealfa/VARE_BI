using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class TipoEmpleador
{
    public int IdTipoEmpleador { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Empleador> Empleadors { get; set; } = new List<Empleador>();
}
