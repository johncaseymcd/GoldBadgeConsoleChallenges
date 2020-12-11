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
                "3. Update a claim in the work queue\n" +
                "4. Enter the work queue\n" +
                "5. Exit");
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        ViewAllClaims();
                        Console.WriteLine("\nEnter the claim ID you wish to edit:");
                        string inputEditClaim = Console.ReadLine();
                        bool parseEditClaim = int.TryParse(inputEditClaim, out int editClaim);
                        if (parseEditClaim)
                        {
                            EditExistingClaim(editClaim);
                            goto MainMenu;
                        }
                        else
                        {
                            goto MainMenu;
                        }
                    case 4:
                        Console.Clear();
                        WorkClaims();
                        goto MainMenu;
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
        SetClaimID:
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

            // Automatically set IsApproved to false, must be manually set to true by user
            newClaim.IsApproved = false;

            claimManipulator.CreateClaim(newClaim);
            return newClaim;
        }

        // View all claims
        private void ViewAllClaims()
        {
            _claimQueue = claimManipulator.GetAllClaims();
            Console.Clear();
            foreach(var claim in _claimQueue)
            {
                var claimWindow = claim.ClaimDate - claim.LossDate;
                Console.WriteLine($"{claim.ClaimID}. {claim.ClaimType} claim:\t {claim.ClaimDescription}\t\n" +
                    $"Loss occurred on {claim.LossDate.ToShortDateString()} and was reported {claimWindow.Days} days later on {claim.ClaimDate.ToShortDateString()}.\t\n" +
                    $"Loss amount is {string.Format(new CultureInfo("en-us", true), "{0:C}", claim.ClaimAmount)}\t\n");
            }
        }

        // Edit a claim
        private void EditExistingClaim(int claimID)
        {
            Console.Clear();
            var claimToEdit = claimManipulator.FindClaimByID(claimID);
            if (claimToEdit != null)
            {
                var newClaim = CreateNewClaim();
                bool wasEdited = claimManipulator.EditClaim(claimID, newClaim);
                if (wasEdited)
                {
                    claimManipulator.DeleteClaim(newClaim.ClaimID);
                    Console.WriteLine("Claim successfully updated.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Could not update claim. Please try again.");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Claim not found with that ID. Please try again.");
                Console.ReadKey();
            }
        }

        // Delete a claim
        private void WorkClaims()
        {
            _claimQueue = claimManipulator.GetAllClaims();
        WorkNextClaim:
            Console.Clear();
            bool wasDeleted = false;
            var claim = _claimQueue.Peek();
        SkippedClaim:
            while(claim != null)
            {
                var claimWindow = claim.ClaimDate - claim.LossDate;
                Console.WriteLine($"{claim.ClaimID}. {claim.ClaimType} claim:\t {claim.ClaimDescription}\t\n" +
                    $"Loss occurred on {claim.LossDate.ToShortDateString()} and was reported {claimWindow.Days} days later on {claim.ClaimDate.ToShortDateString()}.\t\n" +
                    $"Loss amount is {string.Format(new CultureInfo("en-us", true), "{0:C}", claim.ClaimAmount)}\t\n");
                Console.WriteLine("Would you like to approve or deny this claim? Press X to skip to the next claim.");
                string inputToProcess = Console.ReadLine().ToLower();
                if (inputToProcess == "approve")
                {
                    if (!claim.IsValid)
                    {
                        Console.WriteLine("Loss reporting date is outside the valid window. Claim will be sent to manager for review.");
                        Console.ReadKey();
                        goto WorkNextClaim;
                    }
                    wasDeleted = claimManipulator.DeleteClaim(claim.ClaimID);
                    if (wasDeleted)
                    {
                        _claimQueue.Dequeue();
                        Console.WriteLine("Claim successfully approved.");
                        goto WorkNextClaim;
                    }
                    else
                    {
                        Console.WriteLine("Claim could not be processed. Please try again.");
                        Console.ReadKey();
                        goto WorkNextClaim;
                    }
                }
                else if (inputToProcess == "deny")
                {
                    wasDeleted = claimManipulator.DeleteClaim(claim.ClaimID);
                    if (wasDeleted)
                    {
                        _claimQueue.Dequeue();
                        Console.WriteLine("Claim successfully denied.");
                        Console.ReadKey();
                        goto WorkNextClaim;
                    }
                    else
                    {
                        Console.WriteLine("Claim could not be processed. Please try again.");
                        Console.ReadKey();
                        goto WorkNextClaim;
                    }
                }
                else if (inputToProcess == "x")
                {
                    goto SkippedClaim;
                }
                else
                {
                    PressAnyKey();
                }
            }
            
        }
        
        // Pre-populate claims in queue
        private void SeedClaimQueue()
        {
            var keriSmithLossDate = new DateTime(2019, 12, 26);
            var keriSmithClaimDate = new DateTime(2020, 1, 6);
            var keriSmithClaimValidity = keriSmithClaimDate <= keriSmithLossDate + thirtyDays && keriSmithClaimDate >= keriSmithLossDate;
            var keriSmithClaim = new Claim(1, ClaimType.Home, "Electrical fire due to faulty wiring.", 243273.87m, keriSmithLossDate, keriSmithClaimDate, keriSmithClaimValidity, false);
            claimManipulator.CreateClaim(keriSmithClaim);

            var jazColemanLossDate = new DateTime(2020, 06, 04);
            var jazColemanClaimDate = new DateTime(2020, 07, 04);
            var jazColemanClaimValidity = jazColemanClaimDate <= jazColemanClaimDate + thirtyDays && jazColemanClaimDate >= jazColemanLossDate;
            var jazColemanClaim = new Claim(2, ClaimType.Theft, "Stolen musical equipment (a guitar, several synthesizers, several microphones, and a violin).", 8753.29m, jazColemanLossDate, jazColemanClaimDate, jazColemanClaimValidity, false);
            claimManipulator.CreateClaim(jazColemanClaim);

            var markDanielewskiLossDate = new DateTime(2020, 05, 15);
            var markDanielewskiClaimDate = new DateTime(2020, 05, 14);
            var markDanielewskiClaimValidity = markDanielewskiClaimDate <= markDanielewskiLossDate + thirtyDays && markDanielewskiClaimDate >= markDanielewskiLossDate;
            var markDanielewskiClaim = new Claim(3, ClaimType.Home, "Basement destroyed by minotaur. House must be rebuilt due to carpentry error.", 550000.00m, markDanielewskiLossDate, markDanielewskiClaimDate, markDanielewskiClaimValidity, false);
            claimManipulator.CreateClaim(markDanielewskiClaim);

            var stephenKingLossDate = new DateTime(2020, 03, 07);
            var stephenKingClaimDate = new DateTime(2020, 03, 10);
            var stephenKingClaimValidity = stephenKingClaimDate <= stephenKingLossDate + thirtyDays && stephenKingClaimDate >= stephenKingLossDate;
            var stephenKingClaim = new Claim(4, ClaimType.Vehicle, "1958 Plymouth Fury possessed by demon. Total loss.", 250.00m, stephenKingLossDate, stephenKingClaimDate, stephenKingClaimValidity, false);
            claimManipulator.CreateClaim(stephenKingClaim);

            var tomRobbinsLossDate = new DateTime(2020, 10, 10);
            var tomRobbinsClaimDate = new DateTime(2020, 10, 31);
            var tomRobbinsClaimValidity = tomRobbinsClaimDate <= tomRobbinsLossDate + thirtyDays && tomRobbinsClaimDate >= tomRobbinsLossDate;
            var tomRobbinsClaim = new Claim(5, ClaimType.Theft, "Stolen bottle of perfume.", 1000000.00m, tomRobbinsLossDate, tomRobbinsClaimDate, tomRobbinsClaimValidity, false);
            claimManipulator.CreateClaim(tomRobbinsClaim);
        }
    }
}
