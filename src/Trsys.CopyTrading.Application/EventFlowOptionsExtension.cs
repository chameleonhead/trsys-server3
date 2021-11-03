using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Read.Queries;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseCopyTradeApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(DistributionGroupAddSubscriberCommand),
                    typeof(DistributionGroupRemoveSubscriberCommand),
                    typeof(DistributionGroupPublishOpenCommand),
                    typeof(DistributionGroupPublishCloseCommand)
                )
                .AddCommandHandlers(
                    typeof(DistributionGroupAddSubscriberCommandHandler),
                    typeof(DistributionGroupRemoveSubscriberCommandHandler),
                    typeof(DistributionGroupPublishOpenCommandHandler),
                    typeof(DistributionGroupPublishCloseCommandHandler)
                )
                .AddEvents(
                    typeof(DistributionGroupSubscriberAddedEvent),
                    typeof(DistributionGroupSubscriberRemovedEvent),
                    typeof(DistributionGroupOpenPublishedEvent),
                    typeof(DistributionGroupClosePublishedEvent)
                );
            options
                .RegisterServices(sr =>
                {
                    sr.RegisterType(typeof(CopyTradeReadModelLocator));
                })
                .UseInMemoryReadStoreFor<DistributionGroupReadModel>()
                .UseInMemoryReadStoreFor<CopyTradeReadModel, CopyTradeReadModelLocator>()
                .AddQueryHandler<CopyTradeReadModelAllQueryHandler, CopyTradeReadModelAllQuery, IReadOnlyCollection<CopyTradeReadModel>>();
            return options;
        }
    }
}
