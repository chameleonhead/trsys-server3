using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventFlow;
using EventFlow.Queries;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;
using Trsys.CopyTrading.Domain;

namespace Trsys.BackOffice
{
    public class UserService : IUserService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public UserService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<UserDto> FindUserByUsernameAsync(string username, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var login = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(username.ToUpperInvariant()), cancellationToken);
            if (login == null)
            {
                return null;
            }
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<UserReadModel>(login.Id), cancellationToken);
            if (user == null)
            {
                return null;
            }
            return new UserDto()
            {
                Id = login.Id,
                Username = user.Username,
                PasswordHash = login.PasswordHash,
                Nickname = user.Nickname,
                Roles = user.Roles,
            };
        }

        public async Task RegisterUserIfNotExistsAsync(string username, string passwordHash, string role, string nickname, IEnumerable<string> distributionGroupIds, CancellationToken cancellationToken = default)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(username.ToUpperInvariant()), cancellationToken);
            if (user == null)
            {
                var commandBus = resolver.Resolve<ICommandBus>();
                await commandBus.PublishAsync(new UserCreateAdministratorCommand(UserId.New, new Username("admin"), new HashedPassword("P@ssw0rd"), new UserNickname("管理者"), new() { DistributionGroupId.New }), CancellationToken.None);
            }
        }

        public async Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserChangePasswordCommand(UserId.With(userId), new HashedPassword(newPassword)), CancellationToken.None);
        }
    }
}