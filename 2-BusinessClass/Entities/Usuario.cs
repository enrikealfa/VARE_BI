using System;
using System.Collections.Generic;

namespace SSF.PortalBI.BusinessClass.Entities;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public byte[] ClaveHash { get; set; } = null!;

    public byte[] ClaveSalt { get; set; } = null!;

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? FechaUltimoAcceso { get; set; }

    public int IntentosFallidos { get; set; }

    public bool Bloqueado { get; set; }

    public virtual ICollection<CargaArchivo> CargaArchivos { get; set; } = new List<CargaArchivo>();

    public virtual ICollection<Rol> IdRols { get; set; } = new List<Rol>();
}
