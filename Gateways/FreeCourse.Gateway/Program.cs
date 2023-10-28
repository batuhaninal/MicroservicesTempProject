using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.IdentityModel.Tokens.Jwt;
using FreeCourse.Gateway.DelegateHandlers;

var builder = WebApplication.CreateBuilder(args);

// sub degerini otomatik olarak nameidentifier claime mapleme ozelligini devre disi biraktik
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme",opt =>
{
    // Token alinacak service url
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "resource_gateway";
    opt.RequireHttpsMetadata = false;
});

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName}.json").AddEnvironmentVariables(); 
});

builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();

var app = builder.Build();

await app.UseOcelot();

app.Run();
