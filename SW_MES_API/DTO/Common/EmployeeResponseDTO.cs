using Microsoft.Identity.Client;

namespace SW_MES_API.DTO.Common
{
    public class EmployeeResponseDTO
    {
        public int EmployeeID { get; set; }
        public required string Name { get; set; }
    }
}
