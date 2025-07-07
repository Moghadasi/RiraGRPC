using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Rira.Contracts.User.Command;
using Rira.Models.User;
using Rira.Services.User.Command;

namespace Rira.ServicesTests.User.Command
{
    public class AddUserHandlerTests : RiraUnitTestsBase
    {
        /// <summary>
        /// وقتی به درستی کار می کند
        /// </summary>
        [Fact]
        public async Task TestForWhenWorksFine()
        {
            //Arrange
            var init = await Initialize();
            var input = new AddUserInput("John", "Wick", "1234567890", new DateOnly(1986, 1, 25));

            //Act
            var result = await init.Handler.Handle(input, default);

            //Assert
            var addedUser = await RiraQuery.Users.ValidPredicate().FirstOrDefaultAsync(m => m.UserId == result.UserId);
            Assert.NotNull(addedUser);
            Assert.Equal("John", addedUser.FirstName);
            Assert.Equal("Wick", addedUser.LastName);
            Assert.Equal("1234567890", addedUser.NationalCode);
            Assert.Equal(new DateOnly(1986, 1, 25), addedUser.Birthday);
        }

        #region Private Methods

        private async Task<(IRequestHandler<AddUserInput, AddUserOutput> Handler, UserEntity ExistUser)> Initialize()
        {
            RiraCommand.Add(Out(out UserEntity user, new("Adam", "Freeman", "0123456789", new DateOnly(1990, 10, 10))));
            await RiraCommand.SaveChangesAsync();

            var loggerMock = new Mock<ILogger<AddUserHandler>>();

            var handler = new AddUserHandler(RiraCommand, loggerMock.Object);
            return (handler, user);
        }

        #endregion

    }
}
