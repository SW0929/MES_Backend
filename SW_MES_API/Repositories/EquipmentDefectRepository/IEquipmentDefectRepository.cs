using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;

namespace SW_MES_API.Repositories.EquipmentDefectRepository
{
    public interface IEquipmentDefectRepository
    {
        // 설비 결함 처리
        Task<EquipmentDefectResoponseDTO> HandleEquipmentDefectAsync(int defectID, EquipmentDefectRequestDTO request);
        // 설비 결함 등록
        Task<CreateEquipmentDefectResponseDTO> RegisterEquipmentDefectAsync(CreateEquipmentDefectRequestDTO request);
    }
}
