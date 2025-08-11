using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Operator;

namespace SW_MES_API.Services.Operator
{
    public class WorkStartService : IWorkStartService
    {
        private readonly IWorkStartRepository _workStartRepository;
        private readonly AppDbContext _context;
        public WorkStartService(IWorkStartRepository workStartRepository, AppDbContext context)
        {
            _workStartRepository = workStartRepository;
            _context = context; // Assuming the repository has a Context property
        }

        public async Task<CompleteLotProcessResponseDTO> WorkComplete(int lotProcessCode)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // LotProcess + Process + Lot 조인 (Sequence 포함)
                var lotProcess = await (from lp in _context.LotProcess
                                        join l in _context.Lot on lp.LotCode equals l.LotCode
                                        join p in _context.Process on lp.ProcessCode equals p.ProcessCode
                                        where lp.LotProcessCode == lotProcessCode
                                        select new
                                        {
                                            LotProcess = lp,
                                            Lot = l,
                                            Process = p
                                        }).FirstOrDefaultAsync();

                if (lotProcess == null)
                    throw new Exception("Lot process not found");

                // 현재 공정이 마지막 공정인지 판단 (Process 테이블에 Sequence 컬럼 있다고 가정)
                int currentSequence = lotProcess.Process.Sequence; // int형 공정 순서
                var lastProcessCode = "PRC-006";

                bool isLastProcess = lotProcess.Process.ProcessCode == lastProcessCode;

                var lotStatus = isLastProcess ? "완료" : "진행 중";

                // 1) 현재 LotProcess 완료 처리
                await _workStartRepository.CompleteAssignedLot(
                    lotProcess.LotProcess.LotProcessCode,
                    "완료",
                    lotProcess.Process.ProcessCode);

                // 2) 다음 공정이 있으면 LotProcess 신규 생성
                if (!isLastProcess)
                {
                    // 다음 공정 조회: 현재 공정 Sequence + 1 인 공정
                    var nextProcess = await _context.Process
                        .Where(p => p.Sequence == currentSequence + 1)
                        .FirstOrDefaultAsync();

                    if (nextProcess == null)
                        throw new Exception("다음 공정을 찾을 수 없습니다.");

                    // 다음 공정 LotProcess 생성
                    var newLotProcess = new LotProcess
                    {
                        LotCode = lotProcess.LotProcess.LotCode,
                        ProcessCode = nextProcess.ProcessCode,
                        Status = "대기",   // 초기 상태는 대기 혹은 적절한 값
                        GoodQty = null,
                        DefectQty = null,
                        StartDate = null,
                        EndDate = null,
                        EquipmentCode = null,
                        IssuedBy = null,
                        DefectCause = null
                    };

                    _context.LotProcess.Add(newLotProcess);
                    await _context.SaveChangesAsync();
                }

                // 트랜잭션 커밋
                await transaction.CommitAsync();

                return new CompleteLotProcessResponseDTO
                {
                    Message = "작업 종료 성공",
                    LotProcessCode = lotProcess.LotProcess.LotProcessCode,
                    Status = "완료"
                };
            }
            catch
            {
                // 오류 시 롤백
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<StartLotProcessResponseDTO> WorkStart(int lotProcessCode)
        {
            // 트랜잭션 시작
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // LotProcess + Process + Lot 조인 (Sequence 확인)
                var lotProcess = await (from lp in _context.LotProcess
                                        join l in _context.Lot on lp.LotCode equals l.LotCode
                                        join p in _context.Process on lp.ProcessCode equals p.ProcessCode
                                        where lp.LotProcessCode == lotProcessCode
                                        select new
                                        {
                                            LotProcess = lp,
                                            Lot = l,
                                            Process = p
                                        }).FirstOrDefaultAsync();

                if (lotProcess == null)
                    throw new Exception("Lot process not found");

                // 해당 Lot의 최대 Sequence 조회 (마지막 공정 여부 판단)
                string lastProcessCode = "PRC-006";

                bool isLastProcess = lotProcess.Process.ProcessCode.Equals(lastProcessCode);
                var lotStatus = isLastProcess ? "완료" : "진행 중";

                // Repository 호출 (DB 변경 작업)
                await _workStartRepository.StartAssignedLot(lotProcess.LotProcess.LotProcessCode, lotStatus, lotProcess.Process.ProcessCode);

                // 트랜잭션 커밋
                await transaction.CommitAsync();

                return new StartLotProcessResponseDTO
                {
                    Message = "작업 시작 성공",
                    LotProcessCode = lotProcess.LotProcess.LotProcessCode,
                    Status = "진행 중"
                };
            }
            catch
            {
                // 오류 시 롤백
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
