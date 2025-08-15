using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin.WorkOrderList
{
    public interface IWorkOrderRepository
    {
        public Task<List<WorkOrder>> GetAllWorkOrdersAsync();

        public Task<List<WorkOrder>> GetWorkOrdersByDateAsync(DateTime date);
    }
}
