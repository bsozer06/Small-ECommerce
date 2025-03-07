﻿using ECommerceAPI.API.Extensions;
using ECommerceAPI.API.Filters;
using ECommerceAPI.Application;
using ECommerceAPI.Application.Validators.Products;
using ECommerceAPI.Infrastructure;
using ECommerceAPI.Infrastructure.Filters;
using ECommerceAPI.Infrastructure.Services.Storage.Azure;
using ECommerceAPI.Persistance;
using ECommerceAPI.SignalR;
using ETicaretAPI.API.Configurations.ColumnWriters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using NpgsqlTypes;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Client'dan gelen request neticesinde olusturulan HttpContext nesenesini katmanlardaki class'lar uzeirnden
// businesslogic erişebilmemizi sağlayan bir serbvistir.
builder.Services.AddHttpContextAccessor();    

builder.Services.AddPersistanceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

builder.Services.AddStorage<ECommerceAPI.Infrastructure.Services.Storage.Local.LocalStorage>();
//builder.Services.AddStorage<AzureStorage>();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy
        .WithOrigins("http://localhost:4200", "https://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
));

#region Serilog

Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(builder.Configuration.GetConnectionString("PostgreSQL"), "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter(NpgsqlDbType.Text)},
            {"message_template", new MessageTemplateColumnWriter(NpgsqlDbType.Text)},
            {"level", new LevelColumnWriter(true , NpgsqlDbType.Varchar)},
            {"time_stamp", new TimestampColumnWriter(NpgsqlDbType.Timestamp)},
            {"exception", new ExceptionColumnWriter(NpgsqlDbType.Text)},
            {"log_event", new LogEventSerializedColumnWriter(NpgsqlDbType.Json)},
            {"user_name", new UsernameColumnWriter()}
        })
    .WriteTo.Seq(builder.Configuration["Seq:ServerURL"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);

#endregion

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<ValidationFilter>();
    opts.Filters.Add<RolePermissionFilter>();
})
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true, // Olusturulacak token degerini kimlerin/hangi originlerin/sitelerin kullanici belirledigimiz degerdir. -> www.bilmemne.com
            ValidateIssuer = true, // Olusturulacak token degerini kimin dagittigi ifade edecegimiz alandir. -> www.myapi.com
            ValidateLifetime = true, // Olusturulan token degerini süresini kontrol edecek olan dogrulamadir.
            ValidateIssuerSigningKey = true, // Üretilecek token degerinin ugulamamiza ait bir deger oldugunu ifade eden secury key verisinin dogrulanmasidir.

            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters)
                => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name     // JWT üzerinde Name claim'ine karşılık gelen degeri
                                                // User.Identity.Property 'den elde edebiliriz.
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

// wwwroot dosyasının kullanımı için
app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseCors();

app.UseHttpsRedirection();
app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

// Serilog db tablosuna kullanıcı bilgileirni middleware üzeirnden eklenmesi !!!
app.Use(async (context, next) =>
{
    var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();
