using SW_MES_API.DTO.Operator.AssignedLots;
using SW_MES_API.DTO.Operator.Performance;
using SW_MES_API.Models;

namespace SW_MES_API.Repositories.LotProcessRepository
{
    public interface ILotProcessRepository
    {
        #region 작업자

        // 할당 된 Lot 작업 시작
        Task StartAssignedLot(int lotProcessCode, string lotStatus, string processCode);

        // 할당 된 작업 완료
        Task CompleteAssignedLot(int lotProcessCode, string lotStatus, string processCode);

        // 작업 완료한 Lot 실적 입력
        Task<PerformanceResponseDTO> LotPerformance(int lotProcessCode, PerformanceRequestDTO request);

        // 작업자가 할당 된 작업 목록 조회
        Task<List<AssignedLotsDTO>> GetAssignedLotsAsync(AssignedLotsRequestDTO request);
        #endregion

        #region 관리자

        // 작업자, 장비 할당
        Task<LotProcess> GetLotProcessByLotCodeAsync(string lotCode, string processCode);

        // LotProcess의 작업자와 설비를 업데이트
        Task UpdateAssignmentAsync(LotProcess lotProcess, int employeeID, string equipmentCode);

        // LotProcess 추가
        Task AddLotProcessesAsync(List<LotProcess> lotProcesses);
        // LotProcess 삭제
        Task DeleteLotProcessAsyc(string lotCode);
        #endregion
    }
}
