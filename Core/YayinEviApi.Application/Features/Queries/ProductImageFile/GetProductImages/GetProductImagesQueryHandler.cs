using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Repositories;

namespace YayinEviApi.Application.Features.Queries.ProductImageFile.GetProductImages
{
    public class GetProductImagesQueryHandler : IRequestHandler<GetProductImagesQueryRequest, List<GetProductImagesQueryResponse>>
    {
        readonly IProductReadRepository _productReadRepository;
        readonly IConfiguration configuration;
        readonly IWebHostEnvironment _hostingEnvironment;
        public GetProductImagesQueryHandler(IProductReadRepository productReadRepository, IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _productReadRepository = productReadRepository;
            this.configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<GetProductImagesQueryResponse>> Handle(GetProductImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Product? product = await _productReadRepository.Table.Include(p => p.ProductImageFiles)
                   .FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return product?.ProductImageFiles.Select(p => new GetProductImagesQueryResponse
            {
                Path = p.Path,//Path.Combine(_hostingEnvironment.WebRootPath,$"{p.Path}"),//$"{configuration["BaseStorageUrl"]}/{p.Path}",
                FileName = p.FileName,
                Id = p.Id,
                Showcase=p.Showcase
            }).ToList();
        }
    }
}
