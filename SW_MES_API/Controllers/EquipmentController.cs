using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SW_MES_API.Services.Common;

namespace SW_MES_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController: ControllerBase
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
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
                Equipments = equipments.Equipments
            });

        }

        [HttpGet("process/{processCode}")]
        public async Task<IActionResult> GetEquipmentByProcess(string processCode)
        {
            var equipments = await _equipmentService.GetEquipmentByProcessAsync(processCode);

            if (equipments.Equipments == null || equipments.Equipments == null || equipments.Equipments.Count == 0)
                return NotFound(new { message = $"공정 '{processCode}'에 대한 설비가 없습니다." });

            return Ok(new
            {
                message = equipments.Message,
                Equipments = equipments.Equipments
            });
        }
    }
}
