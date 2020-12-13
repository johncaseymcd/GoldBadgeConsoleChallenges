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
        MainMenu:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome! What would you like to do?\n\n" +
                "1. Add a new claim to the work queue\n" +
                "2. View all claims in the work queue\n" +
                "3. Enter the work queue\n" +
                "4. Exit");
            string inputChoice = Console.ReadLine();
            bool parseChoice = int.TryParse(inputChoice, out int whatToDo);
            if (parseChoice)
            {
                switch (whatToDo)
                {
                    case 1:
                        var newClaim = CreateNewClaim();
                        goto MainMenu;
                    case 2:
                        ViewAllClaims();
                        Console.ReadKey();
                        goto MainMenu;
                    case 3:
                        Console.Clear();
                        WorkClaims();
                        goto MainMenu;
                    case 4:
                        Console.WriteLine("Press any key to exit.");
                        Console.ReadKey();
                        Environment.Exit(0);
                        break;
                    default:
                        PressAnyKey();
                        goto MainMenu;
                }
            }
            else
            {
                PressAnyKey();
                goto MainMenu;
            }
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

            // Set Claim ID
            newClaim.ClaimID = _claimQueue.Count + 1;

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
                        newClaim.ClaimType = ClaimType.Vehicle;
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
            bool isValid = claimDate <= lossDate + thirtyDays && claimDate >= lossDate;
            newClaim.IsValid = isValid;

            claimManipulator.CreateClaim(newClaim);
            return newClaim;
        }

        // Helper method to display claim information
        private void ViewClaimByID(int claimID)
        {
            _claimQueue = claimManipulator.GetAllClaims();
            foreach(var claim in _claimQueue)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (claimID == claim.ClaimID)
                {
                    var claimWindow = claim.ClaimDate - claim.LossDate;
                    Console.WriteLine($"{claim.ClaimID}. {claim.ClaimType} claim: {claim.ClaimDescription}\n" +
                        $"Loss occurred on {claim.LossDate.ToShortDateString()} and was reported {claimWindow.Days} days later on {claim.ClaimDate.ToShortDateString()}.\n" +
                        $"Loss amount is {string.Format(new CultureInfo("en-us", true), "{0:C}", claim.ClaimAmount)}");
                    if (claim.IsValid)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("This claim is valid.\n");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine("This claim is invalid and may require further investigation.\n");
                    }
                }
            }
        }

        // View all claims
        private void ViewAllClaims()
        {
            Console.Clear();
            _claimQueue = claimManipulator.GetAllClaims();
            foreach(var claim in _claimQueue)
            {
                ViewClaimByID(claim.ClaimID);
            }
        }

        // Delete a claim
        private void WorkClaims()
        {
        ProcessClaim:
            Console.Clear();
            _claimQueue = claimManipulator.GetAllClaims();
            int claimCount = _claimQueue.Count;
            if (claimCount > 0)
            {
                var nextClaim = _claimQueue.Peek();
                ViewClaimByID(nextClaim.ClaimID);
                Console.WriteLine("Do you want to process this claim now (y/n)?");
                string toProcess = Console.ReadLine().ToLower();
                if (toProcess == "y")
                {
                    if (nextClaim.IsValid)
                    {
                        _claimQueue.Dequeue();
                        Console.WriteLine("Claim successfully approved.");
                        Console.ReadKey();
                        goto ProcessClaim;
                    }
                    else
                    {
                        Console.WriteLine("Claim is invalid and will be denied. Are you sure you want to process this claim now (y/n)?");
                        string sureToProcess = Console.ReadLine().ToLower();
                        if (sureToProcess == "y")
                        {
                            _claimQueue.Dequeue();
                            Console.WriteLine("Claim successfully denied.");
                            Console.ReadKey();
                            goto ProcessClaim;
                        }
                        else if (sureToProcess == "n")
                        {
                            MainMenu();
                        }
                        else
                        {
                            PressAnyKey();
                            MainMenu();
                        }
                    }
                }
                else if (toProcess == "n")
                {
                    MainMenu();
                }
                else
                {
                    PressAnyKey();
                    MainMenu();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("You're all caught up! No claims currently in the work queue.");
                Console.ReadKey();
                MainMenu();
            }
            
        }
        
        // Pre-populate claims in queue
        private void SeedClaimQueue()
        {
            var keriSmithLossDate = new DateTime(2019, 12, 26);
            var keriSmithClaimDate = new DateTime(2020, 1, 6);
            var keriSmithClaimValidity = keriSmithClaimDate <= keriSmithLossDate + thirtyDays && keriSmithClaimDate >= keriSmithLossDate;
            var keriSmithClaim = new Claim(1, ClaimType.Home, "Electrical fire due to faulty wiring.", 243273.87m, keriSmithLossDate, keriSmithClaimDate, keriSmithClaimValidity);
            claimManipulator.CreateClaim(keriSmithClaim);

            var jazColemanLossDate = new DateTime(2020, 06, 04);
            var jazColemanClaimDate = new DateTime(2020, 07, 04);
            var jazColemanClaimValidity = jazColemanClaimDate <= jazColemanClaimDate + thirtyDays && jazColemanClaimDate >= jazColemanLossDate;
            var jazColemanClaim = new Claim(2, ClaimType.Theft, "Stolen musical equipment (a guitar, several synthesizers, several microphones, and a violin).", 8753.29m, jazColemanLossDate, jazColemanClaimDate, jazColemanClaimValidity);
            claimManipulator.CreateClaim(jazColemanClaim);

            var markDanielewskiLossDate = new DateTime(2020, 05, 15);
            var markDanielewskiClaimDate = new DateTime(2020, 05, 14);
            var markDanielewskiClaimValidity = markDanielewskiClaimDate <= markDanielewskiLossDate + thirtyDays && markDanielewskiClaimDate >= markDanielewskiLossDate;
            var markDanielewskiClaim = new Claim(3, ClaimType.Home, "Basement destroyed by minotaur. House must be rebuilt due to carpentry error.", 550000.00m, markDanielewskiLossDate, markDanielewskiClaimDate, markDanielewskiClaimValidity);
            claimManipulator.CreateClaim(markDanielewskiClaim);

            var stephenKingLossDate = new DateTime(2020, 03, 07);
            var stephenKingClaimDate = new DateTime(2020, 03, 10);
            var stephenKingClaimValidity = stephenKingClaimDate <= stephenKingLossDate + thirtyDays && stephenKingClaimDate >= stephenKingLossDate;
            var stephenKingClaim = new Claim(4, ClaimType.Vehicle, "1958 Plymouth Fury possessed by demon. Total loss.", 250.00m, stephenKingLossDate, stephenKingClaimDate, stephenKingClaimValidity);
            claimManipulator.CreateClaim(stephenKingClaim);

            var tomRobbinsLossDate = new DateTime(2020, 10, 10);
            var tomRobbinsClaimDate = new DateTime(2020, 10, 31);
            var tomRobbinsClaimValidity = tomRobbinsClaimDate <= tomRobbinsLossDate + thirtyDays && tomRobbinsClaimDate >= tomRobbinsLossDate;
            var tomRobbinsClaim = new Claim(5, ClaimType.Theft, "Stolen bottle of perfume.", 1000000.00m, tomRobbinsLossDate, tomRobbinsClaimDate, tomRobbinsClaimValidity);
            claimManipulator.CreateClaim(tomRobbinsClaim);
        }
    }
}
