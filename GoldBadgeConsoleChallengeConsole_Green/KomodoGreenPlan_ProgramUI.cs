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
            string inputChoice = Console.ReadLine();
            bool parseChoice = int.TryParse(inputChoice, out int whatToDo);
            if (parseChoice)
            {
                switch (whatToDo)
                {
                    case 1:
                        AddNewVehicle();
                        goto MainMenu;
                    case 2:
                        ViewAllVehicles();
                        Console.ReadKey();
                        goto MainMenu;
                    case 3:
                        ViewVehiclesByType();
                        Console.ReadKey();
                        goto MainMenu;
                    case 4:
                        EditExistingVehicle();
                        goto MainMenu;
                    case 5:
                        DeleteExistingVehicle();
                        goto MainMenu;
                    case 6:
                        Console.WriteLine("Press any key to exit or press H to return home. . .");
                        if(Console.ReadKey().Key == ConsoleKey.H)
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

        // Helper method to handle user input errors
        private void PressAnyKey()
        {
            Console.WriteLine("Invalid input. Press any key to continue.");
            Console.ReadKey();
        }

        private void AddNewVehicle()
        {

        }

        private void ViewAllVehicles()
        {

        }

        private void ViewVehiclesByType()
        {

        }

        private void EditExistingVehicle()
        {

        }

        private void DeleteExistingVehicle()
        {

        }

        private void SeedVehicleInventory()
        {
            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Gas, BodyType.Convertible, EnginePlacement.Front, Drivetrain.RWD, 2021, "Chevrolet", "Corvette", "Stingray 2LT", 490, 15, 27, 0, 74000, new List<Option> { Option.Leather_Seats, Option.Navigation, Option.Premium_Sound }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Electric, BodyType.SUV, EnginePlacement.Front, Drivetrain.AWD, 2020, "Tesla", "Model X", "Performance", 778, 90, 90, 0, 100000, new List<Option> { Option.Remote_Start, Option.Sunroof }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Hybrid, BodyType.Sedan, EnginePlacement.Front, Drivetrain.FWD, 2018, "Honda", "Accord", "Hybrid EX-L", 212, 47, 47, 30000, 24000, new List<Option> { Option.Bluetooth, Option.Leather_Seats, Option.Seat_Heaters }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Gas, BodyType.Pickup, EnginePlacement.Front, Drivetrain.FourWD, 2016, "Toyota", "Tacoma", "TRD Sport", 278, 19, 24, 63000, 27000, new List<Option> { Option.Bluetooth, Option.Navigation }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Electric, BodyType.Hatchback, EnginePlacement.Front, Drivetrain.FWD, 2019, "Fiat", "500e", null, 111, 113, 113, 21000, 20000, null));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Hybrid, BodyType.Crossover, EnginePlacement.Front, Drivetrain.AWD, 2017, "Acura", "MDX", "SH-AWD", 321, 26, 27, 18000, 41000, new List<Option> { Option.Leather_Seats, Option.Remote_Start, Option.Seat_Heaters, Option.Steering_Wheel_Heater }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Gas, BodyType.Minivan, EnginePlacement.Front, Drivetrain.FWD, 2015, "Chrysler", "Town And Country", "Touring L", 283, 17, 25, 75000, 17000, new List<Option> { Option.Bluetooth, Option.Leather_Seats, Option.Navigation, Option.Remote_Start, Option.Seat_Heaters, Option.Steering_Wheel_Heater }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Hybrid, BodyType.Coupe, EnginePlacement.Middle, Drivetrain.AWD, 2014, "BMW", "i8", null, 357, 28, 76, 14000, 63000, new List<Option> { Option.Bluetooth, Option.Leather_Seats, Option.Navigation, Option.Remote_Start }));
        }
    }
}
