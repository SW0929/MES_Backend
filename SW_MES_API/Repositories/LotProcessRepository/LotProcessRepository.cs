using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Operator.AssignedLots;
using SW_MES_API.DTO.Operator.Performance;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.LotProcessRepository
{

    public class LotProcessRepository : ILotProcessRepository
    {
        private readonly AppDbContext _context;
        public LotProcessRepository(AppDbContext context)
        {
            _context = context;
        }

        #region 작업자
        // 할당 된 작업 완료
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

        // 작업 완료한 Lot 실적 입력
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

        // 할당 된 작업 시작
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

        // 작업자가 할당 된 작업 목록 조회
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

        #endregion

        #region 관리자
        // 작업자, 장비 할당
        // 잠만 이거 LotProcessCode 값으로 해도 되는거 아니가 그걸로 하고 있긴한데 시벌
        public async Task<LotProcess?> GetLotProcessByLotCodeAsync(string lotCode, string processCode)
        {
            return await _context.LotProcess
            .FirstOrDefaultAsync(lp => lp.LotCode == lotCode && lp.ProcessCode == processCode);
        }

        // LotProcess의 작업자와 설비를 업데이트
        public async Task UpdateAssignmentAsync(LotProcess lotProcess, int employeeID, string equipmentCode)
        {
            lotProcess.IssuedBy = employeeID;
            lotProcess.EquipmentCode = equipmentCode;
            _context.LotProcess.Update(lotProcess);
            await _context.SaveChangesAsync();
        }


        // LotProcess를 추가
        public async Task AddLotProcessesAsync(List<LotProcess> lotProcesses)
        {
            await _context.LotProcess.AddRangeAsync(lotProcesses);
        }

        // LotProcess를 삭제
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
        #endregion
    }
}
