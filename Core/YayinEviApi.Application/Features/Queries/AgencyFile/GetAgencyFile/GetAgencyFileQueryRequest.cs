using MediatR;

namespace YayinEviApi.Application.Features.Queries.AgencyFile.GetAgencyFile
{
    public class GetAgencyFileQueryRequest : IRequest<List<GetAgencyFileQueryResponse>>
    {
        public string Id { get; set; }
    }
}
