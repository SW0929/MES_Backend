namespace SW_MES_API.DTO.Admin.Equipment
{
    public class CreateEquipmentRequestDTO
    {
        public required string EquipmentCode { get; set; }
        public required string Name { get; set; }
        public required string ProcessCode { get; set; }
        public required string Status { get; set; }
    }
}
