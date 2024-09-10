namespace Zero.Gateway.Configuration
{
    public record DBLogConfig
    {
        public required string ServerAddress { get; set; }
        public required int Port { get; set; }
        public required string DatabaseName { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public required string ErrorLogCollection { get; set; }
        public required string ActivityLogCollection { get; set; }
        public required string RequestLogCollection { get; set; }
    }
}
