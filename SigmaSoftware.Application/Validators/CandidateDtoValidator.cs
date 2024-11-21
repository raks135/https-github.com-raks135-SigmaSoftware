using FluentValidation;
using SigmaSoftware.Domain.ViewModels;

namespace SigmaSoftware.Application.Validators
{
    using FluentValidation;

    /// <summary>
    /// Validator for the <see cref="CandidateDTO"/> class.
    /// Ensures that the provided candidate details meet validation criteria.
    /// </summary>
    public class CandidateDtoValidator : AbstractValidator<CandidateDTO>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CandidateDtoValidator"/> class.
        /// Sets up validation rules for the <see cref="CandidateDTO"/> properties.
        /// </summary>
        public CandidateDtoValidator()
        {
            // Ensure FirstName is not empty
            RuleFor(c => c.FirstName).NotEmpty()
                .WithMessage("First name is required.");

            // Ensure LastName is not empty
            RuleFor(c => c.LastName).NotEmpty()
                .WithMessage("Last name is required.");

            // Ensure Email is not empty and is a valid email address
            RuleFor(c => c.Email).NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("A valid email address is required.");

            // Validate LinkedInUrl only if it is not null or empty
            RuleFor(c => c.LinkedInUrl)
                .Must(BeAValidUrl)
                .When(c => !string.IsNullOrEmpty(c.LinkedInUrl))
                .WithMessage("LinkedIn URL must be a valid absolute URL.");

            // Validate GitHubUrl only if it is not null or empty
            RuleFor(c => c.GitHubUrl)
                .Must(BeAValidUrl)
                .When(c => !string.IsNullOrEmpty(c.GitHubUrl))
                .WithMessage("GitHub URL must be a valid absolute URL.");
        }

        /// <summary>
        /// Validates whether the provided string is a valid absolute URL.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <returns>True if the URL is valid; otherwise, false.</returns>
        private bool BeAValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }


}
