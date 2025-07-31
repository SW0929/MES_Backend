using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Services.Admin;

namespace SW_MES_API.Controllers.Admin
{
    [ApiController]
    [Route("api/[controller]")]
    public class LotsController: ControllerBase
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
    }
}
