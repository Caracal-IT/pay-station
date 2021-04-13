using System.Collections.Generic;

namespace Caracal.Framework.Data {
    public class PagedData<T> {
        public int PageNumber { get; set; }
        public int NumberOfRows { get; set; } = 10;
        public int NumberOfResults { get; set; }
        
        public List<T> Items { get; set; } = new ();
    }
}