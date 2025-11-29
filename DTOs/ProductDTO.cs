using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi1.DTOs;

public class ProductDTO
{
    public int? Id { get; set; }  // optional for POST, required for PUT

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }

    public int? CategoryId { get; set; }   // link to Category
    public int? SupplierId { get; set; }   // link to Supplier

}