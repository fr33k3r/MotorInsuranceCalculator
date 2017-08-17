using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "Driver Name is required")]     
        public string Name { get; set; }

        [Required(ErrorMessage = "Driver Occupation is required and should be Accountant or Chauffeur")]
        public OccupationType Occupation { get; set; }

        [Required(ErrorMessage = "Driver's Date of Birth is required")]
        public DateTime dateOfBirth { get; set; }

        public List<Claim> Claims { get; set; }

        public Driver ()
        {
            Claims = new List<Claim>();
        }

    }
}
