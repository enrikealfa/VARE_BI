using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class CargaArchivoDetalle
{
    public long IdDetalle { get; set; }

    public int IdCargaArchivo { get; set; }

    public int NumeroLinea { get; set; }

    public string Estado { get; set; } = null!;

    public string? MensajeError { get; set; }

    public virtual CargaArchivo IdCargaArchivoNavigation { get; set; } = null!;
}
