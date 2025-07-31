using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Common
{
    public class EquipmentListRepository: IEquipmentListRepository
    {
        private readonly AppDbContext _context;
        public EquipmentListRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipment>> GetALLEquipmentAsync()
        {
            return await _context.Equipment
                .ToListAsync();
        }

        public async Task<List<Equipment>> GetALLEquipmentByProcessAsync(string ProcessCode)
        {
            return await _context.Equipment
                .Where(e => e.ProcessCode == ProcessCode)
                .ToListAsync();
        }
    }
}
