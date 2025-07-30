using SW_MES_API.DTO.Admin.Lots;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public interface ILotRepository
    {
        Task <List<Lot>> InsertLotsAsync(CreateLotRequestDTO request);
    }
}
