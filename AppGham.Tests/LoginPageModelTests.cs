using AppGham.PageModels;
using AppGham.Services.Interfaces;
using AppGham.Shared.Models;
using FakeItEasy;
using System.Threading.Tasks;
using Xunit;

namespace AppGham.Tests
{
    public class LoginPageModelTests
    {
        public LoginPageModel LoginPageModel { get; set; }

        public IUserService UserService { get; set; }

        public LoginPageModelTests()
        {
            UserService = A.Fake<IUserService>();
            LoginPageModel = A.Fake<LoginPageModel>(option => option.WithArgumentsForConstructor(new object[] { UserService }));
        }

        [Fact]
        public async Task Should_invoke_get_user_when_invoke_save_command()
        {
            //Arrange 
            LoginPageModel.User = new User 
            {
                Name = "Name test",
                Email = "email@email.com",
                Password = "123456"
            };

            A.CallTo(() => UserService.GetUserAsync(LoginPageModel.User.Email));

            //Act  
            await LoginPageModel.SaveCommand.ExecuteAsync();

            //Assert
             A.CallTo(() => UserService.GetUserAsync(LoginPageModel.User.Email))
                .MustHaveHappened();
        }
    }
}
