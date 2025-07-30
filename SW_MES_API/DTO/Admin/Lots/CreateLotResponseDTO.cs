namespace SW_MES_API.DTO.Admin.Lots
{
    public class CreateLotResponseDTO
    {
        public required string Message {  get; set; }
        public List<CreatedLotDTO>? CreatedLots { get; set; }
    }
}
