namespace Zero.Gateway.BasePrototype
{
    public record MainResponse
    {
        // for green message
        public object? Message { get; set; }

        // for red message
        public object? ErrorMessage { get; set; }

        // for yellow message
        public object? WarningMessage { get; set; }

        // all result data 
        public object? Data { get; set; }
    }
}
