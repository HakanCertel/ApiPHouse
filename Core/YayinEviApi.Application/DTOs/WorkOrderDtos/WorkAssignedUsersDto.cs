namespace YayinEviApi.Application.DTOs.WorkOrderDtos
{
    public class WorkAssignedUsersDto
    {
        public string? Id { get; set; }
        public string? WorkOrderId { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? NameAndSurname { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public string? ImagePath { get; set; }
    }
}
