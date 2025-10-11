using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using YayinEviApi.Application.Repositories.IAuthorR;

namespace YayinEviApi.Application.Features.Queries.AuthorFile.GetAuthorFile
{
    public class GetAuthorFileQueryHandler : IRequestHandler<GetAuthorFileQueryRequest, List<GetAuthorFileQueryResponse>>
    {
        readonly IAuthorReadRepository _authorReadRepository;
        readonly IConfiguration configuration;
        readonly IWebHostEnvironment _hostingEnvironment;
        public GetAuthorFileQueryHandler(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IAuthorReadRepository authorReadRepository)
        {
            this.configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _authorReadRepository = authorReadRepository;
        }

        public async Task<List<GetAuthorFileQueryResponse>> Handle(GetAuthorFileQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Author? author = await _authorReadRepository.Table.Include(p => p.AuthorFiles)
                   .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return  author?.AuthorFiles.Select(p => new GetAuthorFileQueryResponse
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
