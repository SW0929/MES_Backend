using Microsoft.EntityFrameworkCore;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public interface IEquipmentRespository
    {
        // 설비 추가
        Task<CreateEquipmentResponse> CreateEquipmentAsync(CreateEquipmentRequestDTO request);

        // 설비 삭제
        Task<DeleteEquipmentResponseDTO> DeleteEquipmentAsync(string equipmentCode);
    }
}
