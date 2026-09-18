namespace SSF.PortalBI.Services
{
    public interface ICargaArchivoProcesadorService
    {
        Task ProcesarAsync(int idCargaArchivo, CancellationToken cancellationToken);
    }
}