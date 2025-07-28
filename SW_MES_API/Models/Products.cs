using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class Products
    {
        [Key]
        public required string ProductCode { get; set; }
        public required string Name { get; set; }
        public required string Model { get; set; }
        public required string Color { get; set; }
        public required int ModelYear { get; set; }
    }
}
