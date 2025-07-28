namespace SW_MES_API.Models
{
    public class WorkOrder
    {
        public required string WorkOrderID { get; set; }
        public required string ProductCode { get; set; }
        public required int Quantity { get; set; }
        public required string SpecialNote { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }

    }
}
