namespace YayinEviApi.Application.DTOs.HelperEntityDtos
{
    public class ProccessCategoryDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public bool IsActive { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
