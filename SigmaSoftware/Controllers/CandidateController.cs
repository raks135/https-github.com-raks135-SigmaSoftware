using Microsoft.AspNetCore.Mvc;
using SigmaSoftware.Domain.Interfaces;
using SigmaSoftware.Domain.ViewModels;

namespace SigmaSoftware.Controllers
{
    /// <summary>
    /// Provides API endpoints for managing candidate records.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateController"/> class.
        /// </summary>
        /// <param name="candidateService">The service to manage candidate records.</param>
        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        /// <summary>
        /// Creates or updates a candidate record.
        /// </summary>
        /// <param name="candidateDTO">The candidate data transfer object containing the candidate's information.</param>
        /// <returns>A response with success or failure details.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(CandidateDTO candidateDTO)
        {
            var response = await _candidateService.CreateUpdateCandidateRecordAsync(candidateDTO);
            if (response.Errors.Count > 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
