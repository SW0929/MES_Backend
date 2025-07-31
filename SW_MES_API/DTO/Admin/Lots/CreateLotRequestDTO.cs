namespace SW_MES_API.DTO.Admin.Lots
{
    public class CreateLotRequestDTO
    {
        public required string WorkOrderID { get; set; }
        public required int CreatedBy { get; set; }
        public List<LotItemDTO>? Lot { get; set; }
    }

}
