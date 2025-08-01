using SW_MES_API.DTO;
using SW_MES_API.DTO.Admin.WorkOrder;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Common;

namespace SW_MES_API.Services.Common
{

    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentListRepository _equipmentListRepository;

        public EquipmentService(IEquipmentListRepository equipmentRepository)
        {
            _equipmentListRepository = equipmentRepository;
        }

        public async Task<EquipmentListResponseDTO> GetAllEquipmentsAsync()
        {
            var equipments = await _equipmentListRepository.GetALLEquipmentAsync();

            if (equipments == null || equipments.Count == 0)
            {
                return new EquipmentListResponseDTO
                {
                    Message = "조회 가능한 설비가 없습니다.",
                    Equipments = []
                };
            }

            var equipmentDTO = equipments.Select(eq => new EquipmentResponseDTO
            {
                EquipmentCode = eq.EquipmentCode,
                Name = eq.Name,
                ProcessCode = eq.ProcessCode,
                Status = eq.Status,
                LastUsedDate = eq.LastUsedDate
            }).ToList();

            return new EquipmentListResponseDTO
            {
                Message = "전체 설비 조회 성공",
                Equipments = equipmentDTO
            };
        }

        public async Task<EquipmentListResponseDTO> GetEquipmentByProcessAsync(string processCode)
        {
            var equipments = await _equipmentListRepository.GetALLEquipmentByProcessAsync(processCode);

            if (equipments == null || equipments.Count == 0)
            {
                return new EquipmentListResponseDTO
                {
                    Message = $"공정 코드 {processCode}에 대한 설비가 없습니다.",
                    Equipments = []
                };
            }

            var equipmentDTO = equipments.Select(eq => new EquipmentResponseDTO
            {
                EquipmentCode = eq.EquipmentCode,
                Name = eq.Name,
                ProcessCode = eq.ProcessCode,
                Status = eq.Status,
                LastUsedDate = eq.LastUsedDate
            }).ToList();

            return new EquipmentListResponseDTO
            {
                Message = $"공정 코드 {processCode}의 설비 조회 성공",
                Equipments = equipmentDTO
            };
        }
    }
}
