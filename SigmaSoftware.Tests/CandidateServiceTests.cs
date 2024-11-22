using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using SigmaSoftware.Application.Services;
using SigmaSoftware.Domain.Entities;
using SigmaSoftware.Domain.Interfaces;
using SigmaSoftware.Domain.ViewModels;
using SigmaSoftware.Shared.Constants;

namespace SigmaSoftware.Tests;

public class CandidateServiceTests
{
    private readonly Mock<IRepository<Candidate>> _mockRepository;
    private readonly Mock<ILogger<CandidateService>> _mockLogger;
    private readonly Microsoft.Extensions.Caching.Memory.MemoryCache _memoryCache;
    private readonly CandidateService _candidateService;

    public CandidateServiceTests()
    {
        _mockRepository = new Mock<IRepository<Candidate>>();
        _mockLogger = new Mock<ILogger<CandidateService>>();
        _memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
        _candidateService = new CandidateService(_mockRepository.Object, _mockLogger.Object, _memoryCache);
    }

    [Fact(DisplayName = "CreateUpdateCandidateRecordAsync_ShouldCreateNewCandidate_WhenNotInCacheOrDatabase")]
    public async Task CreateUpdateCandidateRecordAsync_ShouldCreateNewCandidate_WhenNotInCacheOrDatabase()
    {
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sigma",
            LastName = "Software",
            Email = "sigma.software@example.com",
            PhoneNumber = "123456789",
            BestCallTime = "Afternoon",
            LinkedInUrl = "https://www.linkedin.com/in/SigmaSoftware",
            GitHubUrl = "https://github.com/SigmaSoftware",
            Comment = "New candidate"
        };

        _mockRepository
            .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((Candidate)null);

        var response = await _candidateService.CreateUpdateCandidateRecordAsync(candidateDTO);

        var cachedCandidate = _memoryCache.Get("sigma.software@example.com");
        Assert.NotNull(cachedCandidate);
        Assert.IsType<Candidate>(cachedCandidate);
        Assert.Equal(CandidateServiceConstants.SuccessInsertionMessage, response.Message);
    }

    [Fact(DisplayName = "CreateUpdateCandidateRecordAsync_ShouldUpdateExistingCandidate_WhenInCache")]
    public async Task CreateUpdateCandidateRecordAsync_ShouldUpdateExistingCandidate_WhenInCache()
    {
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sigma",
            LastName = "Software",
            Email = "sigma.software@example.com",
            PhoneNumber = "987654321",
            BestCallTime = "Morning",
            LinkedInUrl = "https://www.linkedin.com/in/SigmaSoftware",
            GitHubUrl = "https://github.com/SigmaSoftware",
            Comment = "Updated candidate"
        };

        var existingCandidate = new Candidate
        {
            FirstName = "Sigma",
            LastName = "Software",
            Email = "sigma.software@example.com",
            PhoneNumber = "123456789",
            BestCallTime = "Afternoon",
            LinkedInUrl = "https://www.linkedin.com/in/SigmaSoftware",
            GitHubUrl = "https://github.com/SigmaSoftware",
            Comment = "Old candidate",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        _memoryCache.Set("sigma.software@example.com", existingCandidate);

        _mockRepository
            .Setup(repo => repo.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(existingCandidate);

        var response = await _candidateService.CreateUpdateCandidateRecordAsync(candidateDTO);

        var cachedCandidate = _memoryCache.Get("sigma.software@example.com");
        Assert.NotNull(cachedCandidate);
        Assert.IsType<Candidate>(cachedCandidate);
        var updatedCandidate = cachedCandidate as Candidate;

        Assert.Equal(candidateDTO.PhoneNumber, updatedCandidate.PhoneNumber);
        Assert.Equal(candidateDTO.BestCallTime, updatedCandidate.BestCallTime);
        Assert.Equal(candidateDTO.LinkedInUrl, updatedCandidate.LinkedInUrl);
        Assert.Equal(candidateDTO.GitHubUrl, updatedCandidate.GitHubUrl);
        Assert.Equal(candidateDTO.Comment, updatedCandidate.Comment);

        Assert.Equal(CandidateServiceConstants.SuccessInsertionMessage, response.Message);
    }

    [Fact(DisplayName = "CreateUpdateCandidateRecordAsync_ShouldAddNewCandidateToCache_WhenNotInCacheOrDatabase")]
    public async Task CreateUpdateCandidateRecordAsync_ShouldAddNewCandidateToCache_WhenNotInCacheOrDatabase()
    {
        var candidateDTO = new CandidateDTO
        {
            FirstName = "Sigma",
            LastName = "Software",
            Email = "sigma.software@example.com",
        };

        await _candidateService.CreateUpdateCandidateRecordAsync(candidateDTO);

        var cachedCandidate = _memoryCache.Get("sigma.software@example.com");
        Assert.NotNull(cachedCandidate);
    }
}