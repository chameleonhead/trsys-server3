using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IUserService
    {
        Task RegisterUserIfNotExistsAsync(string username, string passwordHash, string role, string nickname, IEnumerable<string> distributionGroupIds, CancellationToken none);
        Task<UserDto> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);
        Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken);
    }
}