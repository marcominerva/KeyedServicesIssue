using KeyedServicesIssue.Library;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddKeyedScoped<IDependency, Dependency1>("dependency");

// Creates an IEnumerable<IDpendency> without keys.
builder.Services.AddScoped<IDependency, Dependency2>();
builder.Services.AddScoped<IDependency, Dependency3>();

builder.Services.AddScoped<ISampleService, SampleService>();

builder.Services.AddSingleton<SampleConfig>();
builder.Services.AddScoped<OtherSampleService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.MapGet("api/dependency", ([FromKeyedServices("x")] IDependency dependency) =>
{
    return TypedResults.Ok(dependency.Name);
})
.WithOpenApi(operation =>
{
    operation.Summary = "This endpoint requires an IDependency instance. The application registers <IDependency, Dependency1> with key 'dependency', and an IEnumerable created by adding both <IDependency, Dependency2> and <IDependency, Dependency3> with no key. The endpoint contains the [FromKeyedServices(\"x\")] attribute, so invoking it throws an exception.";
    return operation;
});

app.MapGet("api/external-dependency", (ISampleService sampleService) =>
{
    return TypedResults.Ok(sampleService.DependencyName);
})
.WithOpenApi(operation =>
{
    operation.Summary = "This endpoint requires an ISampleService instance, that in turns requires an IDependency instance. The application registers <IDependency, Dependency1> with key 'dependency', and an IEnumerable created by adding both <IDependency, Dependency2> and <IDependency, Dependency3> with no key. SampleService constructor contains the [FromKeyedServices(\"x\")], but in this case we don't get any exception: instead, the last IDependency registered instance with no key is resolved (Dependency3).";
    return operation;
});

app.MapGet("api/sample-config", ([FromKeyedServices("x")] SampleConfig config) =>
{
    return TypedResults.Ok(config.Value);
})
.WithOpenApi(operation =>
{
    operation.Summary = "This endpoint requires a SampleConfig instance. SampleConfig has been registered with no key, but the endpoint contains the [FromKeyedServices(\"x\")] attribute, so invoking it throws an exception.";
    return operation;
});

app.MapGet("api/external-sample-config", (OtherSampleService otherSampleService) =>
{
    return TypedResults.Ok(otherSampleService.ConfigValue);
})
.WithOpenApi(operation =>
{
    operation.Summary = "This endpoint requires an OtherSampleService instance, that in turns requires a SampleConfig instance. SampleConfig has been registered with no key, and OtherSampleService construnctor contains the [FromKeyedServices(\"x\")] attribute, but in this case we don't get any exception: instead, the SampleConfig registered with no key is resolved. See also the OtherSampleService constructor for another issue that regards the ServiceProvider.";
    return operation;
});

app.Run();