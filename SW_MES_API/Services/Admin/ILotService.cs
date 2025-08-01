using SW_MES_API.DTO;
using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Services.Admin
{
    public interface ILotService
    {
        Task<List<Lot>> InsertLotsAsync(CreateLotRequestDTO request);

        Task<UpdateLotsResponseDTO> UpdateLotsAsync(string lotCode, UpdateLotsRequestDTO request);

        Task<DeleteLotsResponseDTO> DeleteLotsAsync(string lotCode);


        // 작업/설비할당
        Task<AssignLotResponseDTO> AssignLotAsync(AssignLotRequestDTO request);
    }
}
