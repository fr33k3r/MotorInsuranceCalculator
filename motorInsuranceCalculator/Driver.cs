using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorInsuranceCalculator
{
    public enum OccupationType
    {
        Chauffeur,
        Accountant
    }
    class Driver
    {   
        public string Name { get; set; }

        public OccupationType Occupation { get; set; }

        public DateTime dateOfBirth { get; set; }

        public List<Claim> Claims { get; set; }

        public int NoOfClaims
        {
            get
            {
                return Claims.Count;
            }
        }         

        public Driver ()
        {
            //Initialize List of Driver's Claims
            Claims = new List<Claim>();
        }

        

    }
}
