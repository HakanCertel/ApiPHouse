namespace YayinEviApi.Application.Features.Queries.MaterialFile.GetMaterialFile
{
    public class GetMaterialFileQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public bool Showcase { get; set; }
        public bool IsActive { get; set; }
    }
}
