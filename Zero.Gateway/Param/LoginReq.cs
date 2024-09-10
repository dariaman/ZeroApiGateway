namespace Zero.Gateway.Param
{
    public record LoginReq
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
