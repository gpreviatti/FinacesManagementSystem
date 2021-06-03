using System.Collections.Generic;
using Domain.Dtos.Category;

namespace Domain.ViewModels
{
    public class CategoryUpdateViewModel
    {
        public CategoryUpdateDto Category { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }
    }
}
