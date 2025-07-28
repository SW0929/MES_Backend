using System.ComponentModel.DataAnnotations;

namespace SW_MES_API.Models
{
    public class User
    {
        //지금은 이미 만들어진 테이블에 접근하기 위해 [KEY] 를 씀
        // null 허용 이면 타입 뒤에 ? 붙이기
        [Key]
        public int EmployeeID { get; set; } // 사원 번호
        public required string Name { get; set; } // 이름
        public string? Email { get; set; } // 이메일
        public string? Phone { get; set; } // 전화번호
        public required string Role { get; set; } // 역할 (예: Admin, Operator) DB NULL 허용 X 변경 해야 함
        public required string Department { get; set; } // 부서 (예: 프레스, 차체 등)
        public bool IsActive { get; set; } // 활성화 상태 (true: 활성화, false: 비활성화)
        public DateTime CreatedDate { get; set; } // 생성 날짜
        public DateTime? LastLogin {  get; set; } // 마지막 로그인 날짜 (null 허용)

    }
}
