using System;
using System.Collections.Generic;
using Domain.Dtos.Entity;
using Domain.Dtos.Entrance;
using Domain.Dtos.User;

namespace Domain.Dtos.Category
{
    public class CategoryResultDto : EntityResultDto
    {   
        public string Name { get; set; }

        public Guid UserId { get; set; }
        public UserResultDto User { get; set; }

        public Guid CategoryId { get; set; }
        public CategoryResultDto CategoryMain { get; set; }

        public IEnumerable<EntranceResultDto> Entrances { get; set; }
    }
}
