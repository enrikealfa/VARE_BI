using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class NumeroAnualidad
{
    public int IdNumeroAnualidad { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<PagoBeneficio> PagoBeneficios { get; set; } = new List<PagoBeneficio>();
}
