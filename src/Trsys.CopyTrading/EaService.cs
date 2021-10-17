using EventFlow;
using EventFlow.Queries;
using System;
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
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        await commandBus.PublishAsync(new RegisterPublisherSecretKeyCommand(SecretKeyId.New, new SecretKey(key), DistributionGroupId.With(DISTRIBUTION_GROUP_ID), PublisherId.New), CancellationToken.None);
                    }
                    break;
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
                    AccountId accountId;
                    if (subscriber == null)
                    {
                        accountId = AccountId.New;
                        await commandBus.PublishAsync(new RegisterSubscriberSecretKeyCommand(SecretKeyId.New, new SecretKey(key), accountId), CancellationToken.None);
                    }
                    else
                    {
                        accountId = AccountId.With(subscriber.Id);
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

        public async Task<EaSession> GenerateSessionTokenAsync(string key, string keyType)
        {
            switch (keyType)
            {
                case "Publisher":
                    var publisherReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (publisherReadModel == null)
                    {
                        return null;
                    }
                    var publisherToken = tokenProvider.GenerateToken(publisherReadModel.Id, key, keyType);
                    return new EaSession(key, keyType, publisherToken);
                case "Subscriber":
                    var subscriberReadModel = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (subscriberReadModel == null)
                    {
                        return null;
                    }
                    var subscriberToken = tokenProvider.GenerateToken(subscriberReadModel.Id, key, keyType);
                    return new EaSession(key, keyType, subscriberToken);
                default:
                    throw new ArgumentException();
            }
        }

        public Task DiscardSessionTokenAsync(string token, string key, string keyType)
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
