using System.Linq;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.WorkOrderE;

namespace YayinEviApi.Application.DTOs.WorkOrderDtos
{
    public class WorkOrderDto
    {
        public string? Id { get; set; }
        public string? AuthUserId { get; set; }
        public string? AuthUserName { get; set; }
        public string? AuthUserNameSurname { get; set; }
        public string? AuthImagePath { get; set; }
        public string? WorkOrderCode { get; set; }
        public string? ProjectId { get; set; }
        public string? ProjectCode { get; set; }
        public string? ProjectState { get; set; }
        public string? ProccessId { get; set; }
        public string? WorkId { get; set; }
        public string? WorkName { get; set; }
        public string? WorkState { get; set; }
        public DateTime? FinishedDate { get; set; }
        public string? ImagePath { get; set; }
        public string? Bandrol { get; set; }
        public string? WorkOrginalName { get; set; }
        public string? Language { get; set; }
        public string? ISBN { get; set; }
        public string? Barcode { get; set; }
        public int? LastPrintingQuantity { get; set; }
        public DateTime? FirstPrintingDate { get; set; }
        public DateTime? LasttPrintingDate { get; set; }
        public string? CertificateNumber { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorName { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public string? NameTranslating { get; set; }
        public string? NameReducting { get; set; }
        public string? NameReading { get; set; }
        public string? NameDrawing { get; set; }
        public string? NameTypeSetting { get; set; }
        public string? PrintingHouse { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description { get; set; }
        public string? Subject { get; set; }
        public string? ProccessName { get; set; }
        public string? ProccessCategoryId { get; set; }
        public string? ProccessCategoryName { get; set; }
        public string? AssignedUserId { get; set; }
        public string? AssignedUserName { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<WorkAssignedUsers>? WorkAssignedUsers { get; set; }
        public ICollection<WorkOrderMessages>? WorkOrderMessages { get; set; }
        public IList<FileManagement>? Files { get; set; }


    }
}
