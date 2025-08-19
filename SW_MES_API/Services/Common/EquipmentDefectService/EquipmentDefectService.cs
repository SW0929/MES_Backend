using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;
using SW_MES_API.Models;
using SW_MES_API.Repositories.EquipmentDefectRepository;

namespace SW_MES_API.Services.Common.EquipmentDefectService
{
    public class EquipmentDefectService : IEquipmentDefectService
    {
        private readonly IEquipmentDefectRepository _equipmentRepository;
        private readonly AppDbContext _context;
        public EquipmentDefectService(AppDbContext context, IEquipmentDefectRepository equipmentRepository)
        {
            _context = context;
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
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1. 설비 결함 등록
                var equipmentDefect = new EquipmentDefect
                {
                    EquipmentCode = request.EquipmentCode,
                    DefectDate = request.DefectDate,
                    IssuedBy = request.IssuedBy,
                    Status = request.Status,
                    DefectReason = request.DefectReason
                };
                await _equipmentRepository.CreateEquipmentDefect(equipmentDefect);

                // 2. 설비 상태 "고장"으로 변경
                var equipment = await _equipmentRepository.GetEquipment(request.EquipmentCode);

                if (equipment == null)
                    throw new Exception("해당 설비가 존재하지 않습니다.");

                equipment.Status = "고장";
                //SaveChangesAsync() 호출 시 변경 감지가 일어나니까 Update() 호출이 없어도 반영
                //_equipmentRepository.UpdateEquipmentStatus(equipment);

                // 3. 저장 + 트랜잭션 Commit
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return new CreateEquipmentDefectResponseDTO
                {
                    Message = "설비 결함 등록 및 상태 변경 완료"
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new CreateEquipmentDefectResponseDTO
                {
                    Message = $"설비 결함 등록 실패: {ex.Message}"
                };
            }
        }
    }
}
