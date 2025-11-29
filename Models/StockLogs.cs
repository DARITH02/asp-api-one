    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    namespace WebApplicationApi1.Models;

    public class StockLogs
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public string ChangeType { get; set; } = string.Empty; // e.g. "Purchase", "Sale"

        public int QuantityChange { get; set; } // +5 or -3

        public int OldStock { get; set; }

        public int NewStock { get; set; }

        public int? ReferenceId { get; set; } // Optional link to PurchaseDetail or SaleDetail

        public DateTime Date { get; set; } = DateTime.Now;

        public string? Note { get; set; }
        
        
    }