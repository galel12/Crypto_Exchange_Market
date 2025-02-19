using crypto.Startup;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load(); // Load environment variables

var postgresConnection = Environment.GetEnvironmentVariable("PostgresConnection") 
    ?? throw new ArgumentNullException("PostgresConnection environment variable is missing.");

// Register all application services
builder.Services
    .ConfigureServices(postgresConnection)
    .ConfigureAuthentication(builder.Configuration)
    .ConfigureCors()
    .ConfigureSwagger();

var app = builder.Build();

// Configure Middleware Pipeline
app.ConfigureMiddleware();

app.Run();