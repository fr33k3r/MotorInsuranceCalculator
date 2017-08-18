using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorInsuranceCalculator
{
    class Policy
    {
        public DateTime StartDateOfPolicy { get; set; }

        public List<Driver> Drivers { get; set; }

        public double Premium { get; set; }

        //We assume the Policy is valid at first
        public bool IsValid { get; set; } = true;

        public Policy()
        {
            //Initialize List of Associated Drivers 
            Drivers = new List<Driver>();
        }        

        public List<string> checkValidation()
        {
            List<string> errorMessages = new List<string>(); 

            if (StartDateOfPolicy.CompareTo(DateTime.Now) < 0)
            {
                IsValid = false;
                errorMessages.Add("Start Date of Policy");
            }

            
            int youngerDriverAge = Drivers.Min(x => Functions.calculateYearsDifference(StartDateOfPolicy, x.dateOfBirth));            
            IEnumerable<string> youngerDriverName = from driver in Drivers
                                             where Functions.calculateYearsDifference(StartDateOfPolicy,driver.dateOfBirth) == youngerDriverAge
                                             select driver.Name;

            int olderDriverAge = Drivers.Max(x => Functions.calculateYearsDifference(StartDateOfPolicy, x.dateOfBirth));
            IEnumerable<string> olderDriverName = from driver in Drivers
                                                    where Functions.calculateYearsDifference(StartDateOfPolicy, driver.dateOfBirth) == olderDriverAge
                                                    select driver.Name;

            if (youngerDriverAge < 21)
            {
                IsValid = false;

                foreach (string name in youngerDriverName)
                {                    
                    errorMessages.Add("Age of Youngest Driver: " + name);
                }
            }

            if (olderDriverAge > 75)
            {
                IsValid = false;

                foreach (string name in olderDriverName)
                {                    
                    errorMessages.Add("Age of Oldest Driver: " + name);
                }
            }

            int sumOfClaims = 0;
            foreach (Driver dr in Drivers)
            {                               
                if (dr.NoOfClaims > 2)
                {
                    IsValid = false;
                    errorMessages.Add($"Driver {dr.Name} has more than 2 claims");
                }
                sumOfClaims += dr.NoOfClaims;
            }

            if (sumOfClaims > 3)
            {
                IsValid = false;
                errorMessages.Add("Policy has more than 3 claims");
            }

            return errorMessages;
        }        

    }
}
