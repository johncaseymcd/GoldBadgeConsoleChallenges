using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleChallengeConsole_BBQ
{
    class KomodoBBQProgramUI
    {
        private List<Barbecue> _allBarbecues = new List<Barbecue>();
        public void Run()
        {
            SeedBBQList();
            MainMenu();
        }

        private void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Press V to view all barbecues, or press A to input sales for a new barbecue. Press X to exit.");
            string inputChoice = Console.ReadLine().ToLower();
            switch (inputChoice)
            {
                case "a":
                    AddSales();
                    MainMenu();
                    break;
                case "v":
                    ViewBBQs();
                    Console.ReadKey();
                    MainMenu();
                    break;
                case "x":
                    Console.WriteLine("Press any key to exit or press H to return home.");
                    if (Console.ReadKey().Key != ConsoleKey.H)
                    {
                        break;
                    }
                    else
                    {
                        MainMenu();
                        break;
                    }
                default:
                    PressAnyKey();
                    MainMenu();
                    break;
            }
        }

        // Helper method to handle user input errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Please try again.");
            Console.ReadKey();
        }

        private void AddSales()
        {
        EnterDate:
            Console.Clear();
            Console.WriteLine("Enter the date of the barbecue in mm/dd/yyyy format:");
            string inputDate = Console.ReadLine();
            bool parseDate = DateTime.TryParseExact(inputDate, "MM/dd/yyyy", CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out DateTime date);
            if (!parseDate)
            {
                PressAnyKey();
                goto EnterDate;
            }

        Hamburgers:
            Console.Clear();
            Console.WriteLine("How many hamburgers were sold?");
            string inputHB = Console.ReadLine();
            bool parseHB = int.TryParse(inputHB, out int hbSold);
            if (!parseHB)
            {
                PressAnyKey();
                goto Hamburgers;
            }

        VeggieBurgers:
            Console.Clear();
            Console.WriteLine("How many veggie burgers were sold?");
            string inputVB = Console.ReadLine();
            bool parseVB = int.TryParse(inputVB, out int vbSold);
            if (!parseVB)
            {
                PressAnyKey();
                goto VeggieBurgers;
            }

        Hotdogs:
            Console.Clear();
            Console.WriteLine("How many hot dogs were sold?");
            string inputHD = Console.ReadLine();
            bool parseHD = int.TryParse(inputHD, out int hdSold);
            if (!parseHD)
            {
                PressAnyKey();
                goto Hotdogs;
            }

        IceCreamScoops:
            Console.Clear();
            Console.WriteLine("How many ice cream scoops were sold?");
            string inputIC = Console.ReadLine();
            bool parseIC = int.TryParse(inputIC, out int icSold);
            if (!parseIC)
            {
                PressAnyKey();
                goto IceCreamScoops;
            }

        PopcornBags:
            Console.Clear();
            Console.WriteLine("How many bags of popcorn were sold?");
            string inputPC = Console.ReadLine();
            bool parsePC = int.TryParse(inputPC, out int pcSold);
            if (!parsePC)
            {
                PressAnyKey();
                goto PopcornBags;
            }

            var bbq = new Barbecue(date, hbSold, vbSold, hdSold, icSold, pcSold);
            _allBarbecues.Add(bbq);
        }

        private void ViewBBQs()
        {
            Console.Clear();
            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7} \t{8} \t{9} \t{10} \t{11}\n",
                "Date".PadRight(10),
                "Hamburgers".PadRight(10),
                "Veg Burgers".PadRight(10),
                "Hot Dogs".PadRight(10),
                "Ice Cream".PadRight(10),
                "Popcorn".PadRight(10),
                "Total Meal Tickets".PadRight(18),
                "Total Meal Cost".PadRight(18),
                "Total Treat Tickets".PadRight(19),
                "Total Treat Cost".PadRight(16),
                "Overall Tickets".PadRight(15),
                "Overall Cost");
            foreach(var bbq in _allBarbecues)
            {
                Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7} \t{8} \t{9} \t{10} \t{11}\n",
                    bbq.Date.ToShortDateString(),
                    bbq.HBSold.ToString().PadRight(10),
                    bbq.VBSold.ToString().PadRight(10),
                    bbq.HDSold.ToString().PadRight(10),
                    bbq.ICSold.ToString().PadRight(10),
                    bbq.PCSold.ToString().PadRight(10),
                    bbq.BurgerTicketsTaken.ToString().PadRight(18),
                    string.Format(CultureInfo.CurrentUICulture, "{0:C}", bbq.TotalBurgerCost).PadRight(18),
                    bbq.IceCreamTicketsTaken.ToString().PadRight(19),
                    string.Format(CultureInfo.CurrentUICulture, "{0:C}", bbq.TotalIceCreamCost).PadRight(16),
                    bbq.TotalTicketsTaken.ToString().PadRight(15),
                    string.Format(CultureInfo.CurrentUICulture, "{0:C}", bbq.TotalOverallCost));
            }
        }

        private void SeedBBQList()
        {
            _allBarbecues.Add(new Barbecue(new DateTime(2020, 05, 25), 25, 12, 16, 33, 23));
            _allBarbecues.Add(new Barbecue(new DateTime(2019, 07, 04), 40, 21, 28, 53, 60));
            _allBarbecues.Add(new Barbecue(new DateTime(2018, 09, 01), 34, 25, 30, 44, 26));
        }
    }
}
