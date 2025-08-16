using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.DTO.Common;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;

namespace SW_MES_API.Services.Common
{
    public interface IEquipmentService
    {
        // 설비 생성 (관리자)
        Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequestDTO request);

        // 설비 삭제 (관리자)
        Task<DeleteEquipmentResponseDTO> DeleteEquipmentAsync(string equipmentCode);

        // 설비 수정 (관리자)
        Task<UpdateEquipmentResponseDTO> UpdateEquipmentAsync(string equipmentCode, UpdateEquipmentRequestDTO request);


        // 모든 설비 조회
        Task<EquipmentListResponseDTO> GetAllEquipmentsAsync();
        // 특정 공정에 속한 설비 조회
        Task<EquipmentListResponseDTO> GetEquipmentByProcessAsync(string ProcessCode);

    }
}
