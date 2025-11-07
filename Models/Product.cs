using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; } 
        public int Stock { get; set; }
        
        public int? Category_id { get; set; }
        public Category? Category { get; set; }
        
        public int? Supplier_id { get; set; }
        public Supplier? Supplier { get; set; }
        
        

        public Product() { }

        // Convenience constructor
        public Product(string name, decimal price, int stock)
        {
            Name = name;
            Price = price;
            Stock = stock;
        }
    }
}
