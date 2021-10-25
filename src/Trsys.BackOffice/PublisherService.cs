namespace Trsys.BackOffice
{
    public class PublisherService : IPublisherService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public PublisherService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }
    }
}