namespace Trsys.BackOffice
{
    public class DistributionGroupService : IDistributionGroupService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public DistributionGroupService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }
    }
}