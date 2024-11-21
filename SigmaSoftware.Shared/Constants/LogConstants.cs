namespace SigmaSoftware.Shared.Constants
{
    public static class LogConstants
    {
        /// <summary>
        /// The format for log messages, including service name, method name, and message content.
        /// </summary>
        public const string LogFormat = "Service: {0} Method: {1} Message: {2}";

        /// <summary>
        /// Contains standard log messages used throughout the application.
        /// </summary>
        public static class StandardLogMessages
        {
            /// <summary>
            /// Message indicating the start of method execution.
            /// </summary>
            public const string MethodInitiatingMessage = "Starting the execution of the method.";

            /// <summary>
            /// Message indicating the beginning of the validation process for incoming data.
            /// </summary>
            public const string ValidationExecutionInitiationMessage = "Beginning the validation process for the incoming data.";

            /// <summary>
            /// Message indicating that the validation process has failed for the incoming data.
            /// </summary>
            public const string ValidationFailedMessage = "Validation process failed for the incoming data.";

            /// <summary>
            /// Message indicating the start of a database operation, with the operation type included.
            /// </summary>
            public const string DatabaseExecutionInitiationMessage = "Starting the database operation : {0}";

            /// <summary>
            /// Message indicating that the method executed successfully and results are ready.
            /// </summary>
            public const string MethodExecutionSuccessMessage = "The method executed successfully. Results are ready for processing.";

            /// <summary>
            /// Message indicating that an exception occurred during method execution.
            /// </summary>
            public const string MethodExecutionExceptionMessage = "An exception occurred during method execution.";

            /// <summary>
            /// Message to log the initiation of API calls to a specified URL.
            /// This log entry is used to indicate that the process of making API requests
            /// to a particular endpoint is starting, providing the URL for context.
            /// </summary>
            public const string InitiatingAPICalls = "Initiating API calls to the specified URL: {0}.";
        }
    }
}
