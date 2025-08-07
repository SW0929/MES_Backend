using SW_MES_API.Data;
using SW_MES_API.DTO.Operator;

namespace SW_MES_API.Repositories.Operator
{
    public class AssignedLotsListRepository : IAssignedLotsListRepository
    {
        private readonly AppDbContext _context;
        public AssignedLotsListRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<AssignedLotsResponseDTO> GetAssignedLotsAsync(AssignedLotsRequestDTO request)
        {
            var lotProcess = await _context.LotProcess.FindAsync(request.EmployeeID, request.Date);
            if (lot == null)
            {
                return null; // 또는 적절한 예외 처리
            }
            return new AssignedLotsResponseDTO
            {
                
            };
        }
    }
}
