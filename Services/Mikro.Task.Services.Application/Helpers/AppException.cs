using System.Globalization;

namespace Mikro.Task.Services.Application.Helpers
{
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }

    public class NotAuthorizedException : Exception
    {
        public NotAuthorizedException() : base("Not authorized") { }

        public NotAuthorizedException(string message) : base(message) { }

        public NotAuthorizedException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
