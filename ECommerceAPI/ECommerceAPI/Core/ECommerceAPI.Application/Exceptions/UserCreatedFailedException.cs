namespace ECommerceAPI.Application.Exceptions
{
    public class UserCreatedFailedException : Exception
    {
        public UserCreatedFailedException(): base("When user has been creating, error has happened!!")
        {
        }

        public UserCreatedFailedException(string? message) : base(message)
        {
        }

        public UserCreatedFailedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
