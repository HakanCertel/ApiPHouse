using MediatR;

namespace YayinEviApi.Application.Features.Queries.MaterialFile.GetMaterialFile
{
    public class GetMaterialFileQueryRequest : IRequest<List<GetMaterialFileQueryResponse>>
    {
        public string Id { get; set; }
    }
}
