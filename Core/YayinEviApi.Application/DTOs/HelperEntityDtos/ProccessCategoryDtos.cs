using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.DTOs.HelperEntityDtos
{
    public class ProccessCategoryDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string? CreatingUserId { get; set; }
        public string? UpdatingUserId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
