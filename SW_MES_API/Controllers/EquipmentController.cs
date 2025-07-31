using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SW_MES_API.Services.Common;

namespace SW_MES_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipmentController
    {
        private readonly IEquipmentService _equipmentService;

        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEquipments()
        {
            var equipments = await _equipmentService.GetAllEquipmetsAsync();

            if (equipments == null || !equipments.Any())
                return NotFound(new { message = "조회 가능한 설비가 없습니다." , });

            return Ok(new
            {
                message = "설비 조회 성공",
                Equipments = equipments
            });

        }
    }
}
