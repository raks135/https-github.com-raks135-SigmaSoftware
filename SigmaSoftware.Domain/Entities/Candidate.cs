using SigmaSoftware.Domain.ViewModels;

namespace SigmaSoftware.Domain.Entities
{
    /// <summary>
    /// Represents a candidate entity with details such as personal information, contact information, and URLs.
    /// </summary>
    public class Candidate
    {
        /// <summary>
        /// Gets or sets the candidate's first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the candidate's last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the candidate's phone number.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the candidate's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the preferred time to contact the candidate.
        /// </summary>
        public string BestCallTime { get; set; }

        /// <summary>
        /// Gets or sets the candidate's LinkedIn profile URL.
        /// </summary>
        public string LinkedInUrl { get; set; }

        /// <summary>
        /// Gets or sets the candidate's GitHub profile URL.
        /// </summary>
        public string GitHubUrl { get; set; }

        /// <summary>
        /// Gets or sets any additional comments about the candidate.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the candidate record was created.
        /// Defaults to the current UTC date and time.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the date and time when the candidate record was last updated.
        /// Defaults to the current UTC date and time.
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }


}
