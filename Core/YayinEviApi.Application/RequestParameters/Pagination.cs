namespace YayinEviApi.Application.RequestParameters
{
    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;

    }
    public record NullablePagination
    {
        public int? Page { get; set; } = 0;
        public int? Size { get; set; } = 5;

    }
}
