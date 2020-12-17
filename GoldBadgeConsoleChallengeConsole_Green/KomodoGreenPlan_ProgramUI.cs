using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleChallengeConsole_Green
{
    class KomodoGreenPlanProgramUI
    {
        private List<Vehicle> _allVehicles = new List<Vehicle>();
        private VehicleCRUD vehicleManipulator = new VehicleCRUD();

        public void Run()
        {
            Console.SetWindowSize(160, 35);
            SeedVehicleInventory();
            MainMenu();
        }

        private void MainMenu()
        {
        MainMenu:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome! What would you like to do?\n\n" +
                "1. Add a new vehicle\n" +
                "2. View all vehicles\n" +
                "3. View vehicles by fuel type\n" +
                "4. Edit a vehicle entry\n" +
                "5. Delete a vehicle entry\n" +
                "6. Exit");
        }

        private void SeedVehicleInventory()
        {

        }
    }
}
