using SW_MES_API.DTO;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Admin.WorkOrder;
using SW_MES_API.DTO.Common;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Admin;
using SW_MES_API.Repositories.Common;

namespace SW_MES_API.Services.Common
{

    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentRespository _equipmentRepository;

        public EquipmentService(IEquipmentRespository equipmentRespository)
        {
            
            _equipmentRepository = equipmentRespository;
        }

        public async Task<CreateEquipmentResponse> CreateEquipment(CreateEquipmentRequestDTO request)
        {
            return await _equipmentRepository.CreateEquipmentAsync(request);
        }

        public async Task<DeleteEquipmentResponseDTO> DeleteEquipmentAsync(string equipmentCode)
        {
            return await _equipmentRepository.DeleteEquipmentAsync(equipmentCode);
        }

        public async Task<EquipmentListResponseDTO> GetAllEquipmentsAsync()
        {
            var equipments = await _equipmentRepository.GetALLEquipmentAsync();

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
            var equipments = await _equipmentRepository.GetALLEquipmentByProcessAsync(processCode);

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

        public async Task<UpdateEquipmentResponseDTO> UpdateEquipmentAsync(string equipmentCode, UpdateEquipmentRequestDTO request)
        {
            return await _equipmentRepository.UpdateEquipmentAsync(equipmentCode, request);
        }

        
    }
}
