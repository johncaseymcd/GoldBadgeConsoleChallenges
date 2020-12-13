using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldBadgeConsoleAppClasses;
using System.Collections.Generic;

namespace GoldBadgeConsoleAppTests
{
    [TestClass]
    public class UnitTest1
    {
        BadgeCRUD badgeTester = new BadgeCRUD();

        [TestMethod]
        public void TestActivateBadge()
        {
            // Arrange (initialize variables)
            List<Room> testBadgeAccess1 = new List<Room>()
            {
                new Room(1, 110),
                new Room(1, 150),
                new Room (1, 180),
                new Room(2, 240),
                new Room(2, 270),
                new Room(3, 300)
            };
            Badge testBadge1 = new Badge(testBadgeAccess1);
            int beginningCount = 0;

            // Act (add new badge)
            badgeTester.ActivateNewBadge(testBadge1);

            // Assert (count of Dictionary should be > 0)
            int endingCount = badgeTester.GetAllBadges().Count;
            Assert.IsTrue(endingCount > beginningCount, "Activation failed.");
        }

        [TestMethod]
        public void TestViewBadges()
        {
            // Arrange (initialize variables)
            List<Room> testBadgeAccess1 = new List<Room>()
            {
                new Room(1, 110),
                new Room(1, 150),
                new Room (1, 180),
                new Room(2, 240),
                new Room(2, 270),
                new Room(3, 300)
            };
            Badge testBadge1 = new Badge(testBadgeAccess1);

            // Act (add badge to dictionary)
            badgeTester.ActivateNewBadge(testBadge1);

            // Assert (that the dictionary of badges is not empty)
            Assert.IsNotNull(badgeTester.GetAllBadges(), "View failed.");

        }

        [TestMethod]
        public void TestEditBadge()
        {
            // Arrange (initialize variables and add to dictionary)
            List<Room> testBadgeAccess1 = new List<Room>()
            {
                new Room(1, 110),
                new Room(1, 150),
                new Room(1, 180),
                new Room(2, 240),
                new Room(2, 270),
                new Room(3, 300)
            };
            Badge testBadge1 = new Badge(testBadgeAccess1);
            badgeTester.ActivateNewBadge(testBadge1);

            List<Room> testBadgeAccess2 = new List<Room>()
            {
                new Room(1, 120),
                new Room(1, 170),
                new Room(2, 220),
                new Room(2, 250),
                new Room(2, 290)
            };
            Badge testBadge2 = new Badge(testBadgeAccess2);

            bool wasEdited = false;

            // Act (update badge 1 access)
            wasEdited = badgeTester.EditBadgeAccess(testBadge1.BadgeID, testBadge2);

            // Assert (that the EditBadgeAccess method returned true)
            Assert.IsTrue(wasEdited, "Edit failed.");
        }

        [TestMethod]
        public void TestDeactivateBadge()
        {
            // Arrange (initialize variables and add to dictionary)
            List<Room> testBadgeAccess1 = new List<Room>()
            {
                new Room(1, 110),
                new Room(1, 150),
                new Room (1, 180),
                new Room(2, 240),
                new Room(2, 270),
                new Room(3, 300)
            };
            Badge testBadge1 = new Badge(testBadgeAccess1);
            badgeTester.ActivateNewBadge(testBadge1);

            // Act (deactivate badge)
            badgeTester.DeactivateBadge(testBadge1.BadgeID);

            // Assert (that the IsActive property was set to false)
            Assert.IsFalse(testBadge1.IsActive, "Deactivation failed.");
        }
    }
}
