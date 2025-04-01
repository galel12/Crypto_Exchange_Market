using crypto.Startup;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load(); // Load environment variables

// Register all application services
builder.Services
    .ConfigureServices()
    .ConfigureAuthentication(builder.Configuration)
    .ConfigureCors()
    .ConfigureSwagger();

var app = builder.Build();

// Configure Middleware Pipeline
app.ConfigureMiddleware();

app.Run();