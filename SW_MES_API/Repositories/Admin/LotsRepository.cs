using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public class LotsRepository
    {
        private readonly AppDbContext _context;
        public LotsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Lot>> InsertLotsAsync(CreateLotRequestDTO request)
        {
            var lots = lotDTOs.Select(dto => new Lot
            {
                LotCode = dto.

            })
            return await _context.Lot.AddAsync();
        }
    }
}
