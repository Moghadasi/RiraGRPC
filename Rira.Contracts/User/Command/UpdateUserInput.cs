using MediatR;

namespace Rira.Contracts.User.Command
{
    /// <summary>
    /// 
    /// </summary>
    public record UpdateUserInput(int UserId,
        string FirstName,
        string LastName,
        string NationalCode,
        DateOnly Birthday) : IRequest<UpdateUserOutput>;
}