using YayinEviApi.Domain.Enum;

namespace YayinEviApi.Application.DTOs.ProjectDtos
{
    public class ProjectDto
    {
        public string? Id { get; set; }
        public string Code { get; set; }
        public string AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public string WorkId { get; set; }
        public string State { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractFinishDate { get; set; }
        public decimal? ContractPrice { get; set; }

        public string? WorkCode { get; set; }
        public string? WorkName { get; set; }
        public string? WorkOrginalName { get; set; }
        public string? WorkLanguage { get; set; }
        public string? ImagePath { get; set; }
        public string? ISBN { get; set; }
        public string? Barcode { get; set; }
        public int LastPrintingQuantity { get; set; }
        public DateTime? FirstPrintingDate { get; set; }
        public DateTime? LasttPrintingDate { get; set; }
        public string? CertificateNumber { get; set; }
        public string? ProjectName { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorNameAndSurnmae { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? NameTranslating { get; set; }
        public string? NameReducting { get; set; }
        public string? NameReading { get; set; }
        public string? NameDrawing { get; set; }
        public string? NameTypeSetting { get; set; }
        public string? Bandrol { get; set; }
        public string? PrintingHouse { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public string? Subject { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
