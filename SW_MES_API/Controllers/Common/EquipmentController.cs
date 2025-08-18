using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SW_MES_API.DTO.Admin.Equipment;
using SW_MES_API.DTO.Operator;
using SW_MES_API.Services.Common.EquipmentService;

namespace SW_MES_API.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpPost("admin/create")] // 설비 추가
        public async Task<IActionResult> CreateEquipment([FromBody] CreateEquipmentRequestDTO request)
        {
            var result = await _equipmentService.CreateEquipment(request);

            if (result == null)
                return BadRequest(new { message = "설비 추가 실패" });

            return StatusCode(201, new { message = "설비 추가 완료" });

        }

        [HttpPut("admin/{equipmentCode}")] // 설비 수정
        public async Task<IActionResult> UpdateEquipment(string equipmentCode, [FromBody] UpdateEquipmentRequestDTO request)
        {
            var reslt = await _equipmentService.UpdateEquipmentAsync(equipmentCode, request);
            if (reslt == null)
                return NotFound(new { message = "해당 설비를 찾을 수 없습니다." });
            return Ok(new { message = reslt.Message });
        }

        [HttpDelete("admin/{equipmentCode}")] // 설비 삭제
        public async Task<IActionResult> DeleteEquipment(string equipmentCode)
        {
            var result = await _equipmentService.DeleteEquipmentAsync(equipmentCode);
            if (result == null)
                return NotFound(new { message = "해당 설비를 찾을 수 없습니다." });
            return Ok(new { message = result.Message, result.EquipmentCode });
        }

        

        [HttpGet] // 설비 전체 조회
        public async Task<IActionResult> GetAllEquipments()
        {
            var equipments = await _equipmentService.GetAllEquipmentsAsync();

            if (equipments == null || equipments.Equipments == null || equipments.Equipments.Count == 0)
            {
                return NotFound(new { message = "조회된 설비가 없습니다." });
            }

            return Ok(new
            {
                message = "설비 조회 성공",
                equipments.Equipments
            });

        }

        [HttpGet("process/{processCode}")] // 공정별 설비 조회
        public async Task<IActionResult> GetEquipmentByProcess(string processCode)
        {
            var equipments = await _equipmentService.GetEquipmentByProcessAsync(processCode);

            if (equipments.Equipments == null || equipments.Equipments == null || equipments.Equipments.Count == 0)
                return NotFound(new { message = $"공정 '{processCode}'에 대한 설비가 없습니다." });

            return Ok(new
            {
                message = equipments.Message,
                equipments.Equipments
            });
        }
        
    }
}
