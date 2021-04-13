namespace Caracal.Framework.Data {
    public class Filter {
        public string Field { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value1 { get; set; }
        public object Value2 { get; set; }
    }
}