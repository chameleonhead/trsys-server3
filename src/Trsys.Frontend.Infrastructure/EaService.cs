using EventFlow;
using EventFlow.Queries;
using Newtonsoft.Json;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trsys.Core;
using Trsys.Frontend.Abstractions;
using Trsys.Frontend.Application.Read.Models;
using Trsys.Frontend.Application.Write.Commands;
using Trsys.Frontend.Domain;
using Trsys.Frontend.Infrastructure.LogParsing;

namespace Trsys.Frontend.Infrastructure
{
    public class EaService : IEaService
    {
        private readonly EaEventFlowRootResolver resolver;
        private readonly IEaSessionManager sessionManager;

        public EaService(EaEventFlowRootResolver resolver, IEaSessionManager sessionManager)
        {
            this.resolver = resolver;
            this.sessionManager = sessionManager;
        }

        public async Task<SecretKeyDto> FindByKeyAsync(string key, string keyType)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        return null;
                    }
                    return new SecretKeyDto()
                    {
                        Id = publisher.Id,
                        Key = publisher.Key,
                        KeyType = "Publisher",
                    };
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        return null;
                    }
                    return new SecretKeyDto()
                    {
                        Id = subscriber.Id,
                        Key = subscriber.Key,
                        KeyType = "Subscriber",
                    };
                default:
                    throw new ArgumentException();
            }
        }

        public async Task AddSecretKeyAsync(string distributionGroupId, string key, string keyType)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        await commandBus.PublishAsync(new PublisherRegisterCommand(PublisherId.New, new SecretKey(key), DistributionGroupId.With(distributionGroupId)), CancellationToken.None);
                    }
                    break;
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        await commandBus.PublishAsync(new SubscriberRegisterCommand(SubscriberId.New, new SecretKey(key), DistributionGroupId.With(distributionGroupId)), CancellationToken.None);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public async Task RemvoeSecretKeyAsync(string distributionGroupId, string key, string keyType)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        return;
                    }
                    await commandBus.PublishAsync(new PublisherUnregisterCommand(PublisherId.With(publisher.Id), DistributionGroupId.With(distributionGroupId)), CancellationToken.None);
                    await sessionManager.DestroySessionAsync(publisher.Id);
                    return;
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
                    if (subscriber == null)
                    {
                        return;
                    }
                    await commandBus.PublishAsync(new SubscriberUnregisterCommand(SubscriberId.With(subscriber.Id), DistributionGroupId.With(distributionGroupId)), CancellationToken.None);
                    await sessionManager.DestroySessionAsync(subscriber.Id);
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        public async Task<EaSession> GenerateSessionTokenAsync(string key, string keyType)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var commandBus = resolver.Resolve<ICommandBus>();
            switch (keyType)
            {
                case "Publisher":
                    var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
                    if (publisher == null)
                    {
                        return null;
                    }
                    return await sessionManager.CreateSessionAsync(publisher.Id, key, keyType);
                case "Subscriber":
                    var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
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
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var publisher = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<PublisherReadModel>(key), CancellationToken.None);
            if (publisher == null)
            {
                throw new InvalidOperationException();
            }
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new PublisherUpdateOrderTextCommand(PublisherId.With(publisher.Id), new EaOrderText(text)), CancellationToken.None);
        }

        public async Task<OrderText> GetCurrentOrderTextAsync(string key)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
            if (subscriber == null)
            {
                return OrderText.Empty;
            }
            return OrderText.Parse(subscriber.Text);

        }

        public async Task SubscribeOrderTextAsync(DateTimeOffset timestamp, string key, string text)
        {
            var queryProcessor = resolver.Resolve<IQueryProcessor>();
            var subscriber = await queryProcessor.ProcessAsync(new ReadModelByIdQuery<SubscriberReadModel>(key), CancellationToken.None);
            if (subscriber == null)
            {
                throw new InvalidOperationException();
            }
            var commandBus = resolver.Resolve<ICommandBus>();
            await commandBus.PublishAsync(new SubscriberDistributeOrderTextCommand(SubscriberId.With(subscriber.Id), new EaOrderText(text)), CancellationToken.None);
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
