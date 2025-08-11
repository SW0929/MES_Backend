using SW_MES_API.Data;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Operator
{
    public interface IAssignedLotsListRepository
    {
        Task<List<AssignedLotsDTO>> GetAssignedLotsAsync(AssignedLotsRequestDTO request);
    }
}
