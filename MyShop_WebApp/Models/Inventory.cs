using System.ComponentModel.DataAnnotations;

namespace MyShop_WebApp.Models
{
    public class Inventory
    {

        [Key]
        public int Id { get; set; }



        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        [Required]
        public string Description { get; set; }


        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }


        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }

    public class AddInventoryViewModel 
    {
        public int Step { get; set; }
        public Inventory Inventory { get; set; }
    }


}
