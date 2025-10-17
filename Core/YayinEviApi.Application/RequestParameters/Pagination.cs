namespace YayinEviApi.Application.RequestParameters
{
    public record Pagination
    {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        public bool? IsActive { get; set; } = true;
        public string? State { get; set; }

    }
    public record NullablePagination
    {
        public int? Page { get; set; } = 0;
        public int? Size { get; set; } = 5;

    }
}
