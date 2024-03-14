using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Common;
using Data;
using Data.Contracts;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using Services.Services;
using System.Net;
using WebFramework.Configuration;
using WebFramework.Middlewares;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

var _siteSettings = new SiteSettings();
builder.Configuration.GetSection("SiteSettings").Bind(_siteSettings);

// Add services to the container.

try
{

    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Register services
    builder.Services.AddControllers();
    builder.Services.AddDbContext(builder.Configuration);
    builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

    //Register dependency Injection services
    builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        container.AddServices();
    });

    //Register Authentication services
    builder.Services.AddCustomIdentity(_siteSettings.IdentitySettings);
    builder.Services.AddJwtAuthentication(_siteSettings.JwtSettings);

    //builder.Services.AddElmah(builder.Configuration, _siteSettings);

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseCustomExceptionHandler();

    //app.UseElmah();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers().RequireAuthorization();

    app.Run();

}
catch (Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Flush();
    LogManager.Shutdown();
}