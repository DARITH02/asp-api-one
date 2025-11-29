namespace WebApplicationApi1.DTOs;

public class PurchaseDetailsDTO
{
    public int Id { get; set; }
    public int?  purchaseId { get; set; }
    public int productId { get; set; }
    public int quantity { get; set; }
    public decimal unitPrice { get; set; }
    public decimal subTotal { get; set; }
    
}