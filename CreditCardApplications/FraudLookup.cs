using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardApplications
{
    public class FraudLookup
    {
        public bool IsFraudRisk(CreditCardApplication application)
        {
            return CheckApplication(application);
        }


        protected virtual bool CheckApplication(CreditCardApplication application)
        {
            if (application.LastName == "Smith")
            {
                return true;
            }

            return false;
        }
    }
}
