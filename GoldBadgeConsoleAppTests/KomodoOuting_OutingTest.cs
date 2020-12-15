using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using GoldBadgeConsoleAppClasses;
using System.Collections.Generic;

namespace GoldBadgeConsoleAppTests
{
    [TestClass]
    public class KomodoOuting_OutingTest
    {
        OutingCRUD outingTester = new OutingCRUD();

        [TestMethod]
        public void TestAddNewOutingAndGetAllOutings()
        {
            // Arrange (initialize variables)
            var newOutingDate = new DateTime(2020, 04, 07, 12, 00, 00);
            var newOuting = new Outing(newOutingDate, OutingType.Golf, 30);
            int beginningCount = 0;

            // Act (add new Outing to the list)
            outingTester.CreateNewOuting(newOuting);

            // Assert (that the list of outings is not null, and that the count is greater than 0
            int endingCount = outingTester.GetAllOutings().Count; // Also test GetAllOutings method to make sure it is capturing the list properly

            Assert.IsNotNull(outingTester.GetAllOutings(), "Add failed.");
            Assert.IsTrue(endingCount > beginningCount, "Add failed.");
        }
    }
}
