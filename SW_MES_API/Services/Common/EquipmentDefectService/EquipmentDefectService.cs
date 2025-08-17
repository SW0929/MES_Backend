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
        public async Task<EquipmentDefectResoponseDTO> HandleEquipmentDefectAsync(int defectID, EquipmentDefectRequestDTO request)
        {
            return await _equipmentRepository.HandleEquipmentDefectAsync(defectID, request);
        }
        public async Task<CreateEquipmentDefectResponseDTO> CreateEquipmentDefect(CreateEquipmentDefectRequestDTO request)
        {
            return await _equipmentRepository.RegisterEquipmentDefectAsync(request);
        }
    }
}
