using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi1.Models;

public class Supplier
{
    [Key] 
    public int Id { get; set; }
    [Required,MaxLength(50)]
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public ICollection<Product>? Products { get; set; }
}