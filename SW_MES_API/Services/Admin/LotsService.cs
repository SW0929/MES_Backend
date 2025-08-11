using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.LotProcess;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Admin;

namespace SW_MES_API.Services.Admin
{
    /*
    <현재 문제점>
    1. INSERT 할 때 원하는 값을 넣지 못함
    2. ENTITY 한번 확인해 봐야 함.
    3. 여기서 DB 접근 즉, AppDbContext를 사용하지 않아야 함 (수정 필요 함) 트랜잭션 때문에 그런듯
    */

    public class LotsService : ILotService
    {
        private readonly AppDbContext _context;
        private readonly ILotRepository _lotRepository;
        private readonly ILotProcessRepository _lotProcessRepository;

        public LotsService(AppDbContext context, ILotRepository lotRepository, ILotProcessRepository lotProcessRepository)
        {
            _context = context;
            _lotRepository = lotRepository;
            _lotProcessRepository = lotProcessRepository;
        }

        // 가장 초반에 Lot을 생성하는 메서드 임으로 LotProcess의 ProcessCode는 첫 단계인 프레스(PRC-001)로 고정
        public async Task<List<Lot>> InsertLotsAsync(CreateLotRequestDTO request)
        {


            var lots = request.Lot?.Select(dto => new Lot
            {
                WorkOrderID = request.WorkOrderID,
                CreatedBy = request.CreatedBy,
                LotCode = dto.LotCode,
                Quantity = dto.Quantity,
                CurrentProcess = dto.CurrentProcess,
                Status = dto.Status

            }).ToList();

            if (lots == null || lots.Count == 0)
                return []; //new List<Lot>();



            // 트랜잭션 구조
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _lotRepository.AddLotsAsync(lots);

                var lotProcesses = new List<LotProcess>();

                foreach (var lot in lots)
                {
                    var lotProcess = new LotProcess
                    {
                        LotCode = lot.LotCode,
                        ProcessCode = "PRC-001",
                        Status = "대기", // 초기 상태는 진행 중으로 설정
                    };
                    lotProcesses.Add(lotProcess);
                }

                await _lotProcessRepository.AddLotProcessesAsync(lotProcesses);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return lots;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return []; //new List<Lot>();
            }

        }

        public async Task<UpdateLotsResponseDTO> UpdateLotsAsync(string lotCode, UpdateLotsRequestDTO request)
        {
            await _lotRepository.UpdateLotAsync(lotCode, request.Quantity);

            return new UpdateLotsResponseDTO
            {
                Message = "Lot updated successfully",
                LotCode = lotCode,
                UpdatedQuantity = request.Quantity
            };
        }

        public async Task<DeleteLotsResponseDTO> DeleteLotsAsync(string lotCode)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                //LotProcess 모두 삭제 후 Lot 해야지 아니면 FK 제약 때문에 삭제 안됨   
                await _lotProcessRepository.DeleteLotProcessAsyc(lotCode);
                await _lotRepository.DeleteLotAsync(lotCode);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return new DeleteLotsResponseDTO
                {
                    Message = "Lot deleted successfully",
                    LotCode = lotCode
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new DeleteLotsResponseDTO
                {
                    Message = $"Error deleting lot: {ex.Message}",
                    LotCode = lotCode
                };
            }
        }

        public async Task<AssignLotResponseDTO> AssignLotAsync(AssignLotRequestDTO request)
        {
            var lotProcess = await _lotProcessRepository.GetLotProcessByLotCodeAsync(request.LotCode, request.ProcessCode);
            if (lotProcess == null)
                throw new Exception("해당 공정의 LotProcess를 찾을 수 없습니다.");

            await _lotProcessRepository.UpdateAssignmentAsync(lotProcess, request.EmployeeID, request.EquipmentCode);

            return new AssignLotResponseDTO
            {
                Message = "작업자 및 장비 할당 완료",
                LotCode = request.LotCode,
                LotProcessCode = lotProcess.ProcessCode, // 이거 LotCode 인지 고민 좀 해봐야 함.
                AssignedEmployee = request.EmployeeID,
                AssignedEquipmentCode = request.EquipmentCode
            };
        }
    }
}
