namespace YayinEviApi.Application.DTOs.User
{
    public class CreateUser
    {
        public string? UserId { get; set; }
        public string? NameSurname { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public string? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? ImagePath { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}
