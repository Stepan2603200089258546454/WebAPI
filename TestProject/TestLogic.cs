using Logic;
using Logic.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace TestProject
{
    [TestClass]
    public sealed class TestLogic : TestBase
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            RegistrationServices.UseLogic(services, Configuration);
        }

        [TestMethod]
        public async Task TestMethod_UserServices()
        {
            IUserServices service = GetService<IUserServices>();
            string token = await service.RegisterAsync("123@gmail.com", "Qwerty_123456789");
            
            Assert.IsNotEmpty(token);

            string token2 = await service.LoginAsync("123@gmail.com", "Qwerty_123456789");

            Assert.IsNotEmpty(token2);
        }
    }
}
