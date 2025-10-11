namespace YayinEviApi.Application.Features.Queries.GetWorkFiles.GetWorkImages
{
    public class GetPublishFileQueryResponse
    {
        public string Id { get; set; }
        public string WorkId { get; set; }
        public string DepartmentId { get; set; }
        public string UserId { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
        public bool Showcase { get; set; }
        public bool IsActive { get; set; }
    }
}
