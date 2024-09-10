namespace Zero.Gateway.Configuration
{
    public record AppConfig
    {
        public required string Version { get; set; }
        public required string ApplicationName { get; set; }

    }
}
