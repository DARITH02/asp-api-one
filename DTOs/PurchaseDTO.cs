namespace WebApplicationApi1.DTOs;

public class PurchaseDTO
{
    public int Id { get; set; }
    public int? SupplierId { get; set; }
    public DateTime Date { get; set; }
    public decimal TotalAmount { get; set; }
}