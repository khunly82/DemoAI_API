namespace DemoAIAPI.Models
{
    public class Invoice
    {
        public string SupplierRef { get; set; } = null!;
        public string Date { get; set; } = null!;
        public string VAT { get; set; } = null!;
        public List<InvoiceLine> Lines { get; set; } = null!;
    }

    public class InvoiceLine
    {
        public string Label { get; set; } = null!;
        public int Quantity { get; set; }
        public string Ean { get; set; } = null!;
        public decimal UnitPrice { get; set; }
    }
}
