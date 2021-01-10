using System;

namespace CreditCardApplications
{
    public class CreditCardApplicationEvaluator
    {
        private const int AutoReferrealMaxAge = 20;
        private const int HighIncomeThreshold = 100_000;
        private const int LowIncomeThreshold = 20_000;

        private readonly IFrequentFlyerNumberValidator _frequentFlyerNumberValidator;
        private readonly FraudLookup _fraudLookup;

        public int ValidatorLookupCount { get; private set; }

        public CreditCardApplicationEvaluator(
            IFrequentFlyerNumberValidator frequentFlyerNumberValidator,
            FraudLookup fraudLookup = null)
        {
            _frequentFlyerNumberValidator = frequentFlyerNumberValidator ?? throw new System.ArgumentNullException(nameof(frequentFlyerNumberValidator));
            _fraudLookup = fraudLookup;
            _frequentFlyerNumberValidator.ValidatorLookupPerformed += FrequentFlyerNumberValidator_ValidatorLookupPerformed;
        }

        private void FrequentFlyerNumberValidator_ValidatorLookupPerformed(object sender, EventArgs e)
        {
            ++ValidatorLookupCount;
        }

        public CreditCardApplicationDecision Evaluate(CreditCardApplication application)
        {
            if (_fraudLookup != null && _fraudLookup.IsFraudRisk(application))
            {
                return CreditCardApplicationDecision.ReferredToHumanFraudRisk;
            }

            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            if (_frequentFlyerNumberValidator.ServiceInformation.License.Key == "EXPIRED")
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            _frequentFlyerNumberValidator.ValidationMode = application.Age >= 30 ?
                ValidationMode.Detailed :
                ValidationMode.Quick;

            bool isValidFrequentFlyerNumber;
            try
            {
                isValidFrequentFlyerNumber = _frequentFlyerNumberValidator.IsValid(application.FrequenceFlyerNumber);
            }
            catch (Exception)
            {
                // Log exception.
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (!isValidFrequentFlyerNumber)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.Age <= AutoReferrealMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }

        public CreditCardApplicationDecision EvaluateUsingOut(CreditCardApplication application)
        {
            if (application.GrossAnnualIncome >= HighIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoAccepted;
            }

            _frequentFlyerNumberValidator.IsValid(application.FrequenceFlyerNumber, out var isValidFrequentFlyerNumber);

            if (!isValidFrequentFlyerNumber)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.Age <= AutoReferrealMaxAge)
            {
                return CreditCardApplicationDecision.ReferredToHuman;
            }

            if (application.GrossAnnualIncome < LowIncomeThreshold)
            {
                return CreditCardApplicationDecision.AutoDeclined;
            }

            return CreditCardApplicationDecision.ReferredToHuman;
        }
    }
}
