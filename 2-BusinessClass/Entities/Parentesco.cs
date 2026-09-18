using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Parentesco
{
    public int IdParentesco { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();
}
