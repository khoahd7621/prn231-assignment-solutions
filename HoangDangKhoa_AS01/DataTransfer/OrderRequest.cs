namespace DataTransfer
{
    public class OrderRequest
    {
        public string Freight { get; set; }
        public int CustomerID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
