using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Linq.Expressions;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Application.Repositories.IWorkR;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.WorkE;

namespace YayinEviApi.Application.Features.Queries.GetWorkFiles.GetWorkImages
{
    public class GetPublishFileQueryHandler : IRequestHandler<GetPublishFileQueryRequest, List<GetPublishFileQueryResponse>>
    {

        readonly IWorkReadRepository _workReadRepository;
        readonly IConfiguration configuration;
        readonly IWebHostEnvironment _hostingEnvironment;
        private Func<PublishFile,bool> _conditionExpression=x=>x.WorkId!=null;
        public GetPublishFileQueryHandler(IWorkReadRepository workReadRepository, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _workReadRepository = workReadRepository;
            this.configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<List<GetPublishFileQueryResponse>> Handle(GetPublishFileQueryRequest request, CancellationToken cancellationToken)
        {
            if(request.UserId!=null && request.DepartmentId != null)
                _conditionExpression=x=>x.WorkId==request.WorkId & x.UserId==request.UserId & x.DepartmentId==request.DepartmentId;
            else if(request.UserId==null && request.DepartmentId!=null)
                _conditionExpression = x => x.WorkId == request.WorkId & x.DepartmentId == request.DepartmentId;
            else if (request.UserId != null && request.DepartmentId == null)
                _conditionExpression = x => x.WorkId == request.WorkId & x.UserId == request.UserId;
            else
                _conditionExpression = x => x.WorkId == request.WorkId;


            var work = await _workReadRepository.Table.Include(p => p.PublishFiles)
                          .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.WorkId));
            var files= work?.PublishFiles.Where(_conditionExpression).Select(p => new GetPublishFileQueryResponse
            {
                Path = p.Path,//Path.Combine(_hostingEnvironment.WebRootPath,$"{p.Path}"),//$"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id.ToString(),
                UserId = p.UserId,
                DepartmentId=p.DepartmentId,
                Showcase = p.Showcase,
                IsActive=p.IsActive,
            }).ToList();

            return files;
        }
    }
}
