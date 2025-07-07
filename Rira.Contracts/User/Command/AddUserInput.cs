using MediatR;

namespace Rira.Contracts.User.Command
{
    /// <summary>
    /// 
    /// </summary>
    public record AddUserInput(string FirstName,
        string LastName,
        string NationalCode,
        DateOnly Birthday) : IRequest<AddUserOutput>;
}