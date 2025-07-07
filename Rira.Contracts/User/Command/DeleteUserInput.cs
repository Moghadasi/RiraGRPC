using MediatR;

namespace Rira.Contracts.User.Command
{
    /// <summary>
    /// 
    /// </summary>
    public record DeleteUserInput(int UserId) : IRequest<DeleteUserOutput>;
}