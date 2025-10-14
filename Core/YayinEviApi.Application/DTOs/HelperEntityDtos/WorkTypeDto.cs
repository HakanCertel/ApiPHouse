namespace YayinEviApi.Application.DTOs.HelperEntityDtos
{
    public class WorkTypeDto
    {
        public string? Id { get; set; }
        public string? WorkCategoryId { get; set; }
        public string? WorkCategoryName { get; set; }
        public string? TypeCode { get; set; }
        public string? TypeName { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserName { get; set; }
        public string? UpdatingUserId { get; set; }
        public string? UpdatingUserName { get; set; }
    }
}
