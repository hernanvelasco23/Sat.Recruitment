using Sat.Recruitment.Application.Handlers.Users;
using Sat.Recruitment.Domain.Entities;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Handlers
{
    public class UserHandlerTest
    {

        [Fact(DisplayName = "HandleWithEmptyNameShouldReturnNotification")]
        public async Task HandleWithEmptyNameShouldReturnNotification()
        {
            //Arrange
            var user = new UserEntity() { Name = string.Empty, Email = "mike@gmail.com", Phone = "+349 1122354215", Address = "Av. Juan G" };

            //Act
            var request = new CreateUserRequest(string.Empty, user.Email, user.Address, user.Phone, string.Empty, string.Empty);
            var testObject = new CreateUserHandler();
            var result = await testObject.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.Notifications.Count == 1);
            Assert.True(result.Invalid);

        }


        [Fact(DisplayName = "HandleWithCorrectDataShouldWork")]
        public async Task HandleWithCorrectDataShouldWork()
        {
            //Arrange
            var user = new UserEntity() { Name = "Mike", Email = "mike@gmail.com", Phone = "+349 1122354215", Address = "Av. Juan G" };

            //Act
            var request = new CreateUserRequest(user.Name, user.Email, user.Address, user.Phone, "", "");
            var testObject = new CreateUserHandler();
            var result = await testObject.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.Notifications.Count == 0);
            Assert.True(result.Valid);

        }

        [Fact(DisplayName = "HandleWithDuplicateNameShouldNotWork")]
        public async Task HandleWithDuplicateNameShouldNotWork()
        {
            //Arrange
            var user = new UserEntity() { Name = "Franco", Email = "Franco.Perez@gmail.com", Phone = "+349 1122354215", Address = "Av. Juan G" };

            //Act
            var request = new CreateUserRequest(user.Name, user.Email, user.Address, user.Phone, "", "");
            var testObject = new CreateUserHandler();
            var result = await testObject.Handle(request, new System.Threading.CancellationToken());

            //Assert
            Assert.True(result.Notifications.Count == 1);
            Assert.True(result.Invalid);

        }
    }
}
