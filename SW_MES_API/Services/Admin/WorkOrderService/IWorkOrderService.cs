using SW_MES_API.DTO.Admin.WorkOrder;

namespace SW_MES_API.Services.Admin.WorkOrderService
{
    public interface IWorkOrderService
    {
        public Task<WorkOrderListResponseDTO?> GetAllWorkOrdersAsync();

        public Task<WorkOrderListResponseDTO?> GetWorkOrdersByDateAsync(DateTime date);
    }
}
