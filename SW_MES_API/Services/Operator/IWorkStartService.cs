using SW_MES_API.DTO.Operator;
using SW_MES_API.DTO.Operator.Performance;

namespace SW_MES_API.Services.Operator
{
    public interface IWorkStartService
    {
        Task<StartLotProcessResponseDTO> WorkStart(int lotProcessCode);
        Task<CompleteLotProcessResponseDTO> WorkComplete(int lotProcessCode);
        Task<PerformanceResponseDTO> LotPerformance(int lotProcessCode, PerformanceRequestDTO request);
    }
}
