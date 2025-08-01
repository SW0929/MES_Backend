using SW_MES_API.Models;

namespace SW_MES_API.Repositories.Admin
{
    public interface ILotProcessRepository
    {
        Task AddLotProcessesAsync(List<LotProcess> lotProcesses);
        Task DeleteLotProcessAsyc(string lotCode);


        // 작업자, 장비 할당
        Task<LotProcess> GetLotProcessByLotCodeAsync(string lotCode, string processCode);
        Task UpdateAssignmentAsync(LotProcess lotProcess, int employeeID, string equipmentCode);
    }
}
