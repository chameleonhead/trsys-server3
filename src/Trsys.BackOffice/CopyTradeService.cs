namespace Trsys.BackOffice
{
    public class CopyTradeService : ICopyTradeService
    {
        private readonly BackOfficeEventFlowRootResolver resolver;

        public CopyTradeService(BackOfficeEventFlowRootResolver resolver)
        {
            this.resolver = resolver;
        }
    }
}