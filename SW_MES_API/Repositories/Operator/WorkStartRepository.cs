using SW_MES_API.Data;
using SW_MES_API.DTO.Operator;

namespace SW_MES_API.Repositories.Operator
{

    public class WorkStartRepository : IWorkStartRepository
    {
        private readonly AppDbContext _context;
        public WorkStartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CompleteAssignedLot(int lotProcessCode, string lotStatus, string processCode)
        {
            var lotProcess = await _context.LotProcess.FindAsync(lotProcessCode);
            if (lotProcess == null)
                throw new Exception("Lot process not found");
            // LotProcess 상태 변경
            lotProcess.Status = "완료";
            lotProcess.EndDate = DateTime.Now;

            // Lot 상태 변경 + 현재 공정 업데이트
            var lot = await _context.Lot.FindAsync(lotProcess.LotCode);
            if (lot != null)
            {
                lot.Status = lotStatus;
                lot.CurrentProcess = processCode;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<PerformanceResponseDTO> LotPerformance(int lotProcessCode, PerformanceRequestDTO request)
        {
            try
            {
                var performance = await _context.LotProcess.FindAsync(lotProcessCode);
                if (performance == null)
                    return new PerformanceResponseDTO 
                    {
                        Message = "해당 LotProcess를 찾을 수 없습니다.",
                    };
                else
                {
                    // 성능 데이터 처리 로직
                    performance.GoodQty = request.GoodQty;
                    performance.DefectQty = request.DefectQty;
                    performance.DefectCause = request.DefectCause;
                    _context.LotProcess.Update(performance);
                    await _context.SaveChangesAsync();
                    return new PerformanceResponseDTO
                    { Message = "성능 데이터가 성공적으로 업데이트되었습니다." };
                }
                    
            }
            catch (Exception ex)
            {
                // 예외 처리 로직
                throw new Exception($"Error calculating performance: {ex.Message}");
            }


        }

        public async Task StartAssignedLot(int lotProcessCode, string lotStatus, string processCode)
        {
            var lotProcess = await _context.LotProcess.FindAsync(lotProcessCode);
            if (lotProcess == null)
                throw new Exception("Lot process not found");

            // LotProcess 상태 변경
            lotProcess.Status = "진행 중";
            lotProcess.StartDate = DateTime.Now;

            // Lot 상태 변경 + 현재 공정 업데이트
            var lot = await _context.Lot.FindAsync(lotProcess.LotCode);
            if (lot != null)
            {
                lot.Status = lotStatus;
                lot.CurrentProcess = processCode;
            }

            await _context.SaveChangesAsync();
        }
    }
}
