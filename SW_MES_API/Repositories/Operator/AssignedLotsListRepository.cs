using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Operator
{
    public class AssignedLotsListRepository : IAssignedLotsListRepository
    {
        private readonly AppDbContext _context;
        public AssignedLotsListRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<AssignedLotsDTO>> GetAssignedLotsAsync(AssignedLotsRequestDTO request)
        {
            // 작업 날짜를 어떻게 처리할지 고민 해야 함.
            var query = from lp in _context.LotProcess
                        join l in _context.Lot on lp.LotCode equals l.LotCode
                        join wo in _context.WorkOrder on l.WorkOrderID equals wo.WorkOrderID
                        join p in _context.Products on wo.ProductCode equals p.ProductCode
                        join e in _context.Equipment on lp.EquipmentCode equals e.EquipmentCode into eq
                        from e in eq.DefaultIfEmpty()
                        where lp.IssuedBy == request.EmployeeID
                              && (lp.Status == "대기" || lp.Status == "진행중")
                        select new AssignedLotsDTO
                        {
                            LotProcessCode = lp.LotProcessCode,
                            LotCode = lp.LotCode,
                            ProcessCode = lp.ProcessCode,
                            ProductName = p.Name,
                            Quantity = l.Quantity,
                            Status = lp.Status,
                            EquipmentName = e.Name
                        };

            return await query.ToListAsync();
        }
    }
}
