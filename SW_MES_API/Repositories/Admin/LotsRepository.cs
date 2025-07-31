using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public class LotsRepository: ILotRepository
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
    }
}
