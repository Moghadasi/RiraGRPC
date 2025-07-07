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
    public class UpdateUserHandler : IRequestHandler<UpdateUserInput, UpdateUserOutput>
    {

        #region Private Feilds

        private readonly RiraCommandContext _riraCommand;
        private readonly IClockService _clock;
        private readonly ILogger<UpdateUserHandler> _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        public UpdateUserHandler(RiraCommandContext riraCommand,
            IClockService clock,
            ILogger<UpdateUserHandler> logger)
        {
            _riraCommand = riraCommand;
            _clock = clock;
            _logger = logger;
        }

        #endregion

        /// <inheritdoc />
        public async Task<UpdateUserOutput> Handle(UpdateUserInput input, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Start updating user with id {userId}", input.UserId);

            var user = await _riraCommand.Users.ValidPredicate()
                .FirstOrDefaultAsync(m => m.UserId == input.UserId, cancellationToken);

            if (user is null)
                return new UpdateUserOutput(false);

            user.Update(firstName: input.FirstName,
                lastName: input.LastName,
                nationalCode: input.NationalCode,
                birthday: input.Birthday,
                clock: _clock);
            await _riraCommand.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with id {userId} updated", input.UserId);

            return new UpdateUserOutput(true);
        }
    }
}
