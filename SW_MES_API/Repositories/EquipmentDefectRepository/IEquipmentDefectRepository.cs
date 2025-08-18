using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.EquipmentDefectRepository
{
    public interface IEquipmentDefectRepository
    {
        Task<EquipmentDefect?> GetEquipmentDefectAsync(int defectID);

        // 설비 결함 처리
        Task UpdateEquipmentDefectAsync(EquipmentDefect request);
        // 설비 결함 등록
        Task<CreateEquipmentDefectResponseDTO> RegisterEquipmentDefectAsync(CreateEquipmentDefectRequestDTO request);
    }
}
