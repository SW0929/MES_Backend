using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class Equipment
    {
        [Key]
        public required string EquipmentCode { get; set; } // 설비 코드
        public required string Name { get; set; } // 설비 이름
        public required string ProcessCode { get; set; } // 설비 유형 (예: 프레스, 차체 등)
        public required string Status { get; set; } // 설비 상태 (예: 가동 중, 정지 등) NULL 변경 해야 함
        public DateTime? LastUsedDate { get; set; } // 설비 위치 (예: 공장 내 위치)
    }
}
