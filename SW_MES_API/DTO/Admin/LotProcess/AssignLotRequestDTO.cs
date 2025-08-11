namespace SW_MES_API.DTO.Admin.LotProcess
{
    public class AssignLotRequestDTO
    {
        public required string LotCode { get; set; }
        public required string ProcessCode { get; set; }
        public int EmployeeID { get; set; }
        public required string EquipmentCode { get; set; }
    }
}
