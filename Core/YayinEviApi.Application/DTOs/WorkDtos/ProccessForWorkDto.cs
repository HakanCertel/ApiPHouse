using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.DTOs.WorkDtos
{
    public class ProccessForWorkS
    {
        public string? Id { get; set; }
        public string WorkId { get; set; }
        public string ProccessId { get; set; }
        public string? ProccessName { get; set; }
        public string UserId { get; set; }
        public string? UserName { get; set; }
    }
}
