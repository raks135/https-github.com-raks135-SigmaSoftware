namespace SigmaSoftware.Domain.ViewModels
{
    /// <summary>
    /// Represents a standardized response structure for API operations,
    /// containing the result of the operation, any error messages, and metadata.
    /// </summary>
    /// <typeparam name="T">The type of the result contained in the response.</typeparam>
    public class BaseResponse<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse{T}"/> class.
        /// </summary>
        public BaseResponse()
        {
            Errors = new List<string>();
        }

        /// <summary>
        /// Gets a value indicating whether there are any errors in the response.
        /// </summary>
        public bool HasError => Errors.Any();

        /// <summary>
        /// Gets or sets the list of error messages that occurred during the operation.
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Gets or sets the total number of records (for paginated responses).
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// Gets or sets the result of the operation, which can be of any type.
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// Gets or sets a list of results of type T.
        /// </summary>
        public List<T> Results { get; set; } = new List<T>();

        /// <summary>
        /// Gets or sets an optional message providing additional information about the operation.
        /// </summary>
        public string? Message { get; set; }
    }
}
