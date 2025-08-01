using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public interface ILotProcessRepository
    {
        Task AddLotProcessesAsync(List<LotProcess> lotProcesses);

        Task DeleteLotProcessAsyc(string lotCode);
    }
}
