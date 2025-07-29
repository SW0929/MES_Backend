namespace SW_MES_API.DTO
{
    public class WorkOrderListResponseDTO
    {
        public string? Message { get; set; }
        public List<WorkOrderResponseDTO>? WorkOrders { get; set; }
    }
}
