using Microsoft.EntityFrameworkCore;
using SW_MES_API.Data;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.EquipmentRepository
{
    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly AppDbContext _context;

        public EquipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        // 새로운 설비 추가
        public async Task<CreateEquipmentResponse> CreateEquipmentAsync(CreateEquipmentRequestDTO request)
        {
            try
            {
                var equipment = new Equipment
                {
                    EquipmentCode = request.EquipmentCode,
                    Name = request.Name,
                    ProcessCode = request.ProcessCode,
                    Status = request.Status,
                };


                await _context.Equipment.AddAsync(equipment);
                await _context.SaveChangesAsync();

                return new CreateEquipmentResponse
                {
                    Message = "설비 추가 완료"
                };
            }
            catch (Exception ex)
            {
                // 로깅하거나 커스텀 예외 던지기
                return new CreateEquipmentResponse
                {
                    Message = $"설비 추가 실패: {ex.Message}"
                };
            }
        }

        // 설비 정보 수정
        public async Task<UpdateEquipmentResponseDTO> UpdateEquipmentAsync(string equipmentCode, UpdateEquipmentRequestDTO request)
        {
            try
            {
                var equipment = await _context.Equipment.FindAsync(equipmentCode);
                if (equipment == null)
                {
                    return new UpdateEquipmentResponseDTO
                    {
                        Message = "해당 설비가 존재하지 않습니다."
                    };
                }
                // 요청 DTO의 값을 사용하여 설비 정보를 업데이트
                equipment.Name = request.Name;
                equipment.ProcessCode = request.ProcessCode;
                equipment.Status = request.Status;

                // 변경된 내용을 데이터베이스에 저장
                _context.Equipment.Update(equipment);
                await _context.SaveChangesAsync();
                return new UpdateEquipmentResponseDTO
                { 
                    Message = "설비 수정 완료" 
                };
            }
            catch (Exception ex)
            {
                // 예외 로그를 남기거나 처리하는 로직이 들어갈 수 있음
                return new UpdateEquipmentResponseDTO
                {
                    Message = $"설비 수정 중 오류 발생: {ex.Message}"
                };
            }
        }
        // 설비 삭제
        public async Task<DeleteEquipmentResponseDTO> DeleteEquipmentAsync(string equipmentCode)
        {
            // 이 부분 서비스에서 처리 해야함.
            try
            {
                // FindAsync 메서드를 사용하여 비동기적으로 설비를 조회 (PK 일 때만 사용 가능) PK 아니면 FirstOrDefaultAsync 사용
                var equipment = await _context.Equipment.FindAsync(equipmentCode);

                if (equipment == null)
                {
                    return new DeleteEquipmentResponseDTO
                    {
                        EquipmentCode = equipmentCode,
                        Message = "해당 설비가 존재하지 않습니다."
                    };
                }

                _context.Equipment.Remove(equipment);
                await _context.SaveChangesAsync();

                return new DeleteEquipmentResponseDTO
                {
                    EquipmentCode = equipmentCode,
                    Message = "설비 삭제 완료"
                };
            }
            catch (Exception ex)
            {
                // 예외 로그를 남기거나 처리하는 로직이 들어갈 수 있음
                return new DeleteEquipmentResponseDTO
                {
                    EquipmentCode = equipmentCode,
                    Message = $"설비 삭제 중 오류 발생: {ex.Message}"
                };
            }


        }
        // 모든 설비 조회
        public async Task<List<Equipment>> GetALLEquipmentAsync()
        {
            return await _context.Equipment
                .ToListAsync();
        }
        // 특정 공정에 속한 모든 설비 조회
        public async Task<List<Equipment>> GetALLEquipmentByProcessAsync(string ProcessCode)
        {
            return await _context.Equipment
                .Where(e => e.ProcessCode == ProcessCode)
                .ToListAsync();
        }
    }
}
