namespace Application.Configurations.Options
{
    public class AuthenticationBearerOptions
    {
        public const string SectionName = "Authentication";
        public string? PrivateKey { get; set; }
        public Schemes? Schemes { get; set; }

    }
}
