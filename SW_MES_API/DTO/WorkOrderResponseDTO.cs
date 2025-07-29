namespace SW_MES_API.DTO
{
    public class WorkOrderResponseDTO
    {
        public required string WorkOrderID { get; set; }
        public required string ProductCode { get; set; }
        public int Quantity { get; set; }
        public string? SpecialNote { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
