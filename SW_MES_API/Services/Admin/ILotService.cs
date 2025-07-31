using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Services.Admin
{
    public interface ILotService
    {
        Task<List<Lot>> InsertLotsAsync(CreateLotRequestDTO request);
    }
}
