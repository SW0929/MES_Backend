namespace SW_MES_API.DTO.Admin.Lots
{
    public class UpdateLotsRequestDTO
    {
        // 지금은 Quantity만 업데이트 가능하다고 가정
        public required int Quantity { get; set; }

    }
}
