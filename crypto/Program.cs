using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.EntityFrameworkCore;
using crypto.Services;
using crypto.Repositories;
using crypto.Models;
using crypto.Data;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// enable async suffix in action names for controllers 
builder.Services.AddMvc(options =>
{
   options.SuppressAsyncSuffixInActionNames = false;
});

Env.Load(); // Loads variables from .env

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        builder => builder.WithOrigins("http://localhost:5173") // Frontend URL
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

var postgresConnection = Environment.GetEnvironmentVariable("PostgresConnection");

if (string.IsNullOrEmpty(postgresConnection))
{
    throw new ArgumentNullException("PostgresConnection environment variable is missing.");
}

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(postgresConnection)
           .LogTo(Console.WriteLine, LogLevel.Information));

//Add Dependencies injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
//builder.Services.AddSingleton<IUserRepository, InMemoryRep>();


builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

if (string.IsNullOrEmpty(jwtKey))
{
    throw new ArgumentNullException("JWT_SECRET_KEY environment variable is missing.");
}

// Configure JWT authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            )
        };
    });

// Add Authorization services
builder.Services.AddAuthorization(); // This line ensures that Authorization services are registered


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Ensure HTTPS redirection is enabled
app.UseExceptionHandler("/Error");
app.UseRouting();
app.UseCors("AllowFrontend"); // Enable CORS middleware here
app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization();  // Enable authorization middleware

app.MapControllers();

app.Run();