using System.ComponentModel.DataAnnotations;

namespace VeggieStoreAPI.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int VegetableId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key Relationships
        public User? User { get; set; }
        public Vegetable? Vegetable { get; set; }
    }
}
