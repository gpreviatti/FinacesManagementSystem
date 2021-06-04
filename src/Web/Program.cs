using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Web App unexpectedly closed");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settings = config.Build();
                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.MSSqlServer(
                            "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=FmsDB",
                            restrictedToMinimumLevel: LogEventLevel.Warning,
                            sinkOptions: new MSSqlServerSinkOptions()
                            {
                                AutoCreateSqlTable = true,
                                TableName = "Logs"
                            })
                        .WriteTo.Debug(
                            outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message}{NewLine}{Exception}",
                            restrictedToMinimumLevel: LogEventLevel.Information
                         )
                        .WriteTo.Console(
                            outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message} {NewLine}{Exception}",
                            restrictedToMinimumLevel: LogEventLevel.Information
                        )
                        .CreateLogger();
                })
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
