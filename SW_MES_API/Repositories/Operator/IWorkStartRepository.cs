using SW_MES_API.DTO.Operator;

namespace SW_MES_API.Repositories.Operator
{
    public interface IWorkStartRepository
    {
        Task StartAssignedLot(int lotProcessCode, string lotStatus, string processCode);

        Task CompleteAssignedLot(int lotProcessCode, string lotStatus, string processCode);

        Task<PerformanceResponseDTO> LotPerformance(int lotProcessCode, PerformanceRequestDTO request);
    }
}
