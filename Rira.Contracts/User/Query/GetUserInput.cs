using MediatR;

namespace Rira.Contracts.User.Query
{
    /// <summary>
    /// 
    /// </summary>
    public record GetUserInput(int UserId) : IRequest<GetUserOutput>;
}