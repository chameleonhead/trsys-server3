using EventFlow;
using EventFlow.Extensions;
using System.Collections.Generic;
using Trsys.BackOffice.Application.Read.Models;
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
                    typeof(UserCreateAdministratorCommand),
                    typeof(UserChangePasswordCommand)
                )
                .AddCommandHandlers(
                    typeof(UserCreateAdministratorCommandHandler),
                    typeof(UserChangePasswordCommandHandler)
                )
                .AddEvents(
                    typeof(UserCreatedEvent),
                    typeof(UserPasswordChangedEvent),
                    typeof(UserRoleAddedEvent),
                    typeof(UserInChargeDistributionGroupAddedEvent),
                    typeof(UserInChargeDistributionGroupRemovedEvent)
                );
            options
                .RegisterServices(sr =>
                {
                    sr.RegisterType(typeof(LoginReadModelLocator));
                })
                .UseInMemoryReadStoreFor<UserReadModel>()
                .UseInMemoryReadStoreFor<LoginReadModel, LoginReadModelLocator>();
            return options;
        }
    }
}
