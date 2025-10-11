using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YayinEviApi.Application.Repositories.IMaterialR;
using YayinEviApi.Domain.Entities.MaterialE;

namespace YayinEviApi.Application.Features.Queries.MaterialFile.GetMaterialFile
{
    public class GetMaterialFileQueryHandler : IRequestHandler<GetMaterialFileQueryRequest, List<GetMaterialFileQueryResponse>>
    {
        readonly IMaterialRepository _materialRepository;
        readonly IConfiguration configuration;
        readonly IWebHostEnvironment _hostingEnvironment;
        public GetMaterialFileQueryHandler(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IMaterialRepository materialRepository)
        {
            this.configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _materialRepository = materialRepository;
        }

        public async Task<List<GetMaterialFileQueryResponse>> Handle(GetMaterialFileQueryRequest request, CancellationToken cancellationToken)
        {
            Material? author = await _materialRepository.Table.Include(p => p.MaterialFiles)
                   .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return  author?.MaterialFiles.Select(p => new GetMaterialFileQueryResponse
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
