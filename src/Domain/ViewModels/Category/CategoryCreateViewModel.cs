using System.Collections.Generic;
using Domain.Dtos.Category;

namespace Domain.ViewModels
{
    public class CategoryCreateViewModel
    {
        public CategoryCreateDto Category { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }
    }
}
