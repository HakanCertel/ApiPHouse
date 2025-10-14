namespace YayinEviApi.Application.DTOs.HelperEntityDtos
{
    public class WorkCategoryDto
    {
        public string? CategoryId { get; set; }
        public string? CategoryCode { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
        public bool IsActive { get; set; }
    }

}
