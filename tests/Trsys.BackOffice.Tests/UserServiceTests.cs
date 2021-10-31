using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;

namespace Trsys.BackOffice.Tests
{
    [TestClass]
    public class UserServiceTests
    {
        [TestMethod]
        public async Task CreateAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IUserService>();
            var userId = await service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None);
            var user = await service.FindByUsernameAsync("username", CancellationToken.None);
            Assert.AreEqual("username", user.Username);
            Assert.AreEqual("password", user.PasswordHash);
            Assert.AreEqual("nickname", user.Nickname);
            CollectionAssert.AreEqual(new[] { "Administrator" }, user.Roles);
        }

        [TestMethod]
        public async Task CreateAsyncFail_SameUsername()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IUserService>();
            await service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None);
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None));
        }

        [TestMethod]
        public async Task UpdateNicknameAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IUserService>();
            var userId = await service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None);
            await service.UpdateNicknameAsync(userId, "nickname2", CancellationToken.None);
            var user = await service.FindByUsernameAsync("username", CancellationToken.None);
            Assert.AreEqual("nickname2", user.Nickname);
        }

        [TestMethod]
        public async Task UpdatePasswordHashAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IUserService>();
            var userId = await service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None);
            await service.UpdatePasswordHashAsync(userId, "password2", CancellationToken.None);
            var user = await service.FindByUsernameAsync("username", CancellationToken.None);
            Assert.AreEqual("password2", user.PasswordHash);
        }

        [TestMethod]
        public async Task UpdateRolesAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IUserService>();
            var userId = await service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None);
            await service.UpdateRolesAsync(userId, Array.Empty<string>(), CancellationToken.None);
            var user = await service.FindByUsernameAsync("username", CancellationToken.None);
            CollectionAssert.AreEqual(Array.Empty<string>(), user.Roles);
        }

        [TestMethod]
        public async Task DeleteAsyncSuccess()
        {
            using var services = new ServiceCollection().AddBackOffice().BuildServiceProvider();
            var service = services.GetRequiredService<IUserService>();
            var userId = await service.CreateAsync("username", "password", "nickname", new[] { "Administrator" }, CancellationToken.None);
            await service.DeleteAsync(userId, CancellationToken.None);
            var user = await service.FindByUsernameAsync("username", CancellationToken.None);
            Assert.IsNull(user);
        }
    }
}
