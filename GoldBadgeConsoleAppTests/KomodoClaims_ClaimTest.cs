using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldBadgeConsoleAppClasses;

namespace GoldBadgeConsoleAppTests
{
    [TestClass]
    public class KomodoClaimsTest
    {
        [TestMethod]
        public void TestAddClaim()
        {
            // Arrange (Create and initialize variables)
            var lossDate = new DateTime(2020, 03, 14);
            var claimDate = new DateTime(2020, 03, 15);
            var claimExpiration = new TimeSpan(30, 0, 0, 0);
            bool isValid = claimDate <= lossDate + claimExpiration;
            var claim = new Claim(1, ClaimType.Vehicle, "Fender bender", 3000.00m, lossDate, claimDate, isValid);

            var claimTester = new ClaimCRUD();
            int beginningCount = claimTester.GetAllClaims().Count;

            // Act (Add new claim to queue)
            claimTester.CreateClaim(claim);

            // Assert (Check the count of the claims queue to make sure it is greater than it was at the beginning)
            int endingCount = claimTester.GetAllClaims().Count;
            Assert.IsTrue(endingCount > beginningCount, "Add was not successful.");
        }

        [TestMethod]
        public void TestReadClaim()
        {
            // Arrange (Create and initialize variables)
            var lossDate = new DateTime(2019, 08, 12);
            var claimDate = new DateTime(2019, 09, 09);
            var claimExpiration = new TimeSpan(30, 0, 0, 0);
            bool isValid = claimDate <= lossDate + claimExpiration;
            var claim = new Claim(1, ClaimType.Home, "House fire", 150000.00m, lossDate, claimDate, isValid);
            var claimTester = new ClaimCRUD();

            // Act (Add claim to queue)
            claimTester.CreateClaim(claim);

            // Assert (List of claims should not be null)
            Assert.IsNotNull(claimTester.GetAllClaims(), "Read was not successful");
        }

        [TestMethod]
        public void TestDeleteClaim()
        {
            // Arrange (Create and initialize variables)
            var lossDate = new DateTime(2020, 08, 11);
            var claimDate = new DateTime(2020, 09, 01);
            var claimExpiration = new TimeSpan(30, 0, 0, 0);
            bool isValid = claimDate <= lossDate + claimExpiration;
            var claim = new Claim(1, ClaimType.Theft, "Stolen TV and laptop", 2500.00m, lossDate, claimDate, isValid);
            var claimTester = new ClaimCRUD();
            claimTester.CreateClaim(claim);

            // Act (Call DeleteClaim() method on the claim
            bool wasDeleted = claimTester.DeleteClaim(claim.ClaimID);

            // Assert (wasDeleted should return true if the delete was successful)
            Assert.IsTrue(wasDeleted, "Delete was not successful.");
        }
    }
}
