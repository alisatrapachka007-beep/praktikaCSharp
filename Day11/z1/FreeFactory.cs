class FreeFactory : SubscriptionFactory
{
    public override ISubscription CreateSubscription() => new FreeSubscription();
}
