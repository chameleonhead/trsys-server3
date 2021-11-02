using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.BackOffice.Application.Read.Models;
using Trsys.BackOffice.Application.Read.Queries;
using Trsys.BackOffice.Application.Write.Commands;
using Trsys.BackOffice.Domain;

namespace Trsys.BackOffice.Application
{
    public static class EventFlowOptionsExtension
    {
        public static IEventFlowOptions UseBackOfficeApplication(this IEventFlowOptions options)
        {
            options
                .AddCommands(
                    typeof(UserCreateCommand),
                    typeof(UserUpdateNicknameCommand),
                    typeof(UserUpdatePasswordCommand),
                    typeof(UserUpdateRolesCommand),
                    typeof(UserDeleteCommand),
                    typeof(DistributionGroupCreateCommand),
                    typeof(DistributionGroupUpdateNameCommand),
                    typeof(DistributionGroupAddSubscriberCommand),
                    typeof(DistributionGroupRemoveSubscriberCommand),
                    typeof(DistributionGroupDeleteCommand),
                    typeof(PublisherCreateCommand),
                    typeof(PublisherUpdateNameCommand),
                    typeof(PublisherUpdateDescriptionCommand),
                    typeof(PublisherDeleteCommand),
                    typeof(SubscriberCreateCommand),
                    typeof(SubscriberUpdateNameCommand),
                    typeof(SubscriberUpdateDescriptionCommand),
                    typeof(SubscriberDeleteCommand),
                    typeof(CopyTradeOpenCommand),
                    typeof(CopyTradeCloseCommand)
                )
                .AddCommandHandlers(
                    typeof(UserCreateCommandHandler),
                    typeof(UserUpdateNicknameCommandHandler),
                    typeof(UserUpdatePasswordCommandHandler),
                    typeof(UserUpdateRolesCommandHandler),
                    typeof(UserDeleteCommandHandler),
                    typeof(DistributionGroupCreateCommandHandler),
                    typeof(DistributionGroupUpdateNameCommandHandler),
                    typeof(DistributionGroupAddSubscriberCommandHandler),
                    typeof(DistributionGroupRemoveSubscriberCommandHandler),
                    typeof(DistributionGroupDeleteCommandHandler),
                    typeof(PublisherCreateCommandHandler),
                    typeof(PublisherUpdateNameCommandHandler),
                    typeof(PublisherUpdateDescriptionCommandHandler),
                    typeof(PublisherDeleteCommandHandler),
                    typeof(SubscriberCreateCommandHandler),
                    typeof(SubscriberUpdateNameCommandHandler),
                    typeof(SubscriberUpdateDescriptionCommandHandler),
                    typeof(SubscriberDeleteCommandHandler),
                    typeof(CopyTradeOpenCommandHandler),
                    typeof(CopyTradeCloseCommandHandler)
                )
                .AddEvents(
                    typeof(UserUsernameChangedEvent),
                    typeof(UserPasswordChangedEvent),
                    typeof(UserNicknameChangedEvent),
                    typeof(UserRoleAddedEvent),
                    typeof(UserRoleRemovedEvent),
                    typeof(UserInChargeDistributionGroupAddedEvent),
                    typeof(UserInChargeDistributionGroupRemovedEvent),
                    typeof(UserDeletedEvent),
                    typeof(DistributionGroupNameChangedEvent),
                    typeof(DistributionGroupPublisherAddedEvent),
                    typeof(DistributionGroupPublisherRemovedEvent),
                    typeof(DistributionGroupSubscriberAddedEvent),
                    typeof(DistributionGroupSubscriberRemovedEvent),
                    typeof(DistributionGroupDeletedEvent),
                    typeof(PublisherNameChangedEvent),
                    typeof(PublisherDescriptionChangedEvent),
                    typeof(PublisherDeletedEvent),
                    typeof(SubscriberNameChangedEvent),
                    typeof(SubscriberDescriptionChangedEvent),
                    typeof(SubscriberDeletedEvent),
                    typeof(CopyTradeOpenedEvent),
                    typeof(CopyTradeClosedEvent)
                );
            options
                .RegisterServices(sr =>
                {
                    sr.RegisterType(typeof(LoginReadModelLocator));
                })
                .UseInMemoryReadStoreFor<UserReadModel>()
                .UseInMemoryReadStoreFor<LoginReadModel, LoginReadModelLocator>()
                .UseInMemoryReadStoreFor<DistributionGroupReadModel>()
                .UseInMemoryReadStoreFor<PublisherReadModel>()
                .UseInMemoryReadStoreFor<SubscriberReadModel>()
                .UseInMemoryReadStoreFor<CopyTradeReadModel>()
                .AddQueryHandler<UserReadModelSearchCountQueryHandler, UserReadModelSearchCountQuery, int>()
                .AddQueryHandler<UserReadModelSearchItemsQueryHandler, UserReadModelSearchItemsQuery, List<UserReadModel>>()
                .AddQueryHandler<DistributionGroupReadModelSearchCountQueryHandler, DistributionGroupReadModelSearchCountQuery, int>()
                .AddQueryHandler<DistributionGroupReadModelSearchItemsQueryHandler, DistributionGroupReadModelSearchItemsQuery, List<DistributionGroupReadModel>>()
                .AddQueryHandler<PublisherReadModelSearchCountQueryHandler, PublisherReadModelSearchCountQuery, int>()
                .AddQueryHandler<PublisherReadModelSearchItemsQueryHandler, PublisherReadModelSearchItemsQuery, List<PublisherReadModel>>()
                .AddQueryHandler<SubscriberReadModelSearchCountQueryHandler, SubscriberReadModelSearchCountQuery, int>()
                .AddQueryHandler<SubscriberReadModelSearchItemsQueryHandler, SubscriberReadModelSearchItemsQuery, List<SubscriberReadModel>>()
                .AddQueryHandler<CopyTradeReadModelSearchCountQueryHandler, CopyTradeReadModelSearchCountQuery, int>()
                .AddQueryHandler<CopyTradeReadModelSearchItemsQueryHandler, CopyTradeReadModelSearchItemsQuery, List<CopyTradeReadModel>>();
            return options;
        }
    }
}
