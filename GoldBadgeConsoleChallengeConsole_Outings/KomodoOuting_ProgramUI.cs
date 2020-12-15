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
                "1. View all outings by date\n" +
                "2. Add a new outing to the list\n" +
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
                        ViewOutingsByDate();
                        Console.ReadKey();
                        goto MainMenu;
                    case 2:
                        AddNewOuting();
                        goto MainMenu;
                    case 3:
                        ViewCostByYear();
                        goto MainMenu;
                    case 4:
                        ViewCostByType();
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
            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4}\n", "Date".PadRight(15), "Type".PadRight(15), "Cost per Person".PadRight(15), "Attendance".PadRight(15), "Total Cost".PadRight(15));
            foreach(var outing in _allOutings)
            {
                Console.WriteLine($"{outing.Key.ToShortDateString().PadRight(15)} \t{outing.Value.Type.ToString().Replace('_', ' ').PadRight(15)} \t{string.Format("{0:C}", outing.Value.CostPerPerson).PadRight(15)} \t{outing.Value.Attendance.ToString().PadRight(15)} \t{string.Format("{0:C}", outing.Value.TotalCost).PadRight(15)}");
            }
        }

        private void ViewCostByYear()
        {

        }

        private void ViewCostByType()
        {

        }

        private void SeedOutingsList()
        {
            var newYearsOuting = new Outing(new DateTime(2020, 01, 01, 00, 00, 00), OutingType.Bowling, 53);
            outingManipulator.CreateNewOuting(newYearsOuting);

            var valentinesOuting = new Outing(new DateTime(2019, 02, 14, 19, 00, 00), OutingType.Concert, 37);
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
