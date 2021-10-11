using EventFlow.Core;

namespace Trsys.CopyTrading.Domain
{
    public class PublisherId : Identity<PublisherId>
    {
        public PublisherId(string value) : base(value)
        {
        }
    }
}