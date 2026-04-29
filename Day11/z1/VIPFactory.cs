class VIPFactory : SubscriptionFactory
{
    public override ISubscription CreateSubscription() => new VIPSubscription();
}
