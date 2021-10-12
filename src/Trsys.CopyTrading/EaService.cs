using EventFlow;
using EventFlow.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Read.Queries;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading
{
    public class EaService : IEaService
    {
        private readonly string DISTRIBUTION_GROUP_ID = DistributionGroupId.New.Value;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;

        public EaService(ICommandBus commandBus, IQueryProcessor queryProcessor)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
        }

        public async Task AddSecretKeyAsync(string key, string keyType)
        {
            switch(keyType)
            {
                case "Publisher":
                    await commandBus.PublishAsync(new AddPublisherCommand(DistributionGroupId.With(DISTRIBUTION_GROUP_ID), new ClientKey(key)), CancellationToken.None);
                    break;
                case "Subscriber":
                    var model = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
                    AccountId accountId;
                    if (model == null)
                    {
                        accountId = AccountId.New;
                        await commandBus.PublishAsync(new CreateAccountCommand(accountId, new ClientKey(key)), CancellationToken.None);
                    } else
                    {
                        accountId = AccountId.With(model.Id);
                    }
                    await commandBus.PublishAsync(new AddSubscriberCommand(DistributionGroupId.With(DISTRIBUTION_GROUP_ID), accountId), CancellationToken.None);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public Task RemvoeSecretKeyAsync(string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task DiscardSessionTokenAsync(string token, string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task<EaSession> GenerateSessionTokenAsync(string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public Task<OrderText> GetCurrentOrderTextAsync()
        {
            throw new NotImplementedException();
        }

        public Task PublishOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, string key, string keyType, string version, string token, string text)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, long eaTimestamp, string key, string keyType, string version, string token, string text)
        {
            throw new NotImplementedException();
        }

        public Task SubscribeOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            throw new NotImplementedException();
        }

        public void SubscribeOrderTextUpdated(Action<OrderText> handler)
        {
            throw new NotImplementedException();
        }

        public void UnsubscribeOrderTextUpdated(Action<OrderText> handler)
        {
            throw new NotImplementedException();
        }

        public Task ValidateSessionTokenAsync(string token, string key, string keyType)
        {
            throw new NotImplementedException();
        }
    }
}
