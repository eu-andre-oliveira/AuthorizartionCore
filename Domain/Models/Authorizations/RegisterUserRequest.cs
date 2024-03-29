namespace Domain.Models.Authorizations
{
    public class RegisterUserRequest
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? RepeatPassword { get; set; }
        public string? Email { get; set; }
        public string[]? Roles { get; set; }
    }
}
