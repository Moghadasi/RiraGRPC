using MediatR;
using Microsoft.Extensions.Logging;
using Rira.Contracts.User.Command;
using Rira.Data;
using Rira.Models.User;

namespace Rira.Services.User.Command
{
    /// <summary>
    /// 
    /// </summary>
    public class AddUserHandler : IRequestHandler<AddUserInput, AddUserOutput>
    {

        #region Private Feilds

        private readonly RiraCommandContext _riraCommand;
        private readonly ILogger<AddUserHandler> _logger;

        #endregion

        #region Ctor

        /// <summary>
        /// 
        /// </summary>
        public AddUserHandler(RiraCommandContext riraCommand, ILogger<AddUserHandler> logger)
        {
            _riraCommand = riraCommand;
            _logger = logger;
        }

        #endregion

        /// <inheritdoc />
        public async Task<AddUserOutput> Handle(AddUserInput input, CancellationToken cancellationToken)
        {
            var user = new UserEntity(input.FirstName,
                input.LastName,
                input.NationalCode,
                input.Birthday);

            _riraCommand.Users.Add(user);
            await _riraCommand.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("User with id {id} added", user.UserId);

            return new AddUserOutput(user.UserId);
        }
    }
}
