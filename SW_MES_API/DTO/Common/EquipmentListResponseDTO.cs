using SW_MES_API.DTO.Admin.Lots;

namespace SW_MES_API.DTO.Common
{
    public class EquipmentListResponseDTO
    {
        public required string Message { get; set; }
        public List<EquipmentResponseDTO> Equipments { get; set; } = [];
    }
}
