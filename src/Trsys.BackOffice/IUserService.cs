using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> SearchAsync(int page, int perPage);
        Task<UserDto> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);
        Task RegisterAdministratorIfNotExistsAsync(string username, string passwordHash, string nickname, CancellationToken none);
        Task ChangePasswordAsync(string userId, string newPassword, CancellationToken cancellationToken);
    }
}