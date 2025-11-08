using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationApi1.Models;

public class Purchase
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Supplier")]
    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    [DataType(DataType.Date)]
    public DateTime Date { get; set; }

    [Column(TypeName = "decimal(18,2)")]  // ✅ Fix precision
    public decimal TotalAmount { get; set; }
    
    public ICollection<PurchaseDetails>? Details { get; set; }
    
    
}