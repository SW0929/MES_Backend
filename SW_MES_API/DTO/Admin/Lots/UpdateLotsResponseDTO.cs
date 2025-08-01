namespace SW_MES_API.DTO.Admin.Lots
{
    public class UpdateLotsResponseDTO
    {
        public required string Message { get; set; }
        public required string LotCode { get; set; }
        public required int UpdatedQuantity { get; set; }
    }
}
