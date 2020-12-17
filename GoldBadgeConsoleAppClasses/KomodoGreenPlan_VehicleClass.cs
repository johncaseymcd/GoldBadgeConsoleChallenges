using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleAppClasses
{
    public enum FuelType
    {
        Electric = 1,
        Gas,
        Hybrid
    }

    public enum BodyType
    {
        Convertible = 1,
        Coupe,
        Crossover,
        Hatchback,
        Minivan,
        Pickup,
        Sedan,
        SUV
    }

    public enum EnginePlacement
    {
        Front = 1,
        Middle,
        Rear
    }

    public enum Drivetrain
    {
        AWD = 1,
        FourWD,
        FWD,
        RWD
    }

    public enum Option
    {
        Bluetooth = 1,
        Leather_Seats,
        Navigation,
        Premium_Sound,
        Remote_Start,
        Seat_Heaters,
        Spoiler,
        Steering_Wheel_Heater,
        Sunroof, 
        None
    }

    public class Vehicle
    {
        public FuelType Fuel { get; set; }
        public BodyType Body { get; set; }
        public EnginePlacement Engine { get; set; }
        public Drivetrain Drivetrain { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Trim { get; set; }
        public int Horsepower { get; set; }
        public int CityMPG { get; set; }
        public int HighwayMPG { get; set; }
        public double Efficiency { get; set; }
        public double Mileage { get; set; }
        public decimal BasePrice { get; set; }
        public List<Option> Options { get; set; }
        public decimal AddOnPrice { get; set; }
        public decimal TotalPrice{ get; set; }

        public Vehicle() { }

        public Vehicle(FuelType fuel, BodyType body, EnginePlacement engine, Drivetrain drivetrain, int year, string make, string model, string trim, int horsepower, int city, int highway, double mileage, decimal basePrice, List<Option> options)
        {
            Fuel = fuel;
            Body = body;
            Engine = engine;
            Drivetrain = drivetrain;
            Year = year;
            Make = make;
            Model = model;
            Trim = trim;
            Horsepower = horsepower;
            CityMPG = city;
            HighwayMPG = highway;
            Efficiency = Math.Round(((city + highway) / 2d), 1);
            Mileage = mileage;
            BasePrice = decimal.Round(basePrice, 2);
            Options = options;
            AddOnPrice = 0m;
            foreach (var option in options)
            {
                switch (option)
                {
                    case Option.Bluetooth:
                        AddOnPrice += 500m;
                        break;
                    case Option.Leather_Seats:
                        AddOnPrice += 1500m;
                        break;
                    case Option.Navigation:
                        AddOnPrice += 2500m;
                        break;
                    case Option.Premium_Sound:
                        AddOnPrice += 4000m;
                        break;
                    case Option.Remote_Start:
                        AddOnPrice += 1000m;
                        break;
                    case Option.Seat_Heaters:
                        AddOnPrice += 2000m;
                        break;
                    case Option.Spoiler:
                        AddOnPrice += 500m;
                        break;
                    case Option.Steering_Wheel_Heater:
                        AddOnPrice += 500m;
                        break;
                    case Option.Sunroof:
                        AddOnPrice += 3000m;
                        break;
                    case Option.None:
                        AddOnPrice = 0m;
                        break;
                }
            }
            TotalPrice = BasePrice + AddOnPrice;
        }
    }

    public class VehicleCRUD
    {
        private List<Vehicle> _vehicleList = new List<Vehicle>();

        // Helper method to find vehicle by year/make/model
        private Vehicle FindVehicleByYMM(int year, string make, string model)
        {
            foreach (var vehicle in _vehicleList)
            {
                if (vehicle.Year == year && vehicle.Make.ToLower() == make.ToLower() && vehicle.Model.ToLower() == model.ToLower())
                {
                    return vehicle;
                }
            }

            return null;
        }

        // Create
        public void CreateVehicle(Vehicle newVehicle)
        {
            _vehicleList.Add(newVehicle);
        }

        // Read
        public List<Vehicle> GetAllVehicles()
        {
            return _vehicleList;
        }

        // Update
        public bool EditVehicle(Vehicle oldVehicle, Vehicle newVehicle)
        {
            var editVehicle = FindVehicleByYMM(oldVehicle.Year, oldVehicle.Make, oldVehicle.Model);
            if (editVehicle != null)
            {
                editVehicle.Fuel = newVehicle.Fuel;
                editVehicle.Body = newVehicle.Body;
                editVehicle.Engine = newVehicle.Engine;
                editVehicle.Drivetrain = newVehicle.Drivetrain;
                editVehicle.Year = newVehicle.Year;
                editVehicle.Make = newVehicle.Make;
                editVehicle.Model = newVehicle.Model;
                editVehicle.Trim = newVehicle.Trim;
                editVehicle.Horsepower = newVehicle.Horsepower;
                editVehicle.CityMPG = newVehicle.CityMPG;
                editVehicle.HighwayMPG = newVehicle.HighwayMPG;
                editVehicle.Efficiency = newVehicle.Efficiency;
                editVehicle.Mileage = newVehicle.Mileage;
                editVehicle.BasePrice = newVehicle.BasePrice;
                editVehicle.Options = newVehicle.Options;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete
        public bool DeleteVehicle(Vehicle vehicle)
        {
            var deleteVehicle = FindVehicleByYMM(vehicle.Year, vehicle.Make, vehicle.Model);
            if (deleteVehicle != null)
            {
                _vehicleList.Remove(deleteVehicle);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
