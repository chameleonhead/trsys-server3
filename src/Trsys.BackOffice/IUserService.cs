using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> SearchAsync(int page, int perPage, CancellationToken cancellationToken);
        Task<UserDto> FindByUsernameAsync(string username, CancellationToken cancellationToken);
        Task CreateAdministratorIfNotExistsAsync(string username, string passwordHash, string nickname, CancellationToken none);
        Task CreateAsync(string username, string passwordHash, string nickname, IEnumerable<string> roles, CancellationToken cancellationToken);
        Task UpdateNicknameAsync(string userId, string nickname, CancellationToken cancellationToken);
        Task UpdatePasswordAsync(string userId, string newPasswordHash, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}