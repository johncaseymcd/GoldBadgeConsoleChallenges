using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoldBadgeConsoleAppClasses;
using System.Globalization;

namespace GoldBadgeConsoleChallengeConsole_Email
{
    class KomodoGreetingProgramUI
    {
        private List<Customer> _allCustomers = new List<Customer>();
        private CustomerCRUD customerManipulator = new CustomerCRUD();

        public void Run()
        {
            Console.SetWindowSize(150, 30);
            SeedCustomerList();
            MainMenu();
        }

        private void MainMenu()
        {
        MainMenu:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Welcome! What would you like to do?\n \n" +
                "1. Add a new customer\n" +
                "2. View all customers\n" +
                "3. Edit a customer\n" +
                "4. Delete a customer\n" +
                "5. Exit");
            string inputChoice = Console.ReadLine();
            bool parseChoice = int.TryParse(inputChoice, out int whatToDo);
            if (parseChoice)
            {
                switch (whatToDo)
                {
                    case 1:
                        AddNewCustomer();
                        goto MainMenu;
                    case 2:
                        ViewAllCustomers();
                        Console.ReadKey();
                        goto MainMenu;
                    case 3:
                        EditCustomer();
                        goto MainMenu;
                    case 4:
                        DeleteCustomer();
                        goto MainMenu;
                    case 5:
                        Console.WriteLine("Press any key to exit or press H to return home. . .");
                        if (Console.ReadKey().Key == ConsoleKey.H)
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

        // Helper method to enter customer details
        private Customer EnterCustomerDetails()
        {
            Console.Clear();
            Console.WriteLine("Enter the customer's first and last name:\n");
            string fullName = Console.ReadLine();
            string firstName = fullName.Split(' ').ElementAt<string>(0);
            string lastName = fullName.Split(' ').ElementAt<string>(1);

        EnterDate:
            Console.Clear();
            Console.WriteLine($"Has {fullName} had a policy with us before (y/n)?\n");
            string inputPriorPolicy = Console.ReadLine().ToLower();
            var type = new CustomerType();
            if (inputPriorPolicy == "y")
            {
                Console.WriteLine("\n" +
                    "What is the expiration date of their most recent policy (in mm/dd/yyyy format)?\n");
                string inputPolicyDate = Console.ReadLine();
                bool parsePolicyDate = DateTime.TryParseExact(inputPolicyDate, "MM/dd/yyyy", CultureInfo.CurrentUICulture, DateTimeStyles.AssumeLocal, out DateTime policyExp);
                TimeSpan sixMonths = new TimeSpan(180, 0, 0, 0);
                if (!parsePolicyDate)
                {
                    PressAnyKey();
                    goto EnterDate;
                }
                if (policyExp >= DateTime.Now - sixMonths)
                {
                    type = CustomerType.Current;
                }
                else
                {
                    type = CustomerType.Past;
                }
                
            }
            else
            {
                type = CustomerType.Potential;
            }

            return new Customer(firstName, lastName, type);
        }

        private void AddNewCustomer()
        {
            var newCustomer = EnterCustomerDetails();
            customerManipulator.CreateCustomer(newCustomer);
        }

        private void ViewAllCustomers()
        {
            Console.Clear();
            _allCustomers = customerManipulator.GetAllCustomers();
            var sorter = _allCustomers.OrderBy(x => x.LastName).ThenBy(x => x.FirstName);
            Console.WriteLine("{0} \t{1} \t{2} \t{3}\n", "First Name".PadRight(10), "Last Name".PadRight(10), "Type".PadRight(10), "Email Template");
            foreach(var customer in sorter)
            {
                switch (customer.Type)
                {
                    case CustomerType.Current:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case CustomerType.Past:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case CustomerType.Potential:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                }
                Console.WriteLine("{0} \t{1} \t{2} \t{3}", customer.FirstName.PadRight(10), customer.LastName.PadRight(10), customer.Type.ToString().PadRight(10), customer.EmailToSend);
            }
        }

        private void EditCustomer()
        {
            
        }

        private void DeleteCustomer()
        {

        }

        private void SeedCustomerList()
        {
            customerManipulator.CreateCustomer(new Customer("Casey", "McDonough", CustomerType.Current));
            customerManipulator.CreateCustomer(new Customer("Mark", "Danielewski", CustomerType.Past));
            customerManipulator.CreateCustomer(new Customer("Keri", "Smith", CustomerType.Potential));
            customerManipulator.CreateCustomer(new Customer("Jaz", "Coleman", CustomerType.Past));
            customerManipulator.CreateCustomer(new Customer("Jerry", "Cantrell", CustomerType.Current));

            customerManipulator.CreateCustomer(new Customer("Poe", "Danielewski", CustomerType.Potential));
            customerManipulator.CreateCustomer(new Customer("Neal", "McDonough", CustomerType.Past));
            customerManipulator.CreateCustomer(new Customer("Will", "Smith", CustomerType.Current));
            customerManipulator.CreateCustomer(new Customer("Ornette", "Coleman", CustomerType.Past));
            customerManipulator.CreateCustomer(new Customer("Blu", "Cantrell", CustomerType.Potential));

            customerManipulator.CreateCustomer(new Customer("Bogdan", "Raczynski", CustomerType.Current));
            customerManipulator.CreateCustomer(new Customer("Lorenzo", "Senni", CustomerType.Potential));
            customerManipulator.CreateCustomer(new Customer("John", "Frusciante", CustomerType.Past));
            customerManipulator.CreateCustomer(new Customer("Mort", "Garson", CustomerType.Potential));
            customerManipulator.CreateCustomer(new Customer("Mary", "Lattimore", CustomerType.Current));
        }
    }
}
