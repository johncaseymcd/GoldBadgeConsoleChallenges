using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldBadgeConsoleAppClasses;
using System.Collections.Generic;

namespace GoldBadgeConsoleAppTests
{
    [TestClass]
    public class KomodoGreenPlan_VehicleTests
    {
        private VehicleCRUD vehicleTester = new VehicleCRUD();

        [TestMethod]
        public void TestCreateAndReadMethods()
        {
            // Arrange (initialize variables)
            var options = new List<Option> { Option.Premium_Sound, Option.Spoiler };
            var newCar = new Vehicle(FuelType.Gas, BodyType.Sedan, EnginePlacement.Front, Drivetrain.FWD, 2011, "Honda", "Civic", "LX", 176, 26, 34, 114000d, 7000m, options);
            int beginningCount = 0;

            // Act (Add the car to the list with the CreateVehicle() method)
            vehicleTester.CreateVehicle(newCar);

            // Assert (that the list of vehicles contains more than 0 items)
            int endingCount = vehicleTester.GetAllVehicles().Count; // Also tests the GetAllVehicles() method
            Assert.IsTrue(endingCount > beginningCount, "Add failed.");
        }

        [TestMethod]
        public void TestEditMethod()
        {
            // Arrange (intialize variables and add old car to list)
            var oldOptions = new List<Option> { Option.Leather_Seats, Option.Premium_Sound, Option.Spoiler, Option.Sunroof };
            var oldCar = new Vehicle(FuelType.Gas, BodyType.Coupe, EnginePlacement.Front, Drivetrain.RWD, 2015, "Dodge", "Challenger", null, 350, 16, 25, 40000d, 27000m, oldOptions);
            vehicleTester.CreateVehicle(oldCar);

            var newOptions = new List<Option> { Option.Bluetooth, Option.Leather_Seats, Option.Navigation, Option.Premium_Sound, Option.Seat_Heaters, Option.Spoiler, Option.Sunroof };
            var newCar = new Vehicle(FuelType.Gas, BodyType.Coupe, EnginePlacement.Front, Drivetrain.RWD, 2015, "Dodge", "Challenger", null, 350, 16, 25, 54389d, 27000m, newOptions);

            bool wasEdited = false;

            // Act (replace oldCar with newCar)
            wasEdited = vehicleTester.EditVehicle(oldCar, newCar);

            // Assert (that the EditVehicle() method returns true)
            Assert.IsTrue(wasEdited, "Edit failed.");
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            // Arrange (initialize variables and add car to list)
            var options = new List<Option> { Option.Bluetooth, Option.Navigation, Option.Remote_Start, Option.Spoiler };
            var deleteCar = new Vehicle(FuelType.Hybrid, BodyType.Hatchback, EnginePlacement.Front, Drivetrain.FWD, 2010, "Toyota", "Prius", "Two", 98, 51, 48, 117000d, 9000m, options);
            vehicleTester.CreateVehicle(deleteCar);
            int beginningCount = vehicleTester.GetAllVehicles().Count;

            // Act (remove the vehicle from the list)
            vehicleTester.DeleteVehicle(deleteCar);

            // Assert (that the DeleteVehicle() method returns true)
            int endingCount = vehicleTester.GetAllVehicles().Count;
            Assert.IsTrue(endingCount < beginningCount, "Delete failed.");
        }
    }
}
