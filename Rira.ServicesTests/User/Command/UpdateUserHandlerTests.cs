using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Rira.Contracts.User.Command;
using Rira.Core.Contracts;
using Rira.Models.User;
using Rira.Services.User.Command;

namespace Rira.ServicesTests.User.Command
{
    public class UpdateUserHandlerTests : RiraUnitTestsBase
    {
        /// <summary>
        /// وقتی به درستی کار می کند
        /// </summary>
        [Fact]
        public async Task TestForWhenWorksFine()
        {
            //Arrange
            var init = await Initialize();
            var input = new UpdateUserInput(init.ExistUser.UserId, "John", "Wick", "1234567890", new DateOnly(1986, 1, 25));

            //Act
            var result = await init.Handler.Handle(input, default);

            //Assert
            Assert.True(result.Done);
            var updatedUser = await RiraQuery.Users.ValidPredicate().FirstOrDefaultAsync(m => m.UserId == input.UserId);
            Assert.NotNull(updatedUser);
            Assert.Equal("John", updatedUser.FirstName);
            Assert.Equal("Wick", updatedUser.LastName);
            Assert.Equal("1234567890", updatedUser.NationalCode);
            Assert.Equal(new DateOnly(1986, 1, 25), updatedUser.Birthday);
        }

        /// <summary>
        /// بررسی اینکه وقتی کاربری با این شناسه وجود ندارد نباید خطا دهد و چون چیزی ویرایش نمیشود
        /// باید خروجی منفی دهد
        /// </summary>
        [Fact]
        public async Task TestForWhenNoUserWithId500ExistMustReturnDoneFalse()
        {
            //Arrange
            var init = await Initialize();
            var input = new UpdateUserInput(500, "John", "Wick", "1234567890", new DateOnly(1986, 1, 25));

            //Act
            var result = await init.Handler.Handle(input, default);

            //Assert
            Assert.False(result.Done);
        }

        #region Private Methods

        private async Task<(IRequestHandler<UpdateUserInput, UpdateUserOutput> Handler, UserEntity ExistUser,
            Mock<IClockService> ClockMock)> Initialize()
        {
            RiraCommand.Add(Out(out UserEntity user, new("Adam", "Freeman", "0123456789", new DateOnly(1990, 10, 10))));
            await RiraCommand.SaveChangesAsync();

            var clockMock = new Mock<IClockService>();
            clockMock.Setup(m => m.Now(It.IsAny<string>())).Returns(new DateTime(2025, 7, 7, 6, 30, 0));

            var loggerMock = new Mock<ILogger<UpdateUserHandler>>();

            var handler = new UpdateUserHandler(RiraCommand, clockMock.Object, loggerMock.Object);
            return (handler, user, clockMock);
        }

        #endregion

    }
}
