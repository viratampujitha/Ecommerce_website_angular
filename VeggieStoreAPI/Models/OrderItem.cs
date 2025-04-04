using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace VeggieStoreAPI.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column("order_id")] // ✅ Maps to MySQL column `order_id`
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        [JsonIgnore] // 🚀 Prevents infinite recursion
        public Order? Order { get; set; }

        [Required]
        [Column("vegetable_id")] // ✅ Maps to MySQL column `vegetable_id`
        public int VegetableId { get; set; }

        [ForeignKey("VegetableId")]
        public Vegetable? Vegetable { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column("price", TypeName = "decimal(10,2)")] // ✅ Maps to MySQL `price`
        public decimal Price { get; set; }
    }
}
