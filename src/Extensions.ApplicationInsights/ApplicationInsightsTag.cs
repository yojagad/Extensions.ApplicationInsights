using Newtonsoft.Json;
using System.Collections.Generic;

namespace Extensions.ApplicationInsights
{
    public class ApplicationInsightsTag
    {
        public string Id { get; set; }

        public IEnumerable<Tag> Tags { get; set; }
    }

    public class Tag
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}