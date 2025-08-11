namespace SW_MES_API.DTO.Operator
{
    public class CompleteLotProcessResponseDTO
    {
        public required string Message { get; set; }
        public int LotProcessCode { get; set; }
        public required string Status { get; set; }
    }
}
