using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldBadgeConsoleAppClasses
{
    public enum CustomerType
    {
        Current = 1,
        Past,
        Potential
    }
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CustomerType Type { get; set; }
        public string EmailToSend { get; set; }

        public Customer(string first, string last, CustomerType type)
        {
            FirstName = first;
            LastName = last;
            Type = type;
            switch (type)
            {
                case CustomerType.Current:
                    EmailToSend = "Thank you for your work with us. We appreciate your loyalty. Here's a coupon.";
                    break;
                case CustomerType.Past:
                    EmailToSend = "It's been a long time since we've heard from you, we want you back.";
                    break;
                case CustomerType.Potential:
                    EmailToSend = "We currently have the lowest rates on Helicopter Insurance!";
                    break;
            }
        }
    }

    public class CustomerCRUD
    {
        private List<Customer> _customerList = new List<Customer>();

        // Helper method to find customers by name
        private Customer FindCustomerByName(string lastName, string firstName)
        {
            foreach (var customer in _customerList)
            {
                if (customer.LastName.ToLower() == lastName.ToLower() && customer.FirstName.ToLower() == firstName.ToLower())
                {
                    return customer;
                }
            }

            return null;
        }

        // Create
        public void CreateCustomer(Customer newCustomer)
        {
            _customerList.Add(newCustomer);
        }

        // Read
        public List<Customer> GetAllCustomers()
        {
            return _customerList;
        }

        // Update
        public bool EditExistingCustomer(Customer oldCustomer, Customer newCustomer)
        {
            var editCustomer = FindCustomerByName(oldCustomer.LastName, oldCustomer.FirstName);
            if (editCustomer != null)
            {
                editCustomer.FirstName = newCustomer.FirstName;
                editCustomer.LastName = newCustomer.LastName;
                editCustomer.Type = newCustomer.Type;
                editCustomer.EmailToSend = newCustomer.EmailToSend;
                return true;
            }
            else
            {
                return false;
            }
        }

        // Delete
        public bool DeleteCustomer(string firstName, string lastName)
        {
            var deleteCustomer = FindCustomerByName(lastName, firstName);
            if (deleteCustomer != null)
            {
                _customerList.Remove(deleteCustomer);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
