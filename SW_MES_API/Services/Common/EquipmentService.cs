using SW_MES_API.DTO;
using SW_MES_API.DTO.Admin.WorkOrder;
using SW_MES_API.Models;
using SW_MES_API.Repositories.Common;

namespace SW_MES_API.Services.Common
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IEquipmentListRepository _equipmentListRepository;

        public EquipmentService(IEquipmentListRepository equipmentService)
        {
            _equipmentListRepository = equipmentService;
        }

        public async Task<EquipmentListResponseDTO> GetAlEquipmetsAsync()
        {
            var equipments = await _equipmentListRepository.GetALLEquipmentAsync();

            if (equipments == null || equipments.Count == 0)
            {
                return new EquipmentListResponseDTO
                {
                    Message = "조회 가능한 설비가 없습니다.",
                    Equipment = [] //new List<EquipmentListDTO>()
                };
            }

            // .Select(...)는 "매핑(mapping)" 작업을 수행
            var equipmentDTO = equipments.Select(wo => new WorkOrderResponseDTO
            {
                WorkOrderID = wo.WorkOrderID,
                ProductCode = wo.ProductCode,
                Quantity = wo.Quantity,
                SpecialNote = wo.SpecialNote,
                StartDate = wo.StartDate,
                EndDate = wo.EndDate
            }).ToList();

            return new WorkOrderListResponseDTO
            {
                Message = "작업 지시 조회 성공",
                WorkOrders = workOrderDTO
            };
            return await _equipmentListRepository.GetALLEquipmentAsync();

        }

        public async Task<List<EquipmentListResponseDTO>> GetEquipmentByProcessAsync(string ProcessCode)
        {
            return await _equipmentListRepository.GetALLEquipmentByProcessAsync(ProcessCode);
        }
    }
}
