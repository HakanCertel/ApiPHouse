using MediatR;

namespace YayinEviApi.Application.Features.Queries.GetWorkFiles.GetWorkImages
{
    public class GetPublishFileQueryRequest: IRequest<List<GetPublishFileQueryResponse>>
    {
        public string? Id { get; set; }
        public string WorkId { get; set; }
        public string? DepartmentId { get; set; }
        public string? UserId { get; set; }
    }
}
