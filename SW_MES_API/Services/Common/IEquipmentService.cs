using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.DTO.Common;
using SW_MES_API.Models;

namespace SW_MES_API.Services.Common
{
    public interface IEquipmentService
    {
        Task<EquipmentListResponseDTO> GetAllEquipmentsAsync();

        Task<EquipmentListResponseDTO> GetEquipmentByProcessAsync(string ProcessCode);
    }
}
