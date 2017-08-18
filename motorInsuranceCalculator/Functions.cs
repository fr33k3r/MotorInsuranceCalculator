using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorInsuranceCalculator
{

    //This class contains helpful functions
    class Functions
    {
        public static int calculateYearsDifference(DateTime d1, DateTime d2)
        {
            int age = d1.Year - d2.Year;
            if (d2 > d1.AddYears(-age)) age--;
            return age;
        }

    }
}
