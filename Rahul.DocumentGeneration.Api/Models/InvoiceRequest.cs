namespace Rahul.DocumentGeneration.Api.Models
{
    public class InvoiceRequest
    {
        public string InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Amount { get; set; }
    }
}