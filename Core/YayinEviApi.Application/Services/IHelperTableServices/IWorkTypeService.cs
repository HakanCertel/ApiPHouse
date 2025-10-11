using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YayinEviApi.Domain.Entities.HelperEntities;

namespace YayinEviApi.Application.Services.IHelperTableServices
{
    public interface IWorkTypeService
    {
        public Task<List<WorkType>> GetAllWorkTypes();
        public Task AddWorktype(WorkType workType);
    }
}
