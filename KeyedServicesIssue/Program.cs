var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKeyedScoped<TestService>("test1");
builder.Services.AddKeyedScoped<TestService>("test2");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/sample", ([FromKeyedServices("test1")] TestService testService) =>
{
    return TypedResults.Ok();
}).WithOpenApi(operation =>
{
    operation.Summary = "This endpoints uses a Keyed Service. Swagger incorrectly requires a body for the request";
    return operation;
});

app.Run();

public class TestService
{
    public void Foo() { }
}