using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.DTOs.HelperEntityDtos
{
    public class ProccessDto
    {
        public string? ProccessId { get; set; }
        public string ProccessCode { get; set; }
        public string ProccessName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public string? ProccessCategoryId { get; set; }
        public string? ProccessCategoryName { get; set; }
    }
}
