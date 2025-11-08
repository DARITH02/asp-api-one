using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationApi1.Models;

public class PurchaseDetails
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Purchase")]
    public int? PurchaseId  { get; set; }
    public Purchase? Purchase { get; set; }
    
    [ForeignKey("Product")]
    public int? ProductId { get; set; }
    public Product? Product { get; set; }
    
    [Required]
    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SubTotal { get; set; }
   
    
}
