using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Common
{
    public class EquipmentDefectRepository : IEquipmentDefectRepository
    {
        private readonly AppDbContext _context;
        public EquipmentDefectRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<EquipmentDefectResoponseDTO> HandleEquipmentDefectAsync(int defectID, EquipmentDefectRequestDTO request)
        {

            try
            {
                var equipmentDefect = await _context.EquipmentDefect.FindAsync(defectID);
                if (equipmentDefect == null)
                {
                    return new EquipmentDefectResoponseDTO
                    {
                        Message = "해당 결함이 존재하지 않습니다."
                    };
                }
                else
                {
                    // 결함 상태 업데이트
                    equipmentDefect.Status = request.Status;
                    equipmentDefect.SolvedBy = request.SolvedBy;
                    equipmentDefect.SolvedDate = request.SolvedDate ?? DateTime.Now;
                    // 변경된 내용을 데이터베이스에 저장
                    _context.EquipmentDefect.Update(equipmentDefect);
                    await _context.SaveChangesAsync();
                    return new EquipmentDefectResoponseDTO
                    {
                        Message = "설비 결함 처리 완료"
                    };
                }

            }
            catch (Exception ex)
            {
                // 예외 로그를 남기거나 처리하는 로직이 들어갈 수 있음
                return new EquipmentDefectResoponseDTO
                {
                    Message = $"결함 처리 중 오류 발생 : {ex.Message}"
                };
            }
        }

        public async Task<CreateEquipmentDefectResponseDTO> RegisterEquipmentDefectAsync(CreateEquipmentDefectRequestDTO request)
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
                await _context.EquipmentDefect.AddAsync(equipmentDefect);

                // 2. 설비 상태 "고장"으로 변경
                var equipment = await _context.Equipment
                    .FirstOrDefaultAsync(e => e.EquipmentCode == request.EquipmentCode);

                if (equipment == null)
                    throw new Exception("해당 설비가 존재하지 않습니다.");

                equipment.Status = "고장"; // 또는 Enum 사용 가능
                _context.Equipment.Update(equipment);

                // 3. 저장 (트랜잭션 범위)
                await _context.SaveChangesAsync();

                // 4. 커밋
                await transaction.CommitAsync();

                return new CreateEquipmentDefectResponseDTO
                {
                    Message = "설비 결함 등록 및 상태 변경 완료"
                };
            }
            catch (Exception ex)
            {
                // 롤백
                await transaction.RollbackAsync();

                return new CreateEquipmentDefectResponseDTO
                {
                    Message = $"설비 결함 등록 실패: {ex.Message}"
                };
            }

        }
    }
}
