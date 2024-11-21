using SigmaSoftware.Domain.ViewModels;

namespace SigmaSoftware.Domain.Interfaces
{
    /// <summary>
    /// Defines the contract for candidate-related operations in the application.
    /// </summary>
    public interface ICandidateService
    {
        /// <summary>
        /// Creates or updates a candidate record asynchronously.
        /// </summary>
        /// <param name="candidateDTO">The candidate data transfer object containing details to be created or updated.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains a <see cref="BaseResponse{T}"/> wrapping the <see cref="CandidateDTO"/> with the operation result.
        /// </returns>
        Task<BaseResponse<CandidateDTO>> CreateUpdateCandidateRecordAsync(CandidateDTO candidateDTO);
    }

}
