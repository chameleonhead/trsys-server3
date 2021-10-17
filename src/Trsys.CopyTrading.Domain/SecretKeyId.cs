using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class SecretKeyId : Identity<SecretKeyId>
    {
        public SecretKeyId(string value) : base(value)
        {
        }
    }
}