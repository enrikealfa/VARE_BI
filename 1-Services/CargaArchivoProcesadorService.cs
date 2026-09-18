using System.Data;
using System.IO.Compression;
using System.Xml;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SSF.PortalBI.BusinessClass.Context;
using SSF.PortalBI.BusinessClass.Entities;

namespace SSF.PortalBI.Services
{
    public class CargaArchivoProcesadorService : ICargaArchivoProcesadorService
    {
        private const int TamanoLote = 2000;
        private const string XsiNamespace = "http://www.w3.org/2001/XMLSchema-instance";

        private readonly SsfBcpeBiContext _context;
        private readonly string _connectionString;

        public CargaArchivoProcesadorService(SsfBcpeBiContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task ProcesarAsync(int idCargaArchivo, CancellationToken cancellationToken)
        {
            var carga = await _context.CargaArchivos.FindAsync(new object?[] { idCargaArchivo }, cancellationToken);
            if (carga == null) return;

            carga.Estado = "PROCESANDO";
            await _context.SaveChangesAsync(cancellationToken);

            int total = 0, correctos = 0, errores = 0;

            try
            {
                using var xmlStream = AbrirStreamXml(carga.RutaArchivo!);

                switch (carga.TipoEntidad)
                {
                    case "afiliado":
                        (total, correctos, errores) = await ProcesarAfiliadoAsync(xmlStream, idCargaArchivo, cancellationToken);
                        break;

                    default:
                        throw new NotSupportedException($"El tipo de entidad '{carga.TipoEntidad}' aun no tiene parseador implementado.");
                }

                carga.Estado = errores > 0 ? "COMPLETADO_CON_ERRORES" : "COMPLETADO";
            }
            catch (Exception ex)
            {
                carga.Estado = "ERROR";
                _context.CargaArchivoDetalles.Add(new CargaArchivoDetalle
                {
                    IdCargaArchivo = idCargaArchivo,
                    NumeroLinea = 0,
                    Estado = "ERROR",
                    MensajeError = $"Error general de procesamiento: {ex.Message}"
                });
            }

            carga.TotalRegistros = total;
            carga.RegistrosCorrectos = correctos;
            carga.RegistrosError = errores;
            carga.FechaFinProceso = DateTime.Now;
            await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Abre el stream del XML: si el archivo es .zip, toma la primera entrada .xml que encuentre dentro.
        /// </summary>
        private static Stream AbrirStreamXml(string rutaArchivo)
        {
            var extension = Path.GetExtension(rutaArchivo).ToLowerInvariant();

            if (extension == ".zip")
            {
                var archivoZip = ZipFile.OpenRead(rutaArchivo);
                var entrada = archivoZip.Entries.FirstOrDefault(e => e.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    ?? throw new InvalidOperationException("El ZIP no contiene ningun archivo .xml.");
                return entrada.Open();
            }

            return new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1_048_576, useAsync: true);
        }

        private async Task<(int total, int correctos, int errores)> ProcesarAfiliadoAsync(Stream xmlStream, int idCargaArchivo, CancellationToken cancellationToken)
        {
            int total = 0, correctos = 0, errores = 0;
            var tabla = CrearTablaAfiliado();

            using var reader = XmlReader.Create(xmlStream, new XmlReaderSettings { Async = true, IgnoreWhitespace = true });

            while (await reader.ReadAsync())
            {
                if (reader.NodeType != XmlNodeType.Element || reader.LocalName != "afiliado")
                    continue;

                total++;
                using var subtree = reader.ReadSubtree();
                await subtree.ReadAsync();
                var campos = LeerCampos(subtree);

                var mensajeError = ConstruirFilaAfiliado(tabla, campos, idCargaArchivo);
                if (mensajeError == null)
                {
                    correctos++;
                }
                else
                {
                    errores++;
                    _context.CargaArchivoDetalles.Add(new CargaArchivoDetalle
                    {
                        IdCargaArchivo = idCargaArchivo,
                        NumeroLinea = total,
                        Estado = "ERROR",
                        MensajeError = mensajeError
                    });
                }

                if (tabla.Rows.Count >= TamanoLote)
                {
                    await InsertarLoteAsync(tabla, "bcpe.afiliado", cancellationToken);
                    tabla.Clear();
                    await ActualizarProgresoAsync(idCargaArchivo, total, correctos, errores, cancellationToken);
                }
            }

            if (tabla.Rows.Count > 0)
                await InsertarLoteAsync(tabla, "bcpe.afiliado", cancellationToken);

            if (errores > 0)
                await _context.SaveChangesAsync(cancellationToken);

            return (total, correctos, errores);
        }

        /// <summary>
        /// Lee todos los campos hijos del elemento actual como pares nombre-valor.
        /// Respeta xsi:nil="true" devolviendo null en vez de cadena vacia.
        /// </summary>
        private static Dictionary<string, string?> LeerCampos(XmlReader subtree)
        {
            var campos = new Dictionary<string, string?>();

            if (subtree.NodeType == XmlNodeType.Element)
                subtree.ReadStartElement();

            while (subtree.NodeType == XmlNodeType.Element)
            {
                var nombre = subtree.LocalName;
                var esNulo = subtree.GetAttribute("nil", XsiNamespace) == "true";
                var valor = subtree.ReadElementContentAsString();
                campos[nombre] = esNulo ? null : valor;
            }

            return campos;
        }

        private static DataTable CrearTablaAfiliado()
        {
            var tabla = new DataTable();
            tabla.Columns.Add("numero_documento", typeof(string));
            tabla.Columns.Add("id_tipo_documento", typeof(int));
            tabla.Columns.Add("primer_nombre", typeof(string));
            tabla.Columns.Add("segundo_nombre", typeof(string));
            tabla.Columns.Add("primer_apellido", typeof(string));
            tabla.Columns.Add("segundo_apellido", typeof(string));
            tabla.Columns.Add("apellido_casada", typeof(string));
            tabla.Columns.Add("conocido_por", typeof(string));
            tabla.Columns.Add("fecha_nacimiento", typeof(DateTime));
            tabla.Columns.Add("genero", typeof(string));
            tabla.Columns.Add("estado_familiar", typeof(string));
            tabla.Columns.Add("nup", typeof(string));
            tabla.Columns.Add("dui", typeof(string));
            tabla.Columns.Add("cip", typeof(string));
            tabla.Columns.Add("carne_residente", typeof(string));
            tabla.Columns.Add("pasaporte", typeof(string));
            tabla.Columns.Add("carne_minoridad", typeof(string));
            tabla.Columns.Add("isss", typeof(string));
            tabla.Columns.Add("inpep", typeof(string));
            tabla.Columns.Add("nit", typeof(string));
            tabla.Columns.Add("tipo_solicitud_afiliacion", typeof(string));
            tabla.Columns.Add("numero_solicitud_afiliacion", typeof(string));
            tabla.Columns.Add("fecha_docum_afiliacion", typeof(DateTime));
            tabla.Columns.Add("fecha_afiliacion", typeof(DateTime));
            tabla.Columns.Add("estado_afiliado", typeof(string));
            tabla.Columns.Add("fecha_fallecimiento", typeof(DateTime));
            tabla.Columns.Add("codigo_pais", typeof(string));
            tabla.Columns.Add("id_tipo_sistema", typeof(int));
            tabla.Columns.Add("tipo_afiliado", typeof(string));
            tabla.Columns.Add("id_carga_archivo", typeof(int));
            tabla.Columns.Add("fecha_creacion", typeof(DateTime));
            return tabla;
        }

        /// <summary>
        /// Convierte los campos crudos del XML a una fila tipada. Devuelve null si todo fue valido,
        /// o el mensaje de error si algun campo obligatorio falta o no se pudo convertir.
        /// </summary>
        private static string? ConstruirFilaAfiliado(DataTable tabla, Dictionary<string, string?> c, int idCargaArchivo)
        {
            var fila = tabla.NewRow();
            try
            {
                fila["numero_documento"] = Requerido(c, "numero_documento");
                fila["id_tipo_documento"] = int.Parse(Requerido(c, "tipo_documento"));
                fila["primer_nombre"] = Requerido(c, "primer_nombre");
                fila["segundo_nombre"] = Opcional(c, "segundo_nombre");
                fila["primer_apellido"] = Requerido(c, "primer_apellido");
                fila["segundo_apellido"] = Opcional(c, "segundo_apellido");
                fila["apellido_casada"] = Opcional(c, "apellido_casada");
                fila["conocido_por"] = Opcional(c, "conocido_por");
                fila["fecha_nacimiento"] = OpcionalFecha(c, "fecha_nacimiento");
                fila["genero"] = Requerido(c, "genero");
                fila["estado_familiar"] = Requerido(c, "estado_familiar");
                fila["nup"] = Opcional(c, "nup");
                fila["dui"] = Opcional(c, "dui");
                fila["cip"] = Opcional(c, "cip");
                fila["carne_residente"] = Opcional(c, "carne_residente");
                fila["pasaporte"] = Opcional(c, "pasaporte");
                fila["carne_minoridad"] = Opcional(c, "carne_minoridad");
                fila["isss"] = Opcional(c, "isss");
                fila["inpep"] = Opcional(c, "inpep");
                fila["nit"] = Opcional(c, "nit");
                fila["tipo_solicitud_afiliacion"] = Opcional(c, "tipo_solicitud_afiliacion");
                fila["numero_solicitud_afiliacion"] = Opcional(c, "numero_solicitud_afiliacion");
                fila["fecha_docum_afiliacion"] = OpcionalFecha(c, "fecha_docum_afiliacion");
                fila["fecha_afiliacion"] = OpcionalFecha(c, "fecha_afiliacion");
                fila["estado_afiliado"] = Requerido(c, "estado_afiliado");
                fila["fecha_fallecimiento"] = OpcionalFecha(c, "fecha_fallecimiento");
                fila["codigo_pais"] = Requerido(c, "codigo_pais");
                fila["id_tipo_sistema"] = int.Parse(Requerido(c, "tipo_sistema"));
                fila["tipo_afiliado"] = Opcional(c, "tipo_afiliado");
                fila["id_carga_archivo"] = idCargaArchivo;
                fila["fecha_creacion"] = DateTime.Now;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            tabla.Rows.Add(fila);
            return null;
        }

        private static string Requerido(Dictionary<string, string?> c, string campo)
        {
            if (!c.TryGetValue(campo, out var valor) || string.IsNullOrWhiteSpace(valor))
                throw new InvalidOperationException($"Campo obligatorio '{campo}' vacio o ausente.");
            return valor;
        }

        private static object Opcional(Dictionary<string, string?> c, string campo)
        {
            return c.TryGetValue(campo, out var valor) && valor != null ? valor : DBNull.Value;
        }

        private static object OpcionalFecha(Dictionary<string, string?> c, string campo)
        {
            if (!c.TryGetValue(campo, out var valor) || valor == null) return DBNull.Value;
            if (!DateTime.TryParse(valor, out var fecha))
                throw new InvalidOperationException($"Fecha invalida en '{campo}': '{valor}'.");
            return fecha;
        }

        private async Task InsertarLoteAsync(DataTable tabla, string tablaDestino, CancellationToken cancellationToken)
        {
            using var conexion = new SqlConnection(_connectionString);
            await conexion.OpenAsync(cancellationToken);

            using var bulkCopy = new SqlBulkCopy(conexion)
            {
                DestinationTableName = tablaDestino,
                BulkCopyTimeout = 300
            };

            foreach (DataColumn columna in tabla.Columns)
                bulkCopy.ColumnMappings.Add(columna.ColumnName, columna.ColumnName);

            await bulkCopy.WriteToServerAsync(tabla, cancellationToken);
        }

        private async Task ActualizarProgresoAsync(int idCargaArchivo, int total, int correctos, int errores, CancellationToken cancellationToken)
        {
            var carga = await _context.CargaArchivos.FindAsync(new object?[] { idCargaArchivo }, cancellationToken);
            if (carga == null) return;

            carga.TotalRegistros = total;
            carga.RegistrosCorrectos = correctos;
            carga.RegistrosError = errores;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}