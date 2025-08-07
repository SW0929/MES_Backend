using SW_MES_API.Data;
using SW_MES_API.DTO.Operator;

namespace SW_MES_API.Repositories.Operator
{
    public interface IAssignedLotsListRepository
    {
        Task<AssignedLotsResponseDTO> GetAssignedLotsAsync(AssignedLotsRequestDTO request);
    }
}
