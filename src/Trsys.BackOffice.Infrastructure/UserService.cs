using EventFlow;
using EventFlow.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.BackOffice.Abstractions;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Read.Queries;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Infrastructure
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
            return new PagedResult<UserDto>(page, perPage, totalCount, items.Select(item => new UserDto()
            {
                Id = item.Id,
                Username = item.Username,
                PasswordHash = item.PasswordHash,
                Nickname = item.Nickname,
                Roles = item.Roles,
            }).ToList());
        }

        public async Task<UserDto> FindByUsernameAsync(string username, CancellationToken cancellationToken)
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

        public async Task CreateAdministratorIfNotExistsAsync(string username, string passwordHash, string nickname, CancellationToken cancellationToken = default)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(username.ToUpperInvariant()), cancellationToken);
            if (user == null)
            {
                var commandBus = resolver.Resolve<ICommandBus>();
                await commandBus.PublishAsync(new UserCreateCommand(
                    UserId.New,
                    new Username(username),
                    new HashedPassword(passwordHash),
                    new UserNickname(nickname),
                    new[] { Role.Administrator }
                    ), CancellationToken.None);
            }
        }

        public async Task<string> CreateAsync(string username, string passwordHash, string nickname, IEnumerable<string> roles, CancellationToken cancellationToken = default)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var user = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<LoginReadModel>(username.ToUpperInvariant()), cancellationToken);
            if (user != null)
            {
                throw new InvalidOperationException();
            }
            var userId = UserId.New;
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserCreateCommand(
                userId,
                new Username(username),
                new HashedPassword(passwordHash),
                new UserNickname(nickname),
                roles.Select(role => Role.Of(role))
                ), CancellationToken.None);
            return userId.Value;
        }

        public async Task UpdatePasswordHashAsync(string userId, string newPasswordHash, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserUpdatePasswordCommand(UserId.With(userId), new HashedPassword(newPasswordHash)), CancellationToken.None);
        }

        public async Task UpdateNicknameAsync(string userId, string nickname, CancellationToken cancellationToken = default)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserUpdateNicknameCommand(UserId.With(userId), new UserNickname(nickname)), CancellationToken.None);
        }

        public async Task UpdateRolesAsync(string userId, IEnumerable<string> roles, CancellationToken cancellationToken = default)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserUpdateRolesCommand(UserId.With(userId), roles.Select(role => Role.Of(role))), CancellationToken.None);
        }

        public async Task DeleteAsync(string userId, CancellationToken cancellationToken)
        {
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new UserDeleteCommand(UserId.With(userId)), CancellationToken.None);
        }
    }
}