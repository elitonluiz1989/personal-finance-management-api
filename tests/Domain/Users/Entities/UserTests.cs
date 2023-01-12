using PersonalFinanceManagement.Domain.Users.Entities;
using PersonalFinanceManagement.Tests.Domain.Users.Builders;

namespace PersonalFinanceManagement.Tests.Domain.Users.Entities
{
    public class UserTests
    {
        [Fact]
        public void ShouldValidate()
        {
            var user = new User();
            var builder = UserBuilder.New();

            Assert.False(user.Validate());
            Assert.False(builder.WithoutUserName().Build().Validate());
            Assert.False(builder.WithoutName().Build().Validate());
            Assert.False(builder.WithoutEmail().Build().Validate());
            Assert.False(builder.WithoutPassword().Build().Validate());
            Assert.False(builder.WithoutUserRole().Build().Validate());
        }
    }
}
