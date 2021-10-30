﻿using EventFlow.Entities;
using Trsys.CopyTrading.Domain;

namespace Trsys.Ea.Domain
{
    public class PublisherEaCopyTradeEntity : Entity<CopyTradeId>
    {
        public PublisherEaCopyTradeEntity(CopyTradeId id, DistributionGroupId distributionGroupId) : base(id)
        {
            DistributionGroupId = distributionGroupId;
        }

        public DistributionGroupId DistributionGroupId { get; }
    }
}