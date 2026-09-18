using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.BusinessClass.Entities;
using SSF.PortalBI.BusinessClass.Security;

namespace SSF.PortalBI.BusinessClass.Seed
{
    public static class DbInitializer
    {
        public const string ROL_ADMINISTRADOR = "Administrador";
        public const string ROL_CONSULTA = "Consulta";

        public static void SeedUsuarioAdmin(SsfBcpeBiContext context)
        {
            if (!context.Rols.Any())
            {
                context.Rols.Add(new Rol { NombreRol = ROL_ADMINISTRADOR, Descripcion = "Acceso total: mantenimientos, carga de archivos, usuarios y reportes BI", Activo = true });
                context.Rols.Add(new Rol { NombreRol = ROL_CONSULTA, Descripcion = "Solo visualiza reportes y graficos BI, sin acceso a mantenimientos ni gestion de usuarios", Activo = true });
                context.SaveChanges();
            }

            if (!context.Usuarios.Any())
            {
                var (hash, salt) = PasswordHasher.HashPassword("Admin123!");
                var rolAdmin = context.Rols.First(r => r.NombreRol == ROL_ADMINISTRADOR);

                var admin = new Usuario
                {
                    NombreUsuario = "admin",
                    Correo = "admin@ssf.local",
                    NombreCompleto = "Administrador del Portal",
                    ClaveHash = hash,
                    ClaveSalt = salt,
                    Activo = true
                };
                admin.IdRols.Add(rolAdmin);

                context.Usuarios.Add(admin);
                context.SaveChanges();
            }
        }
    }
}