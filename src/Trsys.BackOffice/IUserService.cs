using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken);
        Task<UserDto> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);
        Task RegisterAdministratorIfNotExistsAsync(string username, string passwordHash, string nickname, CancellationToken none);
        Task RegisterUserAsync(string username, string passwordHash, string nickname, CancellationToken cancellationToken);
        Task ChangeNicknameAsync(string userId, string nickname, CancellationToken cancellationToken);
        Task ChangePasswordAsync(string userId, string newPasswordHash, CancellationToken cancellationToken);
        Task DeleteUserAsync(string id, CancellationToken cancellationToken);
    }
}