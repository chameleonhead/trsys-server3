using EventFlow;
using EventFlow.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Application.Read.Models;
using Trsys.CopyTrading.Application.Write.Commands;
using Trsys.CopyTrading.Domain;

namespace Trsys.CopyTrading
{
    public class EaService : IEaService
    {
        private readonly string DISTRIBUTION_GROUP_ID = DistributionGroupId.New.Value;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;
        private readonly IEaSessionTokenProvider tokenProvider;
        private readonly IEaSessionTokenValidator tokenValidator;

        public EaService(ICommandBus commandBus, IQueryProcessor queryProcessor, IEaSessionTokenProvider tokenProvider, IEaSessionTokenValidator tokenValidator)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.tokenProvider = tokenProvider;
            this.tokenValidator = tokenValidator;
        }

        public async Task AddSecretKeyAsync(string key, string keyType)
        {
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherEaReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        await commandBus.PublishAsync(new RegisterPublisherEaCommand(PublisherEaId.New, new SecretKey(key), DistributionGroupId.With(DISTRIBUTION_GROUP_ID), PublisherId.New), CancellationToken.None);
                    }
                    break;
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        await commandBus.PublishAsync(new RegisterSubscriberEaCommand(SubscriberEaId.New, new SecretKey(key), DistributionGroupId.With(DISTRIBUTION_GROUP_ID), AccountId.New), CancellationToken.None);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public Task RemvoeSecretKeyAsync(string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public async Task<EaSession> GenerateSessionTokenAsync(string key, string keyType)
        {
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherEaReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        return null;
                    }
                    var publisherToken = tokenProvider.GenerateToken(publisher.Id, key, keyType);
                    return new EaSession(key, keyType, publisherToken);
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        return null;
                    }
                    var subscriberToken = tokenProvider.GenerateToken(subscriber.Id, key, keyType);
                    return new EaSession(key, keyType, subscriberToken);
                default:
                    throw new ArgumentException();
            }
        }

        public Task ValidateSessionTokenAsync(string token, string key, string keyType)
        {
            if (tokenValidator.ValidateToken(key, keyType, token))
            {
                return Task.CompletedTask;
            }
            throw new EaSessionTokenInvalidException();
        }

        public Task DiscardSessionTokenAsync(string token, string key, string keyType)
        {
            throw new NotImplementedException();
        }

        public async Task PublishOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherEaReadModel>(key), CancellationToken.None);
            if (publisher == null)
            {
                throw new InvalidOperationException();
            }
            await commandBus.PublishAsync(new PublisherEaUpdateOrdersCommand(PublisherEaId.With(publisher.Id), new EaOrderText(text)), CancellationToken.None);
        }

        public async Task<OrderText> GetCurrentOrderTextAsync()
        {
            var distributionGroup = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<DistributionGroupReadModel>(DISTRIBUTION_GROUP_ID), CancellationToken.None);
            if (distributionGroup == null)
            {
                return OrderText.Empty;
            }
            return OrderText.From(distributionGroup.CopyTrades.Select(t => new OrderTextItem(t.Id.GetHashCode(), t.Symbol, t.OrderType == "BUY" ? OrderType.Buy : OrderType.Sell, 0, 0, 0)));

        }

        public async Task SubscribeOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
            if (subscriber == null)
            {
                throw new InvalidOperationException();
            }
            await commandBus.PublishAsync(new SubscriberEaUpdateOrdersCommand(SubscriberEaId.With(subscriber.Id), new EaOrderText(text)), CancellationToken.None);
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, string key, string keyType, string version, string token, string text)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, long eaTimestamp, string key, string keyType, string version, string token, string text)
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
    }
}
