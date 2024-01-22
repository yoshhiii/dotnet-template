namespace Relias.UserProfile.Common.Exceptions
{
    /// <summary>
    /// General operation exception
    /// </summary>
    public class OperationException : Exception
    {
        public OperationException(string operation, string errorDetails) : base($"Exception at {operation} : {errorDetails}")
        {
            // NOP
        }
    }
}
