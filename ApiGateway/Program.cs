using CozyCare.SharedKernel.Middlewares;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Load Ocelot configuration
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Add Ocelot
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

// Use global exception handler
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

// No need for Swagger or controllers in Gateway unless explicitly used
app.UseAuthorization();

// Must be the last middleware before Run
await app.UseOcelot();

app.Run();
