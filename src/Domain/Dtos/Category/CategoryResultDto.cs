using System.Collections.Generic;
using Domain.Dtos.Entity;
using Domain.Dtos.Entrace;

namespace Domain.Dtos.Category
{
    public class CategoryResultDto : EntityResultDto
    {   
        public string Name { get; set; }

        public IEnumerable<EntraceResultDto> Entraces { get; set; }
    }
}
