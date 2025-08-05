namespace SW_MES_API.DTO.Admin.Equipment
{
    public class EquipmentDefectRequestDTO
    {
        public required string Status { get; set; } // 설비 상태
        public required int SolvedBy { get; set; } // 처리 관리자 사번
        public DateTime? SolvedDate { get; set; } // 처리 일시
    }
}
