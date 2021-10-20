using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;

namespace Trsys.BackOffice.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseBackOfficeApplication(this IEventFlowOptions options)
        {
            return options;
        }
    }
}
