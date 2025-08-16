using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Repositories.Admin;
using SW_MES_API.Repositories.Common;

namespace SW_MES_API.Services.Common
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
