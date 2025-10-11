using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.Features.Commands.HelperTables.WorkTypeC.CreateWorkType
{
    public class CreateWorkTypeCommandRequest:IRequest<CreateWorkTypeCommandRequest>
    {
        public string WorkTypeCode { get; set; }
        public string WorkTypeName { get; set; }
        public string Description { get; set; }
    }
}
