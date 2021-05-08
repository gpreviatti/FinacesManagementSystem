using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class DatatablesModel<T> where T : class
    {
        public string Draw { get; set; }
        public string Start { get; set; }
        public string Length { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SearchValue { get; set; }

        private int pageSize;
        public int PageSize
        {
            get { return Length != null ? int.Parse(Length) : 0; }
            set { pageSize = value; }
        }

        private int skip;
        public int Skip
        {
            get { return Start != null ? int.Parse(Start) : 0; }
            set { skip = value; }
        }

        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
