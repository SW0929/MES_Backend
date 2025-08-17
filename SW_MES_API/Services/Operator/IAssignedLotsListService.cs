using SW_MES_API.DTO.Operator.AssignedLots;

namespace SW_MES_API.Services.Operator
{
    public interface IAssignedLotsListService
    {
        Task<AssignedLotsResponseDTO> GetAssignedLotsAsync(AssignedLotsRequestDTO request);
    }
}
