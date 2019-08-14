using System;
using Microsoft.Azure.WebJobs.Description;

namespace Extensions.ApplicationInsights
{
    [AttributeUsage(AttributeTargets.All)]
    [Binding]
    public class ApplicationInsightsContextAttribute : Attribute
    {
    }
}
