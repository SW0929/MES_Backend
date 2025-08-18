using Microsoft.EntityFrameworkCore;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;
using SW_MES_API.Repositories.EquipmentDefectRepository;

namespace SW_MES_API.Services.Common.EquipmentDefectService
{
    public class EquipmentDefectService : IEquipmentDefectService
    {
        private readonly IEquipmentDefectRepository _equipmentRepository;
        public EquipmentDefectService(IEquipmentDefectRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        // 설비 결함 조치 (관리자)
        public async Task<EquipmentDefectResoponseDTO> HandleEquipmentDefectAsync(int defectID, EquipmentDefectRequestDTO request)
        {
            try
            {
                var equipmentDefect = await _equipmentRepository.GetEquipmentDefectAsync(defectID);
                if (equipmentDefect == null)
                {
                    return new EquipmentDefectResoponseDTO
                    {
                        Message = "해당 설비 결함이 존재하지 않습니다." 
                    };
                }
                equipmentDefect.Status = request.Status;
                equipmentDefect.SolvedBy = request.SolvedBy;
                equipmentDefect.SolvedDate = request.SolvedDate ?? DateTime.Now;

                await _equipmentRepository.UpdateEquipmentDefectAsync(equipmentDefect);
                return new EquipmentDefectResoponseDTO
                {
                    Message = "설비 결함 처리 완료",
                };

            }
            catch (DbUpdateException ex)
            {
                // DB 관련 에러 → 사용자 친화적 메시지
                return new EquipmentDefectResoponseDTO
                {
                    Message = $"DB 업데이트 중 오류가 발생했습니다. : {ex.Message}"
                };
            }
            catch (Exception ex)
            {
                // 알 수 없는 에러
                return new EquipmentDefectResoponseDTO
                {
                    Message = $"결함 처리 중 오류 발생: {ex.Message}"
                };
            }

        }

        // 설비 결함 등록 (작업자)
        public async Task<CreateEquipmentDefectResponseDTO> CreateEquipmentDefect(CreateEquipmentDefectRequestDTO request)
        {
            return await _equipmentRepository.RegisterEquipmentDefectAsync(request);
        }
    }
}
