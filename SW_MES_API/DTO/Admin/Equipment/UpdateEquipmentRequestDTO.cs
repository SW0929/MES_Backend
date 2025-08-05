namespace SW_MES_API.DTO.Admin.Equipment
{
    public class UpdateEquipmentRequestDTO
    {
        public required string Name { get; set; }
        public required string ProcessCode { get; set; }
        public required string Status { get; set; }
    }
}
