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
            var claim = new Claim(1, ClaimType.Vehicle, "Fender bender", 3000.00m, lossDate, claimDate, isValid, false);

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
            var claim = new Claim(1, ClaimType.Home, "House fire", 150000.00m, lossDate, claimDate, isValid, false);
            var claimTester = new ClaimCRUD();

            // Act (Add claim to queue)
            claimTester.CreateClaim(claim);

            // Assert (List of claims should not be null)
            Assert.IsNotNull(claimTester.GetAllClaims(), "Read was not successful");
        }

        [TestMethod]
        public void TestEditClaim()
        {
            // Arrange (Create and initialize variables)
            var oldLossDate = new DateTime(2020, 05, 04);
            var oldClaimDate = new DateTime(2020, 06, 09);
            var oldClaimExpiration = new TimeSpan(30, 0, 0, 0);
            bool oldIsValid = oldClaimDate <= oldLossDate + oldClaimExpiration;
            var oldClaim = new Claim(1, ClaimType.Home, "Pipe burst", 2000.00m, oldLossDate, oldClaimDate, oldIsValid, false);
            var claimTester = new ClaimCRUD();
            claimTester.CreateClaim(oldClaim);

            // Act (Create a new claim to pass into the edit method)
            var newLossDate = new DateTime(2020, 01, 21);
            var newClaimDate = new DateTime(2020, 02, 02);
            var newClaimExpiration = new TimeSpan(30, 0, 0, 0);
            bool newIsValid = newClaimDate <= newLossDate + newClaimExpiration;
            var newClaim = new Claim(2, ClaimType.Vehicle, "Total loss", 35000.00m, newLossDate, newClaimDate, newIsValid, false);
            bool wasEdited = claimTester.EditClaim(oldClaim.ClaimID, newClaim);

            // Assert (wasEdited should return a value of true if the edit was successful)
            Assert.IsTrue(wasEdited, "Edit was not successful.");
        }

        [TestMethod]
        public void TestDeleteClaim()
        {
            // Arrange (Create and initialize variables)
            var lossDate = new DateTime(2020, 08, 11);
            var claimDate = new DateTime(2020, 09, 01);
            var claimExpiration = new TimeSpan(30, 0, 0, 0);
            bool isValid = claimDate <= lossDate + claimExpiration;
            var claim = new Claim(1, ClaimType.Theft, "Stolen TV and laptop", 2500.00m, lossDate, claimDate, isValid, false);
            var claimTester = new ClaimCRUD();
            claimTester.CreateClaim(claim);

            // Act (Call DeleteClaim() method on the claim
            bool wasDeleted = claimTester.DeleteClaim(claim.ClaimID);

            // Assert (wasDeleted should return true if the delete was successful)
            Assert.IsTrue(wasDeleted, "Delete was not successful.");
        }
    }
}
