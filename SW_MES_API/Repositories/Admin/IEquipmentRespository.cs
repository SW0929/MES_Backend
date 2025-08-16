using Microsoft.EntityFrameworkCore;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator;
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

        // 모든 설비 조회
        Task<List<Equipment>> GetALLEquipmentAsync();

        // 특정 공정에 속한 모든 설비 조회
        Task<List<Equipment>> GetALLEquipmentByProcessAsync(string ProcessCode);

    }
}
