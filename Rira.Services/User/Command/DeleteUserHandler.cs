using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rira.Contracts.User.Command;
using Rira.Core.Contracts;
using Rira.Data;
using Rira.Models.User;

namespace Rira.Services.User.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteUserHandler : IRequestHandler<DeleteUserInput, DeleteUserOutput>
    {

        #region Private Feilds

        private readonly RiraCommandContext _riraCommand;
        private readonly IClockService _clock;
        private readonly ILogger<DeleteUserHandler> _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        public DeleteUserHandler(RiraCommandContext riraCommand,
            IClockService clock, 
            ILogger<DeleteUserHandler> logger)
        {
            _riraCommand = riraCommand;
            _clock = clock;
            _logger = logger;
        }

        #endregion

        /// <inheritdoc />
        public async Task<DeleteUserOutput> Handle(DeleteUserInput input, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start Deleting user with id {userId}", input.UserId);
            var user = await _riraCommand.Users.ValidPredicate()
                .FirstOrDefaultAsync(m => m.UserId == input.UserId, cancellationToken);

            if (user is null)
                return new DeleteUserOutput(false);

            user.Delete(_clock);
            await _riraCommand.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with id {userId} deleted", input.UserId);

            return new DeleteUserOutput(true);
        }
    }
}
