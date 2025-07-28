using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class Process
    {
        [Key]
        public required string ProcessCode { get; set; }
        public required string Name { get; set; }
        public required int Sequence { get; set; }
    }
}
