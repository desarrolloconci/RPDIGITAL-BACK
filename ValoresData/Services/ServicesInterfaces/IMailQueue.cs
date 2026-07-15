using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface IMailQueue
    {
        ValueTask QueueEmailAsync(string subject, string to, string body);
        IAsyncEnumerable<EmailJob> DequeueAllAsync(CancellationToken cancellationToken);
    }

    public record EmailJob(string Subject, string To, string Body);
}
