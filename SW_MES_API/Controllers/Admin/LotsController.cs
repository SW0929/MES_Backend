using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Services.Admin;

namespace SW_MES_API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController : ControllerBase
    {
        private readonly ILotService _lotsService;

        public LotsController(ILotService lotsService)
        {
            _lotsService = lotsService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertLots([FromBody] CreateLotRequestDTO request)
        {
            var result = await _lotsService.InsertLotsAsync(request);

            if (result == null || !result.Any())
                return BadRequest(new { message = "Lot 생성 실패" });

            return Created("", new { message = "Lot 생성 완료", createdLots = result });
        }

        [HttpPatch("{code}")]
        public async Task<IActionResult> UpdateLot(string code, [FromBody] UpdateLotsRequestDTO request)
        {
            var result = await _lotsService.UpdateLotsAsync(code, request);
            if (result == null)
                return NotFound(new { message = $"Lot '{code}'을(를) 찾을 수 없습니다." });
            return Ok(new { message = "Lot 업데이트 완료", updatedLot = result });
        }

        [HttpDelete("{code}")]
        public async Task<IActionResult> DeleteLot(string code)
        {
            var result = await _lotsService.DeleteLotsAsync(code);
            if (result == null)
                return NotFound(new { message = $"Lot '{code}'을(를) 찾을 수 없습니다." });
            return Ok(new { message = "Lot 삭제 완료", deletedLot = result });
        }

        [HttpPatch("lot-process/assign")]
        public async Task<IActionResult> AssignLotProcess([FromBody] AssignLotRequestDTO request)
        {
            var result = await _lotsService.AssignLotAsync(request);
            if (result == null)
                return NotFound(new { message = $"Lot '{request.LotCode}'을(를) 찾을 수 없습니다." });
            return Ok(new { message = "작업자/설비 할당 완료", assignedLot = result });
        }
    }
}
