using System.ComponentModel.DataAnnotations.Schema;

namespace SW_MES_API.DTO
{
    public class LoginResponseDTO
    {
        [NotMapped]
        public int EmployeeID { get; set; } // 사원 번호
        public required string Name { get; set; } // 이름
        public required string Role { get; set; } // 역할 (예: 관리자, 사용자 등)
        public required bool IsActive { get; set; } // 활성화 여부

    }
}
