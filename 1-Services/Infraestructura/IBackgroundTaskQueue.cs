namespace SSF.PortalBI.Services.Infraestructura
{
    public interface IBackgroundTaskQueue
    {
        ValueTask EncolarAsync(Func<IServiceProvider, CancellationToken, ValueTask> workItem);
        ValueTask<Func<IServiceProvider, CancellationToken, ValueTask>> DesencolarAsync(CancellationToken cancellationToken);
    }
}