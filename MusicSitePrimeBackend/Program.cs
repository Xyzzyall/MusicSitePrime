using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicSitePrimeBackend.Configurations;
using MusicSitePrimeBackend.Domain;
using MusicSitePrimeBackend.Domain.Data;
using MusicSitePrimeBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints();
builder.Services.AddSwaggerDoc();

builder.Services.Configure<MongoDbConfiguration>(
    builder.Configuration.GetSection(nameof(MongoDbConfiguration))
);

builder.Services.Configure<JwtConfiguration>(
    builder.Configuration.GetSection(nameof(JwtConfiguration))
);

builder.Services.AddSingleton<MongoDbInstance>();

builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddTransient<UnitOfWork>();


var jwt_configuration = new JwtConfiguration();
builder.Configuration.Bind(nameof(JwtConfiguration), jwt_configuration);
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                jwt_configuration.GetSecretInBytes()
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };
    });

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints();
app.UseOpenApi();
app.UseSwaggerUi3(x => x.ConfigureDefaults());

app.MapGet("/", () => "Hello World!");

app.Run();