using Microsoft.Extensions.DependencyInjection;

namespace KeyedServicesIssue.Library;

public interface ISampleService
{
    string DependencyName { get; }
}

public class SampleService : ISampleService
{
    private readonly IDependency dependency;

    public SampleService([FromKeyedServices("x")] IDependency dependency)
    {
        this.dependency = dependency;
    }

    public string DependencyName => dependency.Name;
}