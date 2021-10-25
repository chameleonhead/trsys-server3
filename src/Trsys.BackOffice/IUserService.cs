using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> SearchAsync(int page, int perPage);
        Task<UserDto> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);
        Task RegisterUserIfNotExistsAsync(string username, string passwordHash, string role, string nickname, IEnumerable<string> distributionGroupIds, CancellationToken none);
        Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken);
    }
}