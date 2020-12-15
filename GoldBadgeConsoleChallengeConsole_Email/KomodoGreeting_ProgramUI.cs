using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleChallengeConsole_Email
{
    class KomodoGreetingProgramUI
    {
        private SortedList<string, Customer> _allCustomers = new SortedList<string, Customer>();

        public void Run()
        {

        }

        private void MainMenu()
        {

        }

        // Helper method to handle user input errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Press any key to continue.");
            Console.ReadKey();
        }

        private void SeedCustomerList()
        {

        }
    }
}
