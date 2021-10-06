using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class AccountId : Identity<AccountId>
    {
        public AccountId(string value) : base(value)
        {
        }
    }
}