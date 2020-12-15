using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleChallengeConsole_Outings
{
    class ProgramUI
    {
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

        }

        private void ViewOutingsByDate()
        {

        }

        private void ViewCostByYear()
        {

        }

        private void ViewCostByType()
        {

        }

        private void SeedOutingsList()
        {

        }
    }
}
