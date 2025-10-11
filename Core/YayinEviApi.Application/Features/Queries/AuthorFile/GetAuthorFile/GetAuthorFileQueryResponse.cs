namespace YayinEviApi.Application.Features.Queries.AuthorFile.GetAuthorFile
{
    public class GetAuthorFileQueryResponse
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public Guid Id { get; set; }
        public bool Showcase { get; set; }
        public bool IsActive { get; set; }
    }
}
