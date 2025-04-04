using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VeggieStoreAPI.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("user_id")]  // ✅ Maps UserId to user_id in MySQL
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        [Required]
        [Column("total_price", TypeName = "decimal(10,2)")]  // ✅ Maps TotalPrice to total_price
        public decimal TotalPrice { get; set; }

        [Required]
        [Column("status")]  // ✅ Maps Status to status
        public string Status { get; set; } = "Pending";  // Enum: Pending, Completed, Cancelled

        [Column("created_at")]  // ✅ Maps CreatedAt to created_at
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Relationship with OrderItems
        public List<OrderItem>? OrderItems { get; set; }
    }
}
