using Auth.Api.Middlewares;
using Auth.Application;
using Auth.Configuration;
using Auth.Domain.Enums;
using Auth.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();

var bankAuthenticationScheme = ServiceAudience.Bank.ToString();

builder.Services.AddAuthentication()
    .AddJwtBearer(bankAuthenticationScheme, options =>
    {
        options.TokenValidationParameters = JwtParameters.ValidationParametersForBankService();
    });

builder.Services.AddAuthorization(options =>
{
    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
        bankAuthenticationScheme);

    defaultAuthorizationPolicyBuilder =
        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            },
            new List<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
});

builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration);


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
