using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Application.Features.Commands.WorkFiles.ChangeShowcaseImage;
using YayinEviApi.Application.Repositories;
using YayinEviApi.Domain.Entities;
using YayinEviApi.Domain.Entities.WorkE;

namespace Yay.Application.Features.Commands.WorkFiles.ChangeShowcaseImage
{
    public class ChangeShowcaseImageCommandHandler : MediatR.IRequestHandler<ChangeShowcaseImageCommandRequest, ChangeShowcaseImageCommandResponse>
    {
        readonly IPublishFileWriteRepository _publishFileWriteRepository;
        private Expression< Func<Selector, bool>> _conditionExpressionData ;
        private Expression<Func<Selector, bool>> _conditionExpressionFile;

        public ChangeShowcaseImageCommandHandler(IPublishFileWriteRepository publishFileWriteRepository)
        {
            _publishFileWriteRepository = publishFileWriteRepository;
        }

        public async Task<ChangeShowcaseImageCommandResponse> Handle(ChangeShowcaseImageCommandRequest request, CancellationToken cancellationToken)
        {
            if (request.UserId != null && request.DepartmentId != null)
            {
                _conditionExpressionData = x =>x.pif.IsActive && 
                                               x.work.Id == Guid.Parse( request.Id) && 
                                               x.pif.UserId == request.UserId && 
                                               x.pif.DepartmentId == request.DepartmentId;

                _conditionExpressionFile = x => x.pif.Id == Guid.Parse(request.ImageId) &&
                                               x.pif.UserId == request.UserId &&
                                               x.pif.DepartmentId == request.DepartmentId; ;

            }
            else if (request.UserId == null && request.DepartmentId != null)
            {
                _conditionExpressionData = x => x.pif.IsActive &&
                                               x.work.Id == Guid.Parse(request.Id) &&
                                               x.pif.DepartmentId == request.DepartmentId;
               
                _conditionExpressionFile = x => x.pif.Id == Guid.Parse(request.ImageId) &&
                                               x.pif.DepartmentId == request.DepartmentId;
            }
            
            
            else if (request.UserId != null && request.DepartmentId == null)
            {
                _conditionExpressionData = x => x.pif.IsActive &&
                                              x.work.Id == Guid.Parse(request.Id) &&
                                              x.pif.UserId == request.UserId;

                _conditionExpressionFile = x => x.pif.Id == Guid.Parse(request.ImageId) &&
                                               x.pif.UserId == request.UserId;
            }
            else
            {
                _conditionExpressionData = x => x.pif.IsActive &&
                                              x.work.Id == Guid.Parse(request.Id);

                _conditionExpressionFile = x => x.pif.Id == Guid.Parse(request.ImageId);
            }
            
            var query = _publishFileWriteRepository.Table
                      .Include(p => p.Works)
                      .SelectMany(p => p.Works, (pif, p)=>  new Selector
                      {
                         pif= pif,
                         work=p
                      });
            var data = await query.FirstOrDefaultAsync(_conditionExpressionData);

            if (data != null)
                data.pif.IsActive = false;

            var image = await query.FirstOrDefaultAsync(_conditionExpressionFile);
            if (image != null)
                image.pif.IsActive = true;

            await _publishFileWriteRepository.SaveAsync();

            return new();
        }
    }

    public class Selector
    {
        public PublishFile pif { get; set; }
        public Work work { get; set; }
    }
}
