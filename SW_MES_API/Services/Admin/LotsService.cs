using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Admin;

namespace SW_MES_API.Services.Admin
{
    /*
    <현재 문제점>
    1. INSERT 할 때 원하는 값을 넣지 못함
    2. ENTITY 한번 확인해 봐야 함.
    */

    public class LotsService: ILotService
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
                CurrentProcess = dto.CurrentProcess

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
                        ProcessCode = "PRC-001"
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
    }
}
