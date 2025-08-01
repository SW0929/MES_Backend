namespace SW_MES_API.DTO.Admin.Lots
{
    public class LotItemDTO
    {
        public required string LotCode { get; set; }
        public int Quantity { get; set; }
        public required string CurrentProcess { get; set; } //DB에 Default 값 설정 함.
        public required string Status { get; set; } //DB에 Default 값 설정 함.
    }
}
