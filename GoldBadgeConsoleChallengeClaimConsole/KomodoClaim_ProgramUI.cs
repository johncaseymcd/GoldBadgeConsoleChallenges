using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleChallengeClaimConsole
{
    class KomodoClaimProgramUI
    {
        // Establish constant to check claim validity
        TimeSpan thirtyDays = new TimeSpan(30, 0, 0, 0);
        
        // Field of claims
        private Queue<Claim> _claimQueue = new Queue<Claim>();

        // Claim object to access CRUD methods
        ClaimCRUD claimManipulator = new ClaimCRUD();

        public void Run()
        {
            SeedClaimQueue();
            MainMenu();
        }

        // Main program menu
        private void MainMenu()
        {
            CreateNewClaim();
        }

        // Helper method to handle errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.ReadKey();
        }

        // Create a new claim
        private Claim CreateNewClaim()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            var newClaim = new Claim();

        // Enter Claim ID
        SetClaimID:
            Console.WriteLine("Enter the claim ID:\n");
            string inputID = Console.ReadLine();
            bool parseID = int.TryParse(inputID, out int newClaimID);
            if (parseID)
            {
                newClaim.ClaimID = newClaimID;
            }
            else
            {
                PressAnyKey();
                goto SetClaimID;
            }

        // Select claim type
        SetClaimType:
            Console.Clear();
            Console.WriteLine("What type of claim is this?\n");
            int typeCounter = 0;
            foreach(var type in Enum.GetNames(typeof(ClaimType)))
            {
                typeCounter++;
                Console.WriteLine($"{typeCounter}. {Enum.GetName(typeof(ClaimType), typeCounter)}");
            }
            string inputType = Console.ReadLine();
            bool parseType = int.TryParse(inputType, out int whichType);
            if (parseType)
            {
                switch (whichType)
                {
                    case 1:
                        newClaim.ClaimType = ClaimType.Car;
                        goto SetClaimDescription;
                    case 2:
                        newClaim.ClaimType = ClaimType.Home;
                        goto SetClaimDescription;
                    case 3:
                        newClaim.ClaimType = ClaimType.Theft;
                        goto SetClaimDescription;
                    default:
                        PressAnyKey();
                        goto SetClaimType;
                }
            }
            else
            {
                PressAnyKey();
                goto SetClaimType;
            }

        // Enter claim description
        SetClaimDescription:
            Console.Clear();
            Console.WriteLine("Briefly describe what happened:\n");
            newClaim.ClaimDescription = Console.ReadLine();

        // Enter claim amount
        SetClaimAmount:
            Console.Clear();
            Console.WriteLine("Enter the amount of the claim:\n");
            string inputAmount = Console.ReadLine();
            bool parseAmount = decimal.TryParse(inputAmount, out decimal claimAmount);
            if (parseAmount)
            {
                newClaim.ClaimAmount = decimal.Round(claimAmount, 2);
            }
            else
            {
                PressAnyKey();
                goto SetClaimAmount;
            }

        // Enter loss date
        SetLossDate:
            Console.Clear();
            Console.WriteLine("Enter the date of the loss (mm/dd/yyyy format)");
            string inputFullLossDate = Console.ReadLine();
            bool parseLossDate = DateTime.TryParseExact(inputFullLossDate, "MM/dd/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.AssumeLocal, out DateTime lossDate);
            if (parseLossDate)
            {
                newClaim.LossDate = lossDate;
            }
            else
            {
                PressAnyKey();
                goto SetLossDate;
            }

        // Enter claim date
        SetClaimDate:
            Console.Clear();
            Console.WriteLine("Enter the date the claim was reported (mm/dd/yyyy format)");
            string inputFullClaimDate = Console.ReadLine();
            bool parseClaimDate = DateTime.TryParseExact(inputFullClaimDate, "MM/dd/yyyy", CultureInfo.InstalledUICulture, DateTimeStyles.AssumeLocal, out DateTime claimDate);
            if (parseClaimDate)
            {
                newClaim.ClaimDate = claimDate;
            }
            else
            {
                PressAnyKey();
                goto SetClaimDate;
            }

            // Check claim date vs. loss date to verify if claim is valid
            bool isValid = claimDate <= lossDate + thirtyDays;
            newClaim.IsValid = isValid;

            // Automatically set IsApproved to false, must be manually set to True by user
            newClaim.IsApproved = false;

            claimManipulator.CreateClaim(newClaim);
            return newClaim;
        }

        // View all claims
        private void ViewAllClaims()
        {

        }

        // Edit a claim
        private void EditExistingClaim()
        {

        }

        // Delete a claim
        private void DeleteExistingClaim()
        {

        }
        
        // Pre-populate claims in queue
        private void SeedClaimQueue()
        {

        }
    }
}
