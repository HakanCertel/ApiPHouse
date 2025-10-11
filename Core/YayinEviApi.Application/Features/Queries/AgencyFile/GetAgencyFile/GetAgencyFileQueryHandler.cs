using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YayinEviApi.Application.Repositories.AgencyR;
using YayinEviApi.Domain.Entities.AgencyE;

namespace YayinEviApi.Application.Features.Queries.AgencyFile.GetAgencyFile
{
    public class GetAgencyFileQueryHandler : IRequestHandler<GetAgencyFileQueryRequest, List<GetAgencyFileQueryResponse>>
    {
        readonly IAgencyReadRepository _agencyReadRepository;
        readonly IConfiguration configuration;
        readonly IWebHostEnvironment _hostingEnvironment;
        public GetAgencyFileQueryHandler(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IAgencyReadRepository agencyReadRepository)
        {
            this.configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _agencyReadRepository = agencyReadRepository;
        }

        public async Task<List<GetAgencyFileQueryResponse>> Handle(GetAgencyFileQueryRequest request, CancellationToken cancellationToken)
        {
            Agency? agency = await _agencyReadRepository.Table.Include(p => p.AgencyFiles)
                   .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return agency?.AgencyFiles.Select(p => new GetAgencyFileQueryResponse
            {
                Path = p.Path,//Path.Combine(_hostingEnvironment.WebRootPath,$"{p.Path}"),//$"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id,
                Showcase=p.Showcase,
                IsActive=p.IsActive
            }).ToList();
        }
    }
}
