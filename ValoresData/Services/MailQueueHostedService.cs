using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ValoresData.Services.ServicesInterfaces;

namespace ValoresData.Services
{
    public class MailQueueHostedService : BackgroundService
    {
        private readonly IMailQueue _mailQueue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MailQueueHostedService> _logger;

        public MailQueueHostedService(IMailQueue mailQueue, IServiceScopeFactory scopeFactory, ILogger<MailQueueHostedService> logger)
        {
            _mailQueue = mailQueue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var job in _mailQueue.DequeueAllAsync(stoppingToken))
            {
                try
                {
                    using var scope = _scopeFactory.CreateScope();
                    var sendMailService = scope.ServiceProvider.GetRequiredService<ISendMailService>();
                    await sendMailService.SendEmailAsync(job.Subject, job.To, job.Body);
                    _logger.LogInformation("Mail enviado en background para {To}", job.To);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "No se pudo enviar el mail en background para {To}", job.To);
                }
            }
        }
    }
}
