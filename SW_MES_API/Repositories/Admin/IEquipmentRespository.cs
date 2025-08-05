using Microsoft.EntityFrameworkCore;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public interface IEquipmentRespository
    {
        // 설비 추가
        Task<CreateEquipmentResponse> CreateEquipmentAsync(CreateEquipmentRequestDTO request);
        // 설비 수정
        Task<UpdateEquipmentResponseDTO> UpdateEquipmentAsync(string equipmentCode, UpdateEquipmentRequestDTO request);

        // 설비 삭제
        Task<DeleteEquipmentResponseDTO> DeleteEquipmentAsync(string equipmentCode);

        // 설비 결함 처리
        Task HandleEquipmentDefectAsync(int defectID, EquipmentDefectRequestDTO request);
    }
}
