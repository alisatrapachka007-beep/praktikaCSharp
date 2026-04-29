class PremiumFactory : SubscriptionFactory
{
    public override ISubscription CreateSubscription() => new PremiumSubscription();
}
