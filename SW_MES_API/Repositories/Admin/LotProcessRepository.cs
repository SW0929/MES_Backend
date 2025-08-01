using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteLotProcessAsyc(string lotCode)
        {
            // LotCode 가 동일한 모든 행에 영향을 줌
            await _context.LotProcess
                .Where(lp => lp.LotCode == lotCode)
                .ExecuteDeleteAsync();

            /* Remove()와 ExecuteDeleteAsync()의 차이점
               Remove() → 조작이 필요한 경우, 전통적인 방식, 삭제 전 추가 로직 (ex. 로그 기록, 외래키 확인 등) 수행 가능
               ExecuteDeleteAsync() → 빠르고 간단한 대량 삭제용, 단순 조건 삭제에 최고, .NET 7.0 이상에서 사용 가능
            */
        }
    }
}
