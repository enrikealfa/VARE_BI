using System.Threading.Channels;

namespace SSF.PortalBI.Services.Infraestructura
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue
    {
        private readonly Channel<Func<IServiceProvider, CancellationToken, ValueTask>> _queue;

        public BackgroundTaskQueue()
        {
            var options = new BoundedChannelOptions(100) { FullMode = BoundedChannelFullMode.Wait };
            _queue = Channel.CreateBounded<Func<IServiceProvider, CancellationToken, ValueTask>>(options);
        }

        public async ValueTask EncolarAsync(Func<IServiceProvider, CancellationToken, ValueTask> workItem)
        {
            await _queue.Writer.WriteAsync(workItem);
        }

        public async ValueTask<Func<IServiceProvider, CancellationToken, ValueTask>> DesencolarAsync(CancellationToken cancellationToken)
        {
            return await _queue.Reader.ReadAsync(cancellationToken);
        }
    }
}