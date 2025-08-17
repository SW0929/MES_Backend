using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Admin.WorkOrder;
using SW_MES_API.Services.Admin.WorkOrderService;

namespace SW_MES_API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkOrderController: ControllerBase
    {
        private readonly IWorkOrderService _workOrderService;
        public WorkOrderController(IWorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkOrders()
        {
            var response = await _workOrderService.GetAllWorkOrdersAsync();

            if (response == null)
            {
                return NotFound(new WorkOrderListResponseDTO
                {
                    Message = "작업 지시가 없습니다.",
                    WorkOrders = [] //new List<WorkOrderResponseDTO>()
                });
            }

            return Ok(response);
        }


        [HttpGet("by-date")]
        public async Task<IActionResult> GetWorkOrdersWithDate([FromQuery] DateTime date)
        {
            var response = await _workOrderService.GetWorkOrdersByDateAsync(date);
            if (response == null)
                return NotFound(new WorkOrderListResponseDTO
                {
                    Message = "해당 날짜에 작업 지시가 없습니다.",
                    WorkOrders = [] //new List<WorkOrderResponseDTO>()
                });
            return Ok(response);
        }

    }
}
