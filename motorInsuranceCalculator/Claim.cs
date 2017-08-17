using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorInsuranceCalculator
{
    class Claim
    {
        [Required(ErrorMessage = "The Date of Claim is required")]
        public DateTime dateOfClaim { get; set; }

    }
}
