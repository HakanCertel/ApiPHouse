using MediatR;

namespace YayinEviApi.Application.Features.Queries.AuthorFile.GetAuthorFile
{
    public class GetAuthorFileQueryRequest : IRequest<List<GetAuthorFileQueryResponse>>
    {
        public string Id { get; set; }
    }
}
