using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class EstadoFamiliar
{
    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Afiliado> Afiliados { get; set; } = new List<Afiliado>();
}
