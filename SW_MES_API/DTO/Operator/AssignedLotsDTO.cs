namespace SW_MES_API.DTO.Operator
{
    public class AssignedLotsDTO
    {
        public required int LotProcessCode { get; set; }
        public required string LotCode { get; set; }
        public required string ProcessCode { get; set; }
        public required string ProductName { get; set; }
        public int Quantity { get; set; }
        public required string Status { get; set; }
        public required string EquipmentName { get; set; }
    }
}
