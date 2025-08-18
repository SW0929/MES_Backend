using SW_MES_API.DTO.Operator;
using SW_MES_API.DTO.Operator.AssignedLots;
using SW_MES_API.DTO.Operator.Performance;

namespace SW_MES_API.Services.Operator.LotProcessService
{
    public interface ILotProcessService
    {
        Task<StartLotProcessResponseDTO> WorkStart(int lotProcessCode);
        Task<CompleteLotProcessResponseDTO> WorkComplete(int lotProcessCode);
        Task<PerformanceResponseDTO> LotPerformance(int lotProcessCode, PerformanceRequestDTO request);

        Task<AssignedLotsResponseDTO> GetAssignedLotsAsync(AssignedLotsRequestDTO request);
    }
}
