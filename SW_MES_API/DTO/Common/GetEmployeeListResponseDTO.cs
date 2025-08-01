namespace SW_MES_API.DTO.Common
{
    public class GetEmployeeListResponseDTO
    {
        public required string Message { get; set; }
        public List<EmployeeResponseDTO> Employees { get; set; } = [];
    }
}
