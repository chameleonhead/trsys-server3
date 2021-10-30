using EventFlow;
using EventFlow.Queries;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.CopyTrading.Domain;
using Trsys.Ea.Application.Read.Models;
using Trsys.Ea.Application.Write.Commands;
using Trsys.Ea.Domain;
using Trsys.Ea.LogParsing;

namespace Trsys.Ea
{
    public class EaService : IEaService
    {
        private readonly string DISTRIBUTION_GROUP_ID = DistributionGroupId.New.Value;
        private readonly ICommandBus commandBus;
        private readonly IQueryProcessor queryProcessor;
        private readonly IEaSessionManager sessionManager;

        public EaService(ICommandBus commandBus, IQueryProcessor queryProcessor, IEaSessionManager sessionManager)
        {
            this.commandBus = commandBus;
            this.queryProcessor = queryProcessor;
            this.sessionManager = sessionManager;
        }

        public async Task AddSecretKeyAsync(string key, string keyType)
        {
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherEaReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        await commandBus.PublishAsync(new PublisherEaRegisterCommand(PublisherEaId.New, new SecretKey(key), DistributionGroupId.With(DISTRIBUTION_GROUP_ID)), CancellationToken.None);
                    }
                    break;
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        await commandBus.PublishAsync(new SubscriberEaRegisterCommand(SubscriberEaId.New, new SecretKey(key), DistributionGroupId.With(DISTRIBUTION_GROUP_ID), SubscriberId.New), CancellationToken.None);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public async Task RemvoeSecretKeyAsync(string key, string keyType)
        {
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherEaReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        return;
                    }
                    await commandBus.PublishAsync(new PublisherEaUnregisterCommand(PublisherEaId.With(publisher.Id), DistributionGroupId.With(DISTRIBUTION_GROUP_ID)), CancellationToken.None);
                    await sessionManager.DestroySessionAsync(publisher.Id);
                    return;
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        return;
                    }
                    var subscriberId = subscriber.GetSubscriberId(DISTRIBUTION_GROUP_ID);
                    if (subscriberId == null)
                    {
                        return;
                    }
                    await commandBus.PublishAsync(new SubscriberEaUnregisterCommand(SubscriberEaId.With(subscriber.Id), DistributionGroupId.With(DISTRIBUTION_GROUP_ID), SubscriberId.With(subscriberId)), CancellationToken.None);
                    await sessionManager.DestroySessionAsync(subscriber.Id);
                    return;
                default:
                    throw new ArgumentException();
            }
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
                    return await sessionManager.CreateSessionAsync(publisher.Id, key, keyType);
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        return null;
                    }
                    return await sessionManager.CreateSessionAsync(subscriber.Id, key, keyType);
                default:
                    throw new ArgumentException();
            }
        }

        public async Task ValidateSessionTokenAsync(string token, string key, string keyType)
        {
            if (await sessionManager.ValidateTokenAsync(key, keyType, token))
            {
                return;
            }
            throw new EaSessionTokenInvalidException();
        }

        public Task DiscardSessionTokenAsync(string token, string key, string keyType)
        {
            return sessionManager.DestroySessionAsync(key, keyType, token);
        }

        public async Task PublishOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherEaReadModel>(key), CancellationToken.None);
            if (publisher == null)
            {
                throw new InvalidOperationException();
            }
            await commandBus.PublishAsync(new PublisherEaUpdateOrderTextCommand(PublisherEaId.With(publisher.Id), new EaOrderText(text)), CancellationToken.None);
        }

        public async Task<OrderText> GetCurrentOrderTextAsync(string key)
        {
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
            if (subscriber == null)
            {
                return OrderText.Empty;
            }
            return OrderText.Parse(subscriber.Text);

        }

        public async Task SubscribeOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberEaReadModel>(key), CancellationToken.None);
            if (subscriber == null)
            {
                throw new InvalidOperationException();
            }
            await commandBus.PublishAsync(new SubscriberEaDistributeOrderTextCommand(SubscriberEaId.With(subscriber.Id), new EaOrderText(text)), CancellationToken.None);
        }

        public Task ReceiveLogAsync(DateTimeOffset serverTimestamp, string key, string keyType, string version, string token, string text)
        {
            var logs = EaLogParser.Parse(serverTimestamp, key, keyType, token, version, text);
            foreach (var logInfo in logs)
            {
                Console.WriteLine(JsonConvert.SerializeObject(logInfo));
            }
            return Task.CompletedTask;
        }
    }
}
