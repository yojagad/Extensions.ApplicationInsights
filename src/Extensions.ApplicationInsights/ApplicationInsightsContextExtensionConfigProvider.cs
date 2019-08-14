using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Description;
using Microsoft.Azure.WebJobs.Host.Config;
using Newtonsoft.Json.Linq;

namespace Extensions.ApplicationInsights
{
    [Extension(nameof(ApplicationInsightsContext))]
    internal class ApplicationInsightsContextExtensionConfigProvider : IExtensionConfigProvider
    {
        private readonly TelemetryConfiguration _telemetryConfig;

        public ApplicationInsightsContextExtensionConfigProvider(TelemetryConfiguration telemetryConfiguration)
        {
            _telemetryConfig = telemetryConfiguration;
        }

        void IExtensionConfigProvider.Initialize(ExtensionConfigContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.AddConverter<JObject, ApplicationInsightsTag>(input => input.ToObject<ApplicationInsightsTag>());
            context.AddConverter<ApplicationInsightsTag, JObject>(input => JObject.FromObject(input));

            var rule = context.AddBindingRule<ApplicationInsightsContextAttribute>();

            rule.BindToCollector(BuildCollector);

            // rule.BindToInput<ApplicationInsightsTag>(new ApplicationInsightsTag { Id = Activity.Current.Id });
            rule.BindToInput(BuildItemFromAttribute);

            /*context.AddBindingRule<ApplicationInsightsContextAttribute>()
                                .BindToCollector(BuildCollector);*/


            // rule.BindToCollector(BuildCollector);

            /*context.AddBindingRule<ApplicationInsightsContextAttribute>()
                .AddConverter<ApplicationInsightsContext, IAsyncCollector<KeyValuePair<string, string>>>(c => c as IAsyncCollector<KeyValuePair<string, string>>)
                .BindToCollector(_ =>
                 {
                     return new ApplicationInsightsContext
                     {
                         InstrumentationKey = _telemetryConfig.InstrumentationKey,
                         OperationId = Activity.Current.Id
                     };
                 });*/
        }

        private ApplicationInsightsTag BuildItemFromAttribute(ApplicationInsightsContextAttribute arg)
        {
            return new ApplicationInsightsTag { Id = Activity.Current.Id, Tags = new List<Tag>() };
        }

        private IAsyncCollector<ApplicationInsightsTag> BuildCollector(ApplicationInsightsContextAttribute attribute)
        {
            return new ApplicationInsightsContext
            {
                InstrumentationKey = _telemetryConfig.InstrumentationKey,
            };
        }
    }
}
