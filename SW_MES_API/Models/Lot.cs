using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class Lot
    {
        [Key]
        public required string LotCode { get; set; } // 로트 코드
        public required string WorkOrderID { get; set; } // 작업 지시 ID
        public required int CreatedBy { get; set; } // 생성자 (사원 번호)
        public required int Quantity { get; set; } // 수량
        public required String CurrentProcess { get; set; } // 현재 공정 코드
        public String? Status { get; set; } // 상태 (예: 진행 중, 완료 등)
    }
}
