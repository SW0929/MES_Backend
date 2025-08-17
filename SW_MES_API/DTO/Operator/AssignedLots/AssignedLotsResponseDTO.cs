namespace SW_MES_API.DTO.Operator.AssignedLots
{
    public class AssignedLotsResponseDTO
    {
        public required string Message { get; set; }
        public List<AssignedLotsDTO>? AssignedLots { get; set; }
        
    }
}
