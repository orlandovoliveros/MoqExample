namespace CreditCardApplications
{
    public enum CreditCardApplicationDecision : byte
    {
        Unknown,
        AutoAccepted,
        AutoDeclined,
        ReferredToHuman,
        ReferredToHumanFraudRisk
    }
}
