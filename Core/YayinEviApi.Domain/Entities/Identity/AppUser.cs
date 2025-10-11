using Microsoft.AspNetCore.Identity;
using YayinEviApi.Domain.Entities.HelperEntities;

namespace YayinEviApi.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public Guid? DepartmentId { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }
        public Department Department { get; set; }

    }
}
