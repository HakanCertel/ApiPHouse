using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.AgencyE;
using YayinEviApi.Domain.Entities.Common;
using YayinEviApi.Domain.Entities.WorkE;
using YayinEviApi.Infrastructure.Enums;

namespace YayinEviApi.Application.DTOs.AgencyDto
{
    [NotMapped]
    public class AgencyS
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
        public string? TaxDeparmant { get; set; }
        public string? TaxNumber { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }
        public string? LocalOrForeing { get; set; }
        public string? PhoneNumber_1 { get; set; }
        public string? PhoneNumber_2 { get; set; }
        public string? ResponsibleName { get; set; }
        public string? Mail { get; set; }
        public string? WebSite { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public ICollection<AgencyFile>? AgencyFiles { get; set; }

    }
    public class AgencyDetail 
    {
        public string? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Address { get; set; }
        public string TaxDeparmant { get; set; }
        public string TaxNumber { get; set; }
        public string Description { get; set; }
        public string? State { get; set; }
        public string? LocalOrForeing { get; set; }
        public string? PhoneNumber_1 { get; set; }
        public string? PhoneNumber_2 { get; set; }
        public string? ResponsibleName { get; set; }
        public string? Mail { get; set; }
        public string? WebSite { get; set; }

        public ICollection<AgencyFile>? AgencyFiles { get; set; }
        public IEnumerable<AuthorForAgency>? AuthorListForAgency { get; set; }
        public IEnumerable<WorkForAgency>? WorkListForAgency { get; set; }
    }
    public class AuthorForAgency
    {
        public string? AuthorId { get; set; }
        public string? AuthorNameSurname { get; set; }
        public string? AuthorImagePath { get; set; }
    }
    public class WorkForAgency
    {
        public string? WorkId { get; set; }
        public string? WorkName { get; set; }
        public string? WorkImagePath { get; set; }
    }
    public class AgencyConnectionInformationL : BaseEntity
    {
        public Guid AgencyId { get; set; }
        public bool IsDefault { get; set; }
        public string Position { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
