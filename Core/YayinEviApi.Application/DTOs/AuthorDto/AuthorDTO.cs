using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities;

namespace YayinEviApi.Application.DTOs.AuthorDto
{
    [NotMapped]
    public class AuthorS
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Language { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserNameSurname { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserNameSurname { get; set; }

        public ICollection<AuthorFile>? AuthorFiles { get; set; }
    }
}
