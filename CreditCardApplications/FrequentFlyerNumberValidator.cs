namespace CreditCardApplications
{
    /// <summary>
    /// Contacts an external service to validiate a frequent flyer number.
    /// 
    /// Some of the reasons we want to mock this external service could include:
    /// - Cost. An external company provides this service and bills per usage.
    /// - Slow. The call takes a long time to complete and will slow down unit tests.
    /// - No test version. The external company doest not prvide a test version,
    ///   only a live "real" data production version.
    /// - Unreliable. The external service fails often, making our tests fail.
    /// - Effort. The effort (and/or complexity) to use the real service makes tests painful to write.
    /// </summary>
    /// <seealso cref="CreditCardApplications.IFrequentFlyerNumberValidator" />
    public class FrequentFlyerNumberValidator : IFrequentFlyerNumberValidator
    {
        public IServiceInformation ServiceInformation => throw new System.NotImplementedException("Simulate this real dependency.");

        public ValidationMode ValidationMode
        {
            get => throw new System.NotImplementedException("Simulate this real dependency.");
            set => throw new System.NotImplementedException("Simulate this real dependency.");
        }

        public bool IsValid(string frequentFlyerNumber)
        {
            throw new System.NotImplementedException("Simulate this real dependency being hard to use.");
        }

        public void IsValid(string frequentFlyerNumber, out bool isValid)
        {
            throw new System.NotImplementedException("Simulate this real dependency being hard to use.");
        }
    }
}
