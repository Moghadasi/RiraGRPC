using MediatR;
using Rira.Contracts.User.Exception;
using Rira.Contracts.User.Query;
using Rira.Models.User;
using Rira.Services.User.Query;

namespace Rira.ServicesTests.User.Query
{
    public class GetUserHandlerTests : RiraUnitTestsBase
    {
        /// <summary>
        /// وقتی به درستی کار می کند
        /// </summary>
        [Fact]
        public async Task TestForWhenWorksFine()
        {
            //Arrange
            var init = await Initialize();
            var input = new GetUserInput(init.ExistUser.UserId);

            //Act
            var result = await init.Handler.Handle(input, default);

            //Assert
            Assert.Equal(input.UserId, result.UserId);
            Assert.Equal("Adam", result.FirstName);
            Assert.Equal("Freeman", result.LastName);
            Assert.Equal("0123456789", result.NationalCode);
            Assert.Equal(new DateOnly(1990, 10, 10), result.Birthday);
        }

        /// <summary>
        /// بررسی اینکه وقتی کاربری با این شناسه وجود ندارد باید خطا دهد
        /// </summary>
        [Fact]
        public async Task TestForWhenNoUserWithId500ExistMustRaiseError()
        {
            //Arrange
            var init = await Initialize();
            var input = new GetUserInput(500);

            //Act && Assert
           await Assert.ThrowsAsync<UserNotFoundException>(() => init.Handler.Handle(input, default));
        }

        #region Private Methods

        private async Task<(IRequestHandler<GetUserInput, GetUserOutput> Handler, UserEntity ExistUser)> Initialize()
        {
            RiraCommand.Add(Out(out UserEntity user, new("Adam", "Freeman", "0123456789", new DateOnly(1990, 10, 10))));
            await RiraCommand.SaveChangesAsync();

            var handler = new GetUserHandler(RiraQuery);
            return (handler, user);
        }

        #endregion

    }
}
