using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator.EquipmentDefect;
using SW_MES_API.Services.Common.EquipmentDefectService;

namespace SW_MES_API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentDefectController : ControllerBase
    {
        private readonly IEquipmentDefectService _equipmentService;
        public EquipmentDefectController(IEquipmentDefectService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        // 설비 결함 처리
        [HttpPatch("admin/defect/{defectID}")]
        public async Task<IActionResult> HandleEquipmentDefect(int defectID, [FromBody] EquipmentDefectRequestDTO request)
        {
            var result = await _equipmentService.HandleEquipmentDefectAsync(defectID, request);
            return Ok(result);
        }
        // 설비 결함 등록
        [HttpPost("operator/defect")] // 설비 결함 등록
        public async Task<IActionResult> CreateEquipmentDefect([FromBody] CreateEquipmentDefectRequestDTO request)
        {
            if (request == null)
                return BadRequest(new { message = "잘못된 요청입니다." });
            var result = await _equipmentService.CreateEquipmentDefect(request);
            if (result == null)
                return BadRequest(new { message = "설비 결함 등록 실패" });
            return StatusCode(201, new { message = "설비 결함 등록 완료" });
        }
    }
}
