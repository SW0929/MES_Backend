using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO;
using SW_MES_API.Services;

namespace SW_MES_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkOrderController: ControllerBase
    {
        private readonly WorkOrderService _workOrderService;
        public WorkOrderController(WorkOrderService workOrderService)
        {
            _workOrderService = workOrderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetWorkOrders()
        {
            var response = await _workOrderService.GetAllWorkOrdersAsync();

            if (response.WorkOrders == null || response.WorkOrders.Count == 0)
            {
                return NotFound(new WorkOrderListResponseDTO
                {
                    Message = "작업 지시가 없습니다.",
                    WorkOrders = new List<WorkOrderResponseDTO>()
                });
            }

            return Ok(response);
        }


        [HttpGet("by-date")]
        public async Task<IActionResult> GetWorkOrdersWithDate([FromQuery] DateTime date)
        {
            var response = await _workOrderService.GetWorkOrdersByDateAsync(date);
            return Ok(response);
        }

    }
}
