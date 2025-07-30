using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public class WorkOrderListRepository
    {
        private readonly AppDbContext _context;

        public WorkOrderListRepository(AppDbContext context)
        {
            _context = context;
        }

        // 날짜 없는 조회
        public async Task<List<WorkOrder>> GetAllWorkOrdersAsync()
        {
            return await _context.WorkOrder.ToListAsync();
        }

        // 날짜 있는 조회
        public async Task<List<WorkOrder>> GetWorkOrdersByDateAsync(DateTime date)
        {
            return await _context.WorkOrder
                .Where(wo => wo.StartDate.Date == date.Date)
                .ToListAsync();
        }

    }
}
