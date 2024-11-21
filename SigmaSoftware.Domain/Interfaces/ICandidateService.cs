using SigmaSoftware.Domain.ViewModels;

namespace SigmaSoftware.Domain.Interfaces
{
    public interface ICandidateService
    {
        Task<BaseResponse<CandidateDTO>> CreateUpdateCandidateRecordAsync(CandidateDTO candidateDTO);
    }
}
