using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Trsys.BackOffice
{
    public interface IBackOfficeService
    {
        Task RegisterUserIfNotExistsAsync(string username, string passwordHash, string role, string nickname, IEnumerable<string> distributionGroupIds, CancellationToken none);
        Task<UserDto> FindUserByUsernameAsync(string username, CancellationToken cancellationToken);
    }
}