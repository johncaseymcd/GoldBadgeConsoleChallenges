using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
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
            Console.SetWindowSize(240, 63);
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
                        Console.Clear();
                        Console.WriteLine("Press E to view all electric cars, press H to view all hybrid cars, or press G to view all gas cars:\n");
                        string inputFuelType = Console.ReadLine().ToLower();
                        switch (inputFuelType)
                        {
                            case "e":
                                ViewVehiclesByType(FuelType.Electric);
                                break;
                            case "h":
                                ViewVehiclesByType(FuelType.Hybrid);
                                break;
                            case "g":
                                ViewVehiclesByType(FuelType.Gas);
                                break;
                            default:
                                PressAnyKey();
                                goto MainMenu;
                        }
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
        SetYMM:
            Console.Clear();
            Console.WriteLine("Enter the year, make, and model of the car (ex. 2011 Honda Civic):\n");
            string inputYMM = Console.ReadLine();
            string[] separateYMM = inputYMM.Split(' ');
            int addYear;
            string addMake;
            string addModel = "";
            string addTrim;
            FuelType addFuel;
            BodyType addBody;
            EnginePlacement addEngine;
            Drivetrain addDrivetrain;
            int addHorsepower;
            int addCity;
            int addHighway;
            double addMileage;
            decimal addBasePrice;

            if (separateYMM != null && separateYMM.Length >= 3)
            {
                bool parseYear = int.TryParse(separateYMM.ElementAt<string>(0), out int vehicleYear);
                if (parseYear)
                {
                    addYear = vehicleYear;
                }
                else
                {
                    PressAnyKey();
                    goto SetYMM;
                }
                addMake = separateYMM.ElementAt<string>(1);
                for (int x = 2; x < separateYMM.Length; x++)
                {
                    addModel += separateYMM.ElementAt<string>(x) + " ";
                }
            }
            else
            {
                PressAnyKey();
                goto SetYMM;
            }

            // Set Trim
            Console.Clear();
            Console.WriteLine("Enter the vehicle's trim (if it's a base model, just press Enter to continue):\n");
            addTrim = Console.ReadLine();

        SetFuelType:
            Console.Clear();
            int fuelCounter = 0;
            Console.WriteLine("What kind of fuel system does the vehicle use?\n");
            foreach(var fuelType in Enum.GetValues(typeof(FuelType)))
            {
                fuelCounter++;
                Console.WriteLine($"{fuelCounter}. {fuelType}");
            }
            string inputFuelType = Console.ReadLine();
            bool parseFuelType = int.TryParse(inputFuelType, out int whichFuelType);
            if (parseFuelType)
            {
                switch (whichFuelType)
                {
                    case 1:
                        addFuel = FuelType.Electric;
                        break;
                    case 2:
                        addFuel = FuelType.Gas;
                        break;
                    case 3:
                        addFuel = FuelType.Hybrid;
                        break;
                    default:
                        PressAnyKey();
                        goto SetFuelType;
                }
            }
            else
            {
                PressAnyKey();
                goto SetFuelType;
            }

        SetBodyType:
            Console.Clear();
            Console.WriteLine("What body style does this vehicle have?\n");
            int bodyCounter = 0;
            foreach(var bodyType in Enum.GetValues(typeof(BodyType)))
            {
                bodyCounter++;
                Console.WriteLine($"{bodyCounter}. {bodyType}");
            }
            string inputBodyType = Console.ReadLine();
            bool parseBodyType = int.TryParse(inputBodyType, out int whichBodyType);
            if (parseBodyType)
            {
                switch (whichBodyType)
                {
                    case 1:
                        addBody = BodyType.Convertible;
                        break;
                    case 2:
                        addBody = BodyType.Coupe;
                        break;
                    case 3:
                        addBody = BodyType.Crossover;
                        break;
                    case 4:
                        addBody = BodyType.Hatchback;
                        break;
                    case 5:
                        addBody = BodyType.Minivan;
                        break;
                    case 6:
                        addBody = BodyType.Pickup;
                        break;
                    case 7:
                        addBody = BodyType.Sedan;
                        break;
                    case 8:
                        addBody = BodyType.SUV;
                        break;
                    default:
                        PressAnyKey();
                        goto SetBodyType;
                }
            }
            else
            {
                PressAnyKey();
                goto SetBodyType;
            }

        SetEnginePlacement:
            Console.Clear();
            Console.WriteLine("Enter the vehicle's engine placement (F for front, M for middle, R for rear):\n");
            string inputEnginePlacement = Console.ReadLine().ToLower();
            if (inputEnginePlacement == "f")
            {
                addEngine = EnginePlacement.Front;
            }
            else if (inputEnginePlacement == "m")
            {
                addEngine = EnginePlacement.Middle;
            }
            else if (inputEnginePlacement == "r")
            {
                addEngine = EnginePlacement.Rear;
            }
            else
            {
                PressAnyKey();
                goto SetEnginePlacement;
            }

        SetDrivetrain:
            Console.Clear();
            Console.WriteLine("Enter the vehicle's drivetrain (4WD, AWD, FWD, or RWD):\n");
            string inputDrivetrain = Console.ReadLine().ToLower();
            if (inputDrivetrain == "4wd")
            {
                addDrivetrain = Drivetrain.FourWD;
            }
            else if (inputDrivetrain == "awd")
            {
                addDrivetrain = Drivetrain.AWD;
            }
            else if(inputDrivetrain == "fwd")
            {
                addDrivetrain = Drivetrain.FWD;
            }
            else if(inputDrivetrain == "rwd")
            {
                addDrivetrain = Drivetrain.RWD;
            }
            else
            {
                PressAnyKey();
                goto SetDrivetrain;
            }

        SetHorsepower:
            Console.Clear();
            Console.WriteLine("Enter the vehicle's horsepower:\n");
            string inputHorsepower = Console.ReadLine();
            bool parseHorsepower = int.TryParse(inputHorsepower, out int horsepower);
            if (parseHorsepower)
            {
                addHorsepower = horsepower;
            }
            else
            {
                PressAnyKey();
                goto SetHorsepower;
            }

        SetMPG:
            Console.Clear();
            Console.WriteLine("Enter the vehicle's city/highway MPG (ex. 24/32):\n");
            string inputMPG = Console.ReadLine();
            string[] mpgs = inputMPG.Split('/');
            if (mpgs != null && mpgs.Length == 2)
            {
                bool parseCityMPG = int.TryParse(mpgs.ElementAt<string>(0), out int cityMPG);
                bool parseHighwayMPG = int.TryParse(mpgs.ElementAt<string>(1), out int highwayMPG);
                if (parseCityMPG && parseHighwayMPG)
                {
                    addCity = cityMPG;
                    addHighway = highwayMPG;
                }
                else
                {
                    PressAnyKey();
                    goto SetMPG;
                }
            }
            else
            {
                PressAnyKey();
                goto SetMPG;
            }

        SetMileage:
            Console.Clear();
            Console.WriteLine("Enter the vehicle's mileage:\n");
            string inputMileage = Console.ReadLine();
            bool parseMileage = double.TryParse(inputMileage, out double mileage);
            if (parseMileage)
            {
                addMileage = mileage;
            }
            else
            {
                PressAnyKey();
                goto SetMileage;
            }

        SetBasePrice:
            Console.Clear();
            Console.WriteLine("Enter the vehicle's base price:\n");
            string inputBasePrice = Console.ReadLine();
            bool parseBasePrice = decimal.TryParse(inputBasePrice, out decimal basePrice);
            if (parseBasePrice)
            {
                addBasePrice = basePrice;
            }
            else
            {
                PressAnyKey();
                goto SetBasePrice;
            }

            var optionsList = new List<Option>();
        AddOptions:
            Console.Clear();
            Console.WriteLine("Add any options (press F to finish)?\n");
            int optionCounter = 0;
            foreach(var option in Enum.GetValues(typeof(Option)))
            {
                optionCounter++;
                Console.WriteLine($"{optionCounter}. {option.ToString().Replace('_', ' ')}");
            }
            string inputOption = Console.ReadLine();
            do
            {
                bool parseOption = int.TryParse(inputOption, out int whichOption);
                if (parseOption)
                {
                    switch (whichOption)
                    {
                        case 1:
                            optionsList.Add(Option.Bluetooth);
                            goto AddOptions;
                        case 2:
                            optionsList.Add(Option.Leather_Seats);
                            goto AddOptions;
                        case 3:
                            optionsList.Add(Option.Navigation);
                            goto AddOptions;
                        case 4:
                            optionsList.Add(Option.Premium_Sound);
                            goto AddOptions;
                        case 5:
                            optionsList.Add(Option.Remote_Start);
                            goto AddOptions;
                        case 6:
                            optionsList.Add(Option.Seat_Heaters);
                            goto AddOptions;
                        case 7:
                            optionsList.Add(Option.Spoiler);
                            goto AddOptions;
                        case 8:
                            optionsList.Add(Option.Steering_Wheel_Heater);
                            goto AddOptions;
                        case 9:
                            optionsList.Add(Option.Sunroof);
                            goto AddOptions;
                        case 10:
                            optionsList.Add(Option.None);
                            goto AddOptions;
                        default:
                            PressAnyKey();
                            goto AddOptions;
                    }
                }
                else if (!parseOption && inputOption.ToLower() != "f")
                {
                    PressAnyKey();
                    goto AddOptions;
                }
            } while (inputOption.ToLower() != "f");

            var newVehicle = new Vehicle(addFuel, addBody, addEngine, addDrivetrain, addYear, addMake, addModel, addTrim, addHorsepower, addCity, addHighway, addMileage, addBasePrice, optionsList);
            vehicleManipulator.CreateVehicle(newVehicle);
        }

        // Helper method to show data header
        private void ShowHeader()
        {
            Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7} \t{8} \t{9} \t{10} \t{11} \t{12} \t{13}\n", "Year".PadRight(5), "Make".PadRight(8), "Model".PadRight(20), "Trim".PadRight(10), "Fuel Type".PadRight(8), "Body Type".PadRight(8), "Engine".PadRight(6), "Drivetrain".PadRight(10), "Horsepower".PadRight(10), "Efficiency".PadRight(10), "Mileage".PadRight(10), "Base Price".PadRight(10), "Cost + Options".PadRight(12), "Options".PadRight(22));
        }

        // Helper method to output vehicles
        private void ShowVehicle(int year, string make, string model)
        {
            _allVehicles = vehicleManipulator.GetAllVehicles();
            foreach (var vehicle in _allVehicles)
            {
                if(vehicle.Year == year && vehicle.Make == make && vehicle.Model == model)
                {
                    if (vehicle.Fuel == FuelType.Electric)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else if (vehicle.Fuel == FuelType.Hybrid)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else if (vehicle.Fuel == FuelType.Gas)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine("{0} \t{1} \t{2} \t{3} \t{4} \t{5} \t{6} \t{7} \t{8} \t{9}/{10}/{11} \t{12} \t{13} \t{14} \t{15}\n",
                        vehicle.Year.ToString().PadRight(5),
                        vehicle.Make.PadRight(8),
                        vehicle.Model.PadRight(20),
                        vehicle.Trim.PadRight(10),
                        vehicle.Fuel.ToString().PadRight(8),
                        vehicle.Body.ToString().PadRight(8),
                        vehicle.Engine.ToString().PadRight(6),
                        vehicle.Drivetrain.ToString().PadRight(10),
                        (vehicle.Horsepower.ToString() + " bhp").PadRight(10),
                        vehicle.CityMPG.ToString(),
                        vehicle.HighwayMPG.ToString(),
                        vehicle.Efficiency.ToString(),
                        vehicle.Mileage.ToString("N").PadRight(10),
                        string.Format(new CultureInfo("en-us", true), "{0:C}", vehicle.BasePrice).PadRight(10),
                        string.Format(new CultureInfo("en-us", true), "{0:C}", vehicle.TotalPrice).PadRight(12),
                        string.Format("{0}", string.Join("\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t", vehicle.Options).Replace('_', ' ')).PadRight(22));
                }
            }
        }

        private void ViewAllVehicles()
        {
            Console.Clear();
            _allVehicles = vehicleManipulator.GetAllVehicles();
            ShowHeader();
            foreach (var vehicle in _allVehicles)
            {
                ShowVehicle(vehicle.Year, vehicle.Make, vehicle.Model);
            }
        }

        private void ViewVehiclesByType(FuelType type)
        {
            Console.Clear();
            _allVehicles = vehicleManipulator.GetAllVehicles();
            ShowHeader();
            foreach(var vehicle in _allVehicles)
            {
                if (vehicle.Fuel == type)
                {
                    ShowVehicle(vehicle.Year, vehicle.Make, vehicle.Model);
                }
            }
        }

        private void EditExistingVehicle()
        {
        EnterOldVehicle:
            Console.Clear();
            ViewAllVehicles();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Enter the year, make, and model of the vehicle you wish to edit:\n");
            string inputYMMT = Console.ReadLine();
            string[] separateYMMT = inputYMMT.Split(' ');
            if (separateYMMT != null && separateYMMT.Length >= 3)
            {
                int year = int.Parse(separateYMMT.ElementAt<string>(0));
                string make = separateYMMT.ElementAt<string>(1);
                string model = separateYMMT.ElementAt<string>(2);
                var oldVehicle = vehicleManipulator.FindVehicleByYMM(year, make, model);
                double editMileage;
                decimal editBasePrice;

            EnterMileage:
                Console.Clear();
                ShowHeader();
                ShowVehicle(year, make, model);
                Console.WriteLine("\n" +
                    "Enter the new mileage:\n");
                if (double.TryParse(Console.ReadLine(), out double newMileage))
                {
                    editMileage = newMileage;
                }
                else
                {
                    PressAnyKey();
                    goto EnterMileage;
                }

            EnterBasePrice:
                Console.Clear();
                ShowHeader();
                ShowVehicle(year, make, model);
                Console.WriteLine("\n" +
                    "Enter the new base price:\n");
                if (decimal.TryParse(Console.ReadLine(), out decimal newBasePrice))
                {
                    editBasePrice = newBasePrice;
                }
                else
                {
                    PressAnyKey();
                    goto EnterBasePrice;
                }
                bool wasEdited = false;
                wasEdited = vehicleManipulator.EditVehicle(oldVehicle, editMileage, editBasePrice);
                if (wasEdited)
                {
                    Console.WriteLine("Vehicle successfully updated.");
                    Console.ReadKey();
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("Could not update vehicle.");
                    Console.ReadKey();
                    MainMenu();
                }
            }
            else
            {
                PressAnyKey();
                goto EnterOldVehicle;
            }
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

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Electric, BodyType.Hatchback, EnginePlacement.Front, Drivetrain.FWD, 2019, "Fiat", "500e", "", 111, 113, 113, 21000, 20000, new List<Option> { Option.None }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Hybrid, BodyType.Crossover, EnginePlacement.Front, Drivetrain.AWD, 2017, "Acura", "MDX", "SH-AWD", 321, 26, 27, 18000, 41000, new List<Option> { Option.Leather_Seats, Option.Remote_Start, Option.Seat_Heaters, Option.Steering_Wheel_Heater }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Gas, BodyType.Minivan, EnginePlacement.Front, Drivetrain.FWD, 2015, "Chrysler", "Town And Country", "Touring L", 283, 17, 25, 75000, 17000, new List<Option> { Option.Bluetooth, Option.Leather_Seats, Option.Navigation, Option.Remote_Start, Option.Seat_Heaters, Option.Steering_Wheel_Heater }));

            vehicleManipulator.CreateVehicle(new Vehicle(FuelType.Hybrid, BodyType.Coupe, EnginePlacement.Middle, Drivetrain.AWD, 2014, "BMW", "i8", "", 357, 28, 76, 14000, 63000, new List<Option> { Option.Bluetooth, Option.Leather_Seats, Option.Navigation, Option.Remote_Start }));
        }
    }
}
