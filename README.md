コピートレードシステム (Backend)
==============================

Domain
------

- TradeDistributionGroup
- TradeSubscription
    - TradeRule
        - PercentageTradeRule
        - FixedVolumeTradeRule
    - AccountId
- Account
    - Balance
- CopyTrade
    - Symbol
    - OrderType
- TradeDistribution
    - AccountId
    - TradeOrder
        - Percentage / FixedVolume
        - Symbol
        - OrderType
