namespace SW_MES_API.DTO.Operator.Performance
{
    public class PerformanceRequestDTO
    {
        public int GoodQty { get; set; }
        public int DefectQty { get; set; }
        public string? DefectCause { get; set; }
    }
}
