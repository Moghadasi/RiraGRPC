using MediatR;
using Microsoft.EntityFrameworkCore;
using Rira.Contracts.User.Exception;
using Rira.Contracts.User.Query;
using Rira.Data;
using Rira.Models.User;

namespace Rira.Services.User.Query
{
    /// <summary>
    /// 
    /// </summary>
    public class GetUserHandler : IRequestHandler<GetUserInput, GetUserOutput>
    {

        #region Private Feilds

        private readonly RiraQueryContext _riraQuery;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        public GetUserHandler(RiraQueryContext riraQuery)
        {
            _riraQuery = riraQuery;
        }

        #endregion

        /// <inheritdoc />
        public async Task<GetUserOutput> Handle(GetUserInput input, CancellationToken cancellationToken)
        {
            var user = await _riraQuery.Users.ValidPredicate()
                .FirstOrDefaultAsync(m => m.UserId == input.UserId, cancellationToken);

            if (user is null)
                throw new UserNotFoundException(input.UserId);

            return new GetUserOutput(user.UserId,
                user.FirstName,
                user.LastName,
                user.NationalCode,
                user.Birthday);
        }
    }
}
