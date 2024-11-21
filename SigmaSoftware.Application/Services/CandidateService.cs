using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SigmaSoftware.Application.Validators;
using SigmaSoftware.Domain.Entities;
using SigmaSoftware.Domain.Interfaces;
using SigmaSoftware.Domain.ViewModels;
using SigmaSoftware.Shared.Constants;

namespace SigmaSoftware.Application.Services
{
    /// <summary>
    /// Provides services for managing candidate records, including creation and update operations.
    /// </summary>
    public class CandidateService : ICandidateService
    {
        #region Private Fields

        private readonly IRepository<Candidate> _repository;
        private readonly ILogger<CandidateService> _logger;
        private readonly CandidateDtoValidator _validator;
        private readonly IMemoryCache _memoryCache;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateService"/> class.
        /// </summary>
        /// <param name="repository">Repository for accessing candidate records.</param>
        /// <param name="logger">Logger for logging operations.</param>
        /// <param name="validator">Validator for candidate DTOs.</param>
        public CandidateService(
            IRepository<Candidate> repository,
            ILogger<CandidateService> logger,
            CandidateDtoValidator validator,
            IMemoryCache memoryCache)
        {
            _repository = repository;
            _logger = logger;
            _validator = validator;
            _memoryCache = memoryCache;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates or updates a candidate record based on the provided candidate information.
        /// </summary>
        /// <param name="candidateDTO">The candidate data transfer object containing candidate information.</param>
        /// <returns>A response object indicating success or failure with additional details.</returns>
        public async Task<BaseResponse<CandidateDTO>> CreateUpdateCandidateRecordAsync(CandidateDTO candidateDTO)
        {
            var response = new BaseResponse<CandidateDTO>();
            _logger.LogInformation(LogMessage(nameof(CreateUpdateCandidateRecordAsync), LogConstants.StandardLogMessages.MethodInitiatingMessage));

            if (!ValidateCandidate(candidateDTO, response))
            {
                return response;
            }

            try
            {
                var cacheKey = candidateDTO.Email.ToLower();  

                if (_memoryCache.TryGetValue(cacheKey, out Candidate cachedCandidate))
                {
                    _logger.LogInformation("Candidate found in cache, updating candidate.");

                    // Update the cached candidate data
                    cachedCandidate.FirstName = candidateDTO.FirstName;
                    cachedCandidate.LastName = candidateDTO.LastName;
                    cachedCandidate.PhoneNumber = candidateDTO.PhoneNumber;
                    cachedCandidate.BestCallTime = candidateDTO.BestCallTime;
                    cachedCandidate.LinkedInUrl = candidateDTO.LinkedInUrl;
                    cachedCandidate.GitHubUrl = candidateDTO.GitHubUrl;
                    cachedCandidate.Comment = candidateDTO.Comment;
                    cachedCandidate.UpdatedAt = DateTime.UtcNow;

                    // Update the database with the new information
                    await _repository.UpdateAsync(cachedCandidate);

                    // Refresh the cache
                    _memoryCache.Set(cacheKey, cachedCandidate, TimeSpan.FromMinutes(5));
                }
                else
                {
                    _logger.LogInformation("Candidate not found in cache, checking database.");

                    var existingCandidate = await _repository.GetByEmailAsync(candidateDTO.Email);
                    if (existingCandidate != null)
                    {
                        // Update the candidate from the database
                        await UpdateCandidateAsync(existingCandidate, candidateDTO);

                        // Refresh cache
                        _memoryCache.Set(cacheKey, existingCandidate, TimeSpan.FromMinutes(5));
                    }
                    else
                    {
                        // Candidate does not exist, create a new one
                        await CreateCandidateAsync(candidateDTO);

                        // Cache the new candidate data
                        var newCandidate = new Candidate
                        {
                            FirstName = candidateDTO.FirstName,
                            LastName = candidateDTO.LastName,
                            PhoneNumber = candidateDTO.PhoneNumber,
                            Email = candidateDTO.Email,
                            BestCallTime = candidateDTO.BestCallTime,
                            LinkedInUrl = candidateDTO.LinkedInUrl,
                            GitHubUrl = candidateDTO.GitHubUrl,
                            Comment = candidateDTO.Comment,
                            CreatedAt = DateTime.UtcNow
                        };

                        _memoryCache.Set(cacheKey, newCandidate, TimeSpan.FromMinutes(5));
                    }
                }

                response.Message = CandidateServiceConstants.SuccessInsertionMessage;
                _logger.LogInformation(LogMessage(nameof(CreateUpdateCandidateRecordAsync), LogConstants.StandardLogMessages.MethodExecutionSuccessMessage));
                return response;
            }
            catch (Exception ex)
            {
                HandleException(ex, response, nameof(CreateUpdateCandidateRecordAsync));
                return response;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Validates the candidate data transfer object.
        /// </summary>
        /// <param name="candidateDTO">The candidate DTO to validate.</param>
        /// <param name="response">The response object to hold validation errors, if any.</param>
        /// <returns>True if validation succeeds; otherwise, false.</returns>
        private bool ValidateCandidate(CandidateDTO candidateDTO, BaseResponse<CandidateDTO> response)
        {
            _logger.LogInformation(LogMessage(nameof(ValidateCandidate), LogConstants.StandardLogMessages.ValidationExecutionInitiationMessage));

            var validationResult = _validator.Validate(candidateDTO);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning(LogMessage(nameof(ValidateCandidate), LogConstants.StandardLogMessages.ValidationFailedMessage));
                response.Errors.AddRange(validationResult.Errors.Select(x => x.ErrorMessage));
                return false;
            }

            return true;
        }

        /// <summary>
        /// Updates an existing candidate record with new information.
        /// </summary>
        /// <param name="existingCandidate">The existing candidate entity to update.</param>
        /// <param name="candidateDTO">The new candidate information.</param>
        /// <returns>A task representing the asynchronous update operation.</returns>
        private async Task UpdateCandidateAsync(Candidate existingCandidate, CandidateDTO candidateDTO)
        {
            _logger.LogInformation(LogMessage(nameof(UpdateCandidateAsync), string.Format(LogConstants.StandardLogMessages.DatabaseExecutionInitiationMessage, nameof(_repository.UpdateAsync))));

            existingCandidate.FirstName = candidateDTO.FirstName;
            existingCandidate.LastName = candidateDTO.LastName;
            existingCandidate.PhoneNumber = candidateDTO.PhoneNumber;
            existingCandidate.BestCallTime = candidateDTO.BestCallTime;
            existingCandidate.LinkedInUrl = candidateDTO.LinkedInUrl;
            existingCandidate.GitHubUrl = candidateDTO.GitHubUrl;
            existingCandidate.Comment = candidateDTO.Comment;
            existingCandidate.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(existingCandidate);
        }

        /// <summary>
        /// Creates a new candidate record with the provided information.
        /// </summary>
        /// <param name="candidateDTO">The candidate information to save.</param>
        /// <returns>A task representing the asynchronous creation operation.</returns>
        private async Task CreateCandidateAsync(CandidateDTO candidateDTO)
        {
            _logger.LogInformation(LogMessage(nameof(CreateCandidateAsync), string.Format(LogConstants.StandardLogMessages.DatabaseExecutionInitiationMessage, nameof(_repository.InsertAsync))));

            var newCandidate = new Candidate
            {
                FirstName = candidateDTO.FirstName,
                LastName = candidateDTO.LastName,
                PhoneNumber = candidateDTO.PhoneNumber,
                Email = candidateDTO.Email,
                BestCallTime = candidateDTO.BestCallTime,
                LinkedInUrl = candidateDTO.LinkedInUrl,
                GitHubUrl = candidateDTO.GitHubUrl,
                Comment = candidateDTO.Comment,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.InsertAsync(newCandidate);
        }

        /// <summary>
        /// Handles exceptions and logs error messages.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="response">The response object to hold error details.</param>
        /// <param name="methodName">The name of the method where the exception occurred.</param>
        private void HandleException(Exception ex, BaseResponse<CandidateDTO> response, string methodName)
        {
            _logger.LogError(ex, LogMessage(methodName, LogConstants.StandardLogMessages.MethodExecutionExceptionMessage));
            response.Errors.Add(ex.Message);
        }

        /// <summary>
        /// Formats a log message with class name, method name, and a message.
        /// </summary>
        /// <param name="methodName">The name of the method generating the log message.</param>
        /// <param name="message">The log message.</param>
        /// <returns>A formatted string for logging.</returns>
        private string LogMessage(string methodName, string message)
        {
            return string.Format(LogConstants.LogFormat, GetType().Name, methodName, message);
        }

        #endregion
    }
}
