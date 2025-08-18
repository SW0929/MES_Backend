using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.EquipmentDefectRepository
{
    public class EquipmentDefectRepository : IEquipmentDefectRepository
    {
        private readonly AppDbContext _context;
        public EquipmentDefectRepository(AppDbContext context)
        {
            _context = context;
        }

        // 결함 ID로 설비 결함 조회
        public async Task<EquipmentDefect?> GetEquipmentDefectAsync(int defectID)
        {
            return await _context.EquipmentDefect.FindAsync(defectID);
        }

        #region 관리자
        /// <summary>
        /// 작업자가 등록한 설비 결함을 관리자가 처리하는 메서드
        /// </summary>
        public async Task UpdateEquipmentDefectAsync(EquipmentDefect defect)
        {
            _context.EquipmentDefect.Update(defect); // 결함 정보 업데이트
            await _context.SaveChangesAsync(); // 변경 사항 저장

        }
        #endregion

        #region 작업자
        // 작업자가 설비 결함을 등록하는 메서드 (트랜잭션은 서비스 로직에서 처리)
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
                await _context.EquipmentDefect.AddAsync(equipmentDefect); // 결함 추가

                // 2. 설비 상태 "고장"으로 변경
                var equipment = await _context.Equipment
                    .FirstOrDefaultAsync(e => e.EquipmentCode == request.EquipmentCode);

                if (equipment == null)
                    throw new Exception("해당 설비가 존재하지 않습니다.");

                equipment.Status = "고장"; // 또는 Enum 사용 가능
                _context.Equipment.Update(equipment); // 설비 상태 업데이트

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
        #endregion
    }
}