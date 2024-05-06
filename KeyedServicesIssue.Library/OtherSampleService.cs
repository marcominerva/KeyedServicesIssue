using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace KeyedServicesIssue.Library;

public class OtherSampleService
{
    private readonly SampleConfig config;

    public OtherSampleService([FromKeyedServices("x")] SampleConfig config, IServiceProvider serviceProvider)
    {
        this.config = config;

        // This will returns null because SampleConfig has been registered with no key.
        // However, the dependency in the constructor has been resolved using the no key registration.
        var configFromServiceProvider = serviceProvider.GetKeyedService<SampleConfig>("x");
        Debug.Assert(configFromServiceProvider is not null, "sampleConfig is null");
    }

    public string ConfigValue => config.Value.ToString();
}
