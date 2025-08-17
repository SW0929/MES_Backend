using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.LotRepository
{
    public class LotsRepository : ILotRepository
    {
        private readonly AppDbContext _context;
        public LotsRepository(AppDbContext context)
        {
            _context = context;
        }

        // InsertLotsAsync 메서드는 CreateLotRequestDTO를 받아서 여러 개의 Lot을 생성합니다.
        // request는 CreateLotRequestDTO → 전체 요청
        // dto는 LotItemDTO → 각 Lot의 상세 정보

        public async Task AddLotsAsync(List<Lot> lots)
        {
            await _context.Lot.AddRangeAsync(lots);

        }

        public async Task UpdateLotAsync(string lotCode, int quantity)
        {
            // 조건에 맞는 첫 번째 행을 비동기로 조회합니다. 없으면 null 반환.
            var lot = await _context.Lot.FirstOrDefaultAsync(l => l.LotCode == lotCode);
            if (lot == null)
                throw new Exception("Lot not found");

            lot.Quantity = quantity;
            await _context.SaveChangesAsync();

        }

        // ResponseDTO 만들어서 처리할지 확인 바람.
        public async Task DeleteLotAsync(string lotCode)
        {
            // 조건에 맞는 첫 번째 행을 비동기로 조회합니다. 없으면 null 반환.
            var lot = await _context.Lot.FirstOrDefaultAsync(l => l.LotCode == lotCode);
            if (lot == null)
                throw new Exception("Lot not found");
            _context.Lot.Remove(lot);
            await _context.SaveChangesAsync();
        }
    }
}
