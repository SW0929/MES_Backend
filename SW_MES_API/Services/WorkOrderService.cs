using SW_MES_API.DTO;
using SW_MES_API.Models;
using SW_MES_API.Repositories;

namespace SW_MES_API.Services
{
    public class WorkOrderService
    {
        private WorkOrderListRepository _repository;

        public WorkOrderService(WorkOrderListRepository repository)
        {
            _repository = repository;
        }

        // 날짜 없이 조회
        public async Task<WorkOrderListResponseDTO> GetAllWorkOrdersAsync()
        {
            var workOrders = await _repository.GetAllWorkOrdersAsync();

            if (workOrders == null || workOrders.Count == 0)
            {
                return new WorkOrderListResponseDTO
                {
                    Message = "작업 지시가 없습니다.",
                    WorkOrders = new List<WorkOrderResponseDTO>()
                };
            }

            var workOrderDTOs = workOrders.Select(wo => new WorkOrderResponseDTO
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
                WorkOrders = workOrderDTOs
            };
        }




        // 날짜로 조회
        public async Task<WorkOrderListResponseDTO> GetWorkOrdersByDateAsync(DateTime date)
        {
            // 1. 특정 날짜로 작업 지시 조회
            var workOrders = await _repository.GetWorkOrdersByDateAsync(date);

            // 2. 없으면 빈 리스트 반환
            if (workOrders == null || workOrders.Count == 0)
            {
                return new WorkOrderListResponseDTO
                {
                    Message = "해당 날짜에 작업 지시가 없습니다.",
                    WorkOrders = new List<WorkOrderResponseDTO>()
                };
            }

            // 3. Entity -> DTO 매핑
            var workOrderDTOs = workOrders.Select(wo => new WorkOrderResponseDTO
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
                WorkOrders = workOrderDTOs
            };
        }

    }
}
