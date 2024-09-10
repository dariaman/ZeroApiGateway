using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Zero.Gateway.Configuration;

namespace Zero.Gateway
{

    [ExcludeFromCodeCoverage]
    public static partial class Program
    {
        public static void Main(string[] args)
        {
            string ASPNETCORE_ENVIRONMENT = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Prod";
            IConfiguration _config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{ASPNETCORE_ENVIRONMENT}.json", true)
                .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
                .Build();

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddHttpLogging(opts =>
            {
                opts.LoggingFields = HttpLoggingFields.All;
                opts.MediaTypeOptions.AddText("application/javascript");
                opts.RequestBodyLogLimit = 8192;
                opts.ResponseBodyLogLimit = 8192;
                opts.CombineLogs = true;

                // Reverse proxy headers
                opts.RequestHeaders.Add("X-Real-IP");
                opts.RequestHeaders.Add("X-Forwarded-For");
                opts.RequestHeaders.Add("X-Forwarded-Host");
                opts.RequestHeaders.Add("X-Forwarded-Port");
                opts.RequestHeaders.Add("X-Forwarded-Proto");
                opts.RequestHeaders.Add("X-Forwarded-Scheme");
                opts.RequestHeaders.Add("X-Scheme");

                // Distributed tracing heades
                opts.RequestHeaders.Add("X-Request-ID");
                opts.RequestHeaders.Add("X-Trace");
            });

            // Add services to the container.

            builder.Services.RegisterDIConfig(_config);

            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    opt.JsonSerializerOptions.AllowTrailingCommas = true;
                    opt.JsonSerializerOptions.WriteIndented = true;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = null;
                    opt.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Host.UseSerilog((hostBuilderContext, service, loggerConfig) =>
            {
                var dbLog = service.GetRequiredService<IOptions<DBLogConfig>>().Value;
                var appConfig = service.GetRequiredService<IOptions<AppConfig>>().Value;

                loggerConfig
                    .ReadFrom.Configuration(hostBuilderContext.Configuration)
                    .Enrich.WithProperty("ENV", ASPNETCORE_ENVIRONMENT)
                    .Enrich.WithProperty("IPlocal", Dns.GetHostEntry(Dns.GetHostName()).AddressList)
                    .Enrich.WithProperty("Version", appConfig.Version)
                    .Enrich.WithProperty("ApplicationName", appConfig.ApplicationName)
                    .WriteTo.MongoDBBson($"mongodb://{dbLog.ServerAddress}:{dbLog.Port}/{dbLog.DatabaseName}", dbLog.ErrorLogCollection);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.EnvironmentName != "Prod")
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpLogging();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        } // end public static void Main(string[] args)

    }
}