using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SSF.PortalBI.Services.Infraestructura
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IServiceProvider _serviceProvider;

        public QueuedHostedService(IBackgroundTaskQueue taskQueue, IServiceProvider serviceProvider)
        {
            _taskQueue = taskQueue;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DesencolarAsync(stoppingToken);
                using var scope = _serviceProvider.CreateScope();
                try
                {
                    await workItem(scope.ServiceProvider, stoppingToken);
                }
                catch
                {
                    // TODO: registrar en log cuando integremos Serilog.
                }
            }
        }
    }
}