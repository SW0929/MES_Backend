namespace SW_MES_API.DTO.Operator.EquipmentDefect
{
    public class CreateEquipmentDefectRequestDTO
    {
        public required string EquipmentCode { get; set; }
        public required DateTime DefectDate { get; set; } = DateTime.Now;
        public int IssuedBy { get; set; }
        public string Status { get; set; } = "미해결";
        public string? DefectReason { get; set; }
    }
}
