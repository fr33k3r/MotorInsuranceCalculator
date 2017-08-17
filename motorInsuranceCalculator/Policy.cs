using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorInsuranceCalculator
{
    class Policy
    {
        [Required(ErrorMessage = "Start date of Policy is required")]
        public DateTime startDateOfPolicy { get; set; }

        [Required(ErrorMessage = "You need to at least add one driver")]
        public List<Driver> Drivers { get; set; }

        public Policy()
        {
            Drivers = new List<Driver>();
        }

    }
}
