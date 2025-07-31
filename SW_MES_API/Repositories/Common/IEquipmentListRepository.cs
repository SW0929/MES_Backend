using SW_MES_API.DTO;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Common
{
    public interface IEquipmentListRepository
    {
        // 모든 설비 조회
        Task<List<Equipment>> GetALLEquipmentAsync();

        // 특정 공정에 속한 모든 설비 조회
        Task<List<Equipment>> GetALLEquipmentByProcessAsync(string ProcessCode);
    }
}
