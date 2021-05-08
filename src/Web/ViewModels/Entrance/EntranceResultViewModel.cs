using Domain.Dtos.Entrance;
using Domain.Models;

namespace Web.ViewModels.Entrance
{
    public class EntranceResultViewModel
    {
        public DatatablesModel<EntranceResultDto> PaginationModel { get; set; }
    }
}
