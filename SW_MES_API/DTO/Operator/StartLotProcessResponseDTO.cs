namespace SW_MES_API.DTO.Operator
{
    public class StartLotProcessResponseDTO
    {
        public required string Message { get; set; }
        public required int LotProcessCode { get; set; }
        public required string Status { get; set; }
    }
}
