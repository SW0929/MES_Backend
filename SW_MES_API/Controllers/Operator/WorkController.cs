using Microsoft.AspNetCore.Mvc;
using SW_MES_API.Services.Operator;

namespace SW_MES_API.Controllers.Operator
{
    [ApiController]
    [Route("api/operator/[controller]")]
    public class WorkController : ControllerBase
    {
        private readonly IWorkStartService _workService;
        public WorkController(IWorkStartService workService)
        {
            _workService = workService;
        }

        // 작업 시작
        [HttpPatch("start/{lotProcessCode:int}")]
        public async Task<IActionResult> StartLot(int lotProcessCode)
        {
            var response = await _workService.WorkStart(lotProcessCode);
            return Ok(response);
        }

        [HttpPatch("complete/{lotProcessCode:int}")]
        public async Task<IActionResult> CompleteLot(int lotProcessCode)
        {
            var response = await _workService.WorkComplete(lotProcessCode);
            return Ok(response);
        }
    }
}
