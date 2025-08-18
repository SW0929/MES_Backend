using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;

namespace SW_MES_API.Services.Common.EquipmentDefectService
{
    public interface IEquipmentDefectService
    {
        // 설비 결함 처리 (관리자)
        Task<EquipmentDefectResoponseDTO> HandleEquipmentDefectAsync(int defectID, EquipmentDefectRequestDTO request);
        // 설비 결함 등록 (작업자)
        Task<CreateEquipmentDefectResponseDTO> CreateEquipmentDefect(CreateEquipmentDefectRequestDTO request);
    }
}
