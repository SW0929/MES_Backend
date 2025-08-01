namespace SW_MES_API.DTO
{
    public class AssignLotResponseDTO
    {
        public required string Message { get; set; }
        public required string LotProcessCode { get; set; } // 이거 LotCode 인지 고민 좀 해봐야 함.
        public required string LotCode { get; set; } 
        public int AssignedEmployee { get; set; }
        public required string AssignedEquipmentCode { get; set; }
    }
}
