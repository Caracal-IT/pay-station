using System.Collections.Generic;

namespace Caracal.PayStation.Web.ViewModel {
    public class ListViewModel<TItems> {
        public List<TItems> Items { get; set; }
    }
}