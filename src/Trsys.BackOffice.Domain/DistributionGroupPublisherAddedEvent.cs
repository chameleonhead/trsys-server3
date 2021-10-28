﻿using EventFlow.Aggregates;

namespace Trsys.BackOffice.Domain
{
    public class DistributionGroupPublisherAddedEvent : AggregateEvent<DistributionGroupAggregate, DistributionGroupId>
    {
        public DistributionGroupPublisherAddedEvent(PublisherId publisherId)
        {
            PublisherId = publisherId;
        }

        public PublisherId PublisherId { get; }
    }
}