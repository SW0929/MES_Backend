using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class LotProcess
    {
        [Key]
        //public int LotPrcessCode { get; set; }
        public required string LotCode { get; set; }
        public required string ProcessCode { get; set; }
        public int? GoodQty { get; set; }
        public int? DefectQty { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? IssuedBy { get; set; }
        public string? EquipmentCode { get; set; }
        public required string Status { get; set; }
        public string? DefectCause { get; set; }

    }
}