using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MotorInsuranceCalculator
{   

    class MotorInsuranceCalculator
    {
        static void Main(string[] args)
        {            

            Policy policy = new Policy();

            try
            {
                // If the string has been input by an end user, you might want to format it according to the current culture:   
                IFormatProvider culture = System.Threading.Thread.CurrentThread.CurrentCulture;              

                // Assuming we are in Ireland, we specify exactly how to interpret the string.                  
                //IFormatProvider culture = new CultureInfo("en-IE", true);

                Console.Write("Start Date of the Policy: ");
                policy.StartDateOfPolicy = DateTime.Parse(Console.ReadLine(), culture, DateTimeStyles.AssumeLocal);    
                            

                int numberOfDrivers;
                do
                {
                    Console.Write("Number of Drivers: ");
                    numberOfDrivers = Int32.Parse(Console.ReadLine());
                }
                while (numberOfDrivers < 1 || numberOfDrivers > 5);

                for (int i = 1; i <= numberOfDrivers; i++)
                {
                    Driver driver = new Driver();

                    Console.Write($"Name of {i}st Driver: ");
                    driver.Name = Console.ReadLine();

                    int occupationType;
                    do
                    {
                        Console.Write("Driver's Occupation: Type 0 for 'Chauffeur' or 1 for 'Accountant'): ");
                        occupationType = Int32.Parse(Console.ReadLine());
                    }
                    while (occupationType != 0 && occupationType != 1);
                    driver.Occupation = (OccupationType)occupationType;

                    Console.Write("Date of Birth: ");
                    driver.dateOfBirth = DateTime.Parse(Console.ReadLine(), culture, DateTimeStyles.AssumeLocal).Date;
                   

                    int numberOfClaims;
                    do
                    {
                        Console.Write("Number of Claims: ");
                        numberOfClaims = Int32.Parse(Console.ReadLine());
                    }
                    while (numberOfClaims < 0 || numberOfClaims > 5);

                    for (int j = 1; j <= numberOfClaims; j++)
                    {
                        Claim claim = new Claim();                       

                        Console.Write($"Date of {j}st Claim: ");
                        claim.DateOfClaim = DateTime.Parse(Console.ReadLine(), culture, DateTimeStyles.AssumeLocal).Date;
                        claim.DateOfClaim.ToShortDateString();

                        driver.Claims.Add(claim);                       
                    }

                    policy.Drivers.Add(driver);
                }

                List<string> errorMessages = policy.checkValidation();
                if (policy.IsValid)
                {
                    double premium = 500;

                    bool chauffeurExists = policy.Drivers.Exists(x => x.Occupation == OccupationType.Chauffeur);
                    bool accountantExists = policy.Drivers.Exists(x => x.Occupation == OccupationType.Accountant);

                    if (chauffeurExists && !accountantExists)
                    {
                        premium += 0.1 * premium;
                    }
                    else if (accountantExists && !chauffeurExists)
                    {
                        premium -= 0.1 * premium;
                    }

                    int youngerDriverAge = policy.Drivers.Min(x => Functions.calculateYearsDifference(policy.StartDateOfPolicy, x.dateOfBirth));

                    if (youngerDriverAge >= 21 && youngerDriverAge <= 25)
                    {
                        premium += 0.2 * premium;
                    }
                    else if (youngerDriverAge >= 26 && youngerDriverAge <= 75)
                    {
                        premium -= 0.1 * premium;
                    }

                    foreach (Driver dr in policy.Drivers)
                    {
                        foreach (Claim cl in dr.Claims)
                        {
                            if (Functions.calculateYearsDifference(policy.StartDateOfPolicy, cl.DateOfClaim) < 1)                            
                            {
                                premium += 0.2 * premium;
                            }
                            else if (Functions.calculateYearsDifference(policy.StartDateOfPolicy, cl.DateOfClaim) >= 2  && Functions.calculateYearsDifference(policy.StartDateOfPolicy, cl.DateOfClaim) <= 5)
                            {
                                premium += 0.1 * premium;
                            }
                        }
                    }

                    policy.Premium = premium;


                    //Assisting Messages

                    //Console.WriteLine($"Start Date of Policy: {policy.StartDateOfPolicy.ToShortDateString()}");
                    //foreach (Driver dr in policy.Drivers)
                    //{
                    //    Console.WriteLine($"Name of Driver: {dr.Name}");
                    //    Console.WriteLine($"Occupation of Driver: {dr.Occupation}");
                    //    Console.WriteLine($"Date of Birth of Driver: {dr.dateOfBirth.ToShortDateString()}");

                    //    foreach (Claim cl in dr.Claims)
                    //    {
                    //        Console.WriteLine($"Date of Claim: {cl.DateOfClaim.ToShortDateString()}");
                    //    }
                    //}

                    //Console.WriteLine("Final Premium: " + policy.Premium.ToString("c", culture));
                    Console.WriteLine($"Final Premium: {policy.Premium} pounds");
                }
                else
                {
                    Console.WriteLine("Policy Declined");                  
                    foreach (string er in errorMessages)
                    {
                        Console.WriteLine(er);
                    }
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message + " Try again.");
            }

            Console.ReadKey();
        }        
    }
}
