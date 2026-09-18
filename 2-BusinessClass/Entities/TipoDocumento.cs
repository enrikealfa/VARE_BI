using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class TipoDocumento
{
    public int IdTipoDocumento { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Afiliado> Afiliados { get; set; } = new List<Afiliado>();

    public virtual ICollection<Cotizante> Cotizantes { get; set; } = new List<Cotizante>();

    public virtual ICollection<PagoBeneficio> PagoBeneficios { get; set; } = new List<PagoBeneficio>();

    public virtual ICollection<Prestacion> Prestacions { get; set; } = new List<Prestacion>();
}
