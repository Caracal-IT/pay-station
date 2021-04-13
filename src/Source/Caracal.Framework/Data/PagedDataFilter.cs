using System.Collections.Generic;

namespace Caracal.Framework.Data {
    public class PagedDataFilter {
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; } = 10;

        public List<Filter> Filters { get; set; } = new();
    }
}