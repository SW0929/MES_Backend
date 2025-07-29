using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class LotProcess
    {
        [Key]
        public required int LotPrcessCode { get; set; }
        public required string LotCode { get; set; }
        public required string ProcessCode { get; set; }
        public int? GoodQty { get; set; }
        public int? DefectQty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public required int IssuedBy { get; set; }
        public required string EquipmentCode { get; set; }
        public string? WorkCenterCode { get; set; }
    }
}
