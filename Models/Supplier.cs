using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi1.Models;

public class Supplier
{
    [Key] 
    public int Id { get; set; }
    
    [Required,MaxLength(50)]
    public string Name { get; set; }
    
    [Required,MaxLength(50)]
    public string ContactPerson { get; set; }
    
    [Required,MaxLength(10)]
    public string Phone { get; set; }
    
    public string Address { get; set; }
    
    public ICollection<Product>? Products { get; set; }
    
    public ICollection<Purchase> ? Purchases { get; set; }
}