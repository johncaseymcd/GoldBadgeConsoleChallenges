using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using GoldBadgeConsoleAppClasses;
using System.Collections.Generic;

namespace GoldBadgeConsoleAppTests
{
    [TestClass]
    public class KomodoGreeting_CustomerTest
    {
        private CustomerCRUD customerTester = new CustomerCRUD();
        
        [TestMethod]
        public void TestAddAndReadMethods()
        {
            // Arrange (initialize variables)
            var newCustomer = new Customer("Casey", "McDonough", CustomerType.Current);

            // Act (add newCustomer to the _customerList field using the Create method
            customerTester.CreateCustomer(newCustomer);

            // Assert (that the field of customers is not null, and that the count is greater than 0)
            var custList = customerTester.GetAllCustomers();
            Assert.IsNotNull(custList, "Add failed."); // Also tests the Get/Read method to make sure it reads the field properly
            Assert.IsTrue(custList.Count > 0, "Add failed.");
        }

        [TestMethod]
        public void TestEditMethod()
        {
            // Arrange (intialize variables)
            var oldCustomer = new Customer("Casey", "McDonough", CustomerType.Current);
            var newCustomer = new Customer("Gessenia", "Rivas", CustomerType.Potential);
            bool wasEdited = false;

            // Act (add oldCustomer to the list, then edit to newCustomer)
            customerTester.CreateCustomer(oldCustomer);
            wasEdited = customerTester.EditExistingCustomer(oldCustomer, newCustomer);

            // Assert (that the Edit method returns true)
            Assert.IsTrue(wasEdited, "Edit failed");
        }

        [TestMethod]
        public void TestDeleteMethod()
        {
            // Arrange (initialize variables)
            var newCustomer = new Customer("Casey", "McDonough", CustomerType.Past);
            bool wasDeleted = false;
            customerTester.CreateCustomer(newCustomer);

            // Act (remove newCustomer from the field of customers)
            wasDeleted = customerTester.DeleteCustomer(newCustomer.FirstName, newCustomer.LastName);

            // Assert (that the Delete method returns true, and that the field of customers is empty)
            Assert.IsTrue(wasDeleted, "Delete failed.");
            Assert.IsTrue(customerTester.GetAllCustomers().Count == 0);
        }
    }
}
