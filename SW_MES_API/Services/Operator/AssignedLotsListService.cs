using SW_MES_API.DTO.Operator;
using SW_MES_API.Repositories.Operator;

namespace SW_MES_API.Services.Operator
{
    public class AssignedLotsListService : IAssignedLotsListService
    {
        private readonly IAssignedLotsListRepository _repository;
        public AssignedLotsListService(IAssignedLotsListRepository repository)
        {
            _repository = repository;
        }
        public async Task<AssignedLotsResponseDTO> GetAssignedLotsAsync(AssignedLotsRequestDTO request)
        {
            var assignedLots = await _repository.GetAssignedLotsAsync(request);

            if (assignedLots == null || assignedLots.Count == 0)
            {
                return new AssignedLotsResponseDTO
                {
                    Message = "할당된 Lot이 없습니다.",
                    AssignedLots = new List<AssignedLotsDTO>()
                };
            }

            return new AssignedLotsResponseDTO
            {
                Message = "할당된 Lot 목록 조회 성공",
                AssignedLots = assignedLots
            };
        }
    }    
}
