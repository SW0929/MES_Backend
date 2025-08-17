using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Operator.AssignedLots;
using SW_MES_API.Services.Operator;

namespace SW_MES_API.Controllers.Operator
{
    [ApiController]
    [Route("api/operator/[controller]")]
    public class AssingedLotsController : ControllerBase
    {
        private readonly IAssignedLotsListService _assignedLotsListService;
        public AssingedLotsController(IAssignedLotsListService assignedLotsListService)
        {
            _assignedLotsListService = assignedLotsListService;
        }
        // 작업자에게 할당된 Lot 목록 조회
        [HttpGet("assigned")]
        public async Task<IActionResult> GetAssignedLots([FromQuery] AssignedLotsRequestDTO request)
        {
            var response = await _assignedLotsListService.GetAssignedLotsAsync(request);
            return Ok(response);
        }
    }
}
