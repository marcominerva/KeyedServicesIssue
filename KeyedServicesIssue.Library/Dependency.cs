namespace KeyedServicesIssue.Library;

public interface IDependency
{
    string Name { get; }
}

public class Dependency1 : IDependency
{
    public string Name => nameof(Dependency1);
}

public class Dependency2 : IDependency
{
    public string Name => nameof(Dependency2);
}

public class Dependency3 : IDependency
{
    public string Name => nameof(Dependency3);
}
