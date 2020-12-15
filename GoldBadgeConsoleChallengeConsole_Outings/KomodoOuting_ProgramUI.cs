using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleChallengeConsole_Outings
{
    public class ProgramUI
    {
        private OutingCRUD outingManipulator = new OutingCRUD();
        private SortedList<DateTime, Outing> _allOutings = new SortedList<DateTime, Outing>();

        public void Run()
        {
            SeedOutingsList();
            MainMenu();
        }

        private void MainMenu()
        {
        MainMenu:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome! What would you like to do?\n\n" +
                "1. Add a new outing to the list\n" +
                "2. View all company outings\n" +
                "3. View cost for all outings by year\n" +
                "4. View cost for all outings by type\n" +
                "5. Exit");
            string inputChoice = Console.ReadLine();
            bool parseChoice = int.TryParse(inputChoice, out int whatToDo);
            if (parseChoice)
            {
                switch (whatToDo)
                {
                    case 1:
                        AddNewOuting();
                        goto MainMenu;
                    case 2:
                        ViewOutingsByDate();
                        Console.ReadKey();
                        goto MainMenu;
                    case 3:
                        ViewCostByYear();
                        Console.ReadKey();
                        goto MainMenu;
                    case 4:
                        ViewCostByType();
                        Console.ReadKey();
                        goto MainMenu;
                    case 5:
                        Console.WriteLine("Press any key to exit or press H to return home.");
                        if (Console.ReadKey().Key == ConsoleKey.H)
                        {
                            goto MainMenu;
                        }
                        else
                        {
                            Environment.Exit(0);
                            break;
                        }
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

        // Helper method to handle input errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Press any key to continue.");
            Console.ReadKey();
        }

        private void AddNewOuting()
        {
        EnterDate:
            Console.Clear();
            Console.WriteLine("Enter the date of the outing in mm/dd/yyyy format:");
            string inputOutingDate = Console.ReadLine();
            bool parseOutingDate = DateTime.TryParseExact(inputOutingDate, "MM/dd/yyyy", CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out DateTime outingDate);
            if (!parseOutingDate)
            {
                PressAnyKey();
                goto EnterDate;
            }

        EnterType:
            Console.Clear();
            int typeCounter = 0;
            var outingType = new OutingType();
            Console.WriteLine("What type of outing is this?\n");
            foreach(var type in Enum.GetValues(typeof(OutingType)))
            {
                typeCounter++;
                Console.WriteLine($"{typeCounter}. {type.ToString().Replace('_', ' ')}");
            }
            string inputType = Console.ReadLine();
            bool parseType = int.TryParse(inputType, out int whichType);
            if (parseType)
            {
                switch (whichType)
                {
                    case 1:
                        outingType = OutingType.Amusement_Park;
                        break;
                    case 2:
                        outingType = OutingType.Bowling;
                        break;
                    case 3:
                        outingType = OutingType.Concert;
                        break;
                    case 4:
                        outingType = OutingType.Golf;
                        break;
                    default:
                        PressAnyKey();
                        goto EnterType;
                }
            }
            else
            {
                PressAnyKey();
                goto EnterType;
            }

        EnterAttendance:
            Console.Clear();
            Console.WriteLine("How many attendees were at this outing?");
            string inputAttendance = Console.ReadLine();
            bool parseAttendance = int.TryParse(inputAttendance, out int outingAttendance);
            if (!parseAttendance)
            {
                PressAnyKey();
                goto EnterAttendance;
            }

            outingManipulator.CreateNewOuting(new Outing(outingDate, outingType, outingAttendance));
        }

        private void ViewOutingsByDate()
        {
            _allOutings = outingManipulator.GetAllOutings();
            Console.Clear();
            decimal totalCostForAll = 0.00m;
            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}\n", "Date".PadRight(15), "Type".PadRight(15), "Cost per Person".PadRight(15), "Attendance".PadRight(15), "Total Cost".PadRight(15));
            foreach(var outing in _allOutings)
            {
                totalCostForAll += outing.Value.TotalCost;
                Console.WriteLine($"{outing.Key.ToShortDateString().PadRight(15)} \t{outing.Value.Type.ToString().Replace('_', ' ').PadRight(15)} \t{string.Format("{0:C}", outing.Value.CostPerPerson).PadRight(15)} \t{outing.Value.Attendance.ToString().PadRight(15)} \t{string.Format("{0:C}", outing.Value.TotalCost).PadRight(15)}");
            }
            decimal budget = totalCostForAll / _allOutings.Count;
            if (budget < 4000)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine($"\n" +
                $"The total cost of all outings is: {string.Format("{0:C}", totalCostForAll)}");
        }

        private void ViewCostByYear()
        {
            _allOutings = outingManipulator.GetAllOutings();
        EnterYear:
            Console.Clear();
            Console.WriteLine("Which year would you like to view?");
            string inputYear = Console.ReadLine();
            bool parseYear = int.TryParse(inputYear, out int year);
            Console.Clear();
            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}\n", "Date".PadRight(15), "Type".PadRight(15), "Cost per Person".PadRight(15), "Attendance".PadRight(15), "Total Cost".PadRight(15));
            decimal totalCostForYear = 0.00m;
            int outingCounter = 0;
            foreach (var outing in _allOutings)
            {
                if (year == outing.Key.Year)
                {
                    totalCostForYear += outing.Value.TotalCost;
                    outingCounter++;
                    Console.WriteLine($"{outing.Key.Month.ToString()}/{outing.Key.Day.ToString().PadRight(15 - outing.Key.Month.ToString().Length + 1)} \t{outing.Value.Type.ToString().Replace('_', ' ').PadRight(15)} \t{string.Format("{0:C}", outing.Value.CostPerPerson).PadRight(15)} \t{outing.Value.Attendance.ToString().PadRight(15)} \t{string.Format("{0:C}", outing.Value.TotalCost).PadRight(15)}");
                }
            }
            decimal budget = 0.00m;
            if (outingCounter != 0)
            {
                budget = totalCostForYear / outingCounter;
            }
            else
            {
                Console.Clear();
                PressAnyKey();
                goto EnterYear;
            }
            if (budget < 4000)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine(" \n" +
                "Total cost for {0}: {1}", year, string.Format("{0:C}", totalCostForYear));

        }

        private void ViewCostByType()
        {
            _allOutings = outingManipulator.GetAllOutings();
        EnterType:
            Console.Clear();
            Console.WriteLine("Which type of outing would you like to view?\n");
            int typeCounter = 0;
            foreach(var type in Enum.GetValues(typeof(OutingType)))
            {
                typeCounter++;
                Console.WriteLine($"{typeCounter}. {type.ToString().Replace('_', ' ')}");
            }
            string inputType = Console.ReadLine();
            bool parseType = int.TryParse(inputType, out int whichType);
            var outingType = new OutingType();
            if (parseType)
            {
                switch (whichType)
                {
                    case 1:
                        outingType = OutingType.Amusement_Park;
                        break;
                    case 2:
                        outingType = OutingType.Bowling;
                        break;
                    case 3:
                        outingType = OutingType.Concert;
                        break;
                    case 4:
                        outingType = OutingType.Golf;
                        break;
                    default:
                        PressAnyKey();
                        goto EnterType;
                }
            }
            else
            {
                PressAnyKey();
                goto EnterType;
            }
            Console.Clear();
            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}\n", "Date".PadRight(15), "Type".PadRight(15), "Cost per Person".PadRight(15), "Attendance".PadRight(15), "Total Cost".PadRight(15));
            decimal totalCostForType = 0.00m;
            int outingCounter = 0;
            foreach(var outing in _allOutings)
            {
                if (outing.Value.Type == outingType)
                {
                    totalCostForType += outing.Value.TotalCost;
                    outingCounter++;
                    Console.WriteLine($"{outing.Key.ToShortDateString().PadRight(15)} \t{outing.Value.Type.ToString().Replace('_', ' ').PadRight(15)} \t{string.Format("{0:C}", outing.Value.CostPerPerson).PadRight(15)} \t{outing.Value.Attendance.ToString().PadRight(15)} \t{string.Format("{0:C}", outing.Value.TotalCost).PadRight(15)}");
                }
            }
            decimal budgetForType = totalCostForType / outingCounter;
            if (budgetForType < 4000)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine($"\n" +
                $"The total cost for all {outingType.ToString().Replace('_', ' ')} outings is: {string.Format("{0:C}", totalCostForType)}");
        }

        private void SeedOutingsList()
        {
            var newYearsOuting = new Outing(new DateTime(2020, 01, 01, 00, 00, 00), OutingType.Bowling, 53);
            outingManipulator.CreateNewOuting(newYearsOuting);

            var valentinesOuting = new Outing(new DateTime(2019, 02, 14, 19, 00, 00), OutingType.Concert, 47);
            outingManipulator.CreateNewOuting(valentinesOuting);

            var springOuting = new Outing(new DateTime(2019, 03, 15, 18, 00, 00), OutingType.Golf, 41);
            outingManipulator.CreateNewOuting(springOuting);

            var taxTimeOuting = new Outing(new DateTime(2018, 04, 15, 11, 00, 00), OutingType.Golf, 22);
            outingManipulator.CreateNewOuting(taxTimeOuting);

            var mothersDayOuting = new Outing(new DateTime(2019, 05, 10, 20, 00, 00), OutingType.Concert, 34);
            outingManipulator.CreateNewOuting(mothersDayOuting);

            var fathersDayOuting = new Outing(new DateTime(2018, 06, 17, 18, 00, 00), OutingType.Bowling, 46);
            outingManipulator.CreateNewOuting(fathersDayOuting);

            var fourthOfJulyOuting = new Outing(new DateTime(2019, 07, 04, 10, 00, 00), OutingType.Amusement_Park, 67);
            outingManipulator.CreateNewOuting(fourthOfJulyOuting);

            var summerOuting = new Outing(new DateTime(2018, 08, 11, 15, 00, 00), OutingType.Concert, 28);
            outingManipulator.CreateNewOuting(summerOuting);

            var backToSchoolOuting = new Outing(new DateTime(2018, 09, 01, 12, 00, 00), OutingType.Golf, 52);
            outingManipulator.CreateNewOuting(backToSchoolOuting);

            var halloweenOuting = new Outing(new DateTime(2019, 10, 31, 20, 00, 00), OutingType.Amusement_Park, 66);
            outingManipulator.CreateNewOuting(halloweenOuting);

            var thanksgivingOuting = new Outing(new DateTime(2020, 11, 26, 13, 00, 00), OutingType.Bowling, 39);
            outingManipulator.CreateNewOuting(thanksgivingOuting);

            var christmasOuting = new Outing(new DateTime(2018, 12, 25, 14, 00, 00), OutingType.Concert, 34);
            outingManipulator.CreateNewOuting(christmasOuting);
        }
    }
}
