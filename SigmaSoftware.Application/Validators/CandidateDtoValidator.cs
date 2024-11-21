using FluentValidation;
using SigmaSoftware.Domain.ViewModels;

namespace SigmaSoftware.Application.Validators
{
    public class CandidateDtoValidator : AbstractValidator<CandidateDTO>
    {
        public CandidateDtoValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.Email).NotEmpty().EmailAddress();
            RuleFor(c => c.LinkedInUrl).Must(BeAValidUrl).When(c => !string.IsNullOrEmpty(c.LinkedInUrl));
            RuleFor(c => c.GitHubUrl).Must(BeAValidUrl).When(c => !string.IsNullOrEmpty(c.GitHubUrl));
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }

}
