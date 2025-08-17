using SW_MES_API.DTO.Operator.AssignedLots;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.WorkOrderRepository
{
    public interface IWorkOrderRepository
    {
        // 작업 지시서 모두 조회
        public Task<List<WorkOrder>> GetAllWorkOrdersAsync();
        // 특정 날짜의 작업 지시서 조회
        public Task<List<WorkOrder>> GetWorkOrdersByDateAsync(DateTime date);

        
    }
}
