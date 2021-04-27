using System.Collections.Generic;
using Domain.Dtos.Category;

namespace Web.ViewModels.Category
{
    public class CategoryUpdateViewModel
    {
        public CategoryUpdateDto Category { get; set; }

        public IEnumerable<CategoryResultDto> Categories { get; set; }
    }
}
