using Zero.Gateway.Configuration;

namespace Zero.Gateway.Middleware
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDIMiddleware(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<AppConfig>(_config.GetSection("AppConfiguration"));

            //services.AddHttpLoggingInterceptor<HttpLoggingInterceptor>();

            return services;
        }
    }
}
