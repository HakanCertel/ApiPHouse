using System.ComponentModel.DataAnnotations.Schema;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.WorkE;

namespace YayinEviApi.Application.DTOs.WorkDtos
{
    [NotMapped]
    public class WorkS
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? WorkOrginalName { get; set; }
        public string? Language { get; set; }
        public string? ISBN { get; set; }
        public string? Barcode { get; set; }
        public int LastPrintingQuantity { get; set; }
        public DateTime FirstPrintingDate { get; set; }
        public DateTime LasttPrintingDate { get; set; }
        public string? CertificateNumber { get; set; }
        public string? ProjectName { get; set; }
        public string? AuthorId { get; set; }
        public string? CategoryId { get; set; }
        public string? AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public string? NameTranslating { get; set; }
        public string? NameReducting { get; set; }
        public string? NameReading { get; set; }
        public string? NameDrawing { get; set; }
        public string? NameTypeSetting { get; set; }
        public string? Bandrol { get; set; }
        public string? PrintingHouse { get; set; }
        public int StockQuantity { get; set; }
        public string? Description { get; set; }
        public string Subject { get; set; }
        public ICollection<PublishFile>? PublishFiles { get; set; }


    }
    public class WorkL
    {
        public string Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? WorkOrginalName { get; set; }
        public string? Language { get; set; }
        public string? ISBN { get; set; }
        public string? Barcode { get; set; }
        public int? LastPrintingQuantity { get; set; }
        public DateTime? FirstPrintingDate { get; set; }
        public DateTime? LasttPrintingDate { get; set; }
        public string? CertificateNumber { get; set; }
        public string? ProjectName { get; set; }
        public string? AuthorId { get; set; }
        public string? AuthorNameAndSurnmae { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? AgencyId { get; set; }
        public string? AgencyName { get; set; }
        public string? NameTranslating { get; set; }
        public string? NameReducting { get; set; }
        public string? NameReading { get; set; }
        public string? NameDrawing { get; set; }
        public string? NameTypeSetting { get; set; }
        public string? Bandrol { get; set; }
        public string? PrintingHouse { get; set; }
        public int? StockQuantity { get; set; }
        public string? Description { get; set; }
        public string? Subject { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public ICollection<PublishFile>? PublishFiles { get; set; }

    }
}
