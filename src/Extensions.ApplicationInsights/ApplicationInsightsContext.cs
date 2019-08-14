using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace Extensions.ApplicationInsights
{
    public class ApplicationInsightsContext : IAsyncCollector<ApplicationInsightsTag>
    {
        public string InstrumentationKey { get; set; }

        public string OperationId { get; set; }

        public Task AddAsync(ApplicationInsightsTag item, CancellationToken cancellationToken = default)
        {
            foreach(Tag tag in item.Tags)
            {
                Activity.Current.AddTag(tag.Key, tag.Value);
            }

            return Task.CompletedTask;
        }

        public Task FlushAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }
    }
}
