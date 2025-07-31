using SW_MES_API.Data;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public class LotProcessRepository : ILotProcessRepository
    {
        private readonly AppDbContext _context;

        public LotProcessRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLotProcessesAsync(List<LotProcess> lotProcesses)
        {
            await _context.LotProcess.AddRangeAsync(lotProcesses);
        }
    }
}
