namespace SW_MES_API.DTO
{
    public class EquipmentResponseDTO
    {
        public required string EquipmentCode { get; set; }
        public required string Name { get; set; }
        public required string ProcessCode { get; set; }
        public required string Status { get; set; }
        public DateTime? LastUsedDate { get; set; }
    }
}
