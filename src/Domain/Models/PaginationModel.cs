using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class PaginationModel<T> where T : class
    {
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int) Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowNext => CurrentPage < TotalPages;
        public IEnumerable<T> Data { get; set; }
    }
}
