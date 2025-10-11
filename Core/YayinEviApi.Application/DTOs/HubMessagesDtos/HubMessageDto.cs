namespace YayinEviApi.Application.DTOs.HubMessagesDtos
{
    public class HubMessageDto
    {
        public string? Id { get; set; }
        public string? WorkOrderId { get; set; }
        public string? MessageHead { get; set; }
        public string? MessageBody { get; set; }
        public string? SendedUserId { get; set; }
        public string? SendedUserNameSurname { get; set; }
        public string? CreatingUserId { get; set; }
        public string? CreatingUserNameSurname { get; set; }
        public bool? Readed { get; set; }
        public bool? IsShowed { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
