using Rira.Core.Exceptions;

namespace Rira.Contracts.User.Exception
{
    /// <inheritdoc />
    public class UserNotFoundException : DomainException
    {
        /// <inheritdoc />
        public UserNotFoundException(int userId) : base(ExceptionCodes.UserNotFoundError, $"User with id {userId} was not found")
        {
        }
    }
}
