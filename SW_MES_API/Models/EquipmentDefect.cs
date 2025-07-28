using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class EquipmentDefect
    {
        [Key]
        public required int DefectCode { get; set; }
        public required string EquipmentCode { get; set; }
        public required DateTime DefectDate { get; set; }
        public required int IssuedBy { get; set; }
        public string? Status { get; set; } //DB Default 값 변경 해야 함.
        public int? SolvedBy { get; set; }
        public DateTime? SolvedDate { get; set; }
    }
}
