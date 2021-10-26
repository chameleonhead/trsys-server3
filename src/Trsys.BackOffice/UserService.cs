using EventFlow;
using EventFlow.Queries;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Read.Queries;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice
{
    public class UserService : IUserService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public UserService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }

        public async Task<PagedResult<UserDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var totalCount = await queryProcessor.ProcessAsync(new UserReadModelSearchCountQuery(), cancellationToken);
            if (totalCount == 0)
            {
                return new PagedResult<UserDto>(page, perPage, 0, new());
            }
            var items = await queryProcessor.ProcessAsync(new UserReadModelSearchItemsQuery(page, perPage), cancellationToken);
            return new PagedResult<UserDto>(page, perPage, totalCount, items.Select(e => new UserDto()
            {
                Id = e.Id,
                Username = e.Username,
                PasswordHash = e.PasswordHash,
                Nickname = e.Nickname,
                Roles = e.Roles,
            }).ToList());
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
                PasswordHash = user.PasswordHash,
                Nickname = user.Nickname,
                Roles = user.Roles,
            };
        }

        public async Task RegisterAdministratorIfNotExistsAsync(string username, string passwordHash, string nickname, CancellationToken cancellationToken = default)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(username.ToUpperInvariant()), cancellationToken);
            if (user == null)
            {
                var commandBus = resolver.Resolve<ICommandBus>();
                await commandBus.PublishAsync(new UserCreateAdministratorCommand(
                    UserId.New,
                    new Username(username),
                    new HashedPassword(passwordHash),
                    new UserNickname(nickname)
                    ), CancellationToken.None);
            }
        }

        public async Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserChangePasswordCommand(UserId.With(userId), new HashedPassword(newPassword)), CancellationToken.None);
        }
    }
}