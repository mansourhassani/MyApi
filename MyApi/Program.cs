using Autofac.Core;
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

var _siteSetting = new SiteSettings();
builder.Configuration.GetSection("SiteSettings").Bind(_siteSetting);

// Add services to the container.

try
{

builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});
builder.Services.Configure<SiteSettings>(builder.Configuration.GetSection(nameof(SiteSettings)));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddJwtAuthentication(_siteSetting.JwtSetting);

//builder.Services.AddElmah<SqlErrorLog>(options =>
//{
//    options.Path = _siteSetting.ElmahPath;
//    options.ConnectionString = builder.Configuration.GetConnectionString("Elmah");
//    options.OnPermissionCheck = httpcontext =>
//    {
//        return true;
//    };
//});

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
catch(Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Flush();
    LogManager.Shutdown();
}