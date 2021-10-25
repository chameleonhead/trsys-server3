namespace Trsys.BackOffice
{
    public class SubscriberService : ISubscriberService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public SubscriberService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }
    }
}