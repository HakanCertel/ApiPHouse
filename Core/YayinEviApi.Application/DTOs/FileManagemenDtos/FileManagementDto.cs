using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YayinEviApi.Application.DTOs.FileManagemenDtos
{
    public class FileManagementDto
    {
        public string? Id { get; set; }
        public string FileName { get; set; }
        public string? Path { get; set; }
        public string? Storage { get; set; }
        public bool IsActive { get; set; }
        public string EntityId { get; set; }
        public string? WhichPage { get; set; }
        public string? WhichClass { get; set; }
        public string? AddingUserId { get; set; }
    }
}
