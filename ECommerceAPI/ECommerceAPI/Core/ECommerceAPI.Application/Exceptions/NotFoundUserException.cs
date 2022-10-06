namespace ECommerceAPI.Application.Exceptions
{
    public class NotFoundUserException : Exception
    {
        public NotFoundUserException(): base("User's mail or name is error!!")
        {
        }

        public NotFoundUserException(string? message) : base(message)
        {
        }

        public NotFoundUserException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
