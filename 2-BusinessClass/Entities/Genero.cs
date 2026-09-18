using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Genero
{
    public string Codigo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Afiliado> Afiliados { get; set; } = new List<Afiliado>();

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();
}
