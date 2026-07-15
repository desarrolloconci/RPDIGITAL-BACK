using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using ValoresData.Services.ServicesInterfaces;

namespace ValoresData.Services
{
    public class MailQueue : IMailQueue
    {
        private readonly Channel<EmailJob> _channel = Channel.CreateUnbounded<EmailJob>();

        public ValueTask QueueEmailAsync(string subject, string to, string body)
        {
            return _channel.Writer.WriteAsync(new EmailJob(subject, to, body));
        }

        public async IAsyncEnumerable<EmailJob> DequeueAllAsync([EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (await _channel.Reader.WaitToReadAsync(cancellationToken))
            {
                while (_channel.Reader.TryRead(out var job))
                {
                    yield return job;
                }
            }
        }
    }
}
