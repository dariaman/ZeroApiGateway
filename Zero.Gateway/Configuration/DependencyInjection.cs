namespace Zero.Gateway.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterDIConfig(this IServiceCollection services, IConfiguration _config)
        {
            services.Configure<AppConfig>(_config.GetSection("AppConfiguration"));

            services.Configure<DBUserConfig>(_config.GetSection("Database:DBUser"));
            services.Configure<DBLogConfig>(_config.GetSection("Database:DBLog"));

            return services;
        }

    }
}
