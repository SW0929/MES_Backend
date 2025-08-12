using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class EquipmentDefect
    {
        [Key]
        public int DefectCode { get; set; }
        public required string EquipmentCode { get; set; }
        public required DateTime DefectDate { get; set; }
        public required int IssuedBy { get; set; }
        public required string Status { get; set; } //(미해결, 해결, 해결 중)
        public int? SolvedBy { get; set; }
        public DateTime? SolvedDate { get; set; }
        public string? DefectReason { get; set; }
    }
}
