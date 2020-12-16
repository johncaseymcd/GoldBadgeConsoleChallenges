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

        public Customer() { }

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
        private SortedList<string, Customer> _customerList = new SortedList<string, Customer>();

        // Helper method to find customers by name
        private Customer FindCustomerByName(string lastName, string firstName)
        {
            foreach (var customer in _customerList)
            {
                if (customer.Key == lastName && customer.Value.FirstName == firstName)
                {
                    return customer.Value;
                }
            }

            return null;
        }

        // Create
        public void CreateCustomer(Customer newCustomer)
        {
            _customerList.Add(newCustomer.LastName, newCustomer);
        }

        // Read
        public SortedList<string, Customer> GetAllCustomers()
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
        public bool DeleteCustomer(Customer oldCustomer)
        {
            var deleteCustomer = FindCustomerByName(oldCustomer.LastName, oldCustomer.FirstName);
            if (deleteCustomer != null)
            {
                _customerList.Remove(oldCustomer.LastName);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
