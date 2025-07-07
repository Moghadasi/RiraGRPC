using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Rira.Core.DependencyInjection;
using Rira.Data;
using Rira.Grpc.Interceptors;

namespace Rira.Grpc
{
    /// <summary>
    /// 
    /// </summary>
    public static class HostingExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            var configuration = builder.Configuration;
            //var connectionString = configuration.GetConnectionString("DefaultConnection").EnsureUsable();
            //builder.Services.AddDbContext<RiraCommandContext>(o => o.ConfigDatabaseContext(connectionString));
            builder.Services.AddScoped<RiraCommandContext>(o =>
            {
                var options = new DbContextOptionsBuilder<RiraCommandContext>()
                    .EnableSensitiveDataLogging()
                    .UseInMemoryDatabase("RiraInMemoryDb").Options;
                return new (options);
            });

            builder.Services.AddGrpc(op =>
            {
                op.Interceptors.Add<GrpcGlobalExceptionHandlerInterceptor>();
            });
            DependencyInjectionConfiguration.Setup(builder.Services, configuration);

            return builder.Build();
        }

        /// <summary>
        /// 
        /// </summary>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.Use(async (_, next) =>
            {
                CultureInfo.CurrentCulture = new CultureInfo("fa-IR");
                CultureInfo.CurrentUICulture = CultureInfo.CurrentCulture;
                await next.Invoke();
            });

            app.MapGrpcService<Services.UserService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
           
            return app;
        }

    }
}
